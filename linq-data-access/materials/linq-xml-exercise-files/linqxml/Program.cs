using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Reflection;

namespace linqxml
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateSimpleXml();
            ReadSimpleXml();
            CreateTypeInfo();
            Transformation();
   
        }

        private static void Transformation()
        {
            XDocument document = XDocument.Load("employees.xml");
            XDocument transformed = new XDocument(
                    new XElement("Employees",
                        new XElement("Developers",
                            from e in document.Descendants("Employee")
                            where e.Attribute("Type").Value == "Developer"
                            select new XElement("Employee", e.Value)),
                        new XElement("Sales",
                            from s in document.Descendants("Employee")
                            where s.Attribute("Type").Value == "Sales"
                            select new XElement("Employee", s.Value))));

        }

        private static void CreateTypeInfo()
        {
            XDocument doc = new XDocument(
                new XElement("Types",
                    Assembly.GetExecutingAssembly().GetReferencedAssemblies()
                            .Select(name => Assembly.Load(name))
                            .SelectMany(assembly => assembly.GetTypes())
                            .Select(type => new XElement("Type", type.FullName,
                                              new XAttribute("IsPublic", type.IsPublic)))));

            var publicTypes =
                doc.Element("Types").Elements("Type")
                   .Where(e => (bool)e.Attribute("IsPublic") == true)
                   .Select(e => e.Value)
                   .OrderByDescending(s => s.Length);

            foreach (var name in publicTypes)
            {
                Console.WriteLine(name);
            }



        }

        private static void ReadSimpleXml()
        {
            XNamespace ns = "http://pluralsight.com/Modules";
            XDocument doc = XDocument.Load("modules.xml");

            var elements =
                doc.Descendants(ns + "Module"); 

            foreach (var element in elements)
            {
                string value = (string)element;

            }

        }

        private static void CreateSimpleXml()
        {
            XNamespace ns = "http://pluralsight.com/Modules";
            XNamespace ext = "http://pluralsight.com/Modules/Ext";


            XDocument doc = new XDocument(
                new XElement(ns + "Modules",
                    new XAttribute(XNamespace.Xmlns + "ext", ext),
                    new XElement(ns + "Module", "Introduction to LINQ"),
                    new XElement(ns + "Module", "LINQ and C#"), 
                    new XElement(ext + "Extra", "Some content"),
                    new XElement(ext + "Extra", "Extra content")));


            doc.Root.SetElementValue(ext + "Foo", "bar");
            doc.Root.SetElementValue(ext + "Foo", "baz");
            doc.Root.SetElementValue(ext + "Foo", null);


            doc.Save("modules.xml");            
                 
        }
    }
}
