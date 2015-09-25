using Photogram.WebApp.Models;
using System.Linq;
using System.Web.Mvc;

namespace Photogram.WebApp.Controllers
{
    [Authorize]
    public class SetupController : BaseController
    {
        // GET: Setup
        [HttpGet]
        [ChildActionOnly]
        public ActionResult Index()
        {
            var setup = _db.Setup.FirstOrDefault();
            var currLang = _db.Language.CurrentOrDefault();
            var model = new SetupViewModel
            {
                LCID = currLang.LCID.ToString(),
                Languages = _db.Language.SelectList(currLang)
            };

            if (null == setup)
            {
                return PartialView("_SetupPartial", model);
            }

            model.Published = setup.Published;
            model.Email = setup.Email;
            model.Phone = setup.Phone;
            model.MainTitle = setup.CurrentMainTitleText();
            model.AboutLead = setup.CurrentAboutLeadText();
            model.AboutBody = setup.CurrentAboutBodyText();
            model.ContactLead = setup.CurrentContactLeadText();
            model.Footer = setup.CurrentFooterText();
            model.FacebookURL = setup.FacebookURL;
            model.GitHubURL = setup.GitHubURL;
            model.InstagramURL = setup.InstagramURL;
            model.LinkedInURL = setup.LinkedInURL;

            return PartialView("_SetupPartial", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(SetupViewModel viewModel)
        {
            var currLang = _db.Language.AsEnumerable()
                .Where(x => x.LCID == int.Parse(viewModel.LCID)).FirstOrDefault();

            if (null == currLang)
            {
                currLang = _db.Language.CurrentOrDefault();
            }

            if (ModelState.IsValid)
            {
                var setup = _db.Setup.FirstOrDefault();

                if (null == setup)
                {
                    setup = new Setup();
                    _db.Setup.Add(setup);
                }

                setup.Published = viewModel.Published;
                setup.Email = viewModel.Email;
                setup.Phone = viewModel.Phone;
                setup.FacebookURL = viewModel.FacebookURL;
                setup.GitHubURL = viewModel.GitHubURL;
                setup.InstagramURL = viewModel.InstagramURL;
                setup.LinkedInURL = viewModel.LinkedInURL;

                var mainTitle = setup.MainTitle
                    .Where(x => x.Language.LCID == currLang.LCID)
                    .FirstOrDefault();

                if (!string.IsNullOrEmpty(viewModel.MainTitle))
                {
                    if (null == mainTitle)
                    {
                        mainTitle = new SetupMainTitle
                        {
                            Language = currLang,
                            Setup = setup
                        };
                    }

                    mainTitle.Text = viewModel.MainTitle;
                }
                else if (null != mainTitle)
                {
                    setup.MainTitle.Remove(mainTitle);
                }

                var aboutLead = setup.AboutLead
                    .Where(x => x.Language.LCID == currLang.LCID)
                    .FirstOrDefault();

                if (!string.IsNullOrEmpty(viewModel.AboutLead))
                {
                    if (null == aboutLead)
                    {
                        aboutLead = new SetupAboutLead
                        {
                            Language = currLang,
                            Setup = setup
                        };
                    }

                    aboutLead.Text = viewModel.AboutLead;
                }
                else if (null != aboutLead)
                {
                    setup.AboutLead.Remove(aboutLead);
                }

                var aboutBody = setup.AboutBody
                    .Where(x => x.Language.LCID == currLang.LCID)
                    .FirstOrDefault();

                if (!string.IsNullOrEmpty(viewModel.AboutBody))
                {
                    if (null == aboutBody)
                    {
                        aboutBody = new SetupAboutBody
                        {
                            Language = currLang,
                            Setup = setup
                        };
                    }

                    aboutBody.Text = viewModel.AboutBody;
                }
                else if (null != aboutBody)
                {
                    setup.AboutBody.Remove(aboutBody);
                }

                var contactLead = setup.ContactLead
                    .Where(x => x.Language.LCID == currLang.LCID)
                    .FirstOrDefault();

                if (!string.IsNullOrEmpty(viewModel.ContactLead))
                {
                    if (null == contactLead)
                    {
                        contactLead = new SetupContactLead
                        {
                            Language = currLang,
                            Setup = setup
                        };
                    }

                    contactLead.Text = viewModel.ContactLead;
                }
                else if (null != contactLead)
                {
                    setup.ContactLead.Remove(contactLead);
                }

                var footer = setup.Footer
                    .Where(x => x.Language.LCID == currLang.LCID)
                    .FirstOrDefault();

                if (!string.IsNullOrEmpty(viewModel.Footer))
                {
                    if (null == footer)
                    {
                        footer = new SetupFooter
                        {
                            Language = currLang,
                            Setup = setup
                        };
                    }

                    footer.Text = viewModel.Footer;
                }
                else if (null != footer)
                {
                    setup.Footer.Remove(footer);
                }

                _db.SaveChanges();
            }

            viewModel.Languages = _db.Language.SelectList(currLang);


            return PartialView("_SetupPartial", viewModel);
        }
    }
}