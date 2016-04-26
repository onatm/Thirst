using Nancy.Bootstrappers.Autofac;
using Nancy.Conventions;

namespace Thirst.Web
{
    public class Bootstrapper : AutofacNancyBootstrapper
    {
        protected override void ConfigureConventions(NancyConventions conventions)
        {
            base.ConfigureConventions(conventions);

            conventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("Scripts", @"Scripts"));
            conventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("Content", @"Content"));
        }
    }
}
