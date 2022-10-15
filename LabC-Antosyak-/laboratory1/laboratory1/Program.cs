using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ConsoleApp5
{
    public class Cars
    {
        protected int id;
        public int Id { get { return id; } set { id = value; } }
        protected string surname;
        public string Surname { get { return surname; } set { surname = value; } }
        protected int number;
        public int Number { get { return number; } set { number = value; } }

        protected string brand;
        public string Brand { get { return brand; } set { brand = value; } }
        protected int price;
        public int Price { get { return price; } set { price = value; } }
        
        protected string adress;
        public string Adress { get { return adress; } set { adress = value; } }
        

        public Cars(int id,string surname, int number, string brand, int price, string adress)
        {
            this.id = id;
            this.surname = surname;
            this.number = number;
            this.brand = brand;
            this.price = price;
            this.adress = adress;
            
        }
        public override string ToString()
        {
            return $" Surname: {surname} Number: {number}  Brand: {brand} Price: {price}  Adress: {adress}";
        }
    }
    public interface IListCars
    {
        void Add(Cars cars);
        void Delete(int id);
        void EditPrice(int id,int price);
        void EditAdress(int id,string adress);
        void Show();
    }
    public class ListCar : IListCars
    {
        protected List<Cars> cars;
        public List<Cars> Cars { get { return cars; } set { cars = value; } }
        public ListCar(List<Cars> cars)
        {
            this.cars = cars;
        }
        public void Add(Cars car)
        {
            cars.Add(car);
        }
        public void Delete(int id)
        {
            try
            {
                cars = cars.Where(item => item.Id != id).ToList();
            }
            catch (Exception exexception)
            {
                Console.WriteLine(exexception.Message);
                Console.WriteLine("Something wrong,please check the id.");
            }
        }
        public void EditPrice(int id, int price)
        {
            try
            {
                cars.First(item => item.Id == id).Price = price;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                Console.WriteLine("Something wrong,please check the id.");
            }
        }
        public void EditAdress(int id, string adress)
        {
            try
            {
                cars.First(item => item.Id == id).Adress = adress;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                Console.WriteLine("Something wrong,please check the id.");
            }
        }
        public void Show()
        {
            foreach (Cars car in cars)
            {
                Console.WriteLine(car);
            }
            Console.WriteLine();
        }
    }
        class Program
    {
        static void Main(string[] args)
        {

            ListCar cars = new ListCar(new List<Cars>
            {
                new Cars(1,"Yovbak", 6788, "Porsche",300000,"Uzhgorod"),
                new Cars(2,"Romanov", 7890, "Mercedes",120000,"Svalyava"),
                new Cars(3,"Kopolivich", 0000, "Audi",160000,"Chop"),
                new Cars(4,"Rozymnyak", 0987, "BMW",90000,"Rakhiv"),
                new Cars(5,"Kot", 5678, "Lada",5000,"Beregovo"),
               });
            cars.Show();
            cars.Add(new Cars(5, "Petrenko", 3476, "BMW", 35000, "Mukachevo"));
            cars.Show();
            cars.Delete(3);
            cars.Delete(9);
            cars.Show();
            cars.EditPrice(3, 130000);
            cars.Show();
            cars.EditAdress(5,"Beregovo");
            cars.Show();
            cars.EditPrice(7, 8000);
            cars.EditAdress(12, "Kyiv");
            Console.Write("Бренд:  ");
            var brand = Convert.ToString(Console.ReadLine());
            var task = cars.Cars.Where(item => item.Brand == brand);
            var k = 0;

            foreach (var el in task)
            {
                var x = el.Number;
                   while(x > 0)
                {
                    if (x % 10 == 7)
                    {
                        k++;
                    }
                    x = x / 10;
                }

                
            }
            Console.WriteLine("Cars amount: {0}", k);

            var k2 = cars.Cars.GroupBy(group => group.Brand).Select(item => new { item.Key, Value = item.Count() });
            foreach (var num in k2)
            {
                Console.WriteLine($"Brand: {num.Key}, number of cars: {num.Value}");
            }
        }
    }
}