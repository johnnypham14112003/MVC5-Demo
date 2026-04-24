using Repository.Models;
using Service.Interfaces;
using Service.Services;
using Service.ViewModels;
using System;
using System.IO;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ExampleMVC.Controllers
{
    public class AdminController : Controller
    {
        private readonly IMotorService _motorService;
        private readonly IAccountService _accountService;
        public AdminController(IMotorService motorService, AccountService accountService)
        {
            _motorService = motorService;
            _accountService = accountService;
        }

        // GET: Admin
        public ActionResult Index()
        {
            if (Session["UserId"] == null) return RedirectToAction("Login", "Home");
            //if (!Session["UserRole"].ToString().Equals("Admin"))
            //    return RedirectToAction("Index", "Home");

            return View("Index");//Dashboard
        }

        public ActionResult ListMotor()
        {
            if (Session["UserId"] == null) return RedirectToAction("Login", "Home");

            var data = _motorService.GetAll();
            return View(data);

        }

        [HttpPost]
        public ActionResult Search(string keyword)
        {
            var data = _motorService.Search(keyword);
            return View("ListMotor", data);
        }

        public ActionResult CreateMotor()
        {
            if (Session["UserId"] == null) return RedirectToAction("Login", "Home");

            return View();
        }

        [HttpPost]
        public ActionResult CreateMotor(MotorViewModel newMoto)
        {
            if (Session["UserId"] == null) return RedirectToAction("Login", "Home");

            newMoto.Id = Guid.NewGuid();
            var result = _motorService.Add(newMoto);
            if (result == false) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            return RedirectToAction("ListMotor");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OverrideImage(HttpPostedFileBase fileAttach, Guid motorId)
        {
            if (Session["UserId"] == null) return RedirectToAction("Login", "Home");

            if (fileAttach == null && fileAttach.ContentLength == 0)
            {
                ViewBag.Message = "Please choose an image before apply!";
            }

            var motor = new MotorViewModel();
            try
            {
                string directoryPath = Server.MapPath("~/wwwroot/images/motors");

                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                //Delete old files
                string filePattern = motorId.ToString() + ".*";
                string[] existingFiles = Directory.GetFiles(directoryPath, filePattern);

                foreach (string existingFile in existingFiles)
                {
                    System.IO.File.Delete(existingFile);
                }

                string extension = Path.GetExtension(fileAttach.FileName);
                string newFileName = motorId.ToString() + extension;
                string path = Path.Combine(directoryPath, newFileName);

                fileAttach.SaveAs(path);

                var urlPath = $"/wwwroot/images/motors/{newFileName}";
                motor = _motorService.UpdateImage(motorId, urlPath);

                ViewBag.Message = "Replace image successfully!";
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Error: " + ex.Message;
            }

            return View("EditMotor", motor);
        }

        public ActionResult DetailMotor(Guid id)
        {
            if (id == null || id == Guid.Empty)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var moto = _motorService.GetDetail(id);
            if (moto is null) return HttpNotFound();

            ViewBag.ReturnTitle = "List Motors";
            return View("DetailMotor", moto);
        }

        public ActionResult DeleteMotor(Guid id)
        {
            if (Session["UserId"] == null) return RedirectToAction("Login", "Home");

            var result = _motorService.Delete(id);
            if (result == false) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            return RedirectToAction("ListMotor");
        }

        [ActionName("EditMotor")]
        public ActionResult ViewEditMotor(Guid id)
        {
            if (Session["UserId"] == null) return RedirectToAction("Login", "Home");

            if (id == null || id == Guid.Empty)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var moto = _motorService.GetDetail(id);
            if (moto is null) return HttpNotFound();

            return View("EditMotor", moto);
        }

        [HttpPost]
        public ActionResult EditMotor(MotorViewModel newData)
        {
            if (Session["UserId"] == null) return RedirectToAction("Login", "Home");

            if (newData == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var result = _motorService.Update(newData);
            if (result == false) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            return RedirectToAction("ListMotor");
        }

        //==========================================================================================
        //==============================[ USER CRUD ]===============================================
        public ActionResult ListUser()
        {
            if (Session["UserId"] == null) return RedirectToAction("Login", "Home");

            if (!Session["UserRole"].ToString().Equals("Admin"))
                return RedirectToAction("Index");

            var data = _accountService.GetAll();
            return View(data);
        }

        [HttpPost]
        public ActionResult SearchUser(string keyword, string role)
        {
            if (Session["UserId"] == null) return RedirectToAction("Login", "Home");
            if (!Session["UserRole"].ToString().Equals("Admin"))
                return RedirectToAction("Index");

            var data = _accountService.Search(keyword, role);
            return View("ListUser", data);
        }

        public ActionResult CreateUser()
        {
            if (Session["UserId"] == null) return RedirectToAction("Login", "Home");
            if (!Session["UserRole"].ToString().Equals("Admin"))
                return RedirectToAction("Index");

            return View();
        }

        [HttpPost]
        public ActionResult CreateUser(AccountViewModel newAcc)
        {
            if (Session["UserId"] == null) return RedirectToAction("Login", "Home");
            if (!Session["UserRole"].ToString().Equals("Admin"))
                return RedirectToAction("Index");

            newAcc.Id = Guid.NewGuid();
            var result = _accountService.Add(newAcc);
            if (result == false) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            return RedirectToAction("ListUser");
        }

        public ActionResult DeleteUser(Guid id)
        {
            if (Session["UserId"] == null) return RedirectToAction("Login", "Home");
            if (!Session["UserRole"].ToString().Equals("Admin"))
                return RedirectToAction("Index");

            var result = _accountService.Delete(id);
            if (result == false) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            return RedirectToAction("ListUser");
        }

        [ActionName("EditUser")]
        public ActionResult ViewEditUser(Guid id)
        {
            if (Session["UserId"] == null) return RedirectToAction("Login", "Home");
            if (!Session["UserRole"].ToString().Equals("Admin"))
                return RedirectToAction("Index");

            if (id == null || id == Guid.Empty)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var user = _accountService.GetDetail(id);
            if (user is null) return HttpNotFound();

            return View("EditUser", user);
        }

        [HttpPost]
        public ActionResult EditUser(AccountViewModel newData)
        {
            if (Session["UserId"] == null) return RedirectToAction("Login", "Home");
            if (!Session["UserRole"].ToString().Equals("Admin"))
                return RedirectToAction("Index");

            if (newData == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var result = _accountService.Update(newData);
            if (result == false) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            return RedirectToAction("ListUser");
        }
    }
}