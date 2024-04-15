using CoundAndEat.Api.Extension;
using CoundAndEat.Api.Interfaces;
using CoundAndEat.Dtos.Auth;
using CoundAndEat.Dtos.Recept.Ingridient;
using Microsoft.AspNetCore.Mvc;

namespace CoundAndEat.Api.Controllers
{
    [Route("api/[controller]")]

    [ApiController]
    public class ReceptIngridientController : ControllerBase
    {
        private readonly IReceptIngridientRepository _repository;
        public ReceptIngridientController(IReceptIngridientRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("getRecIngridients")]
        public async Task<ActionResult<List<ReceptIngridientDto>>> GetIngridients()
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
                    var ingridientsDto = ingridients.ConvertToDto();
                    return Ok(ingridientsDto);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ошибка получения данных из базы данных");
            }
        }
        [HttpGet]
        [Route("getRecIngridient/{id:long}")]
        public async Task<ActionResult<ReceptIngridientDto>> GetIngridient(long id)
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
                    var ingridientDto = ingridient.ConvertToDto();
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
        public async Task<IActionResult> AddNewReceptIngridient([FromBody] CreateReceptIngridientDto recIngDto)
        {
            try
            {
                var newRecIng = await _repository.AddNewIngridient(recIngDto);
                if (newRecIng == null)
                {
                    return NoContent();
                }
                return Ok(newRecIng);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ошибка создания");
            }
        }
        [HttpPut]
        public async Task<IActionResult> UpdateReceptIngridient(long id, UpdateReceptIngridientDto upRecIngDto)
        {
            try
            {
                var upRecIng = await _repository.UpdateIngridient(id, upRecIngDto);
                if (upRecIng == null)
                {
                    return NoContent();
                }
                var response = await _repository.GetIngridient(upRecIng.Id);
                var result = response.ConvertToDto();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete]
        public async Task<ActionResult<Status>> DeleteReceptIngridient(long id)
        {
            try
            {
                var recIng = await _repository.DeleteIngridient(id);
                return recIng;
            }
            catch (Exception ex)
            {
                return new Status { Message = ex.Message, StatusCode = 500 };
            }
        }
    }
}
