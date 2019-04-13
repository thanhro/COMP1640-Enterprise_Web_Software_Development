using sb_admin_2.Web.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace sb_admin_2.Web.Controllers
{
    public class StudentController : Controller
    {
        DBModel db = new DBModel();
        // GET: Student
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UploadFile()
        {
            List<Category> listCat = db.Categories.ToList();
            ViewBag.listCat = new SelectList(listCat, "CategoryID", "CategoryName");
            return View();
        }

        public ActionResult UploadFile_Detail(String title, String description, String CategoryID, HttpPostedFileBase file_document)
        {
            User user = (User)Session["User"];
            //creation date
            DateTime now = DateTime.Now;
            //File
            string fileName = Path.GetFileNameWithoutExtension(file_document.FileName);
            string extension = Path.GetExtension(file_document.FileName);
            fileName = fileName + extension;
            fileName = Path.Combine(Server.MapPath("~/Files/"), fileName);
            //CategoryID
            String CatID = CategoryID.ToUpper();
            var CategoryID_ToUpper = Guid.Parse(CatID);
            //add to contribution
            Contribution contribution = new Contribution();
            contribution.Title = title;
            contribution.Description = description;
            contribution.CreationDate = now;
            contribution.Category = CategoryID_ToUpper;
            contribution.Creator = user.UserID;
            contribution.Status = 1;
            db.Contributions.Add(contribution);
            //add to document
            Document document = new Document();
            document.DocumentName = file_document.FileName.Split('.')[0].ToString();
            document.Path = "~/Files/" + file_document.FileName;
            document.UploadDate = now;
            document.Contribution = contribution.ContributionID;
            file_document.SaveAs(fileName);
            db.Documents.Add(document);
            db.SaveChanges();
            //View and Move
            ViewBag.Success = "Upload File Successfully!";
            // Send Mail
            ////Category category = db.Categories.SingleOrDefault(ct => ct.CategoryID == CategoryID_ToUpper);
            ////String email = category.Faculty_Detail.User.Email;
            ////MailMessage message = new MailMessage(user.Email, email, "Require publish", "A new document was uploaded you should response within 14 days.");
            ////message.IsBodyHtml = true;
            ////SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            ////client.EnableSsl = true;
            ////client.Credentials = new System.Net.NetworkCredential(user.Email, "");
            ////client.Send(message);
            //display category
            List<Category> listCat = db.Categories.ToList();
            ViewBag.listCat = new SelectList(listCat, "CategoryID", "CategoryName");
            return View("UploadFile");
        }

        public ActionResult ViewStudentFiles()
        {
            User user = (User)Session["User"];
            var listFiles = from d in db.Documents
                            where d.Contribution1.Creator == user.UserID
                            select d;
            return View(listFiles.ToList());
        }

        public ActionResult View_Detail(String DocumentID)
        {
            // View
            var DocumentID_ToUpper = Guid.Parse(DocumentID);
            Document doc = db.Documents.SingleOrDefault(d => d.DocumentID == DocumentID_ToUpper);
            List<Category> listCat = db.Categories.ToList();
            ViewBag.listCat = new SelectList(listCat, "CategoryID", "CategoryName");
            MultipleModelInOneView_List1 myModel = new MultipleModelInOneView_List1();
            List<Comment> listComment = db.Comments.Where(cm => cm.Contribution == doc.Contribution1.ContributionID && cm.Status == 1).OrderBy(x => x.CommentDate).ToList();
            List<User> listUser = new List<User>();
            for (int i = 0; i < listComment.Count; i++)
            {
                var user_t = listComment[i].Creator;
                User user1 = db.Users.SingleOrDefault(us => us.UserID == user_t);
                listUser.Add(user1);
            }
            myModel.users = listUser;
            myModel.comments = listComment;
            myModel.document = doc;
            myModel.image2 = doc.Contribution1.Images.SingleOrDefault(i => i.Contribution == doc.Contribution);
            if(myModel.image2 == null)
            {
                myModel.image2 = new Image();
            }
            return View(myModel);
        }

        public ActionResult Add_Comment(String DocumentID, String ContributionID, String comment)
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
            var DocumentID_ToUpper = Guid.Parse(DocumentID);
            Document doc = db.Documents.SingleOrDefault(d => d.DocumentID == DocumentID_ToUpper);
            List<Category> listCat = db.Categories.ToList();
            ViewBag.listCat = new SelectList(listCat, "CategoryID", "CategoryName");
            MultipleModelInOneView_List1 myModel = new MultipleModelInOneView_List1();
            List<Comment> listComment = db.Comments.Where(cm => cm.Contribution == ContributionID_Guid && cm.Status == 1).OrderBy(x => x.CommentDate).ToList();
            List<User> listUser = new List<User>();
            for (int i = 0; i < listComment.Count; i++)
            {
                var user_t = listComment[i].Creator;
                User user1 = db.Users.SingleOrDefault(us => us.UserID == user_t);
                listUser.Add(user1);
            }
            myModel.users = listUser;
            myModel.comments = listComment;
            myModel.document = doc;
            myModel.image2 = doc.Contribution1.Images.SingleOrDefault(i => i.Contribution == doc.Contribution);
            if (myModel.image2 == null)
            {
                myModel.image2 = new Image();
            }
            return View("View_Detail", myModel);
        }

        public ActionResult Download(String DocumentID)
        {
            var DocumentID_ToUpper = Guid.Parse(DocumentID);
            Document document = db.Documents.SingleOrDefault(d => d.DocumentID == DocumentID_ToUpper);
            string filepath = Path.Combine(Server.MapPath("~/Files/"), document.Path.Replace("~/Files/", ""));
            byte[] documentBytes = System.IO.File.ReadAllBytes(filepath);
            Response.AddHeader("Content-type", "application/octet-stream");
            Response.AddHeader("Content-Disposition", "attachment; filename=" + document.Path);
            Response.BinaryWrite(documentBytes);
            Response.Flush();
            Response.End();
            return File(documentBytes, System.Net.Mime.MediaTypeNames.Application.Octet, document.Path);
        }

        public ActionResult Edit_Submission(String DocumentID, String ImageID, String new_title, String new_description, String new_CategoryID, HttpPostedFileBase new_file_document, HttpPostedFileBase file_image)
        {
            User user = (User)Session["User"];
            //creation date
            DateTime now = DateTime.Now;
            //File
            string fileName = Path.GetFileNameWithoutExtension(new_file_document.FileName);
            string extension = Path.GetExtension(new_file_document.FileName);
            fileName = fileName + extension;
            fileName = Path.Combine(Server.MapPath("~/Files/"), fileName);
            //CategoryID
            String CatID = new_CategoryID.ToUpper();
            var CategoryID_ToUpper = Guid.Parse(CatID);
            //DocumentID
            String Doc_Upper = DocumentID.ToUpper();
            var DocumentID_ToUpper = Guid.Parse(Doc_Upper);
            Document doc = db.Documents.SingleOrDefault(d => d.DocumentID == DocumentID_ToUpper);
            //add to contribution
            doc.Contribution1.Title = new_title;
            doc.Contribution1.Description = new_description;
            doc.Contribution1.CreationDate = now;
            doc.Contribution1.Category = CategoryID_ToUpper;
            //add to document
            doc.DocumentName = new_file_document.FileName.Split('.')[0].ToString();
            doc.Path = "~/Files/" + new_file_document.FileName;
            doc.UploadDate = now;
            new_file_document.SaveAs(fileName);
            //add to image
            string fileName_Image = Path.GetFileNameWithoutExtension(file_image.FileName);
            string extension_Image = Path.GetExtension(file_image.FileName);
            fileName_Image = fileName_Image + extension_Image;
            fileName_Image = Path.Combine(Server.MapPath("~/Images/"), fileName_Image);
            // Image
            if(ImageID.Equals("00000000-0000-0000-0000-000000000000"))
            {
                Image image = new Image();
                image.ImageName = file_image.FileName.Split('.')[0].ToString();
                image.Path = "~/Images/" + file_image.FileName;
                image.UploadDate = now;
                image.Description = "It is an image.";
                image.Contribution = doc.Contribution1.ContributionID;
                file_image.SaveAs(fileName_Image);
                db.Images.Add(image);
            }
            else
            {
                var ImageID_ToUpper = Guid.Parse(ImageID);
                Image image1 = db.Images.SingleOrDefault(d => d.ImageID == ImageID_ToUpper);
                //update image
                image1.ImageName = file_image.FileName.Split('.')[0].ToString();
                image1.Path = "~/Images/" + file_image.FileName;
                image1.UploadDate = now;
                file_image.SaveAs(fileName_Image);
            }
            //db save changes
            db.SaveChanges();
            //View and Move
            ViewBag.Success = "Edit Submission Successfully!";
            // View
            List<Category> listCat = db.Categories.ToList();
            ViewBag.listCat = new SelectList(listCat, "CategoryID", "CategoryName");
            MultipleModelInOneView_List1 myModel = new MultipleModelInOneView_List1();
            List<Comment> listComment = db.Comments.Where(cm => cm.Contribution == doc.Contribution1.ContributionID && cm.Status == 1).OrderBy(x => x.CommentDate).ToList();
            List<User> listUser = new List<User>();
            for (int i = 0; i < listComment.Count; i++)
            {
                var user_t = listComment[i].Creator;
                User user1 = db.Users.SingleOrDefault(us => us.UserID == user_t);
                listUser.Add(user1);
            }
            myModel.users = listUser;
            myModel.comments = listComment;
            myModel.document = doc;
            myModel.image2 = doc.Contribution1.Images.SingleOrDefault(i => i.Contribution == doc.Contribution);
            if (myModel.image2 == null)
            {
                myModel.image2 = new Image();
            }
            return View("View_Detail", myModel);
        }

        //public ActionResult Additional_File()
        //{
        //    User user = (User)Session["User"];
        //    var listFiles = from d in db.Documents
        //                    where d.Contribution1.Creator == user.UserID
        //                    select d;
        //    return View(listFiles.ToList());
        //}

        //public ActionResult Additional_File_Detail(String DocumentID)
        //{
        //    String Doc_Upper = DocumentID.ToUpper();
        //    var DocumentID_ToUpper = Guid.Parse(Doc_Upper);
        //    Document doc = db.Documents.SingleOrDefault(d => d.DocumentID == DocumentID_ToUpper);
        //    MultipleModelInOneView mymodel = new MultipleModelInOneView();
        //    mymodel.document = doc;
        //    mymodel.image1 = doc.Contribution1.Images.SingleOrDefault(i => i.Contribution == doc.Contribution);
        //    return View(mymodel);
        //}

        //public ActionResult AddImage(String DocumentID, HttpPostedFileBase file_image)
        //{
        //    User user = (User)Session["User"];
        //    //creation date
        //    DateTime now = DateTime.Now;
        //    //File
        //    string fileName = Path.GetFileNameWithoutExtension(file_image.FileName);
        //    string extension = Path.GetExtension(file_image.FileName);
        //    fileName = fileName + extension;
        //    fileName = Path.Combine(Server.MapPath("~/Images/"), fileName);
        //    // Document
        //    String Doc_Upper = DocumentID.ToUpper();
        //    var DocumentID_ToUpper = Guid.Parse(Doc_Upper);
        //    Document doc = db.Documents.SingleOrDefault(d => d.DocumentID == DocumentID_ToUpper);
        //    Image image = new Image();
        //    image.ImageName = file_image.FileName.Split('.')[0].ToString();
        //    image.Path = "~/Images/" + file_image.FileName;
        //    image.UploadDate = now;
        //    image.Description = "It is an image.";
        //    image.Contribution = doc.Contribution1.ContributionID;
        //    file_image.SaveAs(fileName);
        //    db.Images.Add(image);
        //    db.SaveChanges();
        //    // View
        //    MultipleModelInOneView mymodel = new MultipleModelInOneView();
        //    mymodel.document = doc;
        //    mymodel.image1 = doc.Contribution1.Images.SingleOrDefault(i => i.Contribution == doc.Contribution);
        //    ViewBag.Success = "Additional Image Successfully!";
        //    return View("Additional_File_Detail", mymodel);
        //}
    }
}