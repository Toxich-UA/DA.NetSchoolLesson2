using System;
using System.IO;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Lesson2
{
    [Serializable]
    public class Address
    {
        public Address()
        {
        }

        public Address(string line1, string city, string state, string zipCode)
        {
            Line1 = line1;
            City = city;
            State = state;
            ZipCode = zipCode;
        }

        public string Line1 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }

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
                    new XmlSerializer(typeof(Address)).Serialize(writer, this);
                }
                return true;
            }
            return false;
        }

    }
}
