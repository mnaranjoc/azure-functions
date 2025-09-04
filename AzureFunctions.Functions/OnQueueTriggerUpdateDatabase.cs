using System;
using Azure.Storage.Queues.Models;
using AzureFunctions.Functions.Data;
using AzureFunctions.UI.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AzureFunctions.Functions;

public class OnQueueTriggerUpdateDatabase
{
    private readonly ILogger<OnQueueTriggerUpdateDatabase> _logger;
    private readonly ApplicationDbContext _dbContext;

    public OnQueueTriggerUpdateDatabase(ILogger<OnQueueTriggerUpdateDatabase> logger,
        ApplicationDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    [Function(nameof(OnQueueTriggerUpdateDatabase))]
    public void Run([QueueTrigger("AzureQueueTest", Connection = "")] QueueMessage message)
    {
        var messageBody = message.Body.ToString();
        var user = JsonConvert.DeserializeObject<UserModel>(messageBody);

        if (user != null)
        {
            user.Id = 1;
            user.Status = "active";
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            _logger.LogInformation("User stored successfully");
        }
        else
        {
            _logger.LogWarning("Failed to deserialize user object.");
        }
    }
}