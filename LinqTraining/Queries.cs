using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace LinqTraining
{
    internal class Queries
    {
        static AdventureWorks2019Entities context = new AdventureWorks2019Entities();

        public IEnumerable<Address> GetAddresses(string city)
        {
            var addresses = (from a in context.Addresses
                             where a.City.Contains(city)
                             select new
                             {
                                 City = a.City,
                                 AddressLine1 = a.AddressLine1
                             }).ToList();

            foreach (var addr in addresses)
            {
                Console.WriteLine($"{addr.City} | {addr.AddressLine1}");
            }

            var silvia = addresses.Select(x => new Address
            {
                City = x.City,
                AddressLine1 = x.AddressLine1
            }).ToList();

            return silvia;
        }

        public IEnumerable<SalesOrderHeader> GetSales()
        {
            var sales = (from s in context.SalesOrderHeaders
                         orderby s.SubTotal
                         select new
                         {
                             SalesOrderID = s.SalesOrderID,
                             SubTotal = (s.TaxAmt * 100) / s.SubTotal
                         }).ToList().AsEnumerable();

            foreach (var sl in sales)
            {
                Console.WriteLine($"{sl.SalesOrderID} | {sl.SubTotal}");
            }
            var silvia = sales.Select(x => new SalesOrderHeader
            {
                SalesOrderID = x.SalesOrderID,
                SubTotal = x.SubTotal
            }).ToList();

            return silvia;
        }

        //public IEnumerable<Currency> SPAddCurrency(string Code, string Name, DateTime ModifiedDate)
        //{

        //}

        public IEnumerable<SalesOrderHeader> GetTaxPercentage()
        {
            var sales = (from s in context.SalesOrderHeaders
                         orderby s.SubTotal descending
                         select new
                         {
                             SalesOrderID = s.SalesOrderID,
                             CustomerID = s.CustomerID,
                             OrderDate = s.OrderDate,
                             SubTotal = s.SubTotal,
                             Tax_Percent = (s.TaxAmt * 100) / s.SubTotal
                         }).ToList();
            foreach (var s in sales)
            {
                Console.WriteLine($"{s.SalesOrderID} | {s.CustomerID} | {s.OrderDate} | {s.SubTotal} | {s.Tax_Percent}");
            }
            Console.WriteLine("Enter a key to continue ... ");
            Console.ReadKey();

            var salesReturn = sales.Select(x => new SalesOrderHeader
            {
                SalesOrderID = x.SalesOrderID
            });

            return salesReturn;
        }

        //EX 11

        public IEnumerable<Person> GetPersonAndPhoneByLastName(string lastName)
        {
            var person = (from p in context.Person
                          join pp in context.PersonPhones on p.BusinessEntityID equals pp.BusinessEntityID into p_pp
                          from subpp in p_pp.DefaultIfEmpty()
                          where p.LastName.Contains(lastName)
                          select new
                          {
                              BusinessEntityID = p.BusinessEntityID,
                              LastName = p.LastName,
                              FirstName = p.FirstName,
                              PhoneNumber= subpp.PhoneNumber
                          }).ToList();

            foreach(var p in person)
            {
                Console.WriteLine($"{p.BusinessEntityID} | {p.FirstName} | {p.LastName} | {p.PhoneNumber} | {p.PhoneNumber}");
            }

            Console.ReadKey();
            var personPerson = person.Select(x => new Person
            {
                FirstName = x.FirstName,
                LastName = x.LastName,
            });
            return personPerson;
        }

        //EX22
        public void GetNumberOfContacts(int number)
        {
            var contacts = (from c in context.ContactTypes
                            join bec in context.BusinessEntityContacts on c.ContactTypeID equals bec.ContactTypeID into c_bec
                            from subc_bec in c_bec.DefaultIfEmpty()
                            group subc_bec by new { c.ContactTypeID, c.Name } into g
                            select new
                            {
                                ContactTypeID = g.Key.ContactTypeID,
                                Name = g.Key.Name,
                                Count = g.Count(x=>x!=null)
                            }).Where(x => x.Count>=100).ToList();
            
            foreach (var contact in contacts)
            {
                Console.WriteLine($"{contact.ContactTypeID} | {contact.Name} | {contact.Count}");
            }

            Console.ReadKey();
        }

        //ex23
        public void GetSalaryInAWeek()
        {
            var salary = (from p in context.Person
                          join eph in context.EmployeePayHistories
                          on p.BusinessEntityID equals eph.BusinessEntityID into p_eph
                          from subp_eph in p_eph.DefaultIfEmpty()
                          select new
                          {
                              LastName = p.LastName,
                              //SalaryPerWeek = (decimal?)(subp_eph.PayFrequency * 40)
                          }).ToList();

            foreach(var s in salary)
            {
                Console.WriteLine($"{s.LastName}");
            }

            Console.ReadKey();

        }

    }



}

