using ICSharpCode.SharpZipLib.Zip;
using OfficeOpenXml;
using sb_admin_2.Web.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
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

        //View All faculty
        public ActionResult View_All_Faculty()
        {
            var abc = from f in db.Faculties
                      join fd in db.Faculty_Detail
                      on f.FacultyID equals fd.FacultyID
                      where (f.FacultyID == fd.FacultyID)
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
                             && f1.FacultyID == fd1.FacultyID
                             && fd1.Faculty_DetailID == facdId1)
                             select ctb).Count();
                countDocument.Add(count);
            }
            ViewBag.countDocument = countDocument;
            return View(listFacd);
        }

        public ActionResult View_Detail_Faculty(String Faculty_DetailID)
        {
            var facdId = Guid.Parse(Faculty_DetailID);
            var listCTB = from ct in db.Categories
                          join ctb in db.Contributions
                          on ct.CategoryID equals ctb.Category
                          join fd1 in db.Faculty_Detail
                          on ct.Faculty_DetailID equals fd1.Faculty_DetailID
                          join f1 in db.Faculties
                          on fd1.FacultyID equals f1.FacultyID
                          where (ct.CategoryID == ctb.Category && ct.Faculty_DetailID == fd1.Faculty_DetailID
                          && f1.FacultyID == fd1.FacultyID
                          && fd1.Faculty_DetailID == facdId)
                          select ctb;
            return View(listCTB.ToList());
        }

        public ActionResult View_Detail_Document(String ContributionID)
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
        // Download Zip file
        public ActionResult Download_ZipFiles(String ContributionID)
        {
            List<String> allFilePath = new List<string>();
            var ContributionID_Guid = Guid.Parse(ContributionID);
            Contribution contribution = db.Contributions.SingleOrDefault(ctb => ctb.ContributionID == ContributionID_Guid);
            List<Document> list_Document = db.Documents.Where(dc => dc.Contribution == ContributionID_Guid).ToList();
            for(int i = 0; i < list_Document.Count; i++)
            {
                allFilePath.Add(Path.Combine(Server.MapPath("~/Files/"), list_Document[i].Path.Replace("~/Files/", "")));
            }
            List<Image> list_Image = db.Images.Where(im => im.Contribution == ContributionID_Guid).ToList();
            for (int j = 0; j < list_Image.Count; j++)
            {
                allFilePath.Add(Path.Combine(Server.MapPath("~/Images/"), list_Image[j].Path.Replace("~/Images/", "")));
            }
            string zipFileName = contribution.Category1.CategoryName + ".zip";
            Response.ContentType = "application/zip";
            Response.AddHeader("content-disposition", "filename=" + zipFileName);
            byte[] buffer = new byte[4096];
            ZipOutputStream zipOutputStream = new ZipOutputStream(Response.OutputStream);
            zipOutputStream.SetLevel(3);
            try
            {
                for (int k = 0; k < allFilePath.Count; k++)
                {
                    Stream fs = System.IO.File.OpenRead(allFilePath[k]);
                    ZipEntry zipEntry = new ZipEntry(ZipEntry.CleanName(Path.GetFileName(allFilePath[k])));
                    zipEntry.Size = fs.Length;
                    zipOutputStream.PutNextEntry(zipEntry);
                    int count = fs.Read(buffer, 0, buffer.Length);
                    while(count > 0)
                    {
                        zipOutputStream.Write(buffer, 0, count);
                        count = fs.Read(buffer, 0, buffer.Length);
                        if (!Response.IsClientConnected)
                        {
                            break;
                        }
                        Response.Flush();
                    }
                    fs.Close();
                }
                zipOutputStream.Close();
                Response.Flush();
                Response.End();
            }
            catch (Exception)
            {

            }
            return View();
        }

        //Account
        public ActionResult Create_Guest_Account()
        {
            List<Role> listRole = db.Roles.ToList();
            ViewBag.listRole = new SelectList(listRole, "RoleID", "RoleName");
            return View();
        }

        private string encryption(String password)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] encrypt;
            UTF8Encoding encode = new UTF8Encoding();
            //encrypt the given password string into Encrypted data  
            encrypt = md5.ComputeHash(encode.GetBytes(password));
            StringBuilder encryptdata = new StringBuilder();
            //Create a new string by using the encrypted data  
            for (int i = 0; i < encrypt.Length; i++)
            {
                encryptdata.Append(encrypt[i].ToString());
            }
            return encryptdata.ToString();
        }

        public ActionResult Created_Guest_Account_Detail(String email, String password, String name, String address, String phone, String RoleID)
        {
            var RoleID_Guid = Guid.Parse(RoleID);
            //convert password to MD5
            string password_md5 = encryption(password);
            User checj_user = db.Users.SingleOrDefault(us => us.Email.Equals(email));
            if(checj_user != null)
            {
                ViewBag.Error = "This email is exist already!";
            }
            else
            {
                if (Regex.Match(email, @"^[a-zA-Z0-9_\.]{5,32}@[a-z0-9]{2,}(\.[a-z0-9]{2,6}){1,3}$").Success)
                {
                    if (Regex.Match(phone, @"^[0][0-9]{9}").Success)
                    {
                        User user = new User();
                        user.Email = email;
                        user.Password = password_md5;
                        user.Name = name;
                        user.Address = address;
                        user.Phone = phone;
                        user.Role = RoleID_Guid;
                        user.Status = 1;
                        db.Users.Add(user);
                        db.SaveChanges();
                        ViewBag.Success = "Create Account Successfully!";
                    }
                    else
                    {
                        ViewBag.Error = "Invalid Phone!";
                    }
                }
                else
                {
                    ViewBag.Error = "Invalid Email!";
                }
            }
            List<Role> listRole = db.Roles.ToList();
            ViewBag.listRole = new SelectList(listRole, "RoleID", "RoleName");
            return View("Create_Guest_Account");
        }

        public ActionResult View_Statistic(String fromdate, String todate)
        {
            if(fromdate == null || todate == null)
            {
                ViewBag.fromdate = "";
                ViewBag.todate = "";
                return View(db.Contributions.ToList());
            }
            return null;
        }

        public ActionResult Search_Statistic(String fromdate, String todate)
        {
            DateTime fdc = DateTime.Parse(fromdate).Date;
            DateTime tdc = DateTime.Parse(todate).Date;
            DateTime from_date = DateTime.Parse(fromdate).AddDays(-1);
            DateTime to_date = DateTime.Parse(todate).AddDays(1);
            var list_Contributuion = from ctb in db.Contributions
                                     where ctb.Category1.FinalClosureDate >= from_date &&
                                     ctb.Category1.FinalClosureDate <= to_date
                                     select ctb;
            if (fdc.CompareTo(tdc) >= 0)
            {
                ViewBag.Error = "To date always more from date!";
                
            }
            ViewBag.fromdate = from_date.Date;
            ViewBag.todate = to_date.Date;
            return View("View_Statistic", list_Contributuion.ToList());
        }

        private void Export(List<Contribution> contributions)
        {
            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");

            ws.Cells["A1"].Value = "Communication";
            ws.Cells["B1"].Value = "Com1";

            ws.Cells["A2"].Value = "Report";
            ws.Cells["B2"].Value = "Report1";

            ws.Cells["A3"].Value = "Date";
            ws.Cells["B3"].Value = string.Format("{0:dd MMMM yyyy} at {0:HH: mm tt}", DateTimeOffset.Now);

            ws.Cells["A4"].Value = "Total Contributions";
            ws.Cells["B4"].Value = contributions.Count();

            ws.Cells["A6"].Value = "Username";
            ws.Cells["B6"].Value = "Category";
            ws.Cells["C6"].Value = "Title";
            ws.Cells["D6"].Value = "Final Clousre Date";
            ws.Cells["E6"].Value = "Document";
            ws.Cells["F6"].Value = "Image";

            int rowStart = 7;
            foreach (var item in contributions)
            {
                ws.Row(rowStart).Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.LightGray;
                ws.Cells[string.Format("A{0}", rowStart)].Value = item.User.Name;
                ws.Cells[string.Format("B{0}", rowStart)].Value = item.Category1.CategoryName;
                ws.Cells[string.Format("C{0}", rowStart)].Value = item.Title;
                ws.Cells[string.Format("D{0}", rowStart)].Value = DateTimeOffset.Parse(item.Category1.FinalClosureDate.ToString()) ;
                ws.Cells[string.Format("E{0}", rowStart)].Value = item.Documents.Count();
                ws.Cells[string.Format("F{0}", rowStart)].Value = item.Images.Count();
                rowStart++;
            }
            ws.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename="+"ExcelReport.xlsx");
            Response.BinaryWrite(pck.GetAsByteArray());
            Response.End();
        }

        public ActionResult ExportToExcel(String fromdate, String todate)
        {
            if(fromdate == null || todate == null)
            {
                List<Contribution> list_Contribution = db.Contributions.ToList();
                Export(list_Contribution);
            }
            if(fromdate != null || todate != null)
            {
                DateTime from_date = DateTime.Parse(fromdate).AddDays(-1);
                DateTime to_date = DateTime.Parse(todate).AddDays(1);
                var list_Contributuion = from ctb in db.Contributions
                                         where ctb.Category1.FinalClosureDate >= from_date &&
                                         ctb.Category1.FinalClosureDate <= to_date
                                         select ctb;
                List<Contribution> contributions = list_Contributuion.ToList();
                Export(contributions);
            }
            return null;
        }
    }
}