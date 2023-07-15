using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Temporary
{
    internal class Class2 : IInterface
    {

        public async Task WaitSmt()
        {
            Console.WriteLine("Start WaitSmt");
            await Task.Delay(1000);
            Console.WriteLine("Fin WaitSmt");
        }

        public void Foo()
        {
            throw new NotImplementedException();
        }
    }

    interface IInterface
    {
        void Foo();
    }
}
