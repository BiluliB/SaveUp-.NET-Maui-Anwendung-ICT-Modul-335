using Microsoft.AspNetCore.Mvc;
using SaveUpBackend.Interfaces;
using SaveUpModels.DTOs.Requests;
using SaveUpModels.DTOs.Responses;

namespace SaveUpBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SavedMoneyController : Controller
    {
        private readonly ISavedMoneyService _savedMoneyService;

        public SavedMoneyController(ISavedMoneyService savedMoneyService)
        { 
            _savedMoneyService = savedMoneyService;
        
        }


            /// <summary>
            /// Creates a new order submission
            /// </summary>
            /// <param name="createDTO"></param>
            /// <returns>CreateAtAction, BadRequest</returns>
            [HttpPost]
            [ProducesResponseType(typeof(List<SavedMoneyDTO>), StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            public async Task<ActionResult<SavedMoneyDTO>> Create([FromBody] SavedMoneyCreateDTO createDTO)
            {
                try
                {
                    var savedMoneyDTO = await _savedMoneyService.Create(createDTO);
                    return CreatedAtAction(nameof(GetById), new { id = savedMoneyDTO.Id }, savedMoneyDTO);
                }
                catch (InvalidOperationException ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            /// <summary>
            /// Gets all order submissions
            /// </summary>
            /// <returns>Not Found, Ok, BadRequest</returns>
            [HttpGet]
            [ProducesResponseType(typeof(List<SavedMoneyDTO>), StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            public async Task<ActionResult<List<SavedMoneyDTO>>> GetAll()
            {
                try
                {
                    var savedMoneyDTOs = await _savedMoneyService.GetAll();
                    if (savedMoneyDTOs == null)
                    {
                        return NotFound();
                    }
                    return Ok(savedMoneyDTOs);
                }
                catch (FormatException)
                {
                    return BadRequest("Invalid Id format.");
                }
            }

            /// <summary>
            /// Gets an order submission by id
            /// </summary>
            /// <param name="id"></param>
            /// <returns>Not Found, Ok, BadRequest</returns>
            [HttpGet("{id:length(24)}")]
            [ProducesResponseType(typeof(SavedMoneyDTO), StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            public async Task<ActionResult<SavedMoneyDTO>> GetById(string id)
            {
                try
                {
                    var savedMoneyDTO = await _savedMoneyService.GetById(id);
                    if (savedMoneyDTO == null)
                    {
                        return NotFound();
                    }
                    return Ok(savedMoneyDTO);
                }
                catch (FormatException)
                {
                    return BadRequest("Invalid Id format.");
                }
            }        
    }
}
