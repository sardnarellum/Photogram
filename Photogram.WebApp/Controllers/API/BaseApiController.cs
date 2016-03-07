using Photogram.WebApp.Models;
using System.Web.Http;

namespace Photogram.WebApp.Controllers.API
{
    public class BaseApiController : ApiController
    {
        protected PhotogramEntities _db = new PhotogramEntities();

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
