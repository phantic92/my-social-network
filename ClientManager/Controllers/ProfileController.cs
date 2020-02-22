using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClientManager.Controllers
{
    public class ProfileController : Controller
    {
        Models.ClientsEntities db = new Models.ClientsEntities(); 

        // GET: Home
        public ActionResult Index()
        {
            return View(db.persons);
        }

        public ActionResult Search(string name)
        {
            //IEnemerable<Models.Person> result = ...
            var result = db.persons.Where(p => (p.first_name + " " + p.last_name).Contains(name));

            return View("Index", result);
        }

        // GET: Home/Details/5
        public ActionResult Details(int id)
        {
            Models.person thePerson = db.persons.SingleOrDefault(p => p.person_id == id);

            return View(thePerson);
        }

        // GET: Home/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Home/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                int userId = int.Parse(Session["user_id"].ToString());
                Models.person newPerson = new Models.person();
                Models.user theUser = db.users.SingleOrDefault(u => u.user_id == userId);
                //wow
                newPerson.first_name = collection["first_name"];
                newPerson.last_name = collection["last_name"];
                newPerson.notes = collection["notes"];
                newPerson.privacy_setting = collection["privacy_setting"] != null;
                newPerson.gender = collection["gender"];
                theUser.person_id = newPerson.person_id;

                db.persons.Add(newPerson);
                db.SaveChanges();
                
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Home/Edit/5
        public ActionResult Edit(int id)
        {
            Models.person thePerson = db.persons.SingleOrDefault(p => p.person_id == id);

            return View(thePerson);
        }

        // POST: Home/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                Models.person thePerson = db.persons.SingleOrDefault(p => p.person_id == id);

                thePerson.first_name = collection["first_name"];
                thePerson.last_name = collection["last_name"];
                thePerson.notes = collection["notes"];
                thePerson.gender = collection["gender"];

                db.SaveChanges(); 

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Home/Delete/5
        public ActionResult Delete(int id)
        {
            Models.person thePerson = db.persons.SingleOrDefault(p => p.person_id == id);

            return View(thePerson);
        }

        // POST: Home/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                Models.person thePerson = db.persons.SingleOrDefault(p => p.person_id == id);

                db.persons.Remove(thePerson);
                db.SaveChanges(); 

                return RedirectToAction("Index");
            }
            catch
            {
                db.SaveChanges();
                return View();
            }
        }
    }
}
