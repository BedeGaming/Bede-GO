using Autofac;
using Bede.Go.Core;
using Bede.Go.Core.Services;

namespace Bede.Go.Host.AutofacModules
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(Context<>))
                .As(typeof(IContext<>));

            builder.RegisterGeneric(typeof(CrudService<>))
                .As(typeof(ICrudService<>));
        }
    }
}