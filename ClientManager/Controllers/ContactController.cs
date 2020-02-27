using ClientManager.ActionFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClientManager.Controllers
{
    public class ContactController : Controller
    {
        Models.ClientsEntities db = new Models.ClientsEntities(); 

        // GET: Contact
        public ActionResult Index(int id) //person_id
        {
            Models.person thePerson = db.persons.SingleOrDefault(p => p.person_id == id);
            return View(thePerson);
        }

        // GET: Contact/Details/5
        public ActionResult Details(int id)
        {
            Models.contact theContact = db.contacts.SingleOrDefault(c => c.contact_id == id);

            return View(theContact);
        }

        // GET: Contact/Create
        public ActionResult Create(int id) //person_id
        {
            ViewBag.person = db.persons.SingleOrDefault(p => p.person_id == id);

            return View();
        }

        // POST: Contact/Create
        [HttpPost]
        public ActionResult Create(int id, FormCollection collection) //person_id
        {
            try
            {
                // TODO: Add insert logic here
                Models.contact newContact = new Models.contact()
                {
                    person_id = id,
                    info = collection["info"],
                    type = collection["type"]
                };
                db.contacts.Add(newContact);
                db.SaveChanges();

                return RedirectToAction("Index", new { id = id });
            }
            catch
            {
                return View();
            }
        }

        // GET: Contact/Edit/5
        public ActionResult Edit(int id)
        {
            Models.contact theContact = db.contacts.SingleOrDefault(c => c.contact_id == id);

            return View(theContact);
        }

        // POST: Contact/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                Models.contact theContact = db.contacts.SingleOrDefault(c => c.contact_id == id);
                theContact.type = collection["type"];
                theContact.info = collection["info"];
                db.SaveChanges();

                return RedirectToAction("Index", new { id = theContact.person_id });
            }
            catch
            {
                return View("an exception has occured");
            }
        }

        // GET: Contact/Delete/5
        public ActionResult Delete(int id)
        {
            Models.contact theContact = db.contacts.SingleOrDefault(c => c.contact_id == id);

            return View(theContact);
        }

        // POST: Contact/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                Models.contact theContact = db.contacts.SingleOrDefault(c => c.contact_id == id);
                db.contacts.Remove(theContact);

                db.SaveChanges();

                return RedirectToAction("Index", new { id = theContact.person_id});
            }
            catch
            {
                return View();
            }
        }
    }
}
