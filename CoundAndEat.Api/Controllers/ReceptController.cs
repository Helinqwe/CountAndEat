using CoundAndEat.Api.Extension;
using CoundAndEat.Api.Interfaces;
using CoundAndEat.Dtos.Auth;
using CoundAndEat.Dtos.Recept;
using CoundAndEat.Dtos.Recept.Ingridient;
using CoundAndEat.Dtos.Recept.Description;
using CoundAndEat.Dtos.Recept.Image;
using Microsoft.AspNetCore.Mvc;

namespace CoundAndEat.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReceptController : ControllerBase
    {
        private readonly IReceptRepository _repository;
        public ReceptController(IReceptRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("getRecepts")]
        public async Task<ActionResult<List<ReceptDto>>> GetRecepts()
        {
            try
            {
                var recepts = await _repository.GetRecepts();
                if (recepts == null)
                {
                    return NotFound();
                }
                else
                {
                    var receptsDto = recepts.ConvertToDto();
                    return Ok(receptsDto);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ошибка получения данных из базы данных");
            }
        }

        [HttpGet]
        [Route("getRecept/{id:long}")]
        public async Task<ActionResult<ReceptFullDto>> GetRecept(long id)
        {
            try
            {
                var recept = await _repository.GetRecept(id);
                if (recept == null)
                {
                    return BadRequest();
                }
                else
                {
                    var receptDto = recept.ConvertToDto();

                    return Ok(receptDto);
                }

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ошибка получения данных из базы данных");

            }
        }

        [HttpPost]
        [Route("addNewRecept")]
        public async Task<IActionResult> AddNewRecept([FromBody] CreateReceptDto createDto)
        {
            try
            {

                var newRecept = await _repository.AddNewRecept(createDto);

                if (newRecept == null)
                {
                    return NoContent();
                }
                return Ok(createDto);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ошибка создания");
            }
        }

        [HttpPut]
        [Route("updateRecept/{id:long}")]
        public async Task<IActionResult> UpdateRecept(long id, UpdateReceptDto updateDto)
        {
            try
            {
                var recept = await _repository.UpdateRecept(id, updateDto);
                if (recept == null)
                {
                    return NotFound();
                }

                var responce = await _repository.GetRecept(recept.Id);
                var result = responce.ConvertToDto();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete]
        [Route("deleteRecept/{id:long}")]
        public async Task<ActionResult<Status>> DeleteProduct(long id)
        {
            try
            {
                var recept = await _repository.DeleteRecept(id);

                return Ok(recept);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        //////////  ReceptDescription //////////

        [HttpGet]
        [Route("{prodId:long}/getDescriptions")]
        public async Task<ActionResult<List<DescriptionDto>>> GetDescriptionsByRecept(long recId)
        {
            try
            {
                var descriptions = await _repository.GetDescriptionsByRecept(recId);
                var descriptionsDto = descriptions.ConvertToDto();
                return Ok(descriptionsDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ошибка получения данных из базы данных");
            }
        }

        [HttpPost]
        [Route("descriptions/addNewDescription")]
        public async Task<IActionResult> AddNewDescription([FromBody] DescriptionDto descriptionDto)
        {
            try
            {
                var newDescription = await _repository.AddNewDescription(descriptionDto);
                if (newDescription == null)
                {
                    return NoContent();
                }
                return Ok(newDescription);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ошибка создания");
            }
        }

        [HttpPut]
        [Route("descriptions/updateDescription/{descId:long}")]
        public async Task<IActionResult> UpdateDescription(long descId, UpdateDescriptionDto updateDescDto)
        {
            try
            {
                var updateDescription = await _repository.UpdateDescription(descId, updateDescDto);
                if (updateDescDto == null)
                {
                    return NoContent();
                }
                var response = await _repository.GetDescription(descId);
                return Ok(response.ConvertToDto());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete]
        [Route("description/deleteDescription/{id:long}")]
        public async Task<ActionResult<Status>> DeleteDescription(long id)
        {
            try
            {
                var description = await _repository.DeleteDescription(id);
                return Ok(description);
            }
            catch (Exception ex)
            {
                return new Status { Message = ex.Message, StatusCode = 500 };
            }
        }

        ////////// ReceptImages //////////////

        [HttpGet]
        [Route("{prodId:long}/getImages")]
        public async Task<ActionResult<List<ImageDto>>> GetImagesByRecept(long recId)
        {
            try
            {
                var images = await _repository.GetImagesByRecept(recId);
                var imagesDto = images.ConvertToDto();
                return Ok(imagesDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ошибка получения данных из базы данных");
            }
        }

        [HttpPost]
        [Route("images/addNewImage")]
        public async Task<IActionResult> AddNewImage([FromBody] ImageDto image)
        {
            try
            {
                var newImage = await _repository.AddNewImage(image);
                if (newImage == null)
                {
                    return NoContent();
                }
                return Ok(newImage);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ошибка создания");
            }
        }

        [HttpPut]
        [Route("images/updateImages/{charId:long}")]
        public async Task<IActionResult> UpdateImage(long imgId, UpdateImageDto updateImageDto)
        {
            try
            {
                var updateImage = await _repository.UpdateImage(imgId, updateImageDto);
                if (updateImage == null)
                {
                    return NoContent();
                }
                var response = await _repository.GetImage(imgId);
                return Ok(response.ConvertToDto());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete]
        [Route("images/deleteImage/{id:long}")]
        public async Task<ActionResult<Status>> DeleteImage(long id)
        {
            try
            {
                var image = await _repository.DeleteImage(id);
                return Ok(image);
            }
            catch (Exception ex)
            {
                return new Status { Message = ex.Message, StatusCode = 500 };
            }
        }

        [HttpPost]
        [Route("getReceptByIngridients")]
        public async Task<ActionResult<List<ReceptDto>>> GetReceptByIngridients(FindIngridientDto recDto)
        {
            try
            {
                var recepts = await _repository.GetReceptByIngridients(recDto);
                if(recepts == null)
                {
                    return NoContent();
                }
                var receptDtos = recepts.ConvertToDto();
                return receptDtos;

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ошибка получения данных из базы данных");

            }
        }

        [HttpPost]
        [Route("getReceptByColories")]
        public async Task<ActionResult<List<ReceptDto>>> GetReceptByColories(FindColoriesDto coloriesDto)
        {
            try
            {
                var findRecept = await _repository.GetReceptsByColories(coloriesDto);
                var findReceptDto = findRecept.ConvertToDto();
                return Ok(findReceptDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ошибка получения данных из базы данных");
            }
        }
    }
}
