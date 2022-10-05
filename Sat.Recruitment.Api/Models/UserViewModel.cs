﻿using System.ComponentModel.DataAnnotations;

namespace Sat.Recruitment.Api.Models
{
    public class UserViewModel
    {
        [Required(ErrorMessage = "The name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The email is required")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The address is required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "The phone is required")]
        public string Phone { get; set; }
        
        public string UserType { get; set; }
        
        public decimal Money { get; set; }
    }
}