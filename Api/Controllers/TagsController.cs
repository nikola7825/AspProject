using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Helpers;
using Application.Commands;
using Application.DTO;
using Application.Exceptions;
using Application.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly IGetTagsCommand _getTags;
        private readonly IGetTagCommand _getTag;
        private readonly IAddTagCommand _addTag;
        private readonly IEditTagCommand _editTag;
        private readonly IDeleteTagCommand _deleteTag;

        public TagsController(IGetTagsCommand getTags,
                                IGetTagCommand getTag,
                                IAddTagCommand addTag, IEditTagCommand editTag, IDeleteTagCommand deleteTag)
        {
            _getTags = getTags;
            _getTag = getTag;
            _addTag = addTag;
            _editTag = editTag;
            _deleteTag = deleteTag;
        }

        /// <summary>
        /// Returns all tags
        /// </summary>

        // GET: api/Tags
        [HttpGet]
        public IActionResult Get([FromQuery] TagQuery query)
        {
            try
            {
                var tags = _getTags.Execute(query);
                return Ok(tags);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Returns one specific tag 
        /// </summary>

        // GET: api/Tags/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_getTag.Execute(id));
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Insert new tag
        /// </summary>

        // POST: api/Tags
        [LoggedIn]
        [HttpPost]
        public IActionResult Post([FromBody] TagDto dto)
        {
            try
            {
                _addTag.Execute(dto);
                return StatusCode(200);
            }
            catch (EntityAlreadyExistsException)
            {
                return StatusCode(422);
            }
        }

        /// <summary>
        /// Edit tag
        /// </summary>

        // PUT: api/Tags/5
        [LoggedIn]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] TagDto dto)
        {
            try
            {
                dto.Id = id;
                _editTag.Execute(dto);
                return StatusCode(422);
            }
            catch(NotFoundException)
            {
                return NotFound();
            }
            catch (EntityAlreadyExistsException)
            {
                return StatusCode(422);
            }

        }

        /// <summary>
        /// Delete tag
        /// </summary>

        // DELETE: api/ApiWithActions/5
        [LoggedIn]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _deleteTag.Execute(id);
                return StatusCode(204);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }
    }
}
