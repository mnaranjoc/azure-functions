using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using AzureFunctions.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace AzureFunctions.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly BlobServiceClient _blobServiceClient;

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory, BlobServiceClient blobServiceClient)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _blobServiceClient = blobServiceClient;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(UserModel model, IFormFile photo)
        {
            using var httpClient = _httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri("http://localhost:7060/api/");
            var content = new StringContent(JsonConvert.SerializeObject(model));
            var response = await httpClient.PostAsync("OnUserRegisterWriteToQueue", content);

            if (response.IsSuccessStatusCode && photo != null)
            {
                var fileName = $"{model.Name}-{Guid.NewGuid()}{Path.GetExtension(photo.FileName)}";
                var blobContainerClient = _blobServiceClient.GetBlobContainerClient("user-images");
                var blobClient = blobContainerClient.GetBlobClient(fileName);

                var httpHeaders = new BlobHttpHeaders()
                {
                    ContentType = photo.ContentType
                };
                var result = await blobClient.UploadAsync(photo.OpenReadStream(), httpHeaders);
            }

            return RedirectToAction("Index");
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
}
