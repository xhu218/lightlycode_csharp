﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Xml.Linq;
using static System.Console;

namespace JsonSample
{
    class Program
    {
        private const string InventoryFileName = "inventory.json";
        private const string InventoryXmlFileName = "inventory.xml";

        private const string CreateJsonOption = "-j";
        private const string ConvertOption = "-c";
        private const string SerializeOption = "-s";
        private const string DeserializeOption = "-d";
        private const string ReadOption = "-r";

        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                ShowUsage();
                return;
            }

            switch (args[0])
            {
                case CreateJsonOption:
                    CreateJson();
                    break;
                case ConvertOption:
                    ConvertObject();
                    break;
                case SerializeOption:
                    SerializeJson();
                    break;
                case DeserializeOption:
                    DeserializeJson();
                    break;
                case ReadOption:
                    ReaderSample();
                    break;
                default:
                    ShowUsage();
                    break;
            }
        }



        private static void ShowUsage()
        {
            WriteLine("XmlReaderAndWriterSample options");
            WriteLine("\tOptions");
            WriteLine($"\t{CreateJsonOption}\tCreate JSON");
            WriteLine($"\t{ConvertOption}\tConvert Object");
            WriteLine($"\t{SerializeOption}\tSerialize");
            WriteLine($"\t{DeserializeOption}\tDeserialize");
            WriteLine($"\t{ReadOption}\tUse Reader");
        }


        public static void SerializeJson()
        {
            using (StreamWriter writer = File.CreateText(InventoryFileName))
            {
                JsonSerializer serializer = JsonSerializer.Create(new JsonSerializerSettings { Formatting = Formatting.Indented });
                serializer.Serialize(writer, GetInventoryObject());
            }
        }

        public static void ConvertObject()
        {
            Inventory inventory = GetInventoryObject();
            string json = JsonConvert.SerializeObject(inventory, Formatting.Indented);
            WriteLine(json);
            WriteLine();
            Inventory newInventory = JsonConvert.DeserializeObject<Inventory>(json);
            foreach (var product in newInventory.InventoryItems)
            {
                WriteLine(product.ProductName);
            }
        }

        public static Inventory GetInventoryObject() =>
            new Inventory
            {
                InventoryItems = new Product[]
                {
                    new Product
                    {
                        ProductID = 100,
                        ProductName = "Product Thing",
                        SupplierID = 10
                    },
                    new BookProduct
                    {
                        ProductID = 101,
                        ProductName = "How To Use Your New Product Thing",
                        SupplierID = 10,
                        ISBN = "1234567890"
                    }
                }
            };


        public static void DeserializeJson()
        {
            using (StreamReader reader = File.OpenText(InventoryFileName))
            {
                JsonSerializer serializer = JsonSerializer.Create();
                var inventory = serializer.Deserialize(reader, typeof(Inventory)) as Inventory;
                foreach (var item in inventory.InventoryItems)
                {
                    WriteLine(item.ProductName);
                }
            }
        }


        public static string ConvertXmlToJson()
        {
            XElement xmlInventory = XElement.Load(InventoryXmlFileName);
            return JsonConvert.SerializeXNode(xmlInventory);
        }

        private static void CreateJson()
        {
            var book1 = new JObject();
            book1["title"] = "Professional C# 6 and .NET 5 Core";
            book1["publisher"] = "Wrox Press";
            var book2 = new JObject();
            book2["title"] = "Professional C# 5 and .NET 4.5.1";
            book2["publisher"] = "Wrox Press";
            var books = new JArray();
            books.Add(book1);
            books.Add(book2);

            var json = new JObject();
            json["books"] = books;
            WriteLine(json);
        }



        public static void ReaderSample()
        {
            StreamReader textReader = File.OpenText(InventoryFileName);
            using (JsonTextReader jsonReader = new JsonTextReader(textReader) { CloseInput = true })
            {
                while (jsonReader.Read())
                {

                }
            }
        }
    }
}
