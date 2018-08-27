using System;
using System.Collections.Generic;

namespace FunctionalProgramming_Demo
{
    public class FunctionalProgramming<T>
    {
        public delegate T MapHandler(T something);
        public delegate bool FilterHandler(T something);
        public delegate T ReduceHandler(IEnumerable<T> something);

        public T Twice(T input, MapHandler mapHandler)
        {
            return mapHandler(mapHandler(input));
        }

        public IEnumerable<T> Map(IEnumerable<T> list, MapHandler mapFunction)
        {
            foreach(T value in list)
            {
                yield return mapFunction(value);
            }
        }

        public IEnumerable<T> Filter(IEnumerable<T> list, FilterHandler filterFunction)
        {
            foreach (T value in list)
            {
                if(filterFunction(value))
                    yield return value;
            }
        }

        public T Reduce(IEnumerable<T> list, ReduceHandler reduceFunction)
        {
            return reduceFunction(list);
        }

        public void Perform(IEnumerable<T> list, Action<T>callback)
        {
            foreach (T value in list)
            {
                callback(value);
            }
        }

    }

    class Program
    {
        static int Summation(IEnumerable<int> givenList)
        {
            int sumTotal = 0;
            foreach (int a in givenList)
            {
                sumTotal += a;
            }
            return sumTotal;
        }

        static void Main(string[] args)
        {
            List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 11, 10 };
            var fpNumbers = new FunctionalProgramming<int>();
            Console.WriteLine("Squares: ");
            foreach (int value in fpNumbers.Map(numbers, x => (x * x)))
            {
                Console.WriteLine(value);
            }

            Console.WriteLine("Pow^4: ");
            foreach (int value in fpNumbers.Map(numbers, x => fpNumbers.Twice(x, y => (y * y)) ))
            {
                Console.WriteLine(value);
            }

            Console.WriteLine("Even Numbers: ");
            foreach (int value in fpNumbers.Filter(numbers, x => (x % 2 == 0)))
            {
                Console.WriteLine(value);
            }

            Console.WriteLine("Summation: ");
            Console.WriteLine(fpNumbers.Reduce(numbers, Summation));

            Console.WriteLine("Maximum: ");
            Console.WriteLine(
                fpNumbers.Reduce(numbers,

                givenList => {
                    int value = Int32.MinValue;

                    foreach (int a in givenList)
                    {
                       if (a > value)
                             value = a;
                    }

                    return value;
                }
            ));

            Console.WriteLine("Perform: ");
            fpNumbers.Perform(numbers, (given) => Console.WriteLine($"Performing: {given}"));

            List<string> names = new List<string> {
                "Vaishnavi",
                "Vaibhav",
                "Venkatesh",
                "Ravi",
                "Varsha",
                "Karthik",
            };
            var fpNames = new FunctionalProgramming<string>();

            Console.WriteLine("Filter Vs: ");
            var filteredNames = fpNames.Filter(names, x => x.StartsWith("V"));
            foreach (var name in filteredNames)
            {
                Console.WriteLine(name);
            }

            Console.WriteLine("Perform Greet: ");
            fpNames.Perform(names, (given) => Console.WriteLine($"Hello {given}!"));

            Console.ReadLine();
        }
    }
}
