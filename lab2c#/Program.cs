using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace ManXML
{
    public class Man
    {
        public string name { get; set; }
        public  int age { get; set; }
        public string eyes { get; set; }
        public int salary { get; set; }
        public bool apartment { get; set; }

        

        public Man(string name, int age, string eyes, int salary , bool apartment)
        {
            this.name = name;
            this.age = age;
            this.eyes = eyes;
            this.salary = salary;
            this.apartment = apartment;

        }

        public Man()
        {

        }

        public override string ToString()
        {
            return $"Man: {name}, {age} y.o., {eyes} eyes, salary {salary}$, apartment {apartment}";
        }
    }
    public class Men
    {
        public List<Man> men = new List<Man>();

        public Men()
        {

        }

        public void Add(string name, int age, string eyes, int salary, bool apartment)
        {
            men.Add(new Man(name, age , eyes , salary , apartment));
        }

        public void CreatePO(string filename)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Men));
            FileStream fs = new FileStream(filename, FileMode.OpenOrCreate);
            using (fs)
            {
                serializer.Serialize(fs, this);
            }
        }

        public void ReadPO(string filename)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Men));
            FileStream fs = new FileStream(filename, FileMode.OpenOrCreate);
            using (fs)
            {
                Men obj = (Men)serializer.Deserialize(fs);
                this.men = obj.men;
            }
        }

        public void Delete()
        {
            //float maxPrice = this.toys.Max(x => x.price);
            this.men = this.men.Where(elem => elem.apartment == true).ToList();
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            string fileName = @"C:\Users\NIKA\source\repos\ConsoleApp3\ConsoleApp3\XMLFile1.xml";

            Men men = new Men();
            men.Add("J-Hope", 28, "blue", 5380000, true);
            men.Add("RM", 28, "hazel", 3090000, false);
            men.Add("Jimin", 27, "green", 3850000, false);
            men.Add("Jin", 30, "hazel", 950000, true);
            men.Add("Suga", 29, "hazel", 490000, true);
            men.Add("V", 27, "hazel", 90543000, true);
            men.Add("Jungkook", 25, "blue", 5021000, false);
            men.Add("Tamahome", 17, "green", 70000, false);
            men.Add("Hotohori", 21, "hazel", 683000, true);
            men.Add("Nuriko", 20, "blue", 989000, false);
            men.CreatePO(fileName);

            Men men2 = new Men();
            men2.ReadPO(fileName);


            foreach (Man man in men2.men)
            {
                Console.WriteLine(man.ToString());
            }

            men2.Delete();

            foreach (Man man in men2.men)
            {
                Console.WriteLine(man.ToString());
            }
            Console.WriteLine();

            var task = men.men
                .GroupBy(group => $"{group.age}")
                .Select(item => new { item.Key, Value = item.Count() });

            foreach (var item in task)
            {
                Console.WriteLine($"Age: {item.Key}, number of men: {item.Value}");
            }
            Console.WriteLine();
        }
    }
}