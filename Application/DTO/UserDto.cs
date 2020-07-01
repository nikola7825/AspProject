using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.DTO
{
    public class UserDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="First name is required")]
        [RegularExpression("^[A-Z][a-z]{2,20}$", ErrorMessage = "First name is not in a good format")]
        [Display(Name= "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [RegularExpression("^[A-Z][a-z]{2,20}$", ErrorMessage = "Last name is not in a good format")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Username is required")]
        // [RegularExpression("^[A-z0-9._%+-]+@[A-z0-9.-]+.[A-z]{2,4}$", ErrorMessage = "Username (email) is not in a good format")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Display(Name = "Role Title")]
        public string RoleName { get; set; }

        
        [Display(Name = "Role")]
        [RegularExpression("^[1-9]+$", ErrorMessage = "Role is required")]
        public int RoleId { get; set; }
    }
}
