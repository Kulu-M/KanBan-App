using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;

namespace Backend
{
    public class Program
    {
        /// <summary>
        /// Entry Point of Backend
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            //Working Code to fill SQLite Database
            //using (var db = new APIAppDbContext())
            //{
            //    db.User.Add(new User { EMail = "123", Password = "2"});
            //    db.SaveChanges();

            //    Console.WriteLine();
            //    Console.WriteLine("All Users in database:");
            //    foreach (var user in db.User)
            //    {
            //        Console.WriteLine(" - {0}", user.EMail);
            //    }
            //}

            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
