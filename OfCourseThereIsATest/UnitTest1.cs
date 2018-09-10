using DotnetCoreSocialIds;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

using Xunit;

namespace OfCourseThereIsATest
{
    public class Tests
    {
        int[] leap = { 31, 29, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
        int[] notLeap = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

        public IEnumerable<int> Leap { get => leap; }
        public IEnumerable<int> NonLeap { get => notLeap; }

        [Fact]
        public void TestIsLeapYear()
        {
            SocialIDS s = new SocialIDS();
            Assert.True(s.IsLeapYear(2000));
            Assert.False(s.IsLeapYear(2001));
            Assert.False(s.IsLeapYear(1986));
            Assert.True(s.IsLeapYear(1984));
            Assert.False(s.IsLeapYear(1700));
            Assert.False(s.IsLeapYear(1800));
            Assert.False(s.IsLeapYear(1900));
            Assert.False(s.IsLeapYear(2100));
            Assert.False(s.IsLeapYear(2200));
            Assert.False(s.IsLeapYear(2300));
            Assert.False(s.IsLeapYear(2500));
            Assert.False(s.IsLeapYear(2600));
        }

        [Fact]
        public void DaysInFebruary()
        {
            SocialIDS s = new SocialIDS();
            var leapYear = s.MonthsOfYear(1984).ToList();
            var notLeapYear = s.MonthsOfYear(1985).ToList();

            Assert.Equal(29, leapYear[1]);
            Assert.Equal(28, notLeapYear[1]);
        }

        [Fact]
        public void TestTwoLetters()
        {
            SocialIDS s = new SocialIDS();
            Assert.Equal("01", s.MakeTwoLetters(1));
            Assert.Equal("99", s.MakeTwoLetters(99));
            Assert.Equal("100", s.MakeTwoLetters(100));
        }

        [Fact]
        public void TestRange()
        {
            var s = new SocialIDS();
            int[] a = s.Range(0, 100);

            Assert.Equal(100, a.Length);
        }

        [Fact]
        public void TestAllYearGenerate()
        {
            var s = new SocialIDS();
            var months = s.MonthsOfYear(1974);
            int totalDays = NonLeap.Sum();
            Assert.Equal(365, totalDays);

            List<string> alldays = s.GenerateDaysForYear(1974);

            Assert.Equal("010174", alldays.FirstOrDefault());
            Assert.Equal("311274", alldays.LastOrDefault());
            Assert.Contains("030574", alldays);
            Assert.DoesNotContain("290274", alldays);
            Assert.Equal(totalDays, alldays.Count());
        }
        [Fact]
        public void TestAllPossibleYearGenerate()
        {
            var s = new SocialIDS();
            List<string> allPossibles = s.GenerateAllPossiblesForYear(1974);

            Assert.Equal(365 * 99, allPossibles.Count());
            Assert.Contains("030574-30", allPossibles);
        }

        [Fact]
        public void TestCheckSplits()
        {
            var s = new SocialIDS();
            var id = "030574-30";
            var r = s.SplitIdNumberIntoInt(id);

            Assert.Equal(0, r[0]);
            Assert.Equal(8, r.Length);
            Assert.Equal(3, r[1]);

            id = "010100-00";
            r = s.SplitIdNumberIntoInt(id);
            Assert.Equal(0, r[0]);
            Assert.Equal(8, r.Length);
            Assert.Equal(1, r[1]);

        }

        [Fact]
        public void TestChecksum()
        {
            var s = new SocialIDS();
            var id = "030574-30";
            var checksum = s.GenerateChecksumOnIdNumber(id);

            Assert.Equal(3, checksum);

            id = "120160-33";
            checksum = s.GenerateChecksumOnIdNumber(id);
            Assert.Equal(8, checksum);
        }

        [Fact]
        public void TestGenerateCheckSumsToNumbers()
        {
            var s = new SocialIDS();
            var allPossibles = s.GenerateAllWithChecksum(1974);

            Assert.Contains("030574-3039", allPossibles);
            Assert.DoesNotContain("030574-3049", allPossibles);
        }
    }
}
