using ClientManager.ActionFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClientManager.Controllers
{
    public class AddressController : Controller
    {
        Models.ClientsEntities db = new Models.ClientsEntities();

        [AddressFilter]
        // GET: Address
        public ActionResult Index(int id)
        {
            var result = db.persons.SingleOrDefault(p => p.person_id == id);

            return View(result);
        }

        // GET: Address/Details/5
        public ActionResult Details(int id)
        {
            Models.address theAddress = db.addresses.SingleOrDefault(a => a.address_id == id);

            return View(theAddress);
        }

        // GET: Address/Create
        public ActionResult Create(int id)
        {
            //something like ViewBag.Add("key", value); is happening in the line below
            ViewBag.countries
                = db.countries.Select(c => new SelectListItem() { Value = c.country_code, Text = c.country_name });

            ViewBag.person = db.persons.SingleOrDefault(p => p.person_id == id);

            return View();
        }

        // POST: Address/Create
        [HttpPost]
        public ActionResult Create(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                Models.address newAddress = new Models.address()
                {
                    city = collection["city"],
                    country_code = collection["country_code"],
                    description = collection["description"],
                    person_id = id,
                    province = collection["province"],
                    street_address = collection["street_address"],
                    zipcode = collection["zipcode"]
                };
                db.addresses.Add(newAddress);
                db.SaveChanges();

                return RedirectToAction("Index", new { id = id });
            }
            catch
            {
                return View();
            }
        }

        // GET: Address/Edit/5
        public ActionResult Edit(int id)
        {
            //something like ViewBag.Add("key", value); is happening in the line below
            ViewBag.countries
                = db.countries.Select(c => new SelectListItem() { Value = c.country_code, Text = c.country_name });

            Models.address theAddress = db.addresses.SingleOrDefault(a => a.address_id == id);

            return View(theAddress);
        }

        // POST: Address/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                Models.address theAddress = db.addresses.SingleOrDefault(a => a.address_id == id);

                theAddress.description = collection["description"];
                theAddress.street_address = collection["street_address"];
                theAddress.city = collection["city"];
                theAddress.province = collection["province"];
                theAddress.zipcode = collection["zipcode"];
                theAddress.country_code = collection["country_code"];

                db.SaveChanges();

                return RedirectToAction("Index", new { id = theAddress.person_id });
            }
            catch
            {
                return View();
            }
        }

        // GET: Address/Delete/5
        public ActionResult Delete(int id)
        {
            Models.address theAddress = db.addresses.SingleOrDefault(a => a.address_id == id);

            return View(theAddress);
        }

        // POST: Address/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                Models.address theAddress = db.addresses.SingleOrDefault(a => a.address_id == id);
                db.addresses.Remove(theAddress);

                db.SaveChanges();

                return RedirectToAction("Index", new { id = theAddress.person_id });
            }
            catch
            {
                return View();
            }
        }
    }
}
