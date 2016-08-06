using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Registration;
using System.Linq;
using System.Reflection;
using System.Windows;
using Caliburn.Micro;
using Samples.CaliburnMicroIntegration.Demos;

namespace Samples.CaliburnMicroIntegration
{
    public class Bootstrapper : BootstrapperBase
    {
        private CompositionContainer container;

        public Bootstrapper()
        {
            Initialize();
        }

        protected override void Configure()
        {
            var builder = new RegistrationBuilder();

            builder
                .ForType<WindowManager>()
                .Export<IWindowManager>();

            builder
                .ForType<EventAggregator>()
                .Export<IEventAggregator>();

            builder
                .ForTypesDerivedFrom<IDemo>()
                .Export<IDemo>();

            var assembly = Assembly.GetExecutingAssembly();

            builder
                .ForTypesMatching(t => (t.Assembly == assembly) && (t.Name.EndsWith("ViewModel") || t.Name.EndsWith("View")))
                .Export();

            var appCatalog = new ApplicationCatalog(builder);

            container = new CompositionContainer(appCatalog);
        }

        protected override object GetInstance(Type service, string key)
        {
            if ((service == null) && !string.IsNullOrWhiteSpace(key))
            {
                service = AssemblySource.FindTypeByNames(new[] { key });
            }

            var export = container.GetExports(service, null, key).Single();

            return export.Value;
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            var exports = container.GetExports(service, null, null);
            return exports.Select(e => e.Value).ToArray();
        }

        protected override void BuildUp(object instance)
        {
            container.SatisfyImportsOnce(instance);
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }

        protected override void OnExit(object sender, EventArgs e)
        {
            container?.Dispose();
        }
    }
}