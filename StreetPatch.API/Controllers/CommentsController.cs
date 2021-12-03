using System;
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
    }
}
