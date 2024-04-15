using CoundAndEat.Api.Extension;
using CoundAndEat.Api.Interfaces;
using CoundAndEat.Dtos.Follow;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoundAndEat.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FollowController : ControllerBase
    {
        private readonly IFollowRepository _followRepository;
        private readonly IReceptRepository _receptRepository;
        public FollowController(IFollowRepository followRepository, IReceptRepository receptRepository)
        {
            _followRepository = followRepository;
            _receptRepository = receptRepository;
        }

        [HttpGet]
        [Route("{userId}/GetFollowItems")]
        public async Task<ActionResult<List<FollowDto>>> GetFollowItems(long userId)
        {
            try
            {
                var followItems = await _followRepository.GetFollowItems(userId);

                if (followItems == null)
                {
                    return NoContent();
                }

                var recepts = await _receptRepository.GetRecepts();

                if (recepts == null)
                {
                    return NotFound();
                }

                var followItemsDto = followItems.ConvertToDto(recepts);

                return Ok(followItemsDto);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
        }

        [HttpGet("getFollowItem/{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<FollowDto>> GetFollowItem(long id)
        {
            try
            {
                var followItem = await _followRepository.GetFollowItem(id);
                if (followItem == null)
                {
                    return NotFound();
                }
                var recept = await _receptRepository.GetRecept(followItem.ReceptId);

                if (recept == null)
                {
                    return NotFound();
                }
                var followItemDto = followItem.ConvertToDto(recept);

                return Ok(followItemDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Authorize]
        [Route("addFollowItem")]
        public async Task<ActionResult<FollowDto>> PostFollowItem([FromBody] FollowToAddDto followItemToAddDto)
        {
            try
            {
                var newFollowItem = await _followRepository.AddFollowItem(followItemToAddDto);

                if (newFollowItem == null)
                {
                    return NotFound();
                }

                var recept = await _receptRepository.GetRecept(newFollowItem.ReceptId);

                if (recept == null)
                {
                    throw new Exception($"Something went wrong when attempting to retrieve product (productId:({followItemToAddDto.ReceptId})");
                }

                var newFollowItemDto = newFollowItem.ConvertToDto(recept);

                return CreatedAtAction(nameof(GetFollowItem), new { id = newFollowItem.Id }, newFollowItemDto);


            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        [Authorize]
        public async Task<ActionResult<FollowDto>> DeleteFollowItem(long id)
        {
            try
            {
                var followItem = await _followRepository.DeleteFollowItem(id);

                if (followItem == null)
                {
                    return NotFound();
                }

                var recept = await _receptRepository.GetRecept(followItem.ReceptId);

                if (recept == null)
                    return NotFound();

                var followItemDto = followItem.ConvertToDto(recept);

                return Ok(followItemDto);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
