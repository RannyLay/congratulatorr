using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;

namespace поздравлятор
{
    class Birthday
    {
        public string Name;
        public int Day;
        public int Month;
        public int Year;
        public bool IsTexted = false;
        public Birthday(string name, int day, int month, int year)
        {
            Name = name;
            Day = day;
            Month = month;
            Year = year;
        }
    }
}
