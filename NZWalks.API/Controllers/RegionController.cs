using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using domain = NZWalks.API.Models.Domain;
using dto = NZWalks.API.Models.DTO;
using NZWalks.API.Repositories.Abstract;
using System.Formats.Asn1;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class RegionController : Controller
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionController(IRegionRepository regionRepository, IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        /// <summary>
        /// Returs a list of Regions
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<dto.Region>>> GetAllRegionsAsync()
        {
            IEnumerable<domain.Region> regions = await regionRepository.GetAllRegionsAsync();
            if (regions == null || regions.Count() == 0)
                return NoContent();

            var regionsDTO = mapper.Map<List<dto.Region>>(regions);
            return Ok(regionsDTO);
        }

        /// <summary>
        /// Adds a region to the database
        /// </summary>
        /// <param name="addRegionRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<dto.Region>> AddRegionAsync([FromBody] dto.AddRegionRequest addRegionRequest)
        {
            var region = new domain.Region()
            {
                Name = addRegionRequest.Name,
                Code = addRegionRequest.Code,
                Area = addRegionRequest.Area,
                Population = addRegionRequest.Population,
                Lat = addRegionRequest.Lat,
                Long = addRegionRequest.Long
            };
            region = await regionRepository.AddRegionAsync(region);
            if (region == null) return NoContent();
            var regionDTO = mapper.Map<dto.Region>(region);
            return CreatedAtAction(nameof(GetRegionAsync), new { id = region.Id }, regionDTO);
        }

        /// <summary>
        /// Finds a region using the id from route, and returns the region if found, and null otherwise
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetRegionAsync")]
        public async Task<ActionResult<dto.Region>> GetRegionAsync([FromRoute] Guid id)
        {
            var region = await regionRepository.GetRegionAsync(id);
            if (region == null) return NotFound();
            var regionDTO = mapper.Map<dto.Region>(region);
            return Ok(regionDTO);
        }

        /// <summary>
        /// If there is an existing region with the specified Id, then this controller method will update the existing region with the 
        ///     information provided in the request body
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateRegionRequest"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<ActionResult<dto.Region>> UpdateRegionAsync([FromRoute] Guid id, [FromBody] dto.UpdateRegionRequest updateRegionRequest)
        {
            var region = new domain.Region
            {
                Name = updateRegionRequest.Name,
                Code = updateRegionRequest.Code,
                Area = updateRegionRequest.Area,
                Population = updateRegionRequest.Population,
                Lat = updateRegionRequest.Lat,
                Long = updateRegionRequest.Long,
            };
            region = await regionRepository.UpdateRegionAsync(id, region);
            if (region == null) return NotFound();
            var regionDTO = mapper.Map<dto.Region>(region);
            return Ok(regionDTO);
        }

        /// <summary>
        /// If data with the id does exist, then it will be deleted and true will be returned
        ///     If no data is found, then the method will return false.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteRegion([FromRoute] Guid id)
        {
            var region = await regionRepository.DeleteRegionAsync(id);
            if (region == null) return NotFound();
            var regionDTO = mapper.Map<dto.Region>(region);
            return Ok(regionDTO);
        }

        /// <summary>
        /// Deletes all the methods from the database and returns either true/false
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ActionResult<Boolean>> DeleteAllRegions()
        {
            var result = await regionRepository.DeleteAllRegionsAsync();
            return Ok(result);
        }
    }
}
