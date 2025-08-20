using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace AzureFunctions.Functions;

public class OnUserRegisterWriteToQueue
{
    private readonly ILogger<OnUserRegisterWriteToQueue> _logger;

    public OnUserRegisterWriteToQueue(ILogger<OnUserRegisterWriteToQueue> logger)
    {
        _logger = logger;
    }

    [Function("OnUserRegisterWriteToQueue")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
    {
        string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        _logger.LogInformation(requestBody);

        _logger.LogInformation("C# HTTP trigger function processed a request.");
        return new OkObjectResult("Welcome to Azure Functions!");
    }
}