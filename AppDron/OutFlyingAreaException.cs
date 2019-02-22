using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace AppDron
{
    public class OutFlyingAreaException : Exception
    {
        public OutFlyingAreaException() { }
        public OutFlyingAreaException(string message, params object[] formatAddInfo) : base(string.Format(message, formatAddInfo)) { }
        public OutFlyingAreaException(string message, Exception innerException, params object[] formatAddInfo) : base(string.Format(message, formatAddInfo), innerException) { }
        public OutFlyingAreaException(string message) : base(message) { }
        public OutFlyingAreaException(string message, Exception innerException) : base(message, innerException) { }
    }
}
