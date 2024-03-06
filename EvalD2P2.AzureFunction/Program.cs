using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using EvalD2P2.DAL;
using Microsoft.EntityFrameworkCore;
using System;
using EvalD2P2.Repository;
using EvalD2P2.Repository.Contract;
using EvalD2P2.Service;
using EvalD2P2.Service.Contracts;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(services =>
    {
        services.AddDbContext<EvalD2P2DbContext>(options =>
            options.UseSqlServer("Server=localhost;Database=eval;User Id=sa;Password=MyPass@word;TrustServerCertificate=True"));
        services.AddTransient<IEventRepository, EventRepository>();
        services.AddTransient<IEventService, EventService>();
    })
    .Build();

host.Run();