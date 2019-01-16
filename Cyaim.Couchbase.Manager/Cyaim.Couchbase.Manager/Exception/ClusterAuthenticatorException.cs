using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Cyaim.Couchbase.Manager.Exception
{
    [Serializable]
    class ClusterAuthenticatorException : ApplicationException
    {
        public ClusterAuthenticatorException()
        {
        }

        public ClusterAuthenticatorException(string message) : base(message)
        {
        }

        public ClusterAuthenticatorException(string message, System.Exception innerException) : base(message, innerException)
        {
        }

        protected ClusterAuthenticatorException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
