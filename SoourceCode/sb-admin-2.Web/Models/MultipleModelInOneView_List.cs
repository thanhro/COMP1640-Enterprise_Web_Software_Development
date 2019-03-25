using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sb_admin_2.Web.Models
{
    public partial class MultipleModelInOneView_List
    {
        public List<Document> documents { get; set; }
        public List<Image> images { get; set; }
        public List<Comment> comments { get; set; }
        public List<User> users { get; set; }
    }
}