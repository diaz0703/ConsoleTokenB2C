using Microsoft.Identity.Client;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Inicia");
Console.WriteLine("");

const string _clientId = "<client_id>";
const string _tenantId = "<tenant_id>";


IPublicClientApplication app = PublicClientApplicationBuilder
    .Create(_clientId)
    .WithAuthority(AzureCloudInstance.AzurePublic, _tenantId)
    .WithRedirectUri("http://localhost")
    .Build();

AuthenticationResult result = await app.AcquireTokenInteractive(new[] { "User.Read" })
    .ExecuteAsync();

Console.WriteLine($"idToken adquirido: {result.IdToken}");
Console.WriteLine("");
Console.WriteLine($"accessToken adquirido: {result.AccessToken}");



Console.WriteLine("");
Console.WriteLine("Termina");
