using System;
using System.IO;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Lesson2
{
    [Serializable]
    public class ClientBirthdate
    {
        public ClientBirthdate()
        {
        }

        public ClientBirthdate(int day, int mounth, int year)
        {
            Day = day;
            Mounth = mounth;
            Year = year;
        }

        public int Day { get; set; }
        public int Mounth { get; set; }
        public int Year { get; set; }

        public string GetFullBirthdate()
        {
            return ($"{Day} {Mounth} {Year}");
        }
    }
}
