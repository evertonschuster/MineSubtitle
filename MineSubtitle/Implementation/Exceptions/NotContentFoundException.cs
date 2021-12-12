using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSubtitle.Implementation.Exceptions
{
    internal class NotContentFoundException : Exception
    {
        public NotContentFoundException() : base("File without content")
        {
        }
    }
}
