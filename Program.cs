using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWorkASD3
{
    class Program
    {
        public static void print(int[] mas)
        {
            for (int i = 0; i < mas.Length; i++)
            {
                Console.Write(mas[i] + " ");
            }
        }

        public static void printWithColor(string str, ConsoleColor cc)
        {
            Console.ForegroundColor = cc;
            Console.WriteLine(str);
            Console.ResetColor();
        }

        public static void sorts(Random r, int size, bool isPrint)
        {
            int[] mas = new int[] { 3, 1, 2, 2 };

            //for (int i = 0; i < mas.Length; i++)
            //    mas[i] = r.Next(0, size * 2);

            int[] arr1 = (int[])mas.Clone();
            int[] arr2 = (int[])mas.Clone();
            int[] arr3 = (int[])mas.Clone();
            int[] arr4 = (int[])mas.Clone();

            printWithColor("Кол-во элементов массивов: " + size, ConsoleColor.Blue);
            //fullSort(mas, bubbleSort, "Bubble sort", isPrint);
            //fullSort(arr1, quickSort, "Quick sort", isPrint);
            //fullSort(arr2, gnomeSort, "Gnome sort", isPrint);
            fullSort(arr3, radixSort, "Radix sort", isPrint);

            //printWithColor("Auto sort", ConsoleColor.Green);
            //Stopwatch sw = new Stopwatch();
            //sw.Start();
            //Array.Sort(arr4);
            //sw.Stop();
            //printWithColor("Время: " + sw.Elapsed.TotalMilliseconds / 1000d, ConsoleColor.Yellow);
        }

        public static void fullSort(int[] arr, Func<int[], int, int, int[]> func, string nameSort, bool isPrint)
        {
            Stopwatch sw = new Stopwatch();
            printWithColor(nameSort, ConsoleColor.Green);

            if (isPrint)
            {
                Console.Write("Неотсортированый массив: ");
                print(arr);
                Console.WriteLine();
            }
            sw.Start();
            func(arr, 0, arr.Length - 1);
            sw.Stop();
            if (isPrint)
            {
                Console.Write("Отсортированый массив: ");
                print(arr);
                Console.WriteLine();
            }
            printWithColor("Время: " + sw.Elapsed.TotalMilliseconds / 1000d, ConsoleColor.Yellow);
            Console.WriteLine();
            Console.WriteLine();
        }

        // Function for Radix sort
        public static int[] radixSort(int[] arr, int start, int end) {
            int[] tempArr = new int[arr.Length];

            int r = 16;

            int b = 32;

            int[] count = new int[1 << r];
            int[] pref = new int[1 << r];

            int groups = (int)Math.Ceiling((double)b / (double)r);

            int mask = (1 << r) - 1;

            for (int c = 0, shift = 0; c < groups; c++, shift += r) {

                for (int j = 0; j < count.Length; j++)
                    count[j] = 0;

                for (int i = 0; i < arr.Length; i++)
                    count[(arr[i] >> shift) & mask]++;

                pref[0] = 0;
                for (int i = 1; i < count.Length; i++) {
                    pref[i] = pref[i - 1] + count[i - 1];
                }

                for (int i = 0; i < arr.Length; i++) {
                    tempArr[pref[(arr[i] >> shift) & mask]++] = arr[i];
                }

                tempArr.CopyTo(arr, 0);
            }
            return arr;
        }

        // Function for Bubble sort
        public static int[] bubbleSort(int[] arr, int start, int end) {
            int temp;
            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = i + 1; j < arr.Length; j++)
                {
                    if (arr[i] > arr[j])
                    {
                        temp = arr[i];
                        arr[i] = arr[j];
                        arr[j] = temp;
                    }
                }
            }
            return arr;
        }

        // Function for Quick sort
        public static int[] quickSort(int[] mas, int start, int end)
        {
            if (start < end)
            {
                var q = partition(mas, start, end);
                quickSort(mas, start, q);
                quickSort(mas, q + 1, end);
            }
            return mas;
        }

        public static int partition(int[] mas, int start, int end)
        {
            var marker = start - 1;
            for (var i = start; i <= end; i++)
            {
                if (mas[i] <= mas[end])
                {
                    marker++;
                    var temp = mas[marker];
                    mas[marker] = mas[i];
                    mas[i] = temp;
                }
                
            }
            return marker < end ? marker : marker - 1;
        }



        // Function for Gnome sort
        public static int[] gnomeSort(int[] a, int start, int end)
        {
            int i = 1;
            while (i < a.Length)
            {
                if (i == 0 || a[i - 1] <= a[i])
                    i++;
                else
                {
                    int temp = a[i];
                    a[i] = a[i - 1];
                    a[i - 1] = temp;                
                    i--;
                }
            }
            return a;
        }


        static void Main(string[] args)
        {
            bool isPrWork;
            try
            {
                do
                {
                    Random r = new Random();
                    Console.Clear();
                    Console.Write("Введите размер массива: ");
                    int size = Convert.ToInt32(Console.ReadLine());

                    Console.Write("Надо ли выводить массивы или только замеры времени?(Да => 1   Нет => 0): ");
                    bool isPrint = Convert.ToBoolean(Convert.ToByte(Console.ReadLine()));

                    Console.Write("Сколько раз проверять? ");
                    byte count = Convert.ToByte(Console.ReadLine());

                    for (int i = 0; i < count; i++)
                    {
                        sorts(r, i < count / 2 ? size : size * 2, isPrint);
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine();
                    }

                    printWithColor("Еще тест?))(Да => 1   Нет => 0): ", ConsoleColor.Red);
                    isPrWork = Convert.ToBoolean(Convert.ToByte(Console.ReadLine()));
                }
                while (isPrWork);
            }
            catch (Exception) {
                Console.WriteLine("Ошибка!!!");
                Console.ReadKey();
            }
            
        }
    }
}
