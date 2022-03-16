using JuniperTax.Api;

var apiKey = Environment.GetEnvironmentVariable("TaxJarApiKey", EnvironmentVariableTarget.Machine);
Environment.SetEnvironmentVariable("TaxJarApiKey", apiKey);

var app = Startup.BuildApp(args);
app.Run();

