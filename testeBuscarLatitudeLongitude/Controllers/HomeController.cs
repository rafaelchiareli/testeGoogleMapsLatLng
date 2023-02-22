using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using testeBuscarLatitudeLongitude.Models;
using static System.Net.WebRequestMethods;

namespace testeBuscarLatitudeLongitude.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    public List<Endereco> ListaEndereco = new List<Endereco>();

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
        
    {
        ViewBag.latitude = "";
        ViewBag.longitude = "";
        ViewBag.place_id = "";
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Index(Endereco endereco)
    {
        using (var http = new HttpClient())
        {
            var bairro = endereco.Bairro.Replace(' ','+');
            var logradouro = endereco.Logradouro.Replace(' ', '+');
            var cidade = endereco.Cidade.Replace(' ', '+');

            var urlBase = "https://maps.googleapis.com/maps/api/geocode/json?";
            var response = await http.GetAsync($"{urlBase}address={endereco.Tipo}+{endereco.Logradouro}+{endereco.Numero},{bairro},{cidade},{endereco.Estado}&key=AIzaSyBXuoBcyNzqbObSA5e403g1zx66UAXP39E");
            JObject dados = JObject.Parse(await response.Content.ReadAsStringAsync());
            var results = dados.Root;
            ViewBag.latitude = dados["results"].First["geometry"]["location"]["lat"];
            ViewBag.longitude = dados["results"].First["geometry"]["location"]["lng"];
            ViewBag.place_id = dados["results"].First["place_id"];

        }
        
        

        return View();

    }
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

//json retorno

//{
//   "results" : [
//      {
//         "address_components" : [
//            {
//               "long_name" : "353",
//               "short_name" : "353",
//               "types" : [ "street_number" ]
//            },
//            {
//    "long_name" : "Rua O",
//               "short_name" : "R. O",
//               "types" : [ "route" ]
//            },
//            {
//    "long_name" : "Vale do Sol",
//               "short_name" : "Vale do Sol",
//               "types" : [ "political", "sublocality", "sublocality_level_1" ]
//            },
//            {
//    "long_name" : "Pinheiral",
//               "short_name" : "Pinheiral",
//               "types" : [ "administrative_area_level_2", "political" ]
//            },
//            {
//    "long_name" : "Rio de Janeiro",
//               "short_name" : "RJ",
//               "types" : [ "administrative_area_level_1", "political" ]
//            },
//            {
//    "long_name" : "Brasil",
//               "short_name" : "BR",
//               "types" : [ "country", "political" ]
//            },
//            {
//    "long_name" : "27197-000",
//               "short_name" : "27197-000",
//               "types" : [ "postal_code" ]
//            }
//         ],
//         "formatted_address" : "R. O, 353 - Vale do Sol, Pinheiral - RJ, 27197-000, Brasil",
//         "geometry" : {
//    "location" : {
//        "lat" : -22.5042496,
//               "lng" : -44.0185487
//            },
//            "location_type" : "RANGE_INTERPOLATED",
//            "viewport" : {
//        "northeast" : {
//            "lat" : -22.5028325697085,
//                  "lng" : -44.0172290697085
//               },
//               "southwest" : {
//            "lat" : -22.5055305302915,
//                  "lng" : -44.0199270302915
//               }
//    }
//},
//         "place_id" : "EjpSLiBPLCAzNTMgLSBWYWxlIGRvIFNvbCwgUGluaGVpcmFsIC0gUkosIDI3MTk3LTAwMCwgQnJhemlsIjESLwoUChIJz1tuuGG7ngARZVi7Sl8urlAQ4QIqFAoSCf1uK4Vhu54AEZNcyYob89Du",
//         "types" : [ "street_address" ]
//      }
//   ],
//   "status" : "OK"
//}