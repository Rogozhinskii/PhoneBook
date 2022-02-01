﻿using System.ComponentModel.DataAnnotations;

namespace PhoneBook.Models
{
    public class ApplicationRoleListViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Role Name")]
        public string RoleName { get; set; }
        public string Description { get; set; }
        public int NumberOfUsers { get; set; }
    }
}
