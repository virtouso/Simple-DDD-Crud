using System.Reflection;
using Mc2.CrudTest.Application.Basic;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Mc2.CrudTest.Application;

public static class Extensions
{
    public static void InstallMediatr(this IServiceCollection services)
    {
        services.AddMediatR(cfg=>cfg.RegisterServicesFromAssemblies(typeof(MetaResponse<>).Assembly));
     //   services.AddMediatR(typeof(MetaResponse<>).Assembly);
    }
}