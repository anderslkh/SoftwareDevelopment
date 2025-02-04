using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Session1
{
    public class BenchmarkSestoft
    {
        public static void Main()
        {
            /*
            Console.WriteLine("--- Mark 0 ---");
            Mark0();
            Console.WriteLine("\n--- Mark 1 ---");
            Mark1();
            Console.WriteLine("\n--- Mark 2 ---");
            Mark2();
            Console.WriteLine("\n--- Mark 3 ---");
            Mark3();
            */
            //Console.WriteLine("\n--- Mark 4 ---");
            //Mark4();
            //Console.WriteLine("\n--- Mark 5 ---");
            //Mark5();
            Console.WriteLine("\nBenchmark Iterative Search");
            BenchmarkIterativeBinarySearch();
            Console.WriteLine("\nBenchmark Recursive Search");
            BenchmarkRecursiveBinarySearch();

        }

        private static double multiply(int i)
        {
            double x = 1.1 * (double)(i & 0xFF);
            return x * x * x * x * x * x * x * x * x * x
            * x * x * x * x * x * x * x * x * x * x;
        }

        public static void Mark0()
        { // USELESS
            Timer t = new Timer();
            double dummy = multiply(10);
            double time = t.Check() * 1e9;
            Console.WriteLine("{0,6:0.0}", time);
            //System.out.printf("%6.1f ns%n", time);
        }

        public static void Mark1()
        { // NEARLY USELESS
            Timer t = new Timer();
            int count = 1000000;
            for (int i = 0; i < count; i++)
            {
                double dummy = multiply(i);
            }
            double time = t.Check() * 1e9 / count;
            Console.WriteLine("{0,6:0.0}", time);
            //System.out.printf("%6.1f ns%n", time);
        }

        public static double Mark2()
        {
            Timer t = new Timer();
            int count = 100000000;
            double dummy = 0.0;
            for (int i = 0; i < count; i++)
                dummy += multiply(i);
            double time = t.Check() * 1e9 / count;
            Console.WriteLine("{0,6:0.0}", time);
            //System.out.printf("%6.1f ns%n", time);
            return dummy;
        }

        public static double Mark3()
        {
            int n = 10;
            int count = 100000000;
            double dummy = 0.0;
            for (int j = 0; j < n; j++)
            {
                Timer t = new Timer();
                for (int i = 0; i < count; i++)
                    dummy += multiply(i);
                double time = t.Check() * 1e9 / count;
                Console.WriteLine("{0,6:0.0}", time);
                //System.out.printf("%6.1f ns%n", time);
            }
            return dummy;
        }

        static double Mark4()
        {
            int n = 30;
            int count = 100000000;
            double dummy = 0.0;
            double st = 0.0, sst = 0.0;
            for (int j = 0; j < n; j++)
            {
                Timer t = new Timer();
                for (int i = 0; i < count; i++)
                    dummy += multiply(i);
                double time = t.Check() * 1e9 / count;
                st += time;
                sst += time * time;
            }
            double mean = st / n, sdev = Math.Sqrt((sst - mean * mean * n) / (n - 1));
            Console.WriteLine("{0,6:0.0} ns +/- {1,6:0.000}", mean, sdev);
            //Console.WriteLine("%6.1f ns +/- %6.3f %n", mean, sdev);
            //System.out.printf("%6.1f ns +/- %6.3f %n", mean, sdev);
            return dummy;
        }

        public static double Mark5()
        {
            int n = 10, count = 1, totalCount = 0;
            double dummy = 0.0, runningTime = 0.0;
            do
            {
                count *= 2;
                double st = 0.0, sst = 0.0;
                for (int j = 0; j < n; j++)
                {
                    Timer t = new Timer();
                    for (int i = 0; i < count; i++)
                        dummy += multiply(i);
                    runningTime = t.Check();
                    double time = runningTime * 1e9 / count;
                    st += time;
                    sst += time * time;
                    totalCount += count;
                }
                double mean = st / n, sdev = Math.Sqrt((sst - mean * mean * n) / (n - 1));
                Console.WriteLine("{0,6:0.0} ns +/- {1,8:0.00} {2,10}", mean, sdev, count);
                //System.out.printf("%6.1f ns +/- %8.2f %10d%n", mean, sdev, count);
            } while (runningTime < 0.25 && count < int.MaxValue / 2);
            return dummy / totalCount;
        }

        public static int IterativeBinarySearch(int[] arr, int x)
        {
            int low = 0, high = arr.Length - 1;
            while (low <= high)
            {
                int mid = low + (high - low) / 2;

                // Check if x is present at mid
                if (arr[mid] == x)
                    return mid;

                // If x greater, ignore left half
                if (arr[mid] < x)
                    low = mid + 1;

                // If x is smaller, ignore right half
                else
                    high = mid - 1;
            }

            // If we reach here, then element was
            // not present
            return -1;
        }

        static int RecursiveBinarySearch(int[] arr, int low, int high, int x)
        {
            if (high >= low)
            {
                int mid = low + (high - low) / 2;

                // If the element is present at the
                // middle itself
                if (arr[mid] == x)
                    return mid;

                // If element is smaller than mid, then
                // it can only be present in left subarray
                if (arr[mid] > x)
                    return RecursiveBinarySearch(arr, low, mid - 1, x);

                // Else the element can only be present
                // in right subarray
                return RecursiveBinarySearch(arr, mid + 1, high, x);
            }

            // We reach here when element is not present
            // in array
            return -1;
        }

        public static double BenchmarkIterativeBinarySearch()
        {
            int runs = 10;
            int count = 100000000;
            int dummy = 0;
            int[] arr = { 2, 3, 4, 5, 7, 10, 14, 23, 40, 42 };
            int x = 10;
            int result = IterativeBinarySearch(arr, x);
            for (int j = 0; j < runs; j++)
            {
                Timer timer = new Timer();
                for (int i = 0; i < count; i++)
                {
                    dummy += IterativeBinarySearch(arr, x);
                }
                double time = timer.Check() * 1e9 / count;
                Console.WriteLine("{0,6:0.0} ns", time);
            }
            return dummy;
        }

        public static double BenchmarkRecursiveBinarySearch()
        {
            int runs = 10;
            int count = 100000000;
            int dummy = 0;
            int[] arr = { 2, 3, 4, 5, 7, 10, 14, 23, 40, 42 };
            int x = 10;
            int result = RecursiveBinarySearch(arr, 0, arr.Length - 1, x);
            for (int j = 0; j < runs; j++)
            {
                Timer timer = new Timer();
                for (int i = 0; i < count; i++)
                {
                    dummy += RecursiveBinarySearch(arr, 0, arr.Length - 1, x);
                }
                double time = timer.Check() * 1e9 / count;
                Console.WriteLine("{0,6:0.0} ns", time);
            }
            return dummy;
        }
    }
}
