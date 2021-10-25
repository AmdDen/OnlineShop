using System;

namespace OnlineShop.Common.Exceptions
{
    public class EntityNullReferenceException : Exception
    {
        public EntityNullReferenceException(string message) : base(message)
        {
        }
    }
}