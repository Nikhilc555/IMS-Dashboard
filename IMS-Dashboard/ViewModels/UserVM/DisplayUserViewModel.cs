using System.ComponentModel.DataAnnotations;

namespace IMS_Dashboard.ViewModels.UserVM
{
    public class DisplayUserViewModel
    {
        public int id { get; set; }

        [Display(Name = "User Name")]
        public string user_name { get; set; }

        [Display(Name = "Password")]
        public string password { get; set; }

        [Display(Name = "Name")]
        public string name { get; set; }

        [Display(Name = "Role")]
        public string role { get; set; }

        [Display(Name = "Status")]
        public string status { get; set; }
    }
}
