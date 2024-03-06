using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using EvalD2P2.DAL;
using Microsoft.EntityFrameworkCore;
using System;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(services =>
    {
        services.AddDbContext<EvalD2P2DbContext>();
    })
    .Build();

host.Run();