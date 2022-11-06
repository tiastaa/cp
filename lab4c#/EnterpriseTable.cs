using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class EnterpriseTable
    {
        private string tableName;
        public EnterpriseTable()
        {
            tableName = "Enterprise";
        }
        public Enterprise GetById(int id)
        {
            Enterprise enterprise = null;
            SQLiteConnection conn = Singleton.GetInstance();

            using (SQLiteCommand command = new SQLiteCommand("SELECT * FROM " + tableName + " WHERE id = @Id", conn))
            {
                SQLiteParameter param = new SQLiteParameter();
                param.ParameterName = "@Id";
                param.Value = id;

                command.Parameters.Add(param);
                SQLiteDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    enterprise = new Enterprise
                    {
                        id = Convert.ToInt32(reader[0].ToString()),
                        enterpriseName = reader[1].ToString(),
                        employees = Convert.ToInt32(reader[2].ToString()),
                        productName = reader[3].ToString(),
                        country = reader[4].ToString(),

                    };
                }
                reader.Close();
                return enterprise;
            }
        }

        public IEnumerable<Enterprise> GetAll()
        {
            SQLiteConnection conn = Singleton.GetInstance();

            using (SQLiteCommand command = new SQLiteCommand("SELECT * FROM " + tableName, conn))
            {
                SQLiteDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Enterprise enterprise = new Enterprise
                    {
                        id = Convert.ToInt32(reader[0].ToString()),
                        enterpriseName = reader[1].ToString(),
                        employees = Convert.ToInt32(reader[2].ToString()),
                        productName = reader[3].ToString(),
                        country = reader[4].ToString(),
                        
                    };
                    yield return enterprise;
                }
                reader.Close();
            }
        }

        public int GetAvg(int x)
        {
            SQLiteConnection conn = Singleton.GetInstance();

            using (SQLiteCommand command = new SQLiteCommand("SELECT AVG(price) FROM " + tableName + " WHERE prize1 + prize2 + prize3 > " + x, conn))
            {
                SQLiteDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    return Convert.ToInt32(reader[0]);
                }
                return 0;
            }
        }

        public void Save(Enterprise enterprise)
        {
            SQLiteConnection conn = Singleton.GetInstance();

            SQLiteCommand command = null;

            if (enterprise.id < 1)
            {
                using (command = new SQLiteCommand("INSERT INTO " + tableName + "(enterpriseName, employees, productName, country) " +
                    "VALUES (@enterpriseName, @employees, @productName, @country)", conn))
                {
                    command.Parameters.AddWithValue("@enterpriseName", enterprise.enterpriseName);
                    command.Parameters.AddWithValue("@employees", enterprise.employees);
                    command.Parameters.AddWithValue("@productName", enterprise.productName);
                    command.Parameters.AddWithValue("@country", enterprise.country);

                    command.ExecuteNonQuery();
                    command.CommandText = "Select seq from sqlite_sequence where name = '" + tableName + "'";
                    enterprise.id = Convert.ToInt32(command.ExecuteScalar());
                }
            }
            else
            {
                using (command = new SQLiteCommand("UPDATE " + tableName +
                    " SET authorName = @enterpriseName, employees = @authorSurName, price = @price, prize1 = @prize1, prize2 = @prize2, prize3 = @prize3" +
                    " WHERE id = @id", conn))
                {
                    command.Parameters.Add(new SQLiteParameter("@authorName", enterprise.enterpriseName));
                    command.Parameters.Add(new SQLiteParameter("@authorSurName", enterprise.employees));
                    command.Parameters.Add(new SQLiteParameter("@price", enterprise.productName));
                    command.Parameters.Add(new SQLiteParameter("@prize1", enterprise.country));

                    command.Parameters.Add(new SQLiteParameter("@id", enterprise.id));
                    command.ExecuteNonQuery();
                }
            }

        }

        public void Remove(Enterprise enterprise)
        {
            SQLiteConnection conn = Singleton.GetInstance();

            using (SQLiteCommand command = new SQLiteCommand("DELETE FROM " + tableName + " WHERE id = @id", conn))
            {
                command.Parameters.Add(new SQLiteParameter("@id", enterprise.id));
                command.ExecuteNonQuery();
                enterprise.id = 0;
            }
        }
    }
}
