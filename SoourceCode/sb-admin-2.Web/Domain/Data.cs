using sb_admin_2.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sb_admin_2.Web.Domain
{
    public class Data
    {
        public IEnumerable<Navbar> navbarItems()
        {
            var menu = new List<Navbar>();
            if (HttpContext.Current.Session["User"] != null)
            {
                User user = (User)HttpContext.Current.Session["User"];
                if(user.Role1.RoleName.Equals("Student"))
                {
                    menu.Add(new Navbar { Id = 1, nameOption = "Dashboard", controller = "Home", action = "Index", imageClass = "fa fa-dashboard fa-fw", status = true, isParent = false, parentId = 0 });
                    menu.Add(new Navbar { Id = 2, nameOption = "Documents", imageClass = "fa fa-bar-chart-o fa-fw", status = true, isParent = true, parentId = 0 });
                    menu.Add(new Navbar { Id = 3, nameOption = "Upload File", controller = "Student", action = "UploadFile", status = true, isParent = false, parentId = 2 });
                    menu.Add(new Navbar { Id = 4, nameOption = "View My Files", controller = "Student", action = "ViewStudentFiles", status = true, isParent = false, parentId = 2 });
                    return menu.ToList();
                }
                else if (user.Role1.RoleName.Equals("Marketing Coordinator"))
                {
                    menu.Add(new Navbar { Id = 1, nameOption = "Dashboard", controller = "Home", action = "Index", imageClass = "fa fa-dashboard fa-fw", status = true, isParent = false, parentId = 0 });
                    menu.Add(new Navbar { Id = 2, nameOption = "Add Category", controller = "ManageCoordinator", action = "Add_Category", status = true, isParent = false, parentId = 0 });
                    menu.Add(new Navbar { Id = 3, nameOption = "View My Faclties", controller = "ManageCoordinator", action = "View_Faculty", status = true, isParent = false, parentId = 0 });
                    menu.Add(new Navbar { Id = 4, nameOption = "Update Closure Date", controller = "ManageCoordinator", action = "Update_ClosureDate", status = true, isParent = false, parentId = 0 });
                    return menu.ToList();
                }
                else if (user.Role1.RoleName.Equals("Marketing Manager"))
                {
                    menu.Add(new Navbar { Id = 1, nameOption = "Dashboard", controller = "Home", action = "Index", imageClass = "fa fa-dashboard fa-fw", status = true, isParent = false, parentId = 0 });
                    menu.Add(new Navbar { Id = 2, nameOption = "Manage Faculty", imageClass = "fa fa-bar-chart-o fa-fw", status = true, isParent = true, parentId = 0 });
                    menu.Add(new Navbar { Id = 3, nameOption = "Add Faculty", controller = "ManageMarketing", action = "AddFaculty", status = true, isParent = false, parentId = 2 });
                    menu.Add(new Navbar { Id = 4, nameOption = "Assigned Faculty", controller = "ManageMarketing", action = "Assigned_Faculty", status = true, isParent = false, parentId = 2 });
                    menu.Add(new Navbar { Id = 5, nameOption = "View All Faclties", controller = "ManageMarketing", action = "View_All_Faculty", status = true, isParent = false, parentId = 2 });
                    menu.Add(new Navbar { Id = 6, nameOption = "Create Account", controller = "ManageMarketing", action = "Create_Guest_Account", status = true, isParent = false, parentId = 0 });
                    menu.Add(new Navbar { Id = 7, nameOption = "View Statistic", controller = "ManageMarketing", action = "View_Statistic", status = true, isParent = false, parentId = 0 });
                    return menu.ToList();
                }
                else if (user.Role1.RoleName.Equals("Administrator"))
                {
                    menu.Add(new Navbar { Id = 1, nameOption = "Dashboard", controller = "Home", action = "Index", imageClass = "fa fa-dashboard fa-fw", status = true, isParent = false, parentId = 0 });
                    return menu.ToList();
                }
                else if (user.Role1.RoleName.Equals("Guest Account"))
                {
                    menu.Add(new Navbar { Id = 1, nameOption = "Dashboard", controller = "Home", action = "Index", imageClass = "fa fa-dashboard fa-fw", status = true, isParent = false, parentId = 0 });
                    return menu.ToList();
                }
            }
            menu.Clear();
            return menu;
        }
    }
}