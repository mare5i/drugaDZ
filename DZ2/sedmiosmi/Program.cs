using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sedmiosmi
{
    class Program
    {
        static void Main(string[] args)
        {
            // Main method is the only method that
            // can ’t be marked with async .
            // What we are doing here is just a way for us to simulate
            // async - friendly environment you usually have with
            // other .NET application types ( like web apps , win apps etc .)
            // Ignore main method , you can just focus on LetsSayUserClickedAButtonOnGuiMethod() as a
            // first method in call hierarchy .
            var t = Task.Run(async () => await LetsSayUserClickedAButtonOnGuiMethodAsync());
            Console.Read();
        }
        private static async Task LetsSayUserClickedAButtonOnGuiMethodAsync()
        {
            var result = await GetTheMagicNumberAsync();
            Console.WriteLine(result);
        }
        private static async Task<int> GetTheMagicNumberAsync()
        {
            return await IKnowIGuyWhoKnowsAGuyAsync();
        }
        private static async Task<int> IKnowIGuyWhoKnowsAGuyAsync()
        {
            return await IKnowWhoKnowsThisAsync(10) + await IKnowWhoKnowsThisAsync(5);
        }
        private static async Task<int> IKnowWhoKnowsThisAsync(int n)
        {
            return await FactorialDigitSumAsync(n);
        }
        private static async Task<int> FactorialDigitSumAsync(int n)
        {
            int zbroj = 0;
            int fact = 1;
            int i;

            for (i = 1; i < n + 1; i++)
            {
                fact *= i;
            }

            while (fact != 0)
            {
                zbroj += fact % 10;
                fact /= 10;
            }
            return zbroj;
        }
    }
}
