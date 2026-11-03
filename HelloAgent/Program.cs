using Azure.AI.OpenAI;
using Azure.Identity;
using Microsoft.Agents.AI;
using OpenAI.Chat;


// 1. Define the variables we extracted from Microsoft Foungry
var endpoint = Environment.GetEnvironmentVariable("AZURE_OPENAI_ENDPOINT") ?? 
    throw new InvalidOperationException("AZURE_OPENAI_ENDPOINT environment variable is not set.");

var model = Environment.GetEnvironmentVariable("AZURE_OPENAI_DEPLOYMENT_NAME") ?? "gpt-5-mini";

//2. Create the Agent using MAF
AIAgent agent = new AzureOpenAIClient(
    new Uri(endpoint),
    new AzureCliCredential()) // authentication entra id
    .GetChatClient(model)
    .AsAIAgent(instructions: "You are a friendly assistant. Keep your answers brief.");

// 3. invoke the Agent

Console.WriteLine(await agent.RunAsync("What is the largest city in France?"));

