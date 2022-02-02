using Microsoft.AspNetCore.Mvc.Rendering;
using PhoneBook.Common.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PhoneBook.Models
{
    public class EditUserViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public List<SelectListItem> ApplicationRoles { get; set; }
        [Display(Name = "Role")]
        public string ApplicationRoleId { get; set; }
    }
}
