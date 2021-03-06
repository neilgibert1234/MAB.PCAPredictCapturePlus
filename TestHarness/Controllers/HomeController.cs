﻿using MAB.PCAPredictCapturePlus;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;

namespace TestHarness.Controllers
{
    public class HomeController : Controller
    {
        private CapturePlusClient _client = new CapturePlusClient(
            apiVersion: "1.00",
            key: ConfigurationManager.AppSettings["PCAPredictCapturePlusKey"],
            defaultCountries: "GB",
            defaultLanguage: "EN"
        );

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Find(string text)
        {
            var result = _client.Find(text);

            if(result.Error != null)
                return Json(result.Error);

            return Json(result.Items);
        }

        [HttpPost]
        public ActionResult FindByContainer(string text)
        {
            var result = _client.Find(null, text);

            if (result.Error != null)
                return Json(result.Error);

            return Json(result.Items);
        }

        [HttpPost]
        public ActionResult Retrieve(string id)
        {
            var result = _client.Retrieve(id);

            if(result.Error != null)
                return Json(result.Error);

            var model = result.Items.Select(a => new {
                Company = a.Company,
                BuildingName = a.BuildingName,
                Street = a.Street,
                Line1 = a.Line1,
                Line2 = a.Line2,
                Line3 = a.Line3,
                Line4 = a.Line4,
                Line5 = a.Line5,
                City = a.City,
                County = a.Province,
                Postcode = a.PostalCode
            }).First();

            return Json(model);
        }
    }
}