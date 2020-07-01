using Application.Commands;
using Application.DTO;
using Application.Exceptions;
using Application.Helpers;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace EfCommands
{
    public class EfAddPostCommand : EfBaseCommand, IAddPostCommand
    {
        public EfAddPostCommand(EfContext context) : base(context)
        {
        }

        public void Execute(PostDto request)
        {
            if (Context.Posts.Any(p => p.Title == request.Title))
                throw new EntityAlreadyExistsException();

            if (request.Title == null)
                throw new Exception();

            var ext = Path.GetExtension(request.Image.FileName);
            if (!FileUpload.AllowedExtensions.Contains(ext))
            {
                throw new Exception("File extension is not ok");
            }

            var newFileName = Guid.NewGuid().ToString() + "_" + request.Image.FileName;
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", newFileName);
            request.Image.CopyTo(new FileStream(filePath, FileMode.Create));

            var image = new Domain.Image
            {
                Alt = request.Title,
                Path = newFileName
            };

            Context.Images.Add(image);

            var post = new Domain.Post
            {
                Title = request.Title,
                Summary = request.Summary,
                Text = request.Text,
                CategoryId = request.CategoryId,
                UserId = request.UserId,
                Image = image
            };
            Context.Posts.Add(post);


            if (request.AddTagsInPost != null)
            {
                foreach (var tag in request.AddTagsInPost)
                {
                    Context.PostTags.Add(new Domain.PostTag
                    {
                        Post = post,
                        TagId = tag
                    });
                }
            }
           
            Context.SaveChanges();
        }

    }
}
