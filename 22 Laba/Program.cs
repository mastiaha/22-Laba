using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _22_Laba
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите количество элементов массива");
            int n = Convert.ToInt32(Console.ReadLine());
         
            Func<object, int[]> func1 = new Func<object, int[]>(GetArray);
            Task<int[]> task1 = new Task<int[]>(func1, n);

            Func<Task<int[]>, int[]> func2 = new Func<Task<int[]>, int[]>(SumArray);
            Task<int[]> task2 = task1.ContinueWith<int[]>(func2);

            Func<Task<int[]>, int[]> func3 = new Func<Task<int[]>, int[]>(SumArray);
            Task<int[]> task3 = task2.ContinueWith<int[]>(func3);

            Action<Task<int[]>> action = new Action<Task<int[]>>(PrintArray);
            Task task4 = task3.ContinueWith(action);

            task1.Start();
            Console.ReadKey();
        }

        static int[] GetArray(object a)
        {
            int n = (int)a;
            int[] array = new int[n];
            Random random = new Random();
            for (int i = 0; i < n; i++)
            {
                array[i] = random.Next(0, 100);
            }
            return array;
        }

        static int[] SumArray(Task<int[]>task)
        {         
            int[] array = task.Result;
            int rez = array.Sum();
            return array;
        }
        static int[] MaxValue(Task<int[]> task)
        {
            int[] array = task.Result;
            int maxValue = array.Max();
            return array;
        }

        static void PrintArray (Task<int[]> task)
        {
            int[] array = task.Result;
            for (int i = 0; i < array.Count(); i++)
            {
                Console.Write($"{array[i]} ");
            }
            Console.WriteLine("\n");
            
            Console.WriteLine("Сумма всех элементов массива составляет {0}",array.Sum());
            Console.WriteLine("Наибольшее число массива {0}", array.Max());
            Console.WriteLine("Для выхода из программы нажмите любую клавишу");
        }
    }
}
