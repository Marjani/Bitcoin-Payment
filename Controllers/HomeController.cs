using Marjani.Peyment.Bitcoin.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Marjani.Peyment.Bitcoin.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Models.Order model)
        {
            if (model.Total <= 0)
            {
                ModelState.AddModelError("Total", "Upper 0 plz");
            }
            if (ModelState.IsValid)
            {
                var response = Request["g-recaptcha-response"];
                //secret that was generated in key value pair
                const string secret = "{reCAPTCHA secret key}";

                var client = new WebClient();
                var reply =
                    client.DownloadString(
                        string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secret, response));

                var captchaResponse = JsonConvert.DeserializeObject<CaptchaResponse>(reply);

                //when response is false check for the error message
                if (!captchaResponse.Success)
                {
                    if (captchaResponse.ErrorCodes.Count <= 0) return View();

                    var error = captchaResponse.ErrorCodes[0].ToLower();
                    switch (error)
                    {
                        case ("missing-input-secret"):
                            ViewBag.Message = "The secret parameter is missing.";
                            break;
                        case ("invalid-input-secret"):
                            ViewBag.Message = "The secret parameter is invalid or malformed.";
                            break;

                        case ("missing-input-response"):
                            ViewBag.Message = "The response parameter is missing.";
                            break;
                        case ("invalid-input-response"):
                            ViewBag.Message = "The response parameter is invalid or malformed.";
                            break;

                        default:
                            ViewBag.Message = "Error occured. Please try again";
                            break;
                    }
                    return View(model);

                }

                using (var db = new DataAccess.PeymentContext())
                {
                    var entity = new DataAccess.Entities.Order();
                    entity.Email = model.Email;
                    entity.IsPayed = false;
                    entity.Name = model.Name;
                    entity.Total = model.Total;
                    entity.UserName = model.UserName;
                    entity.CreatOn = DateTime.Now;
                    entity.Secret = Guid.NewGuid().ToString();
                    try
                    {
                        db.Orders.Add(entity);
                        db.SaveChanges();
                        return RedirectToAction("Payment", "Checkout", new { id = entity.Id });
                    }
                    catch (Exception)
                    {
                        return View(model);
                    }
                }
            }
            return View(model);
        }

        public ActionResult Peyment(int id)
        {
            return View();
        }

    }
}
