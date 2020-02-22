using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using TotpAuth;

namespace ClientManager.Controllers
{
    /**
      make sure the person you're messing with is the authenticated user if they have privacy settings 
      this is for index / create in pictures,contacts, etc. controllers  
      person_id - > Session["user_id"]

      int id = int.Parse(Session["user_id"].ToString());
    **/
    public class HomeController : Controller
    {
        Models.ClientsEntities db = new Models.ClientsEntities();
        private static Random random = new Random();

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            //find the user record
            string username = collection["username"];
            Models.user theUser = db.users.SingleOrDefault(u => u.username.Equals(username));
            if (theUser != null &&
                Crypto.VerifyHashedPassword(theUser.password_hash, collection["password_hash"]))
            {
                if (theUser.secret != null)
                {
                    Totp totp = new Totp(theUser.secret);
                    string theCode = totp.AuthenticationCode;
                    if (theCode.Equals(collection["validation"]))
                    {
                        Session["user_id"] = theUser.user_id;
                        if (theUser.person_id != null)
                            return RedirectToAction("Index", "Profile");
                        return RedirectToAction("Create", "Profile");
                    }
                }
                else
                {
                    Session["user_id"] = theUser.user_id;
                    if (theUser.person_id != null)
                        return RedirectToAction("Index", "Profile");
                    return RedirectToAction("Create", "Profile");
                }
                ViewBag.error = "Wrong Username/Password/2FA combination!";
                return View();
            }
            else
            {
                ViewBag.error = "Wrong Username/Password/2FA combination!";
                return View();
            }
        }

        // GET: Home/Logout/5
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index");
        }

        private static string RandomBase32String(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        // GET: Home/Create
        public ActionResult Register()
        {
            // Generate a string like this
            //otpauth://totp/Example:alice@google.com?secret=JBSWY3DPEHPK3PXP&issuer=Example
            //string otpauth = "otpauth://totp/Example:alice@google.com?secret=JBSWY3DPEHPK3PXP&issuer=Example

            // Secets that has 16 chars A-Z, not 0, not 1, but 2-7
            string secret = RandomBase32String(16);
            string otpauth = "otpauth://totp/Application:someaccount?secret=" + secret +
                                    "&issuer=Application";

            // To generate a QR code
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(otpauth, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);
            ImageConverter converter = new ImageConverter();
            ViewBag.QRCode = (byte[])converter.ConvertTo(qrCodeImage, typeof(byte[]));
            Session["secret"] = secret;

            return View();
        }

        // POST: Home/Create
        [HttpPost]
        public ActionResult Register(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                string username = collection["username"];
                string secret = Session["secret"].ToString();
                Totp totp = new Totp(secret);

                string theSecret = null;
                if (totp.AuthenticationCode.Equals(collection["validation"].Trim()))
                {
                    theSecret = secret;
                }

                Models.user theUser = db.users.SingleOrDefault(u => u.username.Equals(username));
                if (theUser != null)
                    return RedirectToAction("Register");//todo:provide feedback

                Models.user newUser = new Models.user()
                {
                    username = collection["username"],
                    password_hash = Crypto.HashPassword(collection["password_hash"]),
                    secret = theSecret
                };
                db.users.Add(newUser);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
