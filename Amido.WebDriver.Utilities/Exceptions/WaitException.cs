using System;

namespace Amido.WebDriver.Utilities.Exceptions
{
    public class WaitException : Exception
    {
        public WaitException()
        {
        }

        public WaitException(string msg)
            : base(msg)
        {
        }

        public WaitException(string msg, Exception inner)
            : base(msg, inner)
        {
        }
    }
}
