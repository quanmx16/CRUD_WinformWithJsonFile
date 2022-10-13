using BusinessObject;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DataAccess
{
    public class MemberDAO
    {
        private static string filePath = "appsettings.json";
        private static List<MemberObject> members;
        static MemberDAO()
        {
          readDataFromFile();
        }
        public static void WriteMembersInforToFile(List<MemberObject> members)
        {
            using(StreamWriter stream = new StreamWriter(filePath))
            {
                string json = JsonSerializer.Serialize<List<MemberObject>>(members);
                stream.Write(json);
            }
        }
        private static void readDataFromFile()
        {
            members = new List<MemberObject>();
            using (StreamReader reader = new StreamReader(filePath))
            {
                string json = reader.ReadToEnd();
                members = JsonSerializer.Deserialize<List<MemberObject>>(json);
            }
        }
        public static List<MemberObject> getAllMember()
        {
            return members;
        }
        
    }
}
