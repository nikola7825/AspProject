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
    public class EfEditPostCommand : EfBaseCommand, IEditPostCommand
    {
        public EfEditPostCommand(EfContext context) : base(context)
        {
        }

        public void Execute(GetPostDto request)
        {
            var post = Context.Posts.Find(request.Id);

            if (post == null)
                throw new NotFoundException();

            if (request.Title != post.Title && Context.Posts.Any(p => p.Title == request.Title))
                throw new EntityAlreadyExistsException();

            post.Title = request.Title;
            post.Summary = request.Summary;
            post.Text = request.Text;
            post.UserId = request.UserId;
            post.CategoryId = request.CategoryId;

            foreach (var p in Context.PostTags.Where(p => p.PostId == request.Id))
            {
                Context.PostTags.Remove(p);
            }

            foreach (var tag in request.TagsInPost)
            {
                Context.PostTags.Add(new Domain.PostTag
                {
                    PostId = post.Id,
                    TagId = tag
                });
            }

            if (request.ImageFile != null)
            {
                var ext = Path.GetExtension(request.ImageFile.FileName);
                if (!FileUpload.AllowedExtensions.Contains(ext))
                {
                    throw new Exception("File extension is not ok");
                }

                var newFileName = Guid.NewGuid().ToString() + "_" + request.ImageFile.FileName;
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", newFileName);
                request.ImageFile.CopyTo(new FileStream(filePath, FileMode.Create));

                var image = new Domain.Image
                {
                    Alt = request.Title,
                    Path = newFileName
                };

                Context.Images.Add(image);

                post.Image = image;

                Context.SaveChanges();
            }
            else
            {
                Context.SaveChanges();
            }

        }
    }
}
