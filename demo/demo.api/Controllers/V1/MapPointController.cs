using demo.Business.IServices;
using demo.Model.Dto;
using demo.Model.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace demo.api.Controllers.V1
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class MapPointController : ControllerBase
    {
        private readonly IMapPointService mapPointService;
        public MapPointController(IMapPointService mapPointService)
        {
            this.mapPointService = mapPointService;
        }

        /// <summary>
        /// This function returns nearest atm address.
        /// </summary>
        /// <response code="200">Getting informations successfully completed</response>
        /// <response code="401">Unauthorized</response>/// 
        [MapToApiVersion("1.0")]
        [HttpPost("/api/v{version:apiVersion}/MapPoint/FindNearestAtm")]
        [ProducesResponseType(typeof(ErrorDto), statusCode: StatusCodes.Status400BadRequest)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public IActionResult FindNearestAtm(MapPointDto model)
        {
            if (model != null && model.Latitude != default && model.Longitude != default)
            {

                AtmMapPointDto result = new AtmMapPointDto();
                MapPointViewModel mapPoint = mapPointService.FindNearestAtm(new MapPointViewModel()
                {
                    Latitude = model.Latitude,
                    Longitude = model.Longitude
                }).Result;
                if (mapPoint != null)
                {
                    result.Address = mapPoint.Address;
                    result.City = mapPoint.CityName;
                    result.AtmName = mapPoint.Name;

                    return Ok(result);
                }
                else
                {
                    return Unauthorized();
                }
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
