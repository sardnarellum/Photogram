using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Photogram.WebApp.Models;
using System.Data.Entity;

namespace Photogram.WebApp.Models
{
    public class DbInitializer : CreateDatabaseIfNotExists<PhotogramEntities>
    {
        protected override void Seed(PhotogramEntities context)
        {
            var hun = new Language
            {
                LCID = 1038
            };

            var eng = new Language
            {
                LCID = 1033
            };

            context.Language.Add(hun);
            context.Language.Add(eng);


            context.SaveChanges();
        }
    }
}