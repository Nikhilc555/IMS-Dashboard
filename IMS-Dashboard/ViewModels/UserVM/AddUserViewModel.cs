using IMS_Dashboard.Models.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace IMS_Dashboard.ViewModels.UserVM
{
    public class AddUserViewModel
    {

        public int id { get; set; }

        public string username { get; set; }

        public string password { get; set; }

        public string name { get; set; }

        public int? role_id { get; set; }

        public string? role { get; set; }

        public List<SelectListItem>? Roles { get; set; }

    }
}
