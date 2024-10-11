using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace DotnetCoreSocialIds
{
    public class NewSocialIds
    {
        private List<int> DaysOfMonth { get; set; } = new List<int> { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
        private bool IsLeapYear(int year)
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

        public List<List<int>> GenerateDaysOfYear(int year)
        {
            var yearValue = year % 100;
            var yearArray = new List<int>();
            yearArray.Add(yearValue / 10);
            yearArray.Add(yearValue % 10);
            var ret = new List<List<int>>();
            for (var month = 0; month < DaysOfMonth.Count; month++) {
                if (month == 1 && IsLeapYear(year))
                {
                    DaysOfMonth[month] = 29;
                }
                var monthValue = month + 1;
                var monthArray = new List<int>();
                if (monthValue < 10)
                {
                    monthArray.Add(0);
                    monthArray.Add(monthValue);
                }
                else
                {
                    monthArray.Add(monthValue / 10);
                    monthArray.Add(monthValue % 10);
                }
                var days = Enumerable.Range(1, DaysOfMonth[month]);
                
                foreach (var d in days)
                {
                    var day = new List<int>();
                    if (d < 10)
                    {
                        day.Add(0);
                        day.Add(d);
                    } else
                    {
                        day.Add(d / 10);
                        day.Add(d % 10);
                    }
                    day.AddRange(monthArray);
                    day.AddRange(yearArray);
                    ret.Add(day);
                }
                if (month == 11)
                {
                    var a = 1;
                }
            }
            return ret;
        }

        private int GenerateChecksum(List<int> id)
        {
            int[] parities = new int[] { 3, 2, 7, 6, 5, 4, 3, 2 };
            var sum = 0;
            for (var i = 0; i < 8; i++)
            {
                sum += parities[i] * id[i];
            }
            return 11 - (sum % 11);
        }

        public List<List<int>> GenerateAllPossibleIds(int year)
        {
            var allDays = GenerateDaysOfYear(year);
            var allPossibles = new List<List<int>>();
            var numbers = Enumerable.Range(20, 99);
            
            foreach (var day in allDays)
            {
                foreach (var number in numbers)
                {
                    var possible = new List<int>();
                    possible.AddRange(day);
                    possible.Add(number / 10);
                    possible.Add(number % 10);
                    var sum = GenerateChecksum(possible);
                    
                    var checksum = 11 - (sum % 11);
                    if (checksum < 10)
                    {
                        possible.Add(checksum);
                        allPossibles.Add(possible);
                    }
                }
            }

            return allPossibles;
        }
    }
}
