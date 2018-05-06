using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Lesson2
{
    class Program
    {
        static void Main(string[] args)
        {
            BankClientInformation clientInformation = new BankClientInformation();

            //Read specific xml file
            XmlDocument xmlDoc = new XmlDocument();
            try
            {
                xmlDoc.Load("clientinfo_input.xml");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Source file not exist.");
                return;
            }

            if (xmlDoc.DocumentElement != null)
            {

                clientInformation.FirstName = xmlDoc.SelectSingleNode("/cl/fn")?.InnerText;
                clientInformation.LastName = xmlDoc.SelectSingleNode("/cl/ln")?.InnerText;
                clientInformation.MiddleName = xmlDoc.SelectSingleNode("/cl/mn")?.InnerText;
                clientInformation.PhoneNumber = xmlDoc.SelectSingleNode("/cl/p")?.InnerText;
                clientInformation.EmailAddress = xmlDoc.SelectSingleNode("/cl/e")?.InnerText;


                clientInformation.Birthdate = new ClientBirthdate(
                    int.Parse(xmlDoc.SelectSingleNode("/cl/bd")?.InnerText),
                    int.Parse(xmlDoc.SelectSingleNode("/cl/bm")?.InnerText),
                    int.Parse(xmlDoc.SelectSingleNode("/cl/by")?.InnerText));

                clientInformation.HomeAddress = new Address(
                    xmlDoc.SelectSingleNode("/cl/hl1")?.InnerText,
                    xmlDoc.SelectSingleNode("/cl/hc")?.InnerText,
                    xmlDoc.SelectSingleNode("/cl/hs")?.InnerText,
                    xmlDoc.SelectSingleNode("/cl/hz")?.InnerText);

                clientInformation.WorkAddress = new Address(
                    xmlDoc.SelectSingleNode("/cl/wl1")?.InnerText,
                    xmlDoc.SelectSingleNode("/cl/wc")?.InnerText,
                    xmlDoc.SelectSingleNode("/cl/ws")?.InnerText,
                    xmlDoc.SelectSingleNode("/cl/wz")?.InnerText
                );

            }
            else
            {
                Console.WriteLine("File is empty.");
                return;
            }

            string firstPartFileOutPath = "output";

            if (!Directory.Exists(firstPartFileOutPath))
                Directory.CreateDirectory(firstPartFileOutPath);

            Utility.SerializeObject<BankClientInformation>(clientInformation, $".\\{firstPartFileOutPath}\\clientinfo_output", "XML");
            Console.WriteLine("Xml file was saved.");

            Utility.SerializeObject<BankClientInformation>(clientInformation, $".\\{firstPartFileOutPath}\\clientinfo_output", "JSon");
            Console.WriteLine("JSON file was saved.");

            Utility.SerializeObject<Address>(clientInformation.HomeAddress, $".\\{firstPartFileOutPath}\\homeaddress_output", "json");
            Console.WriteLine("Home address was saved in json file.");

            Utility.SerializeObject<Address>(clientInformation.WorkAddress, $".\\{firstPartFileOutPath}\\workaddress_output", "xml");
            Console.WriteLine("Work address was saved in xml file.");


            //Second part
            string inputFilePath = "operations";

            if (Directory.Exists(inputFilePath))
            {
                string[] files = Directory.GetFiles(inputFilePath);
                if (files.Length != 0)
                {
                    int maxAmount = 0;
                    string maxAmountFileName = "";

                    foreach (string fileName in files)
                    {
                        switch (Path.GetExtension(fileName).ToLower())
                        {
                            case ".xml":
                                try
                                {
                                    xmlDoc.Load(fileName);
                                    if (int.TryParse(xmlDoc.SelectSingleNode("/operation/Amount")?.InnerText, out var amount))
                                        if (maxAmount < amount)
                                        {
                                            maxAmount = amount;
                                            maxAmountFileName = fileName;
                                        }
                                }
                                catch (XmlException)
                                {
                                    Console.WriteLine($"{fileName} has some broken xml tags.");
                                }
                                break;
                            case ".json":
                                try
                                {
                                    using (StreamReader reader = File.OpenText(fileName))
                                    {
                                        var parsed = (JObject)JToken.ReadFrom(new JsonTextReader(reader));
                                        int amount = parsed.SelectToken("Amount").Value<int>();
                                        if (maxAmount < amount)
                                        {
                                            maxAmount = amount;
                                            maxAmountFileName = fileName;
                                        }
                                    }
                                }
                                catch (JsonReaderException)
                                {
                                    Console.WriteLine($"{fileName} has some broken Json tags.");
                                }
                                break;
                        }
                    }
                    switch (Path.GetExtension(maxAmountFileName).ToLower())
                    {
                        case ".xml":

                            var operation = XElement.Load(maxAmountFileName);

                            Console.WriteLine(string.Format(
                                "The operation was performed on {0} with the type of transaction {1} and the amount {2}",
                                operation.Element("Date")?.Value,
                                operation.Attribute("type")?.Value,
                                operation.Element("Amount")?.Value));
                            break;
                        case ".json":
                            using (StreamReader reader = File.OpenText(maxAmountFileName))
                            {
                                var parsed = (JObject)JToken.ReadFrom(new JsonTextReader(reader));
                                Console.WriteLine(string.Format(
                                    "The operation was performed on {0} with the type of transaction {1} and the amount {2}",
                                    parsed.SelectToken("Date").Value<DateTime>(),
                                    parsed.SelectToken("OperationType").Value<string>(),
                                    parsed.SelectToken("Amount").Value<int>()));
                            }
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Directory is empty.");
                }

            }
            else
            {
                Directory.CreateDirectory(inputFilePath);
                Console.WriteLine($"Please place operations files in {inputFilePath} folder.");
            }
        }
    }
}
