using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*
            Singleton.GetInstance();
            SQLiteConnection conn = Singleton.GetInstance();
            
            string Table = "CREATE TABLE Project " +
                "(" +
                "id integer PRIMARY KEY AUTOINCREMENT," +
                "authorName varchar(64) not null," +
                "authorSurName varchar(64) not null," +
                "price REAL not null," +
                "prize1 INTEGER not null," +
                "prize2 INTEGER not null," +
                "prize3 INTEGER not null" +
                ")";
            SQLiteCommand cmd = new SQLiteCommand(Table, conn);

            cmd.ExecuteNonQuery();
            cmd.Dispose();
            */

            EnterpriseTable table = new EnterpriseTable();
            Enterprise enterprise = new Enterprise("Miska", 450, "chocolate","Ua");

            table.Save(enterprise);

            Console.WriteLine(enterprise.id);

            foreach (var item in table.GetAll())
            {
                Console.WriteLine(item);
            }

            enterprise.enterpriseName = "qwer";
            table.Save(enterprise);

            foreach (var item in table.GetAll())
            {
                Console.WriteLine(item);
            }

            table.Remove(enterprise);

            foreach (var item in table.GetAll())
            {
                Console.WriteLine(item);
            }

            Enterprise enterprise1 = table.GetById(1);
            Console.WriteLine(enterprise1);

            Console.WriteLine(table.GetAvg(2));

            Singleton.CloseConnection();

        }
    }
}
