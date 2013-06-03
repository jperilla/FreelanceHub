using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc4;
using Griffin.MvcContrib.RavenDb.Providers;
using Griffin.MvcContrib.Providers.Membership.PasswordStrategies;
using Web.Factories;
using Raven.Client;
using Griffin.MvcContrib.Providers.Membership;
using Griffin.MvcContrib.Providers.Roles;

namespace Web
{
  public static class Bootstrapper
  {
        public static IUnityContainer Initialise()
        {
              var container = BuildUnityContainer();
              DependencyResolver.SetResolver(new UnityDependencyResolver(container));
              return container;
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();
#if DEBUG
            container.RegisterInstance(DocumentStoreFactory.CreateDocumentStoreLocal());
#else
            container.RegisterInstance(DocumentStoreFactory.CreateDocumentStoreServer());
#endif
            RegisterTypes(container);
            container.RegisterInstance(PasswordPolicyFactory.CreatePasswordPolicy());

            return container;
        }

        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IDocumentSession>(new HierarchicalLifetimeManager(), new InjectionFactory(c => c.Resolve<IDocumentStore>().OpenSession()));
            container.RegisterType<IPasswordStrategy, HashPasswordStrategy>();
            container.RegisterType<IAccountRepository, RavenDbAccountRepository>();
            container.RegisterType<IRoleRepository, RavenDbRoleRepository>();
        }
    }
}