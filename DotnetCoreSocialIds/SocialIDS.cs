using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DotnetCoreSocialIds
{
    public class SocialIDS
    {
        List<String> socialIds;
        int[] leapYear;
        int[] normalYear;

        public SocialIDS()
        {
            socialIds = new List<String>();
            leapYear = new int[] { 31, 29, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            normalYear = new int[] { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
        }

        public bool IsLeapYear(int year)
        {
            if ((year % 4 == 0) && (year % 100 != 0) || (year % 400 == 0))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public IEnumerable<int> MonthsOfYear(int year)
        {
            return IsLeapYear(year) ? leapYear : normalYear;
        }

        public String MakeTwoLetters(int number)
        {
            if (number < 10)
            {
                return $"0{number}";
            }
            else
            {
                return $"{number}";
            }
        }

        public int[] Range(int from, int to)
        {
            int[] ret = new int[to - from];
            for (int i = from; i < to; i++)
            {
                ret[i - from] = i;
            }

            return ret;
        }

        public List<string> GenerateDaysForYear(int year)
        {
            var months = MonthsOfYear(year);
            List<String> allDays = new List<string>();
            int m = 0;
            foreach (int days in months)
            {
                for (var i = 0; i < days; i++)
                {
                    allDays.Add($"{ MakeTwoLetters(i + 1)}{MakeTwoLetters(m + 1)}{MakeTwoLetters(year % 100)}");
                }
                m++;
            }
            return allDays;
        }

        public List<string> GenerateAllPossiblesForYear(int year)
        {
            List<string> allDates = GenerateDaysForYear(year);
            List<string> allPossibles = new List<string>();

            foreach (string date in allDates)
            {
                for (int i = 20; i < 100; i++)
                {
                    var s = $"{date}-{ MakeTwoLetters(i)}";
                    allPossibles.Add(s);
                }
            }

            return allPossibles;
        }

        public int[] SplitIdNumberIntoInt(string idnumber)
        {
            idnumber = idnumber.Replace("-", "");
            var r = new int[idnumber.Length];
            for (var i = 0; i < idnumber.Length; i++)
            {
                r[i] = int.Parse(Char.ToString(idnumber[i]));
            }

            return r;
        }

        public int GenerateChecksumOnIdNumber(string idnumber)
        {
            int[] calculateAgainst = new int[] { 3, 2, 7, 6, 5, 4, 3, 2 };
            int[] id = SplitIdNumberIntoInt(idnumber);
            int total = 0;

            for (var i = 0; i < calculateAgainst.Length; i++)
            {
                total += calculateAgainst[i] * id[i];
            }
            var x = total % 11;
            if (x == 0)
            {
                return 0;
            }
            return 11 - x;
        }

        public List<string> GenerateAllWithChecksum(int year)
        {
            List<string> allPossibles = GenerateAllPossiblesForYear(year);
            List<string> allPossiblesWithChecksum = new List<string>();

            foreach (string item in allPossibles)
            {
                var checksum = GenerateChecksumOnIdNumber(item);
                allPossiblesWithChecksum.Add($"{item}{checksum}{year / 100 % 10}");
            }
            //Console.WriteLine($"I just generated {allPossiblesWithChecksum.Count} social ids for year {year}");
            return allPossiblesWithChecksum;
        }
    }
}