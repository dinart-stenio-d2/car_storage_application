using Car.Storage.Application.Administrators.Application.ApiViewModels;
using Car.Storage.Application.Administrators.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace car_storage_application.API.Controllers.V1.Controllers
{

    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class AdministratorsController : CarStorageBaseController
    {
        private readonly IAdministratorsApplicationService administratorsApplicationService;
        private readonly ILogger<AdministratorsController> logger;

        public AdministratorsController(IAdministratorsApplicationService administratorsApplicationService, ILogger<AdministratorsController> logger)
        {
            this.administratorsApplicationService=administratorsApplicationService;
            this.logger=logger;
        }

        /// <summary>
        ///  Endpoint that generates a new resource
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CarViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        //[Authorize(Policy = "Admins")]
        public async Task<IActionResult> Post([FromBody] CarViewModel model)
        {
            try
            {
                this.logger.LogInformation("car_storage_application.API--> Call started for Api : api/Administrators ,Started at -- " + DateTime.UtcNow.ToString("MM/dd/yyyy hh:mm:ss.fff tt"));

                if(model == null)
                {
                    this.logger.LogError($"car_storage_application.API--> Call ended for Api : api/Administrators , Ended at -- " + DateTime.UtcNow.ToString("MM/dd/yyyy hh:mm:ss.fff tt") + ", Data not found");
                    return BadRequest("Values ​​sent are not valid or the request body is empty");
                }
                var result = await administratorsApplicationService.CreateResourceAsync(model);

                if (!result.ValidationResult.IsValid)
                {
                    this.logger.LogError($"car_storage_application.API--> Call ended for Api : api/Administrators , Ended at -- " + DateTime.UtcNow.ToString("MM/dd/yyyy hh:mm:ss.fff tt") + ", Data not found");
                    return BadRequest(result.ValidationResult.Errors);
                }
                else
                {
                    this.logger.LogInformation($"car_storage_application.API--> Call ended for Api : api/Administrators , Ended at -- " + DateTime.UtcNow.ToString("MM/dd/yyyy hh:mm:ss.fff tt") + ", Appointment reasons successful responses");
                    return StatusCode(StatusCodes.Status201Created);
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"car_storage_application.API--> Call ended for Api : api/Administrators , Ended at -- " + DateTime.UtcNow.ToString("MM/dd/yyyy hh:mm:ss.fff tt") + ", ERROR");
                return StatusCode(StatusCodes.Status500InternalServerError, $"ERROR IN: {ex.InnerException}, MESSAGE: {ex.Message}");
            }
        }

        /// <summary>
        ///  Endpoint that update an existing resource
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(CarViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        //[Authorize(Policy = "Admins")]
        public async Task<IActionResult> Put([FromBody] CarViewModel model)
        {
            try
            {
                this.logger.LogInformation("car_storage_application.API--> Call started for Api : api/Administrators ,Started at -- " + DateTime.UtcNow.ToString("MM/dd/yyyy hh:mm:ss.fff tt"));

                var result = await administratorsApplicationService.UpdateResourceAsync(model);

                if (!result.ValidationResult.IsValid)
                {
                    this.logger.LogError($"car_storage_application.API--> Call ended for Api : api/Administrators , Ended at -- " + DateTime.UtcNow.ToString("MM/dd/yyyy hh:mm:ss.fff tt") + ", Data not found");
                    return BadRequest(result.ValidationResult.Errors);
                }
                else
                {
                    this.logger.LogInformation($"car_storage_application.API--> Call ended for Api : api/Administrators , Ended at -- " + DateTime.UtcNow.ToString("MM/dd/yyyy hh:mm:ss.fff tt") + ", Appointment reasons successful responses");
                    return StatusCode(StatusCodes.Status204NoContent);
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"car_storage_application.API--> Call ended for Api : api/Administrators , Ended at -- " + DateTime.UtcNow.ToString("MM/dd/yyyy hh:mm:ss.fff tt") + ", ERROR");
                return StatusCode(StatusCodes.Status500InternalServerError, $"ERROR IN: {ex.InnerException}, MESSAGE: {ex.Message}");
            }
        }

    }
}
