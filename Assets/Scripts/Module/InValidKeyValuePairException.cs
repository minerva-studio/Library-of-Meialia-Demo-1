using System;

namespace Minerva.Module
{
    public class InValidKeyValuePairException : Exception
    {
        public InValidKeyValuePairException() { }
        public InValidKeyValuePairException(string message) : base(message) { }
        public InValidKeyValuePairException(string message, Exception inner) : base(message, inner) { }
        protected InValidKeyValuePairException(
          System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
