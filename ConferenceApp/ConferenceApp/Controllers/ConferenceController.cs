using ConferenceApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConferenceApp.Controllers
{
    public class ConferenceController : Controller
    {
        private static List<ConferenceUser> _conferenceUsers = new List<ConferenceUser>();
        private static SetUsersInfo _confInfo = new SetUsersInfo
        {
            User = new ConferenceUser(),
            Users = _conferenceUsers
        };
        private static int counter = 0;

        public IActionResult Index()
        {
            return View(_conferenceUsers);
        }

        [HttpGet]
        public IActionResult Register()
        {
            if (counter == 1)
            {
                ViewBag.Message = String.Format("Your account has been registered.\\nCurrent Date and time:{0}",DateTime.Now.ToString());
            }
            ViewBag.Added = counter;
            counter++;
            return View(_confInfo);
        }

        [HttpPost]
        public IActionResult Register(SetUsersInfo conferenceUser )
        {
            if (ModelState.IsValid)
            {
                if (_conferenceUsers.Count()!=0)
                    _conferenceUsers.Last().Color = "";
                counter = 1;
                _conferenceUsers.Add(conferenceUser.User);
                _conferenceUsers.Last().Color = "table-warning";
                _confInfo.Users = _conferenceUsers;
                return RedirectToAction(nameof(Register));
            }

            return View();
        }
    }
}
