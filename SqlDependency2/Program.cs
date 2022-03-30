// See https://aka.ms/new-console-template for more information

using SqlDependency2;
using System;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base;
using TableDependency.SqlClient.Base.EventArgs;

namespace MyApp // Note: actual namespace depends on the project name.
{
    public class Program
    {
        private static string _con = "Server=.;Database=ERP;Trusted_Connection=True;";
         
        static void Main(string[] args)
        {
            var mapper = new ModelToTableMapper<Notifications>();
            mapper.AddMapping(c => c.Message, "Message");
            mapper.AddMapping(c => c.Url, "Url");

            using (var dep = new SqlTableDependency<Notifications>(_con, "Notifications", "Identity" ,mapper: mapper))
            {
                dep.OnChanged += Changed;
                dep.Start();

                Console.WriteLine("Press a key to exit");
                Console.ReadKey();

                dep.Stop();
            }
        }

        public static void Changed(object sender, RecordChangedEventArgs<Notifications> e)
        {
            var changedEntity = e.Entity;

            Console.WriteLine("DML operation: " + e.ChangeType);
            Console.WriteLine("Message: " + changedEntity.Message);
            Console.WriteLine("Url: " + changedEntity.Url);
            //Console.WriteLine("Surname: " + changedEntity.Surname);
        }
    }
}





