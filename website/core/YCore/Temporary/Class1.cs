using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Temporary
{
    internal class Class1
    {
        private IInterface class2;

        public Class1(IInterface class2)
        {
            this.class2 = class2;
        }

        public async IAsyncEnumerable<int> GetIntsAsync()
        {
            Console.WriteLine("GetIntsAsync start");
            for (int i = 0; i < 10; i++)
            {
                await Task.Delay(1000);
                yield return i;
            }
            Console.WriteLine("GetIntsAsync finish");
        }

        public async Task GetInt()
        {
            await Task.Delay(2000);
            Console.WriteLine($"Rand: {new Random().Next()}");
        }
    }
}
