using Microsoft.AspNetCore.Mvc;

namespace TailendersApi.WebApi.Controllers
{
    public class BaseController : ControllerBase
    {
        protected const string ObjectIdElement = "http://schemas.microsoft.com/identity/claims/objectidentifier";

        protected const string ReadPermission = "te.read";
        protected const string WritePermission = "te.write";

        public BaseController()
        {
        }
    }
}
