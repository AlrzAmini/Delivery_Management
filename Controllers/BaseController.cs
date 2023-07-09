using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DriversManagement.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        
    }
}
