using Caliburn.Micro;
using System;
using System.Collections.Generic;

using OurMoneyWpf.ConfigureApp;
using OurMoneyWpf.Modules.ForTesting;
using OurMoneyWpf.Modules.ForTesting.ListBoxTest;

namespace OurMoneyWpf
{
    public class AppBootstrapper : BootstrapperBase
    {
        private SimpleContainer container;

        public AppBootstrapper()
        {
            Initialize();
        }

        protected override void Configure()
        {
            container = new SimpleContainer();

            container.Singleton<IWindowManager, WindowManager>();
            container.Singleton<IEventAggregator, EventAggregator>();

            container.PerRequest<IShell, ShellViewModel>();
 
            //-------------------------- Для проб и ошибок -------------------------------------
            container.PerRequest<ForTestingViewModel>();
            container.PerRequest<ListBoxBehindViewModel>();
            //-------------------------- Для проб и ошибок -------------------------------------

            new MainControlsServices(container);
            new DictServices(container);
            new InputServices(container);
            new CommonControlsServices(container);
            new LocalControlsServices(container);

            new RepoServices(container);
        }

        protected override object GetInstance(Type service, string key)
        {
            return container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            container.BuildUp(instance);
        }

        protected override void OnStartup(object sender, System.Windows.StartupEventArgs e)
        {
            DisplayRootViewFor<IShell>();
        }
    }
}