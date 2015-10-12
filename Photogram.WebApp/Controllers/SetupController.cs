using Photogram.WebApp.Models;
using Resources;
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
                Languages = _db.Language.SelectList(currLang),
                ContactBackgroundList =
                    _db.Media.SelectList(Localization.NoBackground),
                AboutBackgroundList =
                    _db.Media.SelectList(Localization.NoBackground)
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

            if (null != setup.AboutBackground)
            {
                model.AboutBackgroundId = setup.AboutBackground.Id;
                model.AboutBackgroundList =
                    _db.Media.SelectList(Localization.NoBackground,
                        setup.AboutBackground);
            }
            else
            {
                model.AboutBackgroundId = -1;
                model.AboutBackgroundList =
                    _db.Media.SelectList(Localization.NoBackground);
            }

            if (null != setup.ContactBackground)
            {
                model.ContactBackgroundId = setup.ContactBackground.Id;
                model.ContactBackgroundList =
                    _db.Media.SelectList(Localization.NoBackground,
                        setup.ContactBackground);
            }
            else
            {
                model.ContactBackgroundId = -1;
                model.ContactBackgroundList =
                    _db.Media.SelectList(Localization.NoBackground);
            }            

            return PartialView("_SetupPartial", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(SetupViewModel model)
        {
            var currLang = _db.Language.AsEnumerable()
                .Where(x => x.LCID == int.Parse(model.LCID)).FirstOrDefault();

            if (null == currLang)
            {
                currLang = _db.Language.CurrentOrDefault();
            }

            if (ModelState.IsValid)
            {
                var setup = _db.Setup.Include("AboutBackground")
                    .Include("ContactBackground").FirstOrDefault();

                if (null == setup)
                {
                    setup = new Setup();
                    _db.Setup.Add(setup);
                }

                setup.Published = model.Published;
                setup.Email = model.Email;
                setup.Phone = model.Phone;
                setup.FacebookURL = model.FacebookURL;
                setup.GitHubURL = model.GitHubURL;
                setup.InstagramURL = model.InstagramURL;
                setup.LinkedInURL = model.LinkedInURL;
                setup.AboutBackground =
                    _db.Media.Where(x => x.Id == model.AboutBackgroundId)
                       .FirstOrDefault();
                setup.ContactBackground =
                    _db.Media.Where(x => x.Id == model.ContactBackgroundId)
                       .FirstOrDefault();


                var mainTitle = setup.MainTitle
                    .Where(x => x.Language.LCID == currLang.LCID)
                    .FirstOrDefault();

                if (!string.IsNullOrEmpty(model.MainTitle))
                {
                    if (null == mainTitle)
                    {
                        mainTitle = new SetupMainTitle
                        {
                            Language = currLang,
                            Setup = setup
                        };

                        setup.MainTitle.Add(mainTitle);
                    }

                    mainTitle.Text = model.MainTitle;
                }
                else if (null != mainTitle)
                {
                    setup.MainTitle.Remove(mainTitle);
                }

                var aboutLead = setup.AboutLead
                    .Where(x => x.Language.LCID == currLang.LCID)
                    .FirstOrDefault();

                if (!string.IsNullOrEmpty(model.AboutLead))
                {
                    if (null == aboutLead)
                    {
                        aboutLead = new SetupAboutLead
                        {
                            Language = currLang,
                            Setup = setup
                        };

                        setup.AboutLead.Add(aboutLead);
                    }

                    aboutLead.Text = model.AboutLead;
                }
                else if (null != aboutLead)
                {
                    setup.AboutLead.Remove(aboutLead);
                }

                var aboutBody = setup.AboutBody
                    .Where(x => x.Language.LCID == currLang.LCID)
                    .FirstOrDefault();

                if (!string.IsNullOrEmpty(model.AboutBody))
                {
                    if (null == aboutBody)
                    {
                        aboutBody = new SetupAboutBody
                        {
                            Language = currLang,
                            Setup = setup
                        };

                        setup.AboutBody.Add(aboutBody);
                    }

                    aboutBody.Text = model.AboutBody;
                }
                else if (null != aboutBody)
                {
                    setup.AboutBody.Remove(aboutBody);
                }

                var contactLead = setup.ContactLead
                    .Where(x => x.Language.LCID == currLang.LCID)
                    .FirstOrDefault();

                if (!string.IsNullOrEmpty(model.ContactLead))
                {
                    if (null == contactLead)
                    {
                        contactLead = new SetupContactLead
                        {
                            Language = currLang,
                            Setup = setup
                        };

                        setup.ContactLead.Add(contactLead);
                    }

                    contactLead.Text = model.ContactLead;
                }
                else if (null != contactLead)
                {
                    setup.ContactLead.Remove(contactLead);
                }

                var footer = setup.Footer
                    .Where(x => x.Language.LCID == currLang.LCID)
                    .FirstOrDefault();

                if (!string.IsNullOrEmpty(model.Footer))
                {
                    if (null == footer)
                    {
                        footer = new SetupFooter
                        {
                            Language = currLang,
                            Setup = setup
                        };

                        setup.Footer.Add(footer);
                    }

                    footer.Text = model.Footer;
                }
                else if (null != footer)
                {
                    setup.Footer.Remove(footer);
                }

                _db.SaveChanges();

                if (null != setup.AboutBackground)
                {
                    model.AboutBackgroundId = setup.AboutBackground.Id;
                    model.AboutBackgroundList =
                        _db.Media.SelectList(Localization.NoBackground,
                            setup.AboutBackground);
                }
                else
                {
                    model.AboutBackgroundId = -1;
                    model.AboutBackgroundList =
                        _db.Media.SelectList(Localization.NoBackground);
                }

                if (null != setup.ContactBackground)
                {
                    model.ContactBackgroundId = setup.ContactBackground.Id;
                    model.ContactBackgroundList =
                        _db.Media.SelectList(Localization.NoBackground,
                            setup.ContactBackground);
                }
                else
                {
                    model.ContactBackgroundId = -1;
                    model.ContactBackgroundList =
                        _db.Media.SelectList(Localization.NoBackground);
                }
            }

            model.Languages = _db.Language.SelectList(currLang);


            return PartialView("_SetupPartial", model);
        }
    }
}