using ImageEventsApi.Models;
using ImageEventsApi.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ImageEventsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImageEventsController : ControllerBase
    {
        [HttpGet("latest")]
        public Results<BadRequest<string>, Ok<IEnumerable<ImageEvent>>> GetLastImageEvents([FromQuery] int pastHours = 5)
        {
            if(pastHours < 0)
            {
                return TypedResults.BadRequest("Cannot pass the pastHours less than 0");
            }

            var data = ImageEventStore.GetLastImageEvents(pastHours);
            return TypedResults.Ok(data);
        }

        [HttpPost]
        public Results<BadRequest<ModelStateDictionary>, Created> GetLastImageEvents([FromBody] CreateImageEventRequest model)
        {
            if (!ModelState.IsValid) 
            {
                return TypedResults.BadRequest(ModelState);
            }
            var data = ImageEvent.CreateImageEvent(model.ImageUrl, model.Description);

            ImageEventStore.Add(data);

            return TypedResults.Created();
        }
    }
}
