using Gnostice.StarDocsSDK;
using sb_admin_2.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
namespace sb_admin_2.Web.Controllers
{
    public class HomeController : Controller
    {
        DBModel db = new DBModel();
        public ActionResult Index()
        {
            var listCTP = db.Contributions.Where(ct => ct.Status == 2).ToList();
            List<MultipleModelInOneView_List1> listAll = new List<MultipleModelInOneView_List1>();
            for (int j = 0; j < listCTP.Count(); j++)
            {
                MultipleModelInOneView_List1 ml = new MultipleModelInOneView_List1();
                var ContributionID_Guid = listCTP[j].ContributionID;
                Document Doc = db.Documents.SingleOrDefault(dc => dc.Contribution == ContributionID_Guid);
                Image Img = db.Images.SingleOrDefault(im => im.Contribution == ContributionID_Guid);
                List<Comment> listCmt = db.Comments.Where(im => im.Contribution == ContributionID_Guid).ToList();
                List<User> listUser = new List<User>();
                for (int i = 0; i < listCmt.Count; i++)
                {
                    var user_t = listCmt[i].Creator;
                    User user1 = db.Users.SingleOrDefault(us => us.UserID == user_t);
                    listUser.Add(user1);
                }
                ml.users = listUser;
                ml.document = Doc;
                ml.image2 = Img;
                ml.comments = listCmt;
                listAll.Add(ml);
            }
            return View(listAll);
        }

        public ActionResult View_More(String DocumentID)
        {
            try
            {
                var DocumentID_Guid = Guid.Parse(DocumentID);
                Document document = db.Documents.SingleOrDefault(d => d.DocumentID == DocumentID_Guid);
                StarDocs starDocs = new StarDocs(new ConnectionInfo(new Uri("https://api.gnostice.com/stardocs/v1"), "1c8bce0d76c944889dbc8fc14f59fe90", "fcd2b3791e2b482bbfced550a5ad02c9"));
                starDocs.Auth.loginApp();
                ViewerSettings viewerSettings = new ViewerSettings();
                viewerSettings.VisibleFileOperationControls.Open = true;
                ViewResponse viewResponse = starDocs.Viewer.CreateView(new FileObject(Path.Combine(Server.MapPath("~/Files/"), document.Path.Replace("~/Files/", ""))), null, viewerSettings);
                return new RedirectResult(viewResponse.Url);
            }
            catch(Exception e)
            {
                e.ToString();
            }

            var listCTP = db.Contributions.Where(ct => ct.Status == 2).ToList();
            List<MultipleModelInOneView_List1> listAll = new List<MultipleModelInOneView_List1>();
            for (int j = 0; j < listCTP.Count(); j++)
            {
                MultipleModelInOneView_List1 ml = new MultipleModelInOneView_List1();
                var ContributionID_Guid = listCTP[j].ContributionID;
                Document Doc = db.Documents.SingleOrDefault(dc => dc.Contribution == ContributionID_Guid);
                Image Img = db.Images.SingleOrDefault(im => im.Contribution == ContributionID_Guid);
                List<Comment> listCmt = db.Comments.Where(im => im.Contribution == ContributionID_Guid).ToList();
                List<User> listUser = new List<User>();
                for (int i = 0; i < listCmt.Count; i++)
                {
                    var user_t = listCmt[i].Creator;
                    User user1 = db.Users.SingleOrDefault(us => us.UserID == user_t);
                    listUser.Add(user1);
                }
                ml.users = listUser;
                ml.document = Doc;
                ml.image2 = Img;
                ml.comments = listCmt;
                listAll.Add(ml);
            }
            return RedirectToAction("Index", listAll);
        }

