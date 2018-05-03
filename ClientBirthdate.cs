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
        public bool SerializeObject(string fileName, string type)
        {
            StreamWriter writer;

            if (type.ToLower() == "json")
            {
                fileName += ".json";
                writer = new StreamWriter(fileName);
                using (writer)
                {
                    writer.Write(JsonConvert.SerializeObject(this, Formatting.Indented));
                }
                return true;
            }

            if (type.ToLower() == "xml")
            {
                fileName += ".xml";
                writer = new StreamWriter(fileName);
                using (writer)
                {
                    new XmlSerializer(typeof(ClientBirthdate)).Serialize(writer, this);
                }
                return true;
            }
            return false;
        }
    }
}
