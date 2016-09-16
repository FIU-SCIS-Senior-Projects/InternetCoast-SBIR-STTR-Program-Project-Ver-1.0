using System;
using System.Linq;
using InternetCoast.Infrastructure.Data.EF.Context;
using InternetCoast.Model.Context;

namespace DbGenerator
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                using (var context = new AppDbContext(new UiContext()))
                {
                    var users = context.User.ToList();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException.Message);
                throw;
            }
            
            Console.WriteLine("DB created");
            Console.ReadKey();
        }
    }
}