        public ActionResult Add_Comment(String DocumentID, String ContributionID, String comment)
        {
            User user = (User)Session["User"];
            var ContributionID_Guid1 = Guid.Parse(ContributionID);
            DateTime now = DateTime.Now;
            Comment comm = new Comment();
            comm.Content = comment;
            comm.Creator = user.UserID;
            comm.Contribution = ContributionID_Guid1;
            comm.CommentDate = now;
            comm.Status = 1;
            db.Comments.Add(comm);
            db.SaveChanges();
            // View
            var listCTP = db.Contributions.Where(ct => ct.Status == 2).ToList();
            List<MultipleModelInOneView_List1> listAll = new List<MultipleModelInOneView_List1>();
            for (int j = 0; j < listCTP.Count(); j++)
            {
                MultipleModelInOneView_List1 ml = new MultipleModelInOneView_List1();
                var ContributionID_Guid = listCTP[j].ContributionID;
                Document Doc = db.Documents.SingleOrDefault(dc => dc.Contribution == ContributionID_Guid);
                Image Img = db.Images.SingleOrDefault(im => im.Contribution == ContributionID_Guid);
                List<Comment> listCmt = db.Comments.Where(im => im.Contribution == ContributionID_Guid).ToList();
                List<User> listUser = new List<User>();
                for (int i = 0; i < listCmt.Count; i++)
                {
                    var user_t = listCmt[i].Creator;
                    User user1 = db.Users.SingleOrDefault(us => us.UserID == user_t);
                    listUser.Add(user1);
                }
                ml.users = listUser;
                ml.document = Doc;
                ml.image2 = Img;
                ml.comments = listCmt;
                listAll.Add(ml);
            }
            return RedirectToAction("Index", listAll);
        }

        //public ActionResult View_Contribution(String ContributionID)
        //{
        //    var ContributionID_Guid = Guid.Parse(ContributionID);
        //    MultipleModelInOneView_List ml = new MultipleModelInOneView_List();
        //    List<Document> listDoc = db.Documents.Where(dc => dc.Contribution == ContributionID_Guid).ToList();
        //    List<Image> listImg = db.Images.Where(im => im.Contribution == ContributionID_Guid).ToList();
        //    List<Comment> listCmt = db.Comments.Where(im => im.Contribution == ContributionID_Guid).ToList();
        //    List<User> listUser = new List<User>();
        //    for (int i = 0; i < listCmt.Count; i++)
        //    {
        //        var user_t = listCmt[i].Creator;
        //        User user1 = db.Users.SingleOrDefault(us => us.UserID == user_t);
        //        listUser.Add(user1);
        //    }
        //    ml.users = listUser;
        //    ml.documents = listDoc;
        //    ml.images = listImg;
        //    ml.comments = listCmt;
        //    ViewBag.ContributionID = ContributionID;
        //    return View(ml);
        //}

        public ActionResult Login()
        {
            return View("Login");
        }

        public ActionResult LoginRole(string email, string pass)
        {
            User user = db.Users.SingleOrDefault(u => u.Email.Equals(email) && u.Password.Equals(pass));
            if (user != null && user.Status == 1)
            {
                Session.Add("User", user);
                return RedirectToAction("Index", Session);
            } else {
                ViewBag.Alert = "Email or Password invalid.";
                return View("Login");
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.RemoveAll();
            return RedirectToAction("Login", "Home");
        }

        //public ActionResult FlotCharts()
        //{
        //    return View("FlotCharts");
        //}

        //public ActionResult MorrisCharts()
        //{
        //    return View("MorrisCharts");
        //}

        public ActionResult Tables()
        {
            return View("Tables");
        }

        public ActionResult Forms()
        {
            return View("Forms");
        }

        public ActionResult Panels()
        {
            return View("Panels");
        }

        public ActionResult Buttons()
        {
            return View("Buttons");
        }

        public ActionResult Notifications()
        {
            return View("Notifications");
        }

        //public ActionResult Typography()
        //{
        //    return View("Typography");
        //}

        public ActionResult Icons()
        {
            return View("Icons");
        }

        public ActionResult Grid()
        {
            return View("Grid");
        }

        //public ActionResult Blank()
        //{
        //    return View("Blank");
        //}
    }
}