using ConferenceApp.Models;
using DNTCaptcha.Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConferenceApp.Controllers
{
    public class ConferenceController : Controller
    {
        private static List<ConferenceUser> _conferenceUsers = new List<ConferenceUser>();
        private IHostingEnvironment hostingEnv;
        public ConferenceController(IHostingEnvironment env)
        {
            this.hostingEnv = env;
        }
        public IActionResult Index()
        {
            return View(_conferenceUsers);
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateDNTCaptcha(
            ErrorMessage = "Proszę wprowadź poprawne dane Captcha",
            CaptchaGeneratorLanguage = Language.English,
            CaptchaGeneratorDisplayMode = DisplayMode.ShowDigits)]
        public IActionResult Register(ConferenceUser conferenceUser)
        {
            if (ModelState.IsValid)
            {
                var FileDic = "Files";
                string FilePath = Path.Combine(hostingEnv.WebRootPath, FileDic);
                if (!Directory.Exists(FilePath))
                    Directory.CreateDirectory(FilePath);
                var fileName = conferenceUser.FileSec.FileName;
                var filePath = Path.Combine(FilePath, fileName);
                using (FileStream fs = System.IO.File.Create(filePath))
                {
                    conferenceUser.FileSec.CopyTo(fs);
                }
                _conferenceUsers.Add(conferenceUser);
                return RedirectToAction(nameof(Index));//Przekierowanie
            }
            else
            {
                if (!ReCaptchaPassed(Request.Form["foo"]))
                {
                    ModelState.AddModelError(string.Empty, "You failed the CAPTCHA.");
                    return View();
                }
            }
            return View();
        }
        public static bool ReCaptchaPassed(string gRecaptchaResponse)
        {
            HttpClient httpClient = new HttpClient();

            var res = httpClient.GetAsync($"https://www.google.com/recaptcha/api/siteverify?secret=6LfdEaAaAAAAAC03M0oLKffB2_BDY-0dFvvf99k&response={ gRecaptchaResponse}").Result;

            if (res.StatusCode != HttpStatusCode.OK)
                return false;

            string JSONres = res.Content.ReadAsStringAsync().Result;
            dynamic JSONdata = JObject.Parse(JSONres);

            if (JSONdata.success != "true")
                return false;

            return true;
        }
    }
}
