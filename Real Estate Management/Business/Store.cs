using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Real_Estate_Management.Business
{
    public static class Store
    {
        private static string houseFilePath = @"../../../Data/houseFile.xml";
        private static string userFilePath = @"../../../Data/userFile.xml";
        private static string sellerFilePath = @"../../../Data/sellerFile.xml";
        private static string contractFilePath = @"../../../Data/contractFile.xml";
        private static string userPhotosPath = @"../../../Data/UserPhotos/";
        
        public static void SaveAllHouses(List<House> houses)
        {
            XmlWriter writer = XmlWriter.Create(houseFilePath);
            XmlSerializer serializer = new XmlSerializer(typeof(List<House>));
            serializer.Serialize(writer, houses);
            writer.Close();
        }
        public static void SaveAllSellers(List<Customer> sellers)
        {
            XmlWriter writer = XmlWriter.Create(sellerFilePath);
            XmlSerializer serializer = new XmlSerializer(typeof(List<Customer>));
            serializer.Serialize(writer, sellers);
            writer.Close();
        }
        public static void SaveAllContracts(List<Contract> contracts)
        {
            XmlWriter writer = XmlWriter.Create(contractFilePath);
            XmlSerializer serializer = new XmlSerializer(typeof(List<Contract>));
            serializer.Serialize(writer, contracts);
            writer.Close();
        }
        public static void SaveAllUsers(List<User> users)
        {
            XmlWriter writer = XmlWriter.Create(userFilePath);
            XmlSerializer serializer = new XmlSerializer(typeof(List<User>));
            serializer.Serialize(writer, users);
            writer.Close();
        }
        public static List<House> ReadAllHouses()
        {
            List<House> houses = new List<House>();
            XmlSerializer deserializer = new XmlSerializer(typeof(List<House>));
            if (File.Exists(houseFilePath))
            {
                StreamReader reader = new StreamReader(houseFilePath);
                houses = (List<House>)deserializer.Deserialize(reader);
                reader.Close();
            }
            return houses;
        }
        public static List<Customer> ReadAllSellers()
        {
            List<Customer> sellers = new List<Customer>();
            XmlSerializer deserializer = new XmlSerializer(typeof(List<Customer>));
            if (File.Exists(sellerFilePath))
            {
                StreamReader reader = new StreamReader(sellerFilePath);
                sellers = (List<Customer>)deserializer.Deserialize(reader);
                reader.Close();
            }
            return sellers;
        }
        public static List<Contract> ReadAllContracts()
        {
            List<Contract> contracts = new List<Contract>();
            XmlSerializer deserializer = new XmlSerializer(typeof(List<Contract>));
            if (File.Exists(contractFilePath))
            {
                StreamReader reader = new StreamReader(contractFilePath);
                contracts = (List<Contract>)deserializer.Deserialize(reader);
                reader.Close();
            }
            return contracts;
        }
        public static List<User> ReadAllUsers()
        {
            List<User> users = new List<User>();
            XmlSerializer deserializer = new XmlSerializer(typeof(List<User>));
            if (File.Exists(userFilePath))
            {
                StreamReader reader = new StreamReader(userFilePath);
                users = (List<User>)deserializer.Deserialize(reader);
                reader.Close();
            }
            return users;
        }

        public static User Login(string email,string password)
        {
            User user = ReadAllUsers().Find(usr => usr.Email.ToLower() == email.ToLower().Trim() && usr.Password == password); ; ;
            return user;
        }
        public static void SaveUserPhoto(string inputPath,string userId)
        {
            File.Copy(inputPath, userPhotosPath + userId, true);
        }
        public static Image ReadUserPhoto(string userId)
        {
            if(File.Exists(userPhotosPath + userId ))
            {
                Image image;
                using (var bmpTemp = new Bitmap(userPhotosPath + userId))
                {
                    image = new Bitmap(bmpTemp);
                }

                return image;
            }
            return null;
        }
        
    }
}
