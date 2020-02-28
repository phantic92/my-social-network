using ClientManager.ActionFilters;
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
                int UserId = int.Parse(Session["user_id"].ToString());
                Models.comment theComment = db.comments.SingleOrDefault(c => c.comment_id == id);

                // TODO: Add update logic 
                theComment.comment1 = collection["comment1"];
                theComment.timestamp = DateTime.Now.ToString();

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

            return View(theComment);
        }

        // POST: Comment/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                Models.comment theComment = db.comments.SingleOrDefault(c => c.comment_id == id);
                db.comments.Remove(theComment);
                db.SaveChanges();

                return RedirectToAction("Index", new { id = theComment.picture_id});
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Like(int id)
        {
            try
            {
                // TODO: Add delete logic here
                int userId = int.Parse(Session["user_id"].ToString());
                int personId = (int)db.users.SingleOrDefault(u => u.user_id == userId).person_id;
                Models.comment theLikedComment = db.comments.SingleOrDefault(p => p.comment_id == id);

                Models.comment_like likedComment = new Models.comment_like
                {
                    comment_id = id,
                    person_id = personId,

                    timestamp = DateTime.Now.ToString(),
                };

                db.comment_like.Add(likedComment);
                db.SaveChanges();

                return RedirectToAction("Index", new { id = theLikedComment.picture_id});
            }
            catch
            {
                db.SaveChanges();
                return View();
            }
        }

        public ActionResult UnLike(int id)
        {
            try
            {
                // TODO: Add delete logic here
                int userId = int.Parse(Session["user_id"].ToString());
                int personId = (int)db.users.SingleOrDefault(u => u.user_id == userId).person_id;
                Models.comment theLikedComment = db.comments.SingleOrDefault(p => p.comment_id == id);

                Models.comment_like likedComment = db.comment_like.SingleOrDefault(p => p.comment_id == id);

                db.comment_like.Remove(likedComment);
                db.SaveChanges();

                return RedirectToAction("Index", new { id = theLikedComment.picture_id });
            }
            catch
            {
                db.SaveChanges();
                return View();
            }
        }
    }
}
