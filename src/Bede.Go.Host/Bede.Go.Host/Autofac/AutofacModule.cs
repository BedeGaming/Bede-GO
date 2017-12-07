using Autofac;
using Bede.Go.Contracts;
using Bede.Go.Core;
using Bede.Go.Core.Services;

namespace Bede.Go.Host.Autofac
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(Context<>))
                .AsSelf();

            builder.RegisterGeneric(typeof(CrudService<>))
                .As(typeof(ICrudService<>));
        }
    }
}