using System;
using System.Threading.Tasks;

namespace AsyncBreakfast
{
    internal class HashBrown { }
    internal class Coffee { }
    internal class Egg { }
    internal class Juice { }
    internal class Toast { }

    class Program
    {
        static async Task main(string[] args)
        {
            Coffee cup = await PourCoffeeAsync();
            Console.WriteLine("coffee is ready");

            Egg eggs = await FryEggsAsync(2);
            Console.WriteLine("eggs are ready");

            HashBrown hashBrown = FryHashBrownsAsync(3);
            Console.WriteLine("hash browns are ready");

            Toast toast = await ToastBreadAsync(2);
            ApplyButterAsync(toast);
            ApplyJamAsync(toast);
            Console.WriteLine("toast is ready");

            Juice oj = PourOJAsync();
            Console.WriteLine("oj is ready");
            Console.WriteLine("Breakfast is ready!");
        }

        private static Juice PourOJAsync()
        {
            Console.WriteLine("Pouring orange juice");
            return new Juice();
        }

        private static void ApplyJamAsync(Toast toast) =>
            Console.WriteLine("Putting jam on the toast");

        private static void ApplyButterAsync(Toast toast) =>
            Console.WriteLine("Putting butter on the toast");

        private static async Task<Toast> ToastBreadAsync(int slices)
        {
            for (int slice = 0; slice < slices; slice++)
            {
                Console.WriteLine("Putting a slice of bread in the toaster");
            }
            Console.WriteLine("Start toasting...");
            Task.Delay(2000).Wait();
            Console.WriteLine("Fire!! The toast is ruined!");
            throw new Exception("Fire!! The toast is on fire!");
            await Task.Delay(1000);
            Console.WriteLine("Remove toast from toaster");

            return new Toast();
        }

        private static HashBrown FryHashBrownsAsync(int patties)
        {
            Console.WriteLine($"putting {patties} hash brown patties in the pan");
            Console.WriteLine("cooking first side of hash browns...");
            Task.Delay(3000).Wait();
            for (int patty = 0; patty < patties; patty++)
            {
                Console.WriteLine("flipping a hash brown patty");
            }
            Console.WriteLine("cooking the second side of hash browns...");
            Task.Delay(3000).Wait();
            Console.WriteLine("Put hash browns on plate");

            return new HashBrown();
        }

        private static async Task<Egg> FryEggsAsync(int howMany)
        {
            Console.WriteLine("Warming the egg pan...");
            Task.Delay(3000).Wait();
            Console.WriteLine($"cracking {howMany} eggs");
            Console.WriteLine("cooking the eggs ...");
            Task.Delay(3000).Wait();
            Console.WriteLine("Put eggs on plate");

            return new Egg();
        }

        private static async Task<Coffee> PourCoffeeAsync()
        {
            Console.WriteLine("Pouring coffee");
            return new Coffee();
        }
    }
}