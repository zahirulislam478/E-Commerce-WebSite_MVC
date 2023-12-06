using E_Commerce_WebSite.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Commerce_WebSite.Controllers
{
    public class AccountsController : Controller
    {
        // Display the login view
        public ActionResult Login()
        {
            return View();
        }

        // Handle user login
        [HttpPost]
        public ActionResult Login(UserModel model)
        {
            // Create a user manager and try to find the user
            var userStore = new UserStore<IdentityUser>();
            var userManager = new UserManager<IdentityUser>(userStore);
            var user = userManager.Find(model.Username, model.Password);

            if (user != null)
            {
                // Sign in the user and redirect to the home page
                var umanager = HttpContext.GetOwinContext().Authentication;
                var ui = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                umanager.SignIn(new AuthenticationProperties() { IsPersistent = false }, ui);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // Show an error message for invalid login
                ModelState.AddModelError("", "Invalid username or password.");
            }
            return View(model);
        }

        // Display the registration view
        public ActionResult Register()
        {
            return View();
        }

        // Handle user registration
        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            // Create a user manager and attempt to register the user
            var store = new UserStore<IdentityUser>();
            var manager = new UserManager<IdentityUser>(store);
            var user = new IdentityUser { UserName = model.Username };
            IdentityResult result = manager.Create(user, model.Password);

            if (result.Succeeded)
            {
                // Redirect to the login page after successful registration
                return RedirectToAction("Login", "Accounts");
            }
            else
            {
                // Show an error message for registration failure
                ModelState.AddModelError("", "Registration failed");
            }
            return View(model);
        }

        // Handle user logout
        public ActionResult Logout()
        {
            // Sign out the user and redirect to the login page
            var umanager = HttpContext.GetOwinContext().Authentication;
            umanager.SignOut();
            return RedirectToAction("Login", "Accounts");
        }
    }
}