using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StreetPatch.Data.Entities;
using StreetPatch.Data.Entities.DTO;
using StreetPatch.Data.Repositories;

namespace StreetPatch.API.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly UsersRepository usersRepository;
        private readonly CommentRepository commentRepository;
        private readonly ReportRepository reportRepository;
        private readonly IMapper mapper;

        public CommentsController(UsersRepository usersRepository, CommentRepository commentRepository, ReportRepository reportRepository, IMapper mapper)
        {
            this.usersRepository = usersRepository;
            this.commentRepository = commentRepository;
            this.reportRepository = reportRepository;
            this.mapper = mapper;
        }

        /// <summary>
        /// Creates a comment for a post, based on a given input.
        /// </summary>
        /// <param name="createCommentDto">The input</param>
        /// <returns>A code 2XX on success along with the ID of the comment, and 4XX or 5XX on errors.</returns>
        /// <response code="201">Returned when there is no problems with the flow.</response>
        /// <response code="400">Returned when there is a problem with the input fields (fields missing).</response>
        /// <response code="401">Returned when no JWT authentication token is provided.</response>
        /// <response code="404">Returned if the provided report ID is invalid..</response>
        /// <response code="500">Returned when there is a problem with persisting the entry in the database.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateAsync(CreateCommentDto createCommentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var report = await this.reportRepository.GetAsync(Guid.Parse(createCommentDto.ReportId));

            if (report == default)
            {
                return NotFound($"Could not find report with id: {createCommentDto.ReportId}.");
            }

            var comment = mapper.Map<CreateCommentDto, Comment>(createCommentDto);

            comment.Report = report;

            var userEmail = this.usersRepository.GetUsernameFromToken(User.Claims);
            comment.Author = await this.usersRepository.GetByUsernameAsync(userEmail);


            var result = await this.commentRepository.AddAsync(comment);

            return result == default ? StatusCode(500, "Could not create comment.") : Created("Create", new { Id = result.Id, Content = result.Content });
        }

        /// <summary>
        /// Updates a report, based on its id and the new information
        /// </summary>
        /// <param name="updatecommentDto">200</param>
        /// <returns>A code 2XX on success, and 4XX on errors.</returns>
        /// <response code="200">Returns the newly created report.</response>
        /// <response code="400">Returned when there is a problem with the input fields (fields missing).</response>
        /// <response code="401">Returned when no JWT authentication token is provided.</response>
        /// <response code="404">Returned when there is no report with the provided ID.</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAsync([Required] UpdateCommentDto updateCommentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var comment = mapper.Map<UpdateCommentDto, Comment>(updateCommentDto);

            var result = await this.commentRepository.UpdateAsync(comment);

            if (result == default)
            {
                return NotFound($"There is no entry with id {updateCommentDto.Id}");
            }

            return Ok(result);
        }

        /// <summary>
        /// Deletes or archives a comment, based on a GUID.
        /// </summary>
        /// <param name="guid">The GUID of a comment which will be deleted</param>
        /// <param name="isSoft">Indicates if comment should be deleted or archived. Default behaviour is deletion.</param>
        /// <returns>A code 2XX on success, and 4XX on errors.</returns>
        /// <response code="204">Returned upon successful deletion.</response>
        /// <response code="400">Returned when there is a problem with the input fields (GUID missing).</response>
        /// <response code="401">Returned when no JWT authentication token is provided.</response>
        /// <response code="404">Returned when there is no comment with the provided ID.</response>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync([Required] Guid guid, bool isSoft = false)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await this.commentRepository.DeleteOrArchiveAsync(guid, isSoft);

            return result != default ? NoContent() : NotFound($"Could not find a comment with id: {guid}");
        }
    }
}
