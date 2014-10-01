using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photogram.Portal.Models;

namespace Photogram.Init
{
    class Program
    {
        static void Main(string[] args)
        {
            PhotogramEntities db = new PhotogramEntities();

            Setup s = new Setup
            {
                Email = "info@andrasmuller.com",
                MainTitle = new TextValue[]
                {
                    new TextValue
                    {
                        Text = "MÜLLER ANDRÁS FOTOGRÁFUS",
                        Language = new Language
                        {
                            Code = "hun",
                            Name = "magyar"
                        }
                    },

                    new TextValue
                    {
                        Text = "ANDRÁS MÜLLER PHOTOGRAPHER",
                        Language = new Language
                        {
                            Code = "eng",
                            Name = "angol"
                        }
                    },

                }
            };

            db.Setup.Add(s);

            db.SaveChanges();
        }
    }
}
