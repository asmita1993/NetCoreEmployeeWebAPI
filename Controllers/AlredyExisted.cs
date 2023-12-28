using System.Runtime.Serialization;

namespace EmployeeWebAPI.Controllers
{
    [Serializable]
    internal class AlredyExisted : Exception
    {
        public AlredyExisted()
        {
        }

        public AlredyExisted(string? message) : base(message)
        {
        }

        public AlredyExisted(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected AlredyExisted(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}