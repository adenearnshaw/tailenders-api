using System;
namespace TailendersApi.Client
{
    public interface ICredentialsProvider
    {
        string UserId { get; }
        string AuthenticationToke { get; }
    }
}
