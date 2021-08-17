using System;
using System.Threading.Tasks;
using WaterSemaphore;

namespace SemaphoreTest
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Water water = new Water();
            Console.WriteLine("Please enter sequence of Hydrogen and Oxygen");
            while (true)
            {
                string input = Console.ReadLine();
                if (input == "e")
                {
                    break;
                }

                await water.BuildWaterAsync(input);
            }
            

        }
    }
}
