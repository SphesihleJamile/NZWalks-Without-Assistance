using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Repositories.Abstract;
using domain = NZWalks.API.Models.Domain;
using dto = NZWalks.API.Models.DTO;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class WalkDifficultyController : Controller
    {
        private readonly IWalkDifficultyRepository walkDifficultyRepository;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes the walkDifficultyRepository object using dependency injection, and intantiates an object of type IMapper
        /// </summary>
        /// <param name="walkDifficultyRepository"></param>
        /// <param name="mapper"></param>
        public WalkDifficultyController(IWalkDifficultyRepository walkDifficultyRepository, IMapper mapper)
        {
            this.walkDifficultyRepository = walkDifficultyRepository;
            this.mapper = mapper;
        }

        /// <summary>
        /// Retuens a list of all WalkDifficulty data from the database
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<dto.WalkDifficulty>>> GetAllWalkDifficultyAsync()
        {
            var walkDifficulty = await walkDifficultyRepository.GetAllWalkDifficultiesAsync();
            if (walkDifficulty == null)
                return NoContent();
            var walkDifficultyDTO = mapper.Map<List<dto.WalkDifficulty>>(walkDifficulty);
            return Ok(walkDifficultyDTO);
        }

        /// <summary>
        /// Searches the database for walkDifficulty data with the specified id and returns it, else returns a NotFound() status
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkDifficultyAsync")]
        public async Task<ActionResult<dto.WalkDifficulty>> GetWalkDifficultyAsync([FromRoute] Guid id)
        {
            var walkDifficulty = await walkDifficultyRepository.GetWalkDifficultyAsync(id);
            if (walkDifficulty == null)
                return NotFound();
            var walkDifficultyDTO = mapper.Map<dto.WalkDifficulty>(walkDifficulty);
            return Ok(walkDifficultyDTO);
        }

        /// <summary>
        /// Takes data from the equest body as the new WalkDifficulty, adds it to the database, and returs a CreatedAtAction response user. Returns a BadRequest status if an error occurs
        /// </summary>
        /// <param name="addWalkDifficultyRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<dto.WalkDifficulty>> AddWalkDifficultyAsync([FromBody] dto.AddWalkDifficultyRequest addWalkDifficultyRequest)
        {
            var walkDifficulty = new domain.WalkDifficulty
            {
                Code = addWalkDifficultyRequest.Code
            };
            walkDifficulty = await walkDifficultyRepository.AddWalkDifficultyAsync(walkDifficulty);
            if (walkDifficulty == null)
                return BadRequest();
            var walkDifficultyDTO = mapper.Map<dto.WalkDifficulty>(walkDifficulty);
            return CreatedAtAction(nameof(GetWalkDifficultyAsync), new { id = walkDifficultyDTO.Id }, walkDifficultyDTO);
        }

        /// <summary>
        /// If the method is able to find the data in the database that corresponds to the provided id, then it will be updated with the new data and a 200 response will be sent to the user.
        /// If an error occurs, then a 404 response status will be sent to the user
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateWalkDifficultyRequst"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<ActionResult<dto.WalkDifficulty>> UpdateWalkDifficultyAsync([FromRoute] Guid id, [FromBody] dto.UpdateWalkDifficultyRequst updateWalkDifficultyRequst)
        {
            var walkDifficulty = new domain.WalkDifficulty
            {
                Code = updateWalkDifficultyRequst.Code
            };
            walkDifficulty = await walkDifficultyRepository.UpdateWalkDifficultyAsync(id, walkDifficulty);
            if (walkDifficulty == null)
                return NotFound();
            var walkDifficultyDTO = mapper.Map<dto.WalkDifficulty>(walkDifficulty);
            return Ok(walkDifficultyDTO);
        }

        /// <summary>
        /// If data with the corresponding id is found in the database, then this method will delete it and return it and a 200 status code to the user.
        /// Else a NotFound status will be returned.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<ActionResult<dto.WalkDifficulty>> DeleteWalkDifficultyAsync([FromRoute] Guid id)
        {
            var walkDifficulty = await walkDifficultyRepository.DeleteWalkDifficultyAsync(id);
            if(walkDifficulty == null) return NotFound();
            var walkDifficultyDTO = mapper.Map<dto.WalkDifficulty>(walkDifficulty);
            return Ok(walkDifficultyDTO);
        }
    }
}
