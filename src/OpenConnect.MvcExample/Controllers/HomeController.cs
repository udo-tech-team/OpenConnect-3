﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OpenConnect.MvcExample.Models;
using OpenConnect.MvcExample.Utils;

namespace OpenConnect.MvcExample.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var links = new List<LoginLink>();

            foreach (var name in OpenConnectUtil.ClientManager.ClientNames)
            {
                var client = OpenConnectUtil.ClientManager.Find(name);

                var link = new LoginLink
                {
                    ImageUrl = "/Content/Images/connect-" + name.Replace(' ', '-') + ".png",
                    NavigateUrl = client.BuildLoginUrl(ResponseType.Code, "http://test.sigcms.com/LoginCallback?clientName=" + name, null, null)
                };

                links.Add(link);
            }
            
            return View(links);
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
