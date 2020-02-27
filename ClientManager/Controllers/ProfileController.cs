using ClientManager.ActionFilters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace ClientManager.Controllers
{
    [LoginFilter]
    public class ProfileController : Controller
    {
        Models.ClientsEntities db = new Models.ClientsEntities();

        // GET: Home
        public ActionResult Index()
        {
            int UserId = int.Parse(Session["user_id"].ToString());
            Models.user theUser = db.users.SingleOrDefault(u => u.user_id == UserId);
            Models.person thePerson = db.persons.SingleOrDefault(p => p.person_id == theUser.person_id);
            ViewBag.username = db.users.SingleOrDefault(u => u.user_id == UserId);

            return View(thePerson);
        }

        public ActionResult Search(string name)
        {
            //IEnemerable<Models.Person> result = ...
            var myUserId = int.Parse(Session["user_id"].ToString());
            var result = db.users.Where(u => (u.username.Contains(name)) && u.user_id != myUserId);

            return View("search", result);
        }

        public ActionResult ModifyPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ModifyPassword(FormCollection collection)
        {
            try
            {
                int userId = int.Parse(Session["user_id"].ToString());
                Models.user theUser = db.users.SingleOrDefault(u => u.user_id == userId);


                theUser.password_hash = Crypto.HashPassword(collection["password_hash"]);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
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

                Session["person_id"] = newPerson.person_id;
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
                thePerson.privacy_setting = collection["privacy_setting"] != null;

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

        public ActionResult SendRequest(int id)
        {
            try
            {
                // TODO: Add delete logic here
                int userId = int.Parse(Session["user_id"].ToString());
                int personId = (int)db.users.SingleOrDefault(u => u.user_id == userId).person_id;

                int requested = (int)db.users.SingleOrDefault(u => u.user_id == id).person_id;

                Models.user requester = db.users.SingleOrDefault(p => p.person_id == personId);
                Models.user theRequested = db.users.SingleOrDefault(p => p.person_id == requested);

                // get status from profile notes 
                Models.person getStatus = db.persons.SingleOrDefault(p => p.person_id == requested);

                Models.friendlink friendRequest = new Models.friendlink
                {

                    requester = (int)requester.person_id,
                    requested = (int)theRequested.person_id,

                    status = getStatus.notes,
                    timestamp = DateTime.Now.ToString(),
                };

                db.friendlinks.Add(friendRequest);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                db.SaveChanges();
                return View();
            }
        }

        public ActionResult CreateMessage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateMessage(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                int userId = int.Parse(Session["user_id"].ToString());
                int senderId = (int)db.users.SingleOrDefault(u => u.user_id == userId).person_id;
                
                Models.person theReceiver = db.persons.SingleOrDefault(p => p.person_id == id); 
                
                Models.message createdMessage = new Models.message
                {
                    sender = senderId,
                    receiver = theReceiver.person_id,

                    message1 = collection["message1"],
                    timestamp = DateTime.Now.ToString(),
                };

                db.messages.Add(createdMessage);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                db.SaveChanges();
                return View();
            }
        }

        //public ActionResult ListMessages(int id)
        //{
        //    int userId = int.Parse(Session["user_id"].ToString());
        //    int senderId = (int)db.users.SingleOrDefault(u => u.user_id == userId).person_id;

        //    int receiverId = db.persons.SingleOrDefault(p => p.person_id == id).person_id;

        //    Models.message receivedMessages = db.messages.Where(r => r.receiver == senderId && senderId == receiverId);

        //    foreach (var message in receivedMessages)
        //    {

        //    }

        //    return View(db.messages.Where((s => s.sender == senderId) || (r => r.receiver == receiverId)));
        //}
    }
}
