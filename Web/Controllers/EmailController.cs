using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Raven.Client;
using Web.Models;
using Google.Apis.Customsearch.v1.Data;
using System.Configuration;

namespace Web.Controllers
{
    [Authorize(Users = "julie.perilla@gmail.com")]
    public class EmailController : BaseController
    {
        MailChimp.MandrillApi MandrillApi = new MailChimp.MandrillApi("Bb7oCaU3xGQra-o96CcCcw");
        MailChimp.MCApi MailChimpApi = new MailChimp.MCApi("a1b611c315fc01dd928be6672a3fd1ab-us6", true);

        public EmailController(IDocumentSession documentSession)
            : base(documentSession)
        {
        }

        //
        // GET: /Email/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Email/Send

        public ActionResult Send()
        {
            string message = string.Empty;
            var userThatWantEmails = RavenSession.Query<Account>().Where(a => a.SendEmailUpdates == true);
            foreach (var user in userThatWantEmails)
            {
                // TODO: can we use a template from Mailchimp?
                user.LoadChargifyInfo();
                user.LoadSitesToSearchString(RavenSession);
                if (user.IsAccountCurrent())
                {
                    if (user.Searches.Count() > 0)
                    {
                        string latestSearch = user.Searches.Reverse().FirstOrDefault();
                        IList<Google.Apis.Customsearch.v1.Data.Result> searchResults = GoogleSearch(latestSearch, user.SitesToSearchString);
                        if(searchResults != null && searchResults.Count() > 0)
                        {
                            MailChimp.Types.Mandrill.Messages.Message newMessage = new MailChimp.Types.Mandrill.Messages.Message();
                            newMessage.FromEmail = "info@freelancehub.io";
                            newMessage.To = new MailChimp.Types.Mandrill.Messages.Recipient[1];
                            newMessage.To[0] = new MailChimp.Types.Mandrill.Messages.Recipient(user.Email, user.CustomerFirstName);
                            newMessage.Subject = searchResults.Count() + " New " + latestSearch.ToUpper() + " Job Leads From Freelance Hub!";
                            newMessage.Html = newMessage.Text = "Hey " + user.CustomerFirstName + ", We found these " + searchResults.Count() + " new job leads from Freelance Hub. Woohoo!\n";
                            foreach (var result in searchResults)
                            {
                                newMessage.Html += "<p>" + result.HtmlTitle + "</p>";
                                newMessage.Html += "<p>" + result.HtmlSnippet + "</p>";
                                newMessage.Text += "<p>" + result.HtmlTitle + "</p>";
                                newMessage.Text += "<p>" + result.HtmlSnippet + "</p>";
                            }
                          
                            newMessage.FromName = "Freelance Hub Team";
                            newMessage.TrackClicks = true;
                            newMessage.AutoHtml = true;
                            // Save these to user account, after deleting last weeks list, then show them on home page. Create link to home page here.
                            MailChimp.Types.MVList<MailChimp.Types.Mandrill.Messages.SendResult> results = MandrillApi.Send(newMessage);
                            foreach (var result in results)
                                message += result.Status.ToString();
                         }                  
                    }
                    else
                    {
                        // TODO: send an email trying to get them to search
                    }
                }
                else
                {
                        // TODO: send an email trying to get them back
                }
            }

            return View("Send", null, message);
        }

        public IList<Result> GoogleSearch(string query, string sitesToSearchString)
        {
            IList<Result> results = new List<Result>();
            string apiKey = ConfigurationManager.AppSettings["googleApiKey"];
            string cx = "013914245334065375847:dl-lwlywdoi";

            var svc = new Google.Apis.Customsearch.v1.CustomsearchService(new Google.Apis.Services.BaseClientService.Initializer { ApiKey = apiKey });
            var listRequest = svc.Cse.List(query + " " + sitesToSearchString);            

            listRequest.Cx = cx;
            var search = listRequest.Execute();
           
            return search.Items;
        }

        //
        // GET: /Email/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Email/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Email/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Email/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Email/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Email/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Email/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
