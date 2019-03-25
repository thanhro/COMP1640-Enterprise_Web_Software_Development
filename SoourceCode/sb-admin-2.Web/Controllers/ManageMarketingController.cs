using sb_admin_2.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace sb_admin_2.Web.Controllers
{
    public class ManageMarketingController : Controller
    {
        DBModel db = new DBModel();
        // GET: ManageMarketing
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddFaculty()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddFaculty_Detail(String facultyname, String description)
        {
            Faculty fac = new Faculty();
            fac.FacultyName = facultyname;
            fac.Description = description;
            db.Faculties.Add(fac);
            db.SaveChanges();
            ViewBag.Success = "Add faculty Successfully!";
            return View("AddFaculty");
        }

        public void displayDropDL()
        {
            List<Faculty> listFac = db.Faculties.ToList();
            ViewBag.listFac = new SelectList(listFac, "FacultyID", "FacultyName");
            string role = "Marketing Coordinator";
            var listMC = from u in db.Users
                         where (u.Role1.RoleName == role)
                         select u; // select all role manage coordinator
            ViewBag.listMC = new SelectList(listMC, "UserID", "Name");
        }

        public ActionResult Assigned_Faculty()
        {
            displayDropDL();
            return View();
        }

        public ActionResult Assigned_Faculty_Detail(String FacultyID, String UserID)
        {
            String Fac = FacultyID.ToUpper();
            String Use = UserID.ToUpper();
            var FacultyID_ToUpper = Guid.Parse(Fac);
            var UserID_ToUpper = Guid.Parse(Use);
            //Faculty faculty = db.Faculties.SingleOrDefault(f => f.FacultyID.Equals(FacultyID_ToUpper));
            //Faculty_Detail fd = db.Faculty_Detail.SingleOrDefault(f => f.FacultyID.Equals(FacultyID_ToUpper));
            var fd = from f in db.Faculty_Detail
                     where (f.FacultyID == FacultyID_ToUpper)
                     select f;
            if (fd.Count() == 0)
            {
                Faculty_Detail faculty_Detail = new Faculty_Detail();
                faculty_Detail.FacultyID = FacultyID_ToUpper;
                faculty_Detail.UserCoordinator = UserID_ToUpper;
                db.Faculty_Detail.Add(faculty_Detail);
                db.SaveChanges();
                ViewBag.Success = "Assigned Faculty Successfully!";
            }
            else
            {
                ViewBag.Error = "This faculty was assigned!";
            }
            displayDropDL();
            return View("Assigned_Faculty");
        }
    }
}