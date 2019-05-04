using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTable
{
    class Program
    {
        static void Main(string[] args)
        {
            FirstTest();
            SecondTest();
            ThirdTest();
            FourthTest();

            Console.ReadKey();
        }

        private static void FirstTest()
        {
            var table = new HashTable(3);

            table.PutPair(1, "aaaaa");
            table.PutPair("second", "bbbbb");
            table.PutPair(56, 1000);

            var first = table.GetValueByKey(1);
            var second = table.GetValueByKey("second");
            var third = table.GetValueByKey(56);

            if ((string)first == "aaaaa" && (string)second == "bbbbb" && (int)third == 1000)
                Console.WriteLine("First test successful");
            else
                Console.WriteLine("First test fail");
        }

        private static void SecondTest()
        {
            var table = new HashTable(3);

            table.PutPair(1, "aaaaa");
            table.PutPair(1, "bbbbb");

            var value = table.GetValueByKey(1);

            if((string)value == "bbbbb")
                Console.WriteLine("Second test successful");
            else
                Console.WriteLine("Second test fail");
        }

        private static void ThirdTest()
        {
            var table = new HashTable(10000);

            for(var i = 0; i < 10000; i++)
                table.PutPair(i, i);

            var value = table.GetValueByKey(4567);

            if((int)value == 4567)
                Console.WriteLine("Third test successful");
            else
                Console.WriteLine("Third test fail");
        }

        private static void FourthTest()
        {
            var table = new HashTable(10000);
            var rand = new Random();

            for (var i = 0; i < 10000; i++)
                table.PutPair(i, i);

            var res = true;
            for(var i = 0; i < 1000; i++)
            {
                var value = table.GetValueByKey(rand.Next(11000, 20000));
                if (value != null)
                    res = false;
            }

            if(res)
                Console.WriteLine("Fourth test successful");
            else
                Console.WriteLine("Fourth test fail");
        }
    }
}
