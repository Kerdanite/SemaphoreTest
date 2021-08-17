using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace WaterSemaphore
{
    public class Water
    {
        private Semaphore semaphoreO = new Semaphore(0, 1);
        private Semaphore semaphoreH = new Semaphore(2, 2);
        private int hCount = 0;

        public async Task BuildWaterAsync(string input)
        {
            hCount = 0;
            List<Task> tasks = new List<Task>();

            foreach (var c in input)
            {
                switch (c)
                {
                    case 'O':
                    {
                        tasks.Add(OThread(ReleaseOxygen));
                        break;
                    }
                    case 'H':
                    {
                        tasks.Add(HTread(ReleaseHydrogen));
                        break;
                    }
                    default:
                        break;
                }
            }

            await Task.WhenAll(tasks);
        }

        private void ReleaseHydrogen()
        {
            Console.WriteLine("H");
        }

        private void ReleaseOxygen()
        {
            Console.WriteLine("O");
        }

        private async Task HTread(Action releaseH)
        {
            await Task.Delay(1);
            semaphoreH.WaitOne();
            releaseH();
            hCount++;

            if (hCount % 2 == 0)
            {
                semaphoreO.Release();
            }
        }

        private async Task OThread(Action releaseO)
        {
            await Task.Delay(1);

            semaphoreO.WaitOne();

            releaseO();

            semaphoreH.Release(2);
        }
    }
}
