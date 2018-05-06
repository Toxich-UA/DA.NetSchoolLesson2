using System.IO;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Formatting = Newtonsoft.Json.Formatting;

namespace Lesson2
{
    public class BankClientInformation
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }

        public ClientBirthdate Birthdate { get; set; }
        public Address HomeAddress { get; set; }
        public Address WorkAddress { get; set; }

       
    }
}
