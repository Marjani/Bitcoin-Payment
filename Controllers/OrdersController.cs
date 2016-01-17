using Marjani.Peyment.Bitcoin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Marjani.Peyment.Bitcoin.Controllers
{
    public class OrdersController : Controller
    {
        //
        // GET: /Orders/

        public ActionResult Index()
        {
            var models = new List<OrderItem>();
            using (var db = new DataAccess.PeymentContext())
            {
                foreach (var item in db.Orders)
                {
                    var model = new OrderItem();
                    model.Address = item.Address;
                    model.confirmations = item.confirmations;
                    model.CreatOn = item.CreatOn;
                    model.Email = item.Email;
                    model.Id = item.Id;
                    model.IsPayed = item.IsPayed;
                    model.PayOn = item.PayOn;
                    model.Total = item.Total;
                    model.Value = item.Value;

                    models.Add(model);

                }
            }
            return View(models);
        }

    }
}
