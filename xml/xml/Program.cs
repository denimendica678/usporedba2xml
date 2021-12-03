using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace xml
{
    class Program
    {
        static void Main(string[] args)
        {
            
            string s1 = @"<Books>
                 <book id='20504' image='C01' name='C# in Depth'/>
                 <book id='20505' image='C02' name='ASP.NET'/>
                 <book id='20506' image='C03' name='LINQ in Action '/>
                 <book id='20507' image='C04' name='Architecting Applications'/>
                </Books>";
            string s2 = @"<Books>
                  <book id='20504' image='C011' name='C# in Depth'/>
                  <book id='20505' image='C02' name='ASP.NET 2.0'/>
                  <book id='20506' image='C03' name='LINQ in Action '/>
                  <book id='20508' image='C04' name='Architecting Applications'/>
                </Books>";

            XDocument prviDoc = XDocument.Parse(s1);
            XDocument drugiDoc = XDocument.Parse(s2);

            var result1 = from xmlBooks1 in prviDoc.Descendants("book")
                          from xmlBooks2 in drugiDoc.Descendants("book")
                          select new
                          {
                              book1 = new
                              {
                                  id = xmlBooks1.Attribute("id").Value,
                                  image = xmlBooks1.Attribute("image").Value,
                                  name = xmlBooks1.Attribute("name").Value
                              },
                              book2 = new
                              {
                                  id = xmlBooks2.Attribute("id").Value,
                                  image = xmlBooks2.Attribute("image").Value,
                                  name = xmlBooks2.Attribute("name").Value
                              }
                          };
            var result2 = from i in result1
                          where (i.book1.id == i.book2.id
                                 || i.book1.image == i.book2.image
                                 || i.book1.name == i.book2.name) &&
                                 !(i.book1.id == i.book2.id
                                 && i.book1.image == i.book2.image
                                 && i.book1.name == i.book2.name)
                          select i;




            foreach (var aa in result2)
            {
                Console.WriteLine("Razlike: " + aa.book1);
                Console.WriteLine("         " + aa.book2 + "\r\n");
            }
            
            Console.ReadKey();

        }

    }
}
