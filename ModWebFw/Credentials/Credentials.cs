using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModWebFw
{
    public static class Credentials
    {
        public static class Software
        {
            public static string Title { get; set; }
            public static string TitleEn { get; set; }
            public static string Description { get; set; }
            public static string Version { get; set; }
            public static string Culture { get; set; }
            public static string BaseUrl { get; set; }
            public static string HomeController { get; set; }
            public static string ErrorView { get; set; }
        }

        public static class Customer
        {
            public static string FullName { get; set; }
            public static string ShortName { get; set; }
            public static string Address { get; set; }
            public static string Region { get; set; }
            public static string Province { get; set; }
            public static string Country { get; set; }
            public static string Phone { get; set; }
            public static string Mersis { get; set; }
            public static string TaxNo { get; set; }
            public static string TaxRegion { get; set; }
            public static string Mail { get; set; }
            public static string Web { get; set; }

            public static class English
            {
                public static string FullName { get; set; }
                public static string ShortName { get; set; }
                public static string Address { get; set; }
                public static string Region { get; set; }
                public static string Province { get; set; }
                public static string Country { get; set; }
                public static string Phone { get; set; }
                public static string Mersis { get; set; }
                public static string TaxNo { get; set; }
                public static string TaxRegion { get; set; }
                public static string Mail { get; set; }
                public static string Web { get; set; }
            }


        }

        public static class Publisher
        {
            public static string FullName { get; set; }
            public static string ShortName { get; set; }
            public static string Phone { get; set; }
            public static string Mail { get; set; }
            public static string Web { get; set; }
            public static string MailSignature { get; set; }
        }

        public static class Security
        {
            public static string Password { get; set; }
        }

        public static class Database
        {
            public static List<List<object>> Objects { get; set; }
            public static List<string> Entities { get; set; }
        }
        
        public static MailCredentials MailCredentials
        {
            get
            {
                IniFile iniFile;
                HashCode hashCode = new HashCode();
                iniFile = new IniFile(AppDomain.CurrentDomain.BaseDirectory + "Config.ini");
                string crypto = iniFile.IniReadValue("Mail", "Crypto");
                if (crypto == "E")
                {
                    return new MailCredentials()
                    {
                        Host = hashCode.DecryptionConfig(iniFile.IniReadValue("Mail", "Host")),
                        Username = hashCode.DecryptionConfig(iniFile.IniReadValue("Mail", "Username")),
                        Password = hashCode.DecryptionConfig(iniFile.IniReadValue("Mail", "Password")),
                        Port = Convert.ToInt32(hashCode.DecryptionConfig(iniFile.IniReadValue("Mail", "Port"))),
                        DefaultAddress = hashCode.DecryptionConfig(iniFile.IniReadValue("Mail", "DefaultAddress"))
                    };
                }
                else
                {
                    return new MailCredentials()
                    {
                        Host = iniFile.IniReadValue("Mail", "Host"),
                        Username = iniFile.IniReadValue("Mail", "Username"),
                        Password = iniFile.IniReadValue("Mail", "Password"),
                        Port = Convert.ToInt32(iniFile.IniReadValue("Mail", "Port")),
                        DefaultAddress = iniFile.IniReadValue("Mail", "DefaultAddress")
                    };
                }
            }
        }
        public static string iniFileAddress { get; set; }
        public static string ConnectionString(string target = "Remote")
        {
            IniFile iniFile;
            HashCode hashCode = new HashCode();
            iniFile = string.IsNullOrEmpty(iniFileAddress) ?  new IniFile(AppDomain.CurrentDomain.BaseDirectory + "Config.ini") : new IniFile(iniFileAddress);
            string crypto = iniFile.IniReadValue(target, "Crypto");
            string host, user_id, password, catalog;
            if (crypto == "E")
            {
                host = hashCode.DecryptionConfig(iniFile.IniReadValue(target, "Host"));
                user_id = hashCode.DecryptionConfig(iniFile.IniReadValue(target, "UserId"));
                password = hashCode.DecryptionConfig(iniFile.IniReadValue(target, "Password"));
                catalog = hashCode.DecryptionConfig(iniFile.IniReadValue(target, "Catalog"));
            }
            else
            {
                host = iniFile.IniReadValue(target, "Host");
                user_id = iniFile.IniReadValue(target, "UserId");
                password = iniFile.IniReadValue(target, "Password");
                catalog = iniFile.IniReadValue(target, "Catalog");
            }
            return new SqlConnectionStringBuilder()
            {
                DataSource = host,
                UserID = user_id,
                Password = password,
                InitialCatalog = catalog,
                MultipleActiveResultSets = true
            }.ToString();
        }
    }
}
