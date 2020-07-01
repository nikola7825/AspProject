using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Application.DTO
{
    public class CategoryDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [MinLength(3, ErrorMessage = "The category name must have at least 3 characters")]
        public string Name { get; set; }
    }
}
