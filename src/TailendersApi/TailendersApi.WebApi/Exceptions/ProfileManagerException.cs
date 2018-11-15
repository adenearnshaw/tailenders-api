using System;

namespace TailendersApi.WebApi.Exceptions
{
	public class ProfileManagerException : Exception
    {
        public ProfileManagerException() : base()
        {}

        public ProfileManagerException(string message) :base (message)
        {}
    }
}
