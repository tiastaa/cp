using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Enterprise
    {
        public int id { get; set; }
        public string enterpriseName { get; set; }
        public int employees { get; set; }
        public string productName { get; set; }
        public string country { get; set; }


        public Enterprise(int id, string enterpriseName, int employees, string productName, string country)
        {
            this.id = id;
            this.enterpriseName = enterpriseName;
            this.employees = employees;
            this.productName = productName;
            this.country = country;
        
        }

        public Enterprise( string enterpriseName, int employees, string productName, string country)
        {

            this.id = 0;
            this.enterpriseName = enterpriseName;
            this.employees = employees;
            this.productName = productName;
            this.country = country;
        }
        public Enterprise() { }

        public override string ToString()
        {
            return $"EnterpriseName:{enterpriseName}  with id {id}";
        }
    }
}
