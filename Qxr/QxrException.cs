using System;

namespace Qxr
{
    [Serializable]
    public class QxrException : Exception
    {
        public QxrException()
        {
            
        }

        public QxrException(string message) : base(message)
        {

        }

        public QxrException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
