using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Lesson2
{
    static class Utility
    {
        public static bool SerializeObject<T>(Object obj, string fileName, string type)
        {
            StreamWriter writer;

            type = type.ToLower();
            if (TypeIsValid(type))
            {
                writer = new StreamWriter($"{fileName}.{type}");
                switch (type)
                {
                    case "json":
                        using (writer)
                        {
                            writer.Write(JsonConvert.SerializeObject(obj, Formatting.Indented));
                        }
                        return true;
                    case "xml":
                        using (writer)
                        {
                            new XmlSerializer(typeof(T)).Serialize(writer, obj);
                        }
                        return true;
                }
            }

            return false;
        }

        private static bool TypeIsValid(string type)
        {
            return type != null && (type.Equals("json", StringComparison.CurrentCultureIgnoreCase) || type.Equals("xml", StringComparison.CurrentCultureIgnoreCase));
            ;
        }
    }
}
