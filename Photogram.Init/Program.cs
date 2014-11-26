using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photogram.WebApp.Models;

namespace Photogram.Init
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("PHOTOGRAM ADATBÁZIS INICIALIZÁCIÓ" + Environment.NewLine);

            try
            {
                PhotogramEntities db = new PhotogramEntities();

                var hun = new Language
                {
                    Code = "hun",
                    Name = "magyar"
                };

                var eng = new Language
                {
                    Code = "eng",
                    Name = "angol"
                };

                var s = new Setup
                {
                    Email = "info@andrasmuller.com",
                    MainTitle = new TextValue[]
                {
                    new TextValue
                    {
                        Text = "MÜLLER ANDRÁS FOTOGRÁFUS",
                        Language = hun
                    },

                    new TextValue
                    {
                        Text = "ANDRÁS MÜLLER PHOTOGRAPHER",
                        Language = eng
                    },

                }
                };

                db.Setup.Add(s);


                var g = new Gallery
                {
                    Title = new TextValue[]
                {
                    new TextValue
                    {
                        Text = "Teszt galéria cím",
                        Language = hun
                    },

                    new TextValue
                    {
                        Text = "Text gallery title",
                        Language = eng
                    }
                },
                    Type = GalleryType.Portfolio,
                    Visible = true,
                    Year = 2014,
                    Position = 1,
                    Description = new TextValue[]
                {
                    new TextValue
                    {
                        Text = "hun Lorem ipsum dolor sit amet, consectetur adipiscing elit. In consectetur et lacus eget iaculis. Nam lobortis lorem quis odio aliquet vehicula. Ut justo tortor, auctor eget laoreet fermentum, varius id.",
                        Language = hun
                    },

                    new TextValue
                    {
                        Text = "eng Lorem ipsum dolor sit amet, consectetur adipiscing elit. In consectetur et lacus eget iaculis. Nam lobortis lorem quis odio aliquet vehicula. Ut justo tortor, auctor eget laoreet fermentum, varius id.",
                        Language = eng
                    }
                }
                };

                db.Gallery.Add(g);


                db.SaveChanges();

                Console.WriteLine("Az adatbázis inicializációja sikeres volt.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Az adatbázis inicializálása közben a következő kivétel keletkezett:"
                    + Environment.NewLine + ex.Message);

                if (null != ex.InnerException)
                    Console.WriteLine("Belső kivétel:" + Environment.NewLine + ex.InnerException.Message);               
            }
            finally
            {
                Console.WriteLine("A kilépéshez üss le egy billentyűt...");
                Console.ReadKey();
            }
        }
    }
}
