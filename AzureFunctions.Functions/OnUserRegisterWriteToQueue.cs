using AzureFunctions.UI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AzureFunctions.Functions;

public class OnUserRegisterWriteToQueue
{
    private readonly ILogger<OnUserRegisterWriteToQueue> _logger;

    public OnUserRegisterWriteToQueue(ILogger<OnUserRegisterWriteToQueue> logger)
    {
        _logger = logger;
    }

    [Function("OnUserRegisterWriteToQueue")]
    [QueueOutput("AzureQueueTest")]
    public async Task<UserModel> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
    {
        string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        var user = JsonConvert.DeserializeObject<UserModel>(requestBody);

        _logger.LogInformation("---------Request body stored in queue.---------");
        return user ?? new UserModel();
    }
}