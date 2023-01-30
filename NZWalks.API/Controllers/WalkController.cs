using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Repositories.Abstract;
using domain = NZWalks.API.Models.Domain;
using dto = NZWalks.API.Models.DTO;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class WalkController : Controller
    {
        private readonly IWalksRepository walksRepository;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes the walksRepository using Dependency Injection, and then creates an instance of IMapper
        /// </summary>
        /// <param name="walksRepository"></param>
        /// <param name="mapper"></param>
        public WalkController(IWalksRepository walksRepository, IMapper mapper)
        {
            this.walksRepository = walksRepository;
            this.mapper = mapper;
        }

        /// <summary>
        /// If found, this method returns a list of all the Walks that are available in the database.
        /// Returns NoContent() otherwise
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<dto.Walk>>> GetAllWalksAsync()
        {
            var walks = await walksRepository.GetAllWalkAsync();
            if (walks == null || walks.Count() == 0)
                return NoContent();
            var walksDTO = mapper.Map<List<dto.Walk>>(walks);
            return Ok(walksDTO);///////////////////////////////////////////////////////////////////// something to do here, what tutorial
        }

        /// <summary>
        /// If found, returns the walk data and a 200 status code.
        /// Else, it returns a NotFound 404 status code
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkAsync")]
        public async Task<ActionResult<dto.Walk>> GetWalkAsync([FromRoute] Guid id)
        {
            var walk = await walksRepository.GetWalkAsync(id);
            if (walk == null) return NotFound();
            var walkDTO = mapper.Map<dto.Walk>(walk);
            return Ok(walkDTO);
        }

        /// <summary>
        /// Takes a new addWalkRequest from the request body, adds it to the database, and returns a CreatedAtAction response status.
        /// Returns a 404 Not found response otherwise.
        /// </summary>
        /// <param name="addWalkRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<dto.Walk>> AddWalkAsync([FromBody] dto.AddWalkRequest addWalkRequest)
        {
            var walk = new domain.Walk
            {
                Name = addWalkRequest.Name,
                Length = addWalkRequest.Length,
                RegionId = addWalkRequest.RegionId,
                WalkDifficultyId = addWalkRequest.WalkDifficultyId
            };
            walk = await walksRepository.AddWalkAsync(walk);
            if(walk == null) return NotFound();
            var walkDTO = mapper.Map<dto.Walk> (walk);
            return CreatedAtAction(nameof(GetWalkAsync), new {id = walkDTO.Id}, walkDTO);
        }

        /// <summary>
        /// Finds the data corresponding to the provided id and updates it using the updateWalkResponse data found in the request body and returns a 200 status.
        /// If an error occurs, then a 404 status code will be returned.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateWalkRequest"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<ActionResult<dto.Walk>> UpdateWalkAsync([FromRoute] Guid id, [FromBody] dto.UpdateWalkRequest updateWalkRequest)
        {
            var walk = new domain.Walk
            {
                Name = updateWalkRequest.Name,
                Length = updateWalkRequest.Length,
                RegionId = updateWalkRequest.RegionId,
                WalkDifficultyId = updateWalkRequest.WalkDifficultyId
            };
            walk = await walksRepository.UpdateWalkAsync(id, walk);
            if(walk == null) return NotFound();
            var walkDTO = mapper.Map<dto.Walk>(walk);
            return Ok(walkDTO);
        }

        /// <summary>
        /// Finds the walk data corresponding to the specified id, removes it from the database and returns it to the user with a 200 status code.
        /// If the data is not found, then a 404 not found status is returned
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<ActionResult<dto.Walk>> DeleteWalkAsync([FromRoute] Guid id)
        {
            var walk = await walksRepository.DeleteWalkAsync(id);
            if(walk == null) return NotFound();
            var walkDTO = mapper.Map<dto.Walk>(walk);
            return Ok(walkDTO);
        }
    }
}
