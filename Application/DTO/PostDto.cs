using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.DTO
{
    public class PostDto
    {   
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [MinLength(3, ErrorMessage = "Post title must have at least 3 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Summary is required")]
        public string Summary { get; set; }

        [Required(ErrorMessage = "Text is required")]
        public string Text { get; set; }

       // public string FirstName { get; set; }

       // public string LastName { get; set; }

       // public string Category { get; set; }
        
        public int ImageId { get; set; }

        [Required(ErrorMessage = "Image is required")]
        public IFormFile Image { get; set; }

        [RegularExpression("^[0-9]*$", ErrorMessage = "Category is required")]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "User is required")]
        [Display(Name = "User")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Tags are required")]
        [Display(Name = "Tags")]
        public List<int> AddTagsInPost { get; set; } 
    }
}
