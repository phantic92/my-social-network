using ClientManager.ActionFilters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClientManager.Controllers
{
    [PictureFilter]
    public class PictureController : Controller
    {
        Models.ClientsEntities db = new Models.ClientsEntities();

        // GET: Picture
        public ActionResult Index(int id)
        {
            Models.person thePerson = db.persons.SingleOrDefault(p => p.person_id == id);

            return View(thePerson);
        }

        public ActionResult ChooseProfilePic(int picture_id, int person_id) // person_id
        {
            try
            {
                Models.person thePerson = db.persons.SingleOrDefault(p => p.person_id == person_id);
                Models.picture thePicture = db.pictures.SingleOrDefault(p => p.picture_id == picture_id);

                thePerson.profile_pic = thePicture.picture_id;
                
                db.SaveChanges();

                return View("Index", thePerson);
            }
            catch
            {
                return View();
            }
        }

        // GET: Picture/Details/5
        public ActionResult Details(int id)
        {
            Models.picture thePic = db.pictures.SingleOrDefault(p => p.picture_id == id);

            return View(thePic);
        }

        // GET: Picture/Create
        public ActionResult Create(int id)
        {
            ViewBag.person = db.persons.SingleOrDefault(p => p.person_id == id);

            return View();
        }

        // POST: Picture/Create
        [HttpPost]
        public ActionResult Create(int id,FormCollection collection, HttpPostedFileBase newPicture)
        {
            try
            {
                // TODO: Add insert logic here
                // type checking
                string[] types = { "image/gif", "image/jpeg", "image/png" };

                if(newPicture != null 
                    && newPicture.ContentLength > 0 
                    && types.Contains(newPicture.ContentType))
                {
                    Guid g = Guid.NewGuid();
                    string filename = g + "." + Path.GetExtension(newPicture.FileName);
                    string path = Server.MapPath("~/Images/");
                    path = Path.Combine(path, filename);
                    newPicture.SaveAs(path);

                    Models.picture newPic = new Models.picture()
                    {
                        caption = collection["caption"],
                        time_info = collection["time_info"],
                        location = collection["location"],
                        relative_path = filename,
                        person_id = id,
                    };

                    db.pictures.Add(newPic);
                    db.SaveChanges();
                }

                return RedirectToAction("Index", new { id = id });
            }
            catch
            {
                return View();
            }
        }

        // GET: Picture/Edit/5
        public ActionResult Edit(int id)
        {
            Models.picture thePic = db.pictures.SingleOrDefault(p => p.picture_id == id);

            return View(thePic);
        }

        // POST: Picture/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection, HttpPostedFileBase newPicture) // picture_id
        {
            try
            {
                // TODO: Add update logic here
                string[] types = { "image/gif", "image/jpeg", "image/png" };
                Models.picture thePic = db.pictures.SingleOrDefault(p => p.picture_id == id);

                if (newPicture != null
                   && newPicture.ContentLength > 0
                   && types.Contains(newPicture.ContentType))
                {
                    Guid g = Guid.NewGuid();
                    string filename = g + "." + Path.GetExtension(newPicture.FileName);
                    string path = Server.MapPath("~/Images/");
                    path = Path.Combine(path, filename);
                    newPicture.SaveAs(path);

                    thePic.caption = collection["caption"];
                    thePic.time_info = collection["time_info"];
                    thePic.location = collection["location"];
                    thePic.relative_path = filename;

                    db.SaveChanges();
                }

                return RedirectToAction("Index", new { id = thePic.person_id });
            }
            catch
            {
                return View();
            }
        }

        // GET: Picture/Delete/5
        public ActionResult Delete(int id)
        {
            Models.picture thePic = db.pictures.SingleOrDefault(p => p.picture_id == id);

            return View(thePic);
        }

        // POST: Picture/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                Models.picture thePic = db.pictures.SingleOrDefault(p => p.picture_id == id);
                db.pictures.Remove(thePic);
                db.SaveChanges();

                return RedirectToAction("Index", new { id = thePic.person_id});
            }
            catch
            {
                return View();
            }
        }
    }
}
