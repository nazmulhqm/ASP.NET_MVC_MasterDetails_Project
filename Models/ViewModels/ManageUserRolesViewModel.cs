using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCProject_Nazmul.Models.ViewModels
{
    public class ManageUserRolesViewModel
    {
        [Required]
        public string UserId { get; set; }
        public string UserName { get; set; }
        public IList<ManageRoleViewModel> ManageRolesViewModel { get; set; }
    }
    public class ManageRoleViewModel
    {
        public string Name { get; set; }
        public bool Checked { get; set; }
    }
}