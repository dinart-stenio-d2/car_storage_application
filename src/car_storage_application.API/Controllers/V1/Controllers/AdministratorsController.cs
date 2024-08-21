using Car.Storage.Application.Administrators.Application.ApiViewModels;
using Car.Storage.Application.Administrators.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

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
        [Route("Car/")]
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
                this.logger.LogInformation("car_storage_application.API--> Call started for Api : api/Administrators/Car ,Started at -- " + DateTime.UtcNow.ToString("MM/dd/yyyy hh:mm:ss.fff tt"));

                if (model == null)
                {
                    this.logger.LogError($"car_storage_application.API--> Call ended for Api : api/Administrators/Car , Ended at -- " + DateTime.UtcNow.ToString("MM/dd/yyyy hh:mm:ss.fff tt") + ", Data not found");
                    return BadRequest("Values ​​sent are not valid or the request body is empty");
                }
                var result = await administratorsApplicationService.CreateResourceAsync(model);

                if (!result.ValidationResult.IsValid)
                {
                    this.logger.LogError($"car_storage_application.API--> Call ended for Api : api/Administrators/Car , Ended at -- " + DateTime.UtcNow.ToString("MM/dd/yyyy hh:mm:ss.fff tt") + ", Data not found");
                    return BadRequest(result.ValidationResult.Errors);
                }
                else
                {
                    this.logger.LogInformation($"car_storage_application.API--> Call ended for Api : api/Administrators/Car , Ended at -- " + DateTime.UtcNow.ToString("MM/dd/yyyy hh:mm:ss.fff tt") + ", Car resource successful responses");
                    return StatusCode(StatusCodes.Status201Created);
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"car_storage_application.API--> Call ended for Api :api/Administrators , Ended at -- " + DateTime.UtcNow.ToString("MM/dd/yyyy hh:mm:ss.fff tt") + ", ERROR");
                return StatusCode(StatusCodes.Status500InternalServerError, $"ERROR IN: {ex.InnerException}, MESSAGE: {ex.Message}");
            }
        }

        /// <summary>
        /// Endpoint that update a resource
        /// </summary>
        /// <param name="model"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Car/{Id:guid}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(CarViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        //[Authorize(Policy = "Admins")]
        public async Task<IActionResult> Put([FromBody] CarViewModel model, [FromRoute] Guid Id)
        {
            try
            {

                this.logger.LogInformation("car_storage_application.API--> Call started the update of a car instance update by the Api : api/Administrators/Car ,Started at -- " + DateTime.UtcNow.ToString("MM/dd/yyyy hh:mm:ss.fff tt"));

                if (model == null || Id  == Guid.Empty)
                {
                    this.logger.LogError($"car_storage_application.API--> Call ended to try update a car instance  for Api : api/Administrators/Car , Ended at -- " + DateTime.UtcNow.ToString("MM/dd/yyyy hh:mm:ss.fff tt") + ", Data not found");
                    return BadRequest("Values ​​sent are not valid or the request body is empty");
                }

                var result = await administratorsApplicationService.UpdateResourceAsync(model,Id);

                if (!result.ValidationResult.IsValid)
                {
                    this.logger.LogError($"car_storage_application.API--> Call ended the update of a car instance update for Api : api/Administrators/Car , Ended at -- " + DateTime.UtcNow.ToString("MM/dd/yyyy hh:mm:ss.fff tt") + ", Error");
                    return BadRequest(result.ValidationResult.Errors);
                }
                else
                {
                    this.logger.LogInformation($"car_storage_application.API--> Call ended for Api : api/Administrators/Car , Ended at -- " + DateTime.UtcNow.ToString("MM/dd/yyyy hh:mm:ss.fff tt") + ", Car resource successful responses");
                    return StatusCode(StatusCodes.Status204NoContent);
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"car_storage_application.API--> Call ended for Api : api/Administrators/Car , Ended at -- " + DateTime.UtcNow.ToString("MM/dd/yyyy hh:mm:ss.fff tt") + ", ERROR");
                return StatusCode(StatusCodes.Status500InternalServerError, $"ERROR IN: {ex.InnerException}, MESSAGE: {ex.Message}");
            }
        }


        /// <summary>
        ///  Endpoint that reccover an existing resource and his details
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Car/{Id:guid}", Name = "GetResourceById")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CarViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        //[Authorize(Policy = "Admins")]
        public async Task<IActionResult> Get([FromRoute] Guid Id)
        {
            try
            {
                this.logger.LogInformation("car_storage_application.API--> Call started for recover a car by the Api : api/Administrators/Details/Id , Started at -- " + DateTime.UtcNow.ToString("MM/dd/yyyy hh:mm:ss.fff tt"));

                if (Id  == Guid.Empty)
                {
                    this.logger.LogError($"car_storage_application.API--> Call ended for try recover a car by the Api : api/Administrators/Details fail the Id ​​sent it is  not valid, Ended at -- " + DateTime.UtcNow.ToString("MM/dd/yyyy hh:mm:ss.fff tt") + ", Data not found");
                    return BadRequest($"the {Id} ​​sent it is  not valid");
                }

                var result = await administratorsApplicationService.GetResourceAsyncById(Id);

                if (!result.ValidationResult.IsValid && result.ValidationResult.Errors.Any(error => error.ErrorMessage.Contains($"There is no data in the database for the Id:{Id}")))
                {
                    this.logger.LogInformation($"car_storage_application.API-->ended for try recover a car by the Api : api/Administrators/Details/{Id.ToString()} Ended at -- " + DateTime.UtcNow.ToString("MM/dd/yyyy hh:mm:ss.fff tt") + ", Data not found");
                    return NotFound(result);
                }
                else
                {
                    return Ok(result);
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
