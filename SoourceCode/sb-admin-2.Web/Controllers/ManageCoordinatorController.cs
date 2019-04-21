using sb_admin_2.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace sb_admin_2.Web.Controllers
{
    public class ManageCoordinatorController : Controller
    {
        DBModel db = new DBModel();
        // GET: ManageCoordinator
        public ActionResult Index()
        {
            return View();
        }

        private void displayFacId()
        {
            User user = (User)Session["User"];
            var abc = from f in db.Faculties
                      join fd in db.Faculty_Detail
                      on f.FacultyID equals fd.FacultyID
                      where (f.FacultyID == fd.FacultyID && fd.User.UserID == user.UserID)
                      select f;
            List<Faculty> listFac = abc.ToList();
            ViewBag.listFac = new SelectList(listFac, "FacultyID", "FacultyName");
        }

        public ActionResult Add_Category()
        {
            try
            {
                displayFacId();
                return View();
            }
            catch(Exception e)
            {
                return View("Error");
            }
        }

        public ActionResult Add_Category_Detail(String categoryname, String closuredate, String finalclosuredate, String FacultyID, String description)
        {
            try
            {
                User user = (User)Session["User"];
                String Fac = FacultyID.ToUpper();
                var FacultyID_ToUpper = Guid.Parse(Fac);
                DateTime datenow = DateTime.Now.Date;
                DateTime cld = DateTime.Parse(closuredate).Date;
                DateTime fnd = DateTime.Parse(finalclosuredate).Date;
                DateTime ClosureDate = DateTime.ParseExact(closuredate, "yyyy-MM-ddTHH:mm", null);
                DateTime FinalClosureDate = DateTime.ParseExact(finalclosuredate, "yyyy-MM-ddTHH:mm", null);
                Faculty_Detail faculty_Detail = db.Faculty_Detail.SingleOrDefault(fd => fd.FacultyID == FacultyID_ToUpper && fd.UserCoordinator == user.UserID);
                var cat = from c in db.Categories
                          where (c.Faculty_Detail.FacultyID == FacultyID_ToUpper && c.CategoryName == categoryname)
                          select c;
                if (cat.Count() == 0)
                {
                    if (datenow.CompareTo(cld) <= 0)
                    {
                        if (cld.CompareTo(fnd) < 0)
                        {
                            Category category = new Category();
                            category.CategoryName = categoryname;
                            category.ClosureDate = ClosureDate;
                            category.FinalClosureDate = FinalClosureDate;
                            category.Description = description;
                            category.Status = 1;
                            category.Faculty_DetailID = faculty_Detail.Faculty_DetailID;
                            db.Categories.Add(category);
                            db.SaveChanges();
                            ViewBag.Success = "Add Category Successfully!";
                        }
                        else
                        {
                            ViewBag.Error = "Final closure date always more  closure date!";
                        }
                    }
                    else
                    {
                        ViewBag.Error = "Closure date always more than or equal date now!" + cld + " das " + datenow;
                    }
                }
                else
                {
                    ViewBag.Error = "This Category is exist already!";
                }
                displayFacId();
                return View("Add_Category");
            }
            catch (Exception e)
            {
                return View("Error");
            }
        }

        public ActionResult View_Faculty()
        {
            try
            {
                User user = (User)Session["User"];
                var abc = from f in db.Faculties
                          join fd in db.Faculty_Detail
                          on f.FacultyID equals fd.FacultyID
                          where (f.FacultyID == fd.FacultyID && fd.User.UserID == user.UserID)
                          select fd;
                List<Faculty_Detail> listFacd = abc.ToList();
                List<int> countDocument = new List<int>();
                for (int i = 0; i < listFacd.Count; i++)
                {
                    var facdId1 = Guid.Parse(listFacd[i].Faculty_DetailID.ToString());
                    var count = (from ct in db.Categories
                                 join ctb in db.Contributions
                                 on ct.CategoryID equals ctb.Category
                                 join fd1 in db.Faculty_Detail
                                 on ct.Faculty_DetailID equals fd1.Faculty_DetailID
                                 join f1 in db.Faculties
                                 on fd1.FacultyID equals f1.FacultyID
                                 where (ct.CategoryID == ctb.Category && ct.Faculty_DetailID == fd1.Faculty_DetailID
                                 && f1.FacultyID == fd1.FacultyID && fd1.UserCoordinator == user.UserID
                                 && fd1.Faculty_DetailID == facdId1)
                                 select ctb).Count();
                    countDocument.Add(count);
                }
                ViewBag.countDocument = countDocument;
                return View(listFacd);
            }
            catch(Exception e)
            {
                return View("Error");
            }
        }

        public ActionResult View_Detail_Faculty(String Faculty_DetailID)
        {
            try
            {
                User user = (User)Session["User"];
                var facdId = Guid.Parse(Faculty_DetailID);
                var listCTB = from ct in db.Categories
                              join ctb in db.Contributions
                              on ct.CategoryID equals ctb.Category
                              join fd1 in db.Faculty_Detail
                              on ct.Faculty_DetailID equals fd1.Faculty_DetailID
                              join f1 in db.Faculties
                              on fd1.FacultyID equals f1.FacultyID
                              where (ct.CategoryID == ctb.Category && ct.Faculty_DetailID == fd1.Faculty_DetailID
                              && f1.FacultyID == fd1.FacultyID && fd1.UserCoordinator == user.UserID
                              && fd1.Faculty_DetailID == facdId)
                              select ctb;
                return View(listCTB.ToList());
            }
            catch(Exception e)
            {
                return View("Error");
            }
        }

        public ActionResult View_Detail_Document(String ContributionID)
        {
            try
            {
                var ContributionID_Guid = Guid.Parse(ContributionID);
                MultipleModelInOneView_List ml = new MultipleModelInOneView_List();
                List<Document> listDoc = db.Documents.Where(dc => dc.Contribution == ContributionID_Guid).ToList();
                List<Image> listImg = db.Images.Where(im => im.Contribution == ContributionID_Guid).ToList();
                List<Comment> listCmt = db.Comments.Where(im => im.Contribution == ContributionID_Guid && im.Status == 1).ToList();
                List<User> listUser = new List<User>();
                for (int i = 0; i < listCmt.Count; i++)
                {
                    var user_t = listCmt[i].Creator;
                    User user1 = db.Users.SingleOrDefault(us => us.UserID == user_t);
                    listUser.Add(user1);
                }
                ml.users = listUser;
                ml.documents = listDoc;
                ml.images = listImg;
                ml.comments = listCmt;
                ViewBag.ContributionID = ContributionID;
                return View(ml);
            }
            catch (Exception e)
            {
                return View("Error");
            }
        }

        public ActionResult Add_Comment(String ContributionID, String comment)
        {
            try
            {
                User user = (User)Session["User"];
                var ContributionID_Guid = Guid.Parse(ContributionID);
                DateTime now = DateTime.Now;
                Comment comm = new Comment();
                comm.Content = comment;
                comm.Creator = user.UserID;
                comm.Contribution = ContributionID_Guid;
                comm.CommentDate = now;
                comm.Status = 1;
                db.Comments.Add(comm);
                db.SaveChanges();
                // View
                MultipleModelInOneView_List ml = new MultipleModelInOneView_List();
                List<Document> listDoc = db.Documents.Where(dc => dc.Contribution == ContributionID_Guid).ToList();
                List<Image> listImg = db.Images.Where(im => im.Contribution == ContributionID_Guid).ToList();
                List<Comment> listCmt = db.Comments.Where(im => im.Contribution == ContributionID_Guid && im.Status == 1).ToList();
                List<User> listUser = new List<User>();
                for (int i = 0; i < listCmt.Count; i++)
                {
                    var user_t = listCmt[i].Creator;
                    User user1 = db.Users.SingleOrDefault(us => us.UserID == user_t);
                    listUser.Add(user1);
                }
                ml.users = listUser;
                ml.documents = listDoc;
                ml.images = listImg;
                ml.comments = listCmt;
                ViewBag.ContributionID = ContributionID;
                return View("View_Detail_Document", ml);
            }
            catch(Exception e)
            {
                return View("Error");
            }
        }

        public ActionResult Publish(String Faculty_DetailID, String ContributionID)
        {
            try
            {
                var CtbID = Guid.Parse(ContributionID);
                Contribution contribution = db.Contributions.SingleOrDefault(ctb => ctb.ContributionID == CtbID);
                contribution.Status = 2;
                db.SaveChanges();
                ViewBag.Success = "Publish This Contribution Successfully!";
                User user = (User)Session["User"];
                var facdId = Guid.Parse(Faculty_DetailID);
                var listCTB = from ct in db.Categories
                              join ctb in db.Contributions
                              on ct.CategoryID equals ctb.Category
                              join fd1 in db.Faculty_Detail
                              on ct.Faculty_DetailID equals fd1.Faculty_DetailID
                              join f1 in db.Faculties
                              on fd1.FacultyID equals f1.FacultyID
                              where (ct.CategoryID == ctb.Category && ct.Faculty_DetailID == fd1.Faculty_DetailID
                              && f1.FacultyID == fd1.FacultyID && fd1.UserCoordinator == user.UserID
                              && fd1.Faculty_DetailID == facdId)
                              select ctb;
                return View("View_Detail_Faculty", listCTB.ToList());
            }
            catch(Exception e)
            {
                return View("Error");
            }
        }

        public ActionResult Update_ClosureDate()
        {
            try
            {
                User user = (User)Session["User"];
                var listCt = from ct in db.Categories
                             join fd1 in db.Faculty_Detail
                             on ct.Faculty_DetailID equals fd1.Faculty_DetailID
                             join f1 in db.Faculties
                             on fd1.FacultyID equals f1.FacultyID
                             where (ct.Faculty_DetailID == fd1.Faculty_DetailID
                             && f1.FacultyID == fd1.FacultyID && fd1.UserCoordinator == user.UserID)
                             select ct;
                List<Category> listCat = listCt.ToList();
                ViewBag.ListCat_Check = listCat;
                ViewBag.listCat = new SelectList(listCat, "CategoryID", "CategoryName");
                return View();
            }
            catch(Exception e)
            {
                return View("Error");
            }
        }

        public ActionResult Update_ClosureDate_Real(String new_finalclosuredate, String CategoryID)
        {
            try
            {
                var CategoryID_Guid = Guid.Parse(CategoryID);
                DateTime new_FinalClosureDate = DateTime.ParseExact(new_finalclosuredate, "yyyy-MM-ddTHH:mm", null);
                Category category = db.Categories.SingleOrDefault(cat => cat.CategoryID == CategoryID_Guid);
                category.FinalClosureDate = new_FinalClosureDate;
                db.SaveChanges();
                ViewBag.Success = "Update closure date successfully!";
                List<Category> listCat = db.Categories.ToList();
                ViewBag.ListCat_Check = listCat;
                ViewBag.listCat = new SelectList(listCat, "CategoryID", "CategoryName");
                return View("Update_ClosureDate");
            }
            catch(Exception e)
            {
                return View("Error");
            }
        }
    }
}