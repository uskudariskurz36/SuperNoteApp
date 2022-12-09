using CheckPasswordStrength;
using Microsoft.AspNetCore.Mvc;
using SuperNoteApp.Entities;
using SuperNoteApp.Helpers;
using SuperNoteApp.Models;
using System;

namespace SuperNoteApp.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                StrengthProperty checkPass = ClassifyStrength.PasswordStrength(model.Password);
                ViewData["err-password-strength-text"] = checkPass.Value;
                ViewData["err-password-strength"] = checkPass.Id;

                if (checkPass.Id == 0)  // şifre weak (zayıf) ise..
                {
                    ModelState.AddModelError("Password", "Şifre uygun değildir.");
                }
            }

            if (ModelState.IsValid)
            {
                UserManager userManager = new UserManager();
                bool done = userManager.AddUser(model.Username, model.Password);

                ViewData["done"] = done;
            }

            return View(model);
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                UserManager userManager = new UserManager();
                User user = userManager.Authenticate(model.Username, model.Password);

                if (user != null)
                {
                    HttpContext.Session.SetInt32("userid", user.Id);
                    HttpContext.Session.SetString("username", user.Username);

                    return RedirectToAction("Index", "Note");
                }
                else
                {
                    ModelState.AddModelError("", "Hatalı kullanıcı adı ya da şifre!");
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Profile()
        {
            int? userid = HttpContext.Session.GetInt32("userid");

            if (userid == null)
            {
                return RedirectToAction("Login");
            }

            UserManager userManager = new UserManager();
            User user = userManager.GetUserById(userid.Value);

            ProfileModel model = new ProfileModel();
            model.Name = user.Name;
            model.Surname = user.Surname;

            return View(model);
        }

        [HttpPost]
        public IActionResult Profile(ProfileModel model)
        {
            int? userid = HttpContext.Session.GetInt32("userid");

            if (userid == null)
            {
                return RedirectToAction("Login");
            }

            if (model.IsUpdatePassword)
            {
                if (string.IsNullOrEmpty(model.Password) || string.IsNullOrEmpty(model.RePassword) ||
                    string.IsNullOrWhiteSpace(model.Password) || string.IsNullOrWhiteSpace(model.RePassword))
                {
                    ModelState.AddModelError("", "Şifre ya da şifre tekrar boş geçilemez.");
                }
            }

            if (ModelState.IsValid)
            {
                UserManager userManager = new UserManager();
                bool done = false;

                if (model.IsUpdatePassword)
                {
                    done = userManager.UpdatePassword(userid.Value, model.Password);
                }
                else
                {
                    done = userManager.UpdateProfile(userid.Value, model.Name, model.Surname);
                }

                //if (done == false)
                if (!done)
                {
                    ModelState.AddModelError("", "İşlem yapılamadı. Bir hata oluştu.");
                }
                else
                {
                    ViewData["ok"] = "Bilgiler kaydedildi.";
                }
            }

            return View(model);
        }


        [HttpGet]
        public IActionResult ProfilePicture()
        {
            int? userid = HttpContext.Session.GetInt32("userid");

            if (userid == null)
            {
                return RedirectToAction("Login");
            }

            UserManager userManager = new UserManager();
            User user = userManager.GetUserById(userid.Value);

            ViewData["PictureName"] = user.Picture;
            //ViewBag.PictureName = user.Picture;

            return View();
        }

        [HttpPost]
        public IActionResult ProfilePicture(ProfilePictureModel model)
        {
            int? userid = HttpContext.Session.GetInt32("userid");

            if (userid == null)
            {
                return RedirectToAction("Login");
            }

            if (ModelState.IsValid)
            {
                string filename = "user_";  // user_
                filename += userid;         // user_1

                // model.NewPicture.ContentType = "image/png"

                string ext = model.NewPicture.ContentType.Split('/')[1];    // png | jpg | jpeg
                filename += "." + ext;      // user_1.png

                FileStream fileStream = new FileStream($"wwwroot/img/{filename}", FileMode.OpenOrCreate);
                model.NewPicture.CopyTo(fileStream);

                fileStream.Close();

                UserManager userManager2 = new UserManager();
                userManager2.UpdateProfilePicture(userid.Value, filename);
            }

            UserManager userManager = new UserManager();
            User user = userManager.GetUserById(userid.Value);

            ViewData["PictureName"] = user.Picture;
            //ViewBag.PictureName = user.Picture;

            return View(model);
        }


        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
