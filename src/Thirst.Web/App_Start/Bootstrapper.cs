using Nancy.Bootstrappers.Autofac;
using Nancy.Conventions;

namespace Thirst.Web
{
    public class Bootstrapper : AutofacNancyBootstrapper
    {
        protected override void ConfigureConventions(NancyConventions conventions)
        {
            base.ConfigureConventions(conventions);

            conventions.ViewLocationConventions.Clear();

            conventions.ViewLocationConventions.Add((viewname, model, context) =>
                string.Concat("Views/", context.ModuleName + "/" + context.ModulePath, viewname));

            conventions.ViewLocationConventions.Add((viewName, model, context) => string.Concat("Views/", viewName));
        }
    }
}
