﻿using ITFestHackathon_API.DTOs;
using ITFestHackathon_API.DTOs.Update;
using ITFestHackathon_API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ITFestHackathon_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    
    public class DiseaseController : ControllerBase 
    {
        private readonly IGetDiseasesRepository _getDiseasesRepository;
        private readonly IAddDiseaseRepository _addDiseaseRepository;
        private readonly IDeleteDiseaseRepository _deleteDiseaseRepository;
        private readonly IUpdateDiseaseRepository _updateDiseaseRepository;
        public DiseaseController(IAddDiseaseRepository addDiseaseRepository,
            IGetDiseasesRepository getDiseasesRepository, 
            IDeleteDiseaseRepository deleteDiseaseRepository,
            IUpdateDiseaseRepository updateDiseaseRepository
            )
        {
            _getDiseasesRepository = getDiseasesRepository;
            _addDiseaseRepository = addDiseaseRepository;
            _deleteDiseaseRepository = deleteDiseaseRepository;
            _updateDiseaseRepository = updateDiseaseRepository;
        }

        [HttpGet]
        [Route("GetDiseases")]
        public async Task<IActionResult> GetDiseases()
        {
            var result = await _getDiseasesRepository.GetDiseasesAsyncRepo();

            if (result == null)
                return BadRequest();

            return Ok(result);
        }

        [HttpPost]
        [Route("AddDisease")]
        public async Task<IActionResult> AddDisease([FromBody] DiseaseDTO diseaseDTO)
        {
            var result = await _addDiseaseRepository.AddDiseaseAsyncRepo(diseaseDTO);

            if (result == 0)
                return BadRequest();

            return Ok(result);
        }

        [HttpDelete]
        [Route("DeleteDisease")]
        public async Task<IActionResult> DeleteDiseaseAsync([FromBody] int DiseaseId)
        {
            var success = await _deleteDiseaseRepository.DeleteDiseaseAsyncRepo(DiseaseId);
            if (success == 1)
            {
                return Ok(success);
            }
            else
            {
                return BadRequest("Delete failed");
            }
        }
        [HttpPatch]
        [Route("UpdateDisease")]
        public async Task<IActionResult> UpdateDiseaseAsync([FromBody] UpdateDiseaseDTO updatediseaseDTO)
        {
            var success = await _updateDiseaseRepository.UpdateDiseaseAsyncRepo(updatediseaseDTO);
            if (success == 1)
            {
                return Ok(success);
            }
            else
            {
                return BadRequest("Update failed");
            }
        }
    }
}
