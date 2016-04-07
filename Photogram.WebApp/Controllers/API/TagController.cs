using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Photogram.WebApp.Controllers.API
{
    public class TagController : BaseApiController
    {
        public IHttpActionResult Get(string searchTerm)
        {
            var tags = string.IsNullOrEmpty(searchTerm)
                ? _db.Tag.Select(x => new { Text = x.Name })
                    .OrderBy(x => x.Text).AsEnumerable()
                : _db.Tag.Where(x => x.Name.Contains(searchTerm)).Select(
                    x => new { Text = x.Name }).OrderBy(x => x.Text).AsEnumerable();

            return Ok(tags);
        }
    }
}
