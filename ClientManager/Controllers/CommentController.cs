using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClientManager.Controllers
{
    public class CommentController : Controller
    {
        Models.ClientsEntities db = new Models.ClientsEntities(); 

        // GET: Comment
        public ActionResult Index(int id) //picture_id
        {
            Models.picture pictureComments = db.pictures.SingleOrDefault(p => p.picture_id == id);

            return View(pictureComments);
        }

        // GET: Comment/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Comment/Create
        [HttpPost]
        public ActionResult Create(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                int userId = int.Parse(Session["user_id"].ToString());
                Models.user theCommenter = db.users.SingleOrDefault(u => u.user_id == userId);

                Models.comment newComment = new Models.comment()
                {
                    picture_id = id,
                    comment1 = collection["comment1"],
                    person_id = (int)theCommenter.person_id,
                    timestamp = DateTime.Now.ToString(),
                };
                db.comments.Add(newComment);
                db.SaveChanges();

                return RedirectToAction("Index", new { id = id });
            }
            catch
            {
                return View();
            }
        }

        // GET: Comment/Edit/5
        public ActionResult Edit(int id)
        {
            Models.comment theComment = db.comments.SingleOrDefault(c => c.comment_id == id);
            return View(theComment);
        }

        // POST: Comment/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                Models.comment theComment = new Models.comment
                {
                    picture_id = id,
                    comment1 = collection["comment1"],
                    timestamp = DateTime.Now.ToString()
                };
                db.SaveChanges();

                return RedirectToAction("Index", new { id = theComment.picture_id});
            }
            catch
            {
                return View();
            }
        }

        // GET: Comment/Delete/5
        public ActionResult Delete(int id)
        {
            Models.comment theComment = db.comments.SingleOrDefault(c => c.comment_id == id);

            db.comments.Remove(theComment);
            db.SaveChanges();

            return View();
        }

        // POST: Comment/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
