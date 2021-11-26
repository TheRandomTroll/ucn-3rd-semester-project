using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StreetPatch.Data.Entities;
using StreetPatch.Data.Entities.DTO;
using StreetPatch.Data.Entities.Enums;
using StreetPatch.Data.Repositories;

namespace StreetPatch.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly ReportRepository reportRepository;
        private readonly UsersRepository usersRepository;
        private readonly IMapper mapper;

        public ReportsController(ReportRepository reportRepository, UsersRepository usersRepository, IMapper mapper)
        {
            this.reportRepository = reportRepository;
            this.usersRepository = usersRepository;
            this.mapper = mapper;
        }

        /// <summary>
        /// Creates a report, based on a couple of input parameters
        /// </summary>
        /// <param name="createReportDto">The input</param>
        /// <returns>On success</returns>
        /// <response code="201">Returns the newly created report.</response>
        /// <response code="400">Returned when there is a problem with the input fields (fields missing).</response>
        /// <response code="500">Returned when there is a problem with persisting the entry in the database.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateAsync(CreateReportDto createReportDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var report = mapper.Map<CreateReportDto, Report>(createReportDto);
            report.Status = ReportStatus.New;

            var userEmail = this.usersRepository.GetUsernameFromToken(User.Claims);
            report.Creator = await this.usersRepository.GetByUsernameAsync(userEmail);

            var result = await this.reportRepository.AddAsync(report);

            return result == default ? StatusCode(500, "Could not create report.") : Created("Create", report);
        }
    }
}
