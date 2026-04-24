using Service.Interfaces;
using Service.ViewModels;
using System;
using System.Net;
using System.Web.Mvc;

namespace ExampleMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IMotorService _motorService;
        public HomeController(IAccountService accountService, IMotorService motorService)
        {
            _accountService = accountService;
            _motorService = motorService;
        }

        //Landing Page - GET (../Home) (../# )
        public ActionResult Index()
        {
            return View();
        }

        //List Model Page - GET (../Home/Showroom)
        public ActionResult Showroom()
        {
            var data = _motorService.GetAll();
            return View(data);
        }
        public ActionResult DetailMotor(Guid id)
        {
            if (id == null || id == Guid.Empty)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var moto = _motorService.GetDetail(id);
            if (moto is null) return HttpNotFound();

            ViewBag.ReturnTitle = "Showroom";
            return View("DetailMotor", moto);
        }

        [HttpPost]
        public ActionResult Search(string keyword)
        {
            var data = _motorService.Search(keyword);
            return View("Showroom", data);
        }

        [ActionName("EditUser")]
        public ActionResult ViewEditUser(Guid id)
        {
            if (Session["UserId"] == null) return RedirectToAction("Login", "Home");

            if (id == null || id == Guid.Empty)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var acc = _accountService.GetDetail(id);
            if (acc is null) return HttpNotFound();

            ViewBag.Controller = "Home";
            return View("EditUser", acc);
        }

        [HttpPost]
        public ActionResult EditUser(AccountViewModel newData)
        {
            if (Session["UserId"] == null) return RedirectToAction("Login", "Home");

            if (newData == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var result = _accountService.Update(newData);
            if (result == false) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            return View("Index");
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Login")]
        [ValidateAntiForgeryToken]
        public ActionResult LoginSubmitted(AuthViewModel input)
        {
            if (ModelState.IsValid)
            {
                var loginInfo = _accountService.Login(input);
                if (loginInfo != null)
                {
                    Session["UserId"] = loginInfo.Id.ToString();
                    Session["UserName"] = loginInfo.Email.ToString();
                    Session["UserRole"] = loginInfo.Role.ToString();
                    ViewBag.User = loginInfo;
                    return RedirectToAction("UserDashBoard");
                }
            }
            ViewBag.Message = "Invalid email or password!";
            return View();//Return Login
        }
        public ActionResult UserDashBoard()
        {
            if (Session["UserId"] == null) return RedirectToAction("Login");

            //if (!Session["UserRole"].ToString().Equals("Admin"))
            //{
            //    return RedirectToAction("Index");
            //}

            return RedirectToAction("Index", "Admin");
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Login");
        }
    }

}