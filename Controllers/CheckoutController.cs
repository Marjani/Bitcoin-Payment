using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Marjani.Peyment.Bitcoin.Controllers
{
    public class CheckoutController : Controller
    {
        //
        // GET: /Checkout/
        private string xpub = "{YOUR XPUB}";
        private string callback_url = "http://{DOMAIN etc: example.com}/Checkout/Receive?invoice_id={0}&secret={1}";
        private string key = "{YOUR KEY}";
        private string BaseUrl = "https://api.blockchain.info/v2/receive?xpub={0}&callback={1}&key={2}";

        public ActionResult Payment(int id)
        {
            var model = new Models.Peyment();
            using (var db = new DataAccess.PeymentContext())
            {
                var entity = db.Orders.Find(id);
                if (entity == null)
                {
                    return RedirectToAction("Index", "Home", null);
                }

                model.Total = entity.Total;
                model.OrderId = entity.Id;

                callback_url = string.Format(callback_url, model.OrderId, entity.Secret);
                callback_url = Uri.EscapeDataString(callback_url);
                BaseUrl = string.Format(BaseUrl, xpub, callback_url, key);

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(BaseUrl);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream resStream = response.GetResponseStream();
                var value = new StreamReader(resStream).ReadToEnd();

                Models.Response res = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<Models.Response>(value);

                model.Adderess = res.address;

            }
            return View(model);
        }

        public ActionResult refresh(int id)
        {
            using (var db = new DataAccess.PeymentContext())
            {
                var entity = db.Orders.Find(id);
                callback_url = string.Format(callback_url, id, entity.Secret);
                callback_url = Uri.EscapeDataString(callback_url);
                BaseUrl = string.Format(BaseUrl, xpub, callback_url, key);

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(BaseUrl);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream resStream = response.GetResponseStream();
                var value = new StreamReader(resStream).ReadToEnd();

                Models.Response res = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<Models.Response>(value);

                return Json(new { address = res.address }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Receive(int invoice_id, string secret)
        {
            if (invoice_id < 1)
            {
                return new EmptyResult();
            }
            using (var db = new DataAccess.PeymentContext())
            {
                var entity = db.Orders.Find(invoice_id);
                if (entity.Secret != secret)
                {
                    return new EmptyResult();
                }
                entity.IsPayed = true;
                entity.Address = Request["address"];
                entity.TransactionHash = Request["transaction_hash"];
                entity.confirmations = Request["confirmations"];
                entity.Value = Request["value"];
                entity.PayOn = DateTime.Now;
                try
                {
                    db.SaveChanges();
                    return Content("*ok*");
                }
                catch
                { }
            }
            return new EmptyResult();

        }
    }
}
