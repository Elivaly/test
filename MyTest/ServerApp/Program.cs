using Microsoft.EntityFrameworkCore;
using MyTest;
using System.Data;
namespace Server
{
    internal class Program
    {
        public static void Main()
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDBContent>();
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=UserManager;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

            using (var content = new AppDBContent(optionsBuilder.Options))
            {
                while (true)
                {
                    var sendingEmails = content.Messages
                        .Where(ev => !ev.IsSent)
                        .ToList();

                    foreach (var emailVerification in sendingEmails)
                    {
                        Console.WriteLine($"{DateTime.Now} {emailVerification.Email} код: {emailVerification.Code}");
                        emailVerification.IsSent = true;
                    }
                    content.SaveChanges();
                    Thread.Sleep(30000);
                }
            }
            Console.ReadKey();
        }
    }
}
