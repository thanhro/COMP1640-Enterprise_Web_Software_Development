using Login_UploadFile.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Login_UploadFile.Controllers
{
    public class HomeController : Controller
    {
        DBModel db = new DBModel();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(string email, string pass)
        {
            Account account = db.Accounts.SingleOrDefault(acc => acc.email.Equals(email) && acc.passwords.Equals(pass));
            if(account != null && account.roleA == 1 && account.status == 1)
            {
                Session.Add("student", account.email);
                return RedirectToAction("About", "Home", Session);
            }
            else if(account != null && account.roleA == 2 && account.status == 1)
            {
                Session.Add("lecturer", account.email);
                return RedirectToAction("Contact", "Home", Session);
            }
            else if (account != null && account.roleA == 3 && account.status == 1)
            {
                Session.Add("manager", account.email);
                return RedirectToAction("Contact", "Home", Session);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            if (Session["student"] != null)
            {
                Session.Remove("student");
            }
            else if(Session["lecturer"] != null)
            {
                Session.Remove("lecturer");
            }
            else
            {
                Session.Remove("manager");
                //Session.Remove("total");
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult About()
        {
            string session = Session["student"].ToString();
            Document dcm = db.Documents.SingleOrDefault(dc => dc.id_Student == dc.Account.id && dc.Account.email.Equals(session));
            ViewBag.Message = "Wellcome student " + session.Split('@')[0].ToString();
            if (dcm == null)
            {
                return View();
            }
            else
            {
                return View(dcm);
            }  
        }

        public ActionResult UploadFile(HttpPostedFileBase file_document)
        {
            var session = Session["student"].ToString();
            Account acc = db.Accounts.SingleOrDefault(ac => ac.email.Equals(session));
            Document dcm = new Document();
            string fileName = Path.GetFileNameWithoutExtension(file_document.FileName);
            string extension = Path.GetExtension(file_document.FileName);
            fileName = fileName + extension;
            fileName = Path.Combine(Server.MapPath("~/Files/"), fileName);
            dcm.path_Document = "~/Files/" + file_document.FileName;
            dcm.type_Document = extension;
            dcm.name_Document = file_document.FileName.Split('.')[0].ToString();
            DateTime date = DateTime.Now;
            dcm.date_Summit = date;
            dcm.id_Student = acc.id;
            dcm.status_Document = 1;
            file_document.SaveAs(fileName);
            db.Documents.Add(dcm);
            db.SaveChanges();
            return RedirectToAction("About");
        }

        public ActionResult Download(string path_Document)
        {
            string filepath = Path.Combine(Server.MapPath("~/Files/"), path_Document);
            byte[] documentBytes = System.IO.File.ReadAllBytes(filepath);
            Response.AddHeader("Content-type", "application/octet-stream");
            Response.AddHeader("Content-Disposition", "attachment; filename=" + path_Document);
            Response.BinaryWrite(documentBytes);
            Response.Flush();
            Response.End();
            return File(documentBytes, System.Net.Mime.MediaTypeNames.Application.Octet, path_Document);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}