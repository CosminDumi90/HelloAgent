
using Azure.AI.OpenAI;
using Azure.Identity;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
// 1. Define the variables we extracted from Microsoft Foundry


var endpoint = Environment.GetEnvironmentVariable("AZURE_OPENAI_ENDPOINT") ?? 
    throw new InvalidOperationException("AZURE_OPENAI_ENDPOINT environment variable is not set.");

var model = Environment.GetEnvironmentVariable("AZURE_OPENAI_DEPLOYMENT_NAME") ?? "gpt-5-mini";


//2. Instantiate the universal chat client

IChatClient chatClient = new AzureOpenAIClient(
    new Uri(endpoint),
    new AzureCliCredential()) // authentication entra id
    .GetChatClient(model)
    .AsIChatClient();

//3. Define the Agent's Anatomy

AIAgent supportAgent = chatClient.AsAIAgent(
    name: "NetworkSupportAgent", 
    instructions: "You are a Tier 1 IT support agent. Your answers must be concise, professional, and limited strict.");

Console.WriteLine($"Agent '{supportAgent.Name}' is online");

string userIssue = "I am getting a DNS resolution error when connection to the corporate VPN from a coffee shop.";
Console.WriteLine($"User Issue: {userIssue}\n");

AgentResponse response = await supportAgent.RunAsync(userIssue);
Console.WriteLine($"Agent Response: {response.Text}");
