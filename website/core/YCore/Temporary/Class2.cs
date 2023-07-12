using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Temporary
{
    internal class Class2 : IInterface
    {
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
