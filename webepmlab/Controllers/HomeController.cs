using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web;
using System.Diagnostics;
using webepmlab.Models;

namespace webepmlab.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITokenAcquisition _tokenAcquisition;

        public HomeController(ILogger<HomeController> logger, ITokenAcquisition _token)
        {
            _logger = logger;
            _tokenAcquisition = _token;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        
        public async Task< IActionResult> LlamarAPI()
        {

var _accesstoken = await _tokenAcquisition.GetAccessTokenForUserAsync(new[] { "api://6f083186-d6d3-40e6-8859-f8743dddcef1/apiepm.accesspolizas" });

            HttpClient _client = new HttpClient();  
            _client.BaseAddress = new Uri("https://localhost:7266/");

            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _accesstoken);


            HttpResponseMessage _respuesta = await _client.GetAsync("WeatherForecast");
            if (_respuesta.IsSuccessStatusCode)
            {
                ViewBag.DatosApi = await _respuesta.Content.ReadAsStringAsync();
            }
            else
            {
                ViewBag.DatosApi = $"Error al obtener datos de la API CODIGO : { (int) _respuesta.StatusCode } MENSAJE : { _respuesta.ReasonPhrase } ";
            }

            return View();
        }



        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
