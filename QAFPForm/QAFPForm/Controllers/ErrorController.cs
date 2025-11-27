using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using QAFPForm.Models;

namespace QAFPForm.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error")]
        public IActionResult Index()
        {
            var model = new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            return View("Error", model);
        }
    }
}