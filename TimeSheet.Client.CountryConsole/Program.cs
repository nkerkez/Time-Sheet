using System;
using TimeSheet;
//using System.Configuration;
//using TimeSheet.Data.Repository;
//using TimeSheet.Services;

namespace TimeSheet.Client.CountryConsole
{
    public class Program
    {
        //private static CountryConsoleClient client = new CountryConsoleClient(new CountryService(new CountryRepository(ConfigurationManager.ConnectionStrings["TimeSheet"].ConnectionString)));
        private static Application timesheet = new Application();
        private static CountryConsoleClient client = new CountryConsoleClient(timesheet.CountryService);

        static void Main(string[] args)
        {
            Print();
        }

        private static void PrintOperations()
        {
            Console.WriteLine("1. Prikazi sve");
            Console.WriteLine("2. Prikazi sa ID - em");
            Console.WriteLine("3. Dodaj");
        }

        private static void PrintCountryOptions()
        {
            PrintOperations();
            int options = int.Parse(Console.ReadLine());
            

            switch(options)
            {
                case 1: client.GetAll();
                    break;
                case 2:
                    {
                        client.GetById();
                        
                    }
                    break;
                case 3: client.Add();
                    break;
            }
            
        }
        private static Guid GetId()
        {
            Console.WriteLine("Unesite ID");
            string text = Console.ReadLine();
            Guid id;
            try
            {
                id = new Guid(text);
            }finally
            {
               
            }
            return id;
            
        }
        public static void Print()
        {
            Console.WriteLine("1. Rad sa drzavama");
            Console.WriteLine("2. Rad sa kategorijama");

            int option = int.Parse(Console.ReadLine());
            
            switch(option)
            {
                case 1: PrintCountryOptions();
                    break;
            }
        }
        

        
    }
}
