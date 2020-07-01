using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Helpers;
using Application.Commands;
using Application.DTO;
using Application.Exceptions;
using Application.Login;
using Application.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {

        private readonly IGetPostCommand _getPost;
        private readonly IGetPostsCommand _getPosts;
        private readonly IAddPostCommand _addPost;
        private readonly IEditPostCommand _editPost;
        private readonly IDeletePostCommand _deletePost;
        private readonly LoggedUser _user;

        public PostsController(IGetPostCommand getPost,
                               IGetPostsCommand getPosts,
                               IAddPostCommand addPost,
                               IEditPostCommand editPost,
                               IDeletePostCommand deletePost, 
                               LoggedUser user)
        {
            _getPost = getPost;
            _getPosts = getPosts;
            _addPost = addPost;
            _editPost = editPost;
            _deletePost = deletePost;
            _user = user;
        }

        /// <summary>
        /// Returns all posts
        /// </summary>

        // GET: api/Posts
        [HttpGet]
        public ActionResult<IEnumerable<PostDto>> Get([FromQuery]PostQuery query)
        {
            try
            {
                return Ok(_getPosts.Execute(query));
            }
            catch(NotFoundException)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Returns one specific post
        /// </summary>

        // GET: api/Posts/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_getPost.Execute(id));
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }


        /// <summary>
        /// Insert new post
        /// </summary>

        // POST: api/Posts
        //[LoggedIn]
        [HttpPost]
        public IActionResult Post([FromForm] PostDto dto)
        {
            try
            {
                _addPost.Execute(dto);
                return StatusCode(204);
            }
            catch(EntityAlreadyExistsException)
            {
                return StatusCode(422);
            }
            catch(Exception e)
            {
                return StatusCode(500, e);
            }
        }

        /// <summary>
        /// Post edit
        /// </summary>

        // PUT: api/Posts/5
        //[LoggedIn]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] GetPostDto dto)
        {
            try
            {
                dto.Id = id;
                _editPost.Execute(dto);
                return StatusCode(204);
            }
            catch(NotFoundException)
            {
                return NotFound();
            }
            catch(EntityAlreadyExistsException)
            {
                return StatusCode(422);
            }
        }

        /// <summary>
        /// Post delete
        /// </summary>

        // DELETE: api/Posts/5
       // [LoggedIn]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _deletePost.Execute(id);
                return StatusCode(204);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }

        }
    }
}
