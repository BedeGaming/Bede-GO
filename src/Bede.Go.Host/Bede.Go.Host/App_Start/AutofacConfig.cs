using Autofac;
using Autofac.Integration.WebApi;
using Bede.Go.Contracts;
using Bede.Go.Core;
using Bede.Go.Core.Services;
using System.Web.Http;

namespace Bede.Go.Host.Autofac
{
    public static class AutofacConfig
    {
        public static void Configure(HttpConfiguration config)
        {
            var builder = new ContainerBuilder();

            builder.RegisterGeneric(typeof(Context<>))
                .AsSelf();

            builder.RegisterGeneric(typeof(CrudService<>))
                .As(typeof(ICrudService<>));

            builder.RegisterApiControllers(System.Reflection.Assembly.GetExecutingAssembly());

            // OPTIONAL: Register the Autofac filter provider.
            builder.RegisterWebApiFilterProvider(config);

            // OPTIONAL: Register the Autofac model binder provider.
            builder.RegisterWebApiModelBinderProvider();

            var container = builder.Build();

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}