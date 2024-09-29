var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.DemoApplication_ApiService>("apiservice");

builder.AddProject<Projects.DemoApplication_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService);

builder.Build().Run();
