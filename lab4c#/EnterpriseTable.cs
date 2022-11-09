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

        public int GetAvg()
        {
            SQLiteConnection conn = Singleton.GetInstance();

            using (SQLiteCommand command = new SQLiteCommand("SELECT AVG(employees) FROM " + tableName , conn))
            {
                SQLiteDataReader reader = command.ExecuteReader();
                int Avg = 0;
                int k = 0;
                while (reader.Read())
                {
                    Avg=Avg+Convert.ToInt32(reader[0]);
                    k++;
                }
                return Avg/k;
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
                    " SET enterpriseName = @enterpriseName, employees = @enterpriseName, productName = @productName, country = @country" +
                    " WHERE id = @id", conn))
                {
                    command.Parameters.Add(new SQLiteParameter("@enterpriseName", enterprise.enterpriseName));
                    command.Parameters.Add(new SQLiteParameter("@employees", enterprise.employees));
                    command.Parameters.Add(new SQLiteParameter("@productName", enterprise.productName));
                    command.Parameters.Add(new SQLiteParameter("@country", enterprise.country));

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
