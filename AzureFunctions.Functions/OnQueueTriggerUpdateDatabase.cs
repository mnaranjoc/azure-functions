using System;
using Azure.Storage.Queues.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace AzureFunctions.Functions;

public class OnQueueTriggerUpdateDatabase
{
    private readonly ILogger<OnQueueTriggerUpdateDatabase> _logger;

    public OnQueueTriggerUpdateDatabase(ILogger<OnQueueTriggerUpdateDatabase> logger)
    {
        _logger = logger;
    }

    [Function(nameof(OnQueueTriggerUpdateDatabase))]
    public void Run([QueueTrigger("AzureQueueTest", Connection = "")] QueueMessage message)
    {
        _logger.LogInformation("C# Queue trigger function processed: {messageText}", message.MessageText);
    }
}