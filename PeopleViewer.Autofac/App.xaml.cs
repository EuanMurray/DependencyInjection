using System.Windows;
using Autofac;
using Autofac.Features.ResolveAnything;
using PeopleViewer.Common;
using PeopleViewer.Presentation;
using PersonDataReader.CSV;
using PersonDataReader.Decorators;
using PersonDataReader.Service;

namespace PeopleViewer.Autofac
{
    public partial class App : Application
    {
        private IContainer Container;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ConfigureContainer();
            ComposeObjects();
            Application.Current.MainWindow.Title = "With Dependency Injection - Autofac";
            Application.Current.MainWindow.Show();
        }

        private void ConfigureContainer()
        {
            var builer = new ContainerBuilder();
            builer.RegisterType<CSVReader>().Named<IPersonReader>("reader").SingleInstance();

            builer.RegisterDecorator<IPersonReader>(
                (c, inner) => new CachingReader(inner), fromKey: "reader");

            builer.RegisterType<PeopleViewerWindow>().InstancePerDependency();
            builer.RegisterType<PeopleViewModel>().InstancePerDependency();

            Container = builer.Build();
        }

        private void ComposeObjects()
        {
            Application.Current.MainWindow = Container.Resolve<PeopleViewerWindow>();
        }
    }
}
