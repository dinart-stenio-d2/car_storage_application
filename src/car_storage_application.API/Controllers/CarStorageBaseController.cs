using Microsoft.AspNetCore.Mvc;

namespace car_storage_application.API.Controllers
{
    [ApiConventionType(typeof(DefaultApiConventions))]
    [ApiController]
    public class CarStorageBaseController : ControllerBase
    {
        //This is the Controller base controller each controller need inherit it 
        //Here  we can create custom validate to view models 
        //custom validate to modelstate 
        //Extension methods to all controller
    }
}
