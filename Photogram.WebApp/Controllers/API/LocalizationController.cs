using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Resources;

namespace Photogram.WebApp.Controllers.API
{
    public class LocalizationController : BaseApiController
    {
        public IHttpActionResult Get(string lang)
        {
            var res = Localization.ResourceManager;

            var dto = res.GetResourceSet(new System.Globalization.CultureInfo(lang), false, false).GetEnumerator();

            return Ok(dto.ToString());
        }
    }
}
