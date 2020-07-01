using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.DTO
{
    public class GetPostDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Summary is required")]
        public string Summary { get; set; }

        [Required(ErrorMessage = "Text is required")]
        public string Text { get; set; }

        [Display(Name="User")]
        [Required(ErrorMessage ="User is required")]
        public int UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Display(Name = "Category")]
        [Required(ErrorMessage ="Category is required")]
        public int CategoryId { get; set; }

        public string Category { get; set; }

        [Display(Name = "Image")]
        public int ImageId { get; set; }

        public string Image { get; set; }

        [Display(Name = "Image")]
        public IFormFile ImageFile { get; set; }

        [Display(Name = "Tags")]
        public IEnumerable<ShowTagInPosts> ShowTagInPosts { get; set; }

        [Required(ErrorMessage = "Tags are required")]
        public List<int> TagsInPost { get; set; }

    }
}
