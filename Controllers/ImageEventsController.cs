using ImageEventsApi.Models;
using ImageEventsApi.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ImageEventsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImageEventsController(IImageEventService imageEventService) : ControllerBase
    {
        public readonly IImageEventService _imageEventService = imageEventService;

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ImageEvent>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("latest")]
        public Results<BadRequest<string>, Ok<IEnumerable<ImageEvent>>> GetLastImageEvents([FromQuery] int pastHours = 5)
        {
            if(pastHours < 0)
            {
                return TypedResults.BadRequest("Cannot pass the pastHours less than 0");
            }

            var data = _imageEventService.GetLastImageEvents(pastHours);
            return TypedResults.Ok(data);
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("latest/count")]
        public Results<BadRequest<string>, Ok<int>> GetCountLastImageEvents([FromQuery] int pastHours = 5)
        {
            if (pastHours < 0)
            {
                return TypedResults.BadRequest("Cannot pass the pastHours less than 0");
            }

            var count = _imageEventService.GetLastHoursCount(pastHours);
            return TypedResults.Ok(count);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public Results<BadRequest<ModelStateDictionary>, Created> GetLastImageEvents([FromBody] CreateImageEventRequest model)
        {
            if (!ModelState.IsValid) 
            {
                return TypedResults.BadRequest(ModelState);
            }

            var data = ImageEvent.CreateImageEvent(model.ImageUrl, model.Description);

            _imageEventService.AddEvent(data);

            return TypedResults.Created();
        }
    }
}
