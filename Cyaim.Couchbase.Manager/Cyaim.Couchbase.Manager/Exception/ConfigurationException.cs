using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Cyaim.Couchbase.Manager.Exception
{
    class ConfigurationException : ApplicationException
    {
        public ConfigurationException()
        {
        }

        public ConfigurationException(string message) : base(message)
        {
        }

        public ConfigurationException(string message, System.Exception innerException) : base(message, innerException)
        {
        }

        protected ConfigurationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
