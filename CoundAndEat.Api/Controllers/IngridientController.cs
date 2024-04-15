using CoundAndEat.Api.Extension;
using CoundAndEat.Api.Interfaces;
using CoundAndEat.Api.Repository;
using CoundAndEat.Dtos.Auth;
using CoundAndEat.Dtos.Category;
using CoundAndEat.Dtos.Ingridient;
using Microsoft.AspNetCore.Mvc;

namespace CoundAndEat.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IngridientController : ControllerBase
    {
        private readonly IIngridientRepository _repository;
        public IngridientController(IIngridientRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("getIngridients")]
        public async Task<ActionResult<List<IngridientDto>>> GetIngridients()
        {
            try
            {
                var ingridients = await _repository.GetIngridients();
                if (ingridients == null)
                {
                    return NotFound();
                }
                else
                {
                    var ingridientDto = ingridients.ConvertToDto();
                    return Ok(ingridientDto);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ошибка получения данных из базы данных");
            }
        }

        [HttpGet]
        [Route("getIngridient/{id:long}")]
        public async Task<ActionResult<IngridientDto>> GetIngridient(long id)
        {
            try
            {
                var ingridient = await _repository.GetIngridient(id);
                if (ingridient == null)
                {
                    return NotFound();
                }
                else
                {
                    var ingridientDto = ingridient.ConverToDto();
                    return Ok(ingridientDto);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ошибка получения данных из базы данных");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddNewIngridient([FromBody] IngridientDto ingridientDto)
        {
            try
            {
                var newIngridient = await _repository.AddNewIngridient(ingridientDto);
                if (newIngridient == null)
                {
                    return NoContent();
                }
                return Ok(newIngridient);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ошибка создания");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateIngridient(long id, UpdateIngridientDto updateDto)
        {
            try
            {
                var updateIngridient = await _repository.UpdateIngridient(id, updateDto);
                if (updateIngridient == null)
                {
                    return NoContent();
                }
                var response = await _repository.GetIngridient(updateIngridient.Id);
                var result = response.ConverToDto();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpDelete]
        public async Task<ActionResult<Status>> DeleteIngridient(long id)
        {
            try
            {
                var ingridient = await _repository.DeleteIngridient(id);
                return ingridient;
            }
            catch (Exception ex)
            {
                return new Status { Message = ex.Message, StatusCode = 500 };
            }
        }
    }
}
