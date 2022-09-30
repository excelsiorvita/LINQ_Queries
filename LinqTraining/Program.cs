using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqTraining
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Queries queries = new Queries();
            Console.WriteLine("Enter city");
            string city =Console.ReadLine();
            queries.GetAddresses(city);

            queries.GetSales();

            queries.GetTaxPercentage();

            Console.WriteLine("Enter last name");
            string lastName = Console.ReadLine();
            queries.GetPersonAndPhoneByLastName(lastName);

            Console.WriteLine("Count group join");
            queries.GetNumberOfContacts(100);

            Console.WriteLine("Salary");
            queries.GetSalaryInAWeek();
        }
    }
}
