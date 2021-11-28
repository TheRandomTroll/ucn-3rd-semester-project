﻿using System;
using System.ComponentModel.DataAnnotations;
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
    [Authorize(AuthenticationSchemes = "Bearer")]
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
        /// <returns>A code 2XX on success, and 4XX or 5XX on errors.</returns>
        /// <response code="201">Returns the Id of the newly created report.</response>
        /// <response code="400">Returned when there is a problem with the input fields (fields missing).</response>
        /// <response code="500">Returned when there is a problem with persisting the entry in the database.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateReportDto createReportDto)
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

            return result == default ? StatusCode(500, "Could not create report.") : Created("Create", new { Id = result.Id });
        }


        /// <summary>
        /// Updates a report, based on its id and the new information
        /// </summary>
        /// <param name="updateReportDto">200</param>
        /// <returns>A code 2XX on success, and 4XX on errors.</returns>
        /// <response code="200">Returns the newly created report.</response>
        /// <response code="400">Returned when there is a problem with the input fields (fields missing).</response>
        /// <response code="404">Returned when there is no report with the provided ID.</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateReportDto updateReportDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var report = mapper.Map<UpdateReportDto, Report>(updateReportDto);

            var result = await this.reportRepository.UpdateAsync(report);

            if (result == default)
            {
                return NotFound($"There is no entry with id {updateReportDto.Id}");
            }

            return Ok(result);
        }

        /// <summary>
        /// Deletes a report, based on a GUID.
        /// </summary>
        /// <param name="guid">The GUID of a report which will be deleted</param>
        /// <returns>A code 2XX on success, and 4XX on errors.</returns>
        /// <response code="204">Returned upon successful deletion.</response>
        /// <response code="400">Returned when there is a problem with the input fields (GUID missing).</response>
        /// <response code="404">Returned when there is no report with the provided ID.</response>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync([Required] Guid guid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await this.reportRepository.DeleteAsync(guid);

            return result != default ? NoContent() : NotFound($"Could not find a report with id: {guid}");
        }
    }
}