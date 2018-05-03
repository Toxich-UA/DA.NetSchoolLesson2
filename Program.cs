using System;
using System.IO;
using System.Xml;

namespace Lesson2
{
    class Program
    {
        static void Main(string[] args)
        {
            BankClientInformation clientInformation = new BankClientInformation();

            //Read specific xml file
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("clientinfo_input.xml");

            if (xmlDoc.DocumentElement != null)
            {
                XmlNodeList nodedList = xmlDoc.DocumentElement.SelectNodes("/cl");
                foreach (XmlNode node in nodedList)
                {
                    clientInformation.FirstName = node.SelectSingleNode("fn")?.InnerText;
                    clientInformation.LastName = node.SelectSingleNode("ln")?.InnerText;
                    clientInformation.MiddleName = node.SelectSingleNode("mn")?.InnerText;
                    clientInformation.PhoneNumber = node.SelectSingleNode("p")?.InnerText;
                    clientInformation.EmailAddress = node.SelectSingleNode("e")?.InnerText;

                    clientInformation.Birthdate = new ClientBirthdate(
                        int.Parse(node.SelectSingleNode("bd")?.InnerText),
                        int.Parse(node.SelectSingleNode("bm")?.InnerText),
                        int.Parse(node.SelectSingleNode("by")?.InnerText));

                    clientInformation.HomeAddress = new Address(
                        node.SelectSingleNode("hl1")?.InnerText,
                        node.SelectSingleNode("hc")?.InnerText,
                        node.SelectSingleNode("hs")?.InnerText,
                        node.SelectSingleNode("hz")?.InnerText);

                    clientInformation.WorkAddress = new Address(
                        node.SelectSingleNode("wl1")?.InnerText,
                        node.SelectSingleNode("wc")?.InnerText,
                        node.SelectSingleNode("ws")?.InnerText,
                        node.SelectSingleNode("wz")?.InnerText
                    );
                }
            }
            else
                return;

            if (!Directory.Exists("output"))
                Directory.CreateDirectory("output");

            clientInformation.SerializeObject(@".\output\clientinfo_output", "XML");
            Console.WriteLine("Xml file was saved.");

            clientInformation.SerializeObject(@".\output\clientinfo_output", "JSon");
            Console.WriteLine("JSON file was saved.");

            clientInformation.HomeAddress.SerializeObject(@".\output\homeaddress_output", "json");
            Console.WriteLine("Home address was saved in json file.");

            clientInformation.WorkAddress.SerializeObject(@".\output\workaddress_output", "xml");
            Console.WriteLine("Work address was saved in xml file.");
        }
    }
}
