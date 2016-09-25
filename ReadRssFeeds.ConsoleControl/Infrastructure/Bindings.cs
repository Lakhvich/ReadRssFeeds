using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject.Modules;
using Ninject;
using System.Reflection;
using System.Runtime.InteropServices;
using ReadRssFeeds.DataProvider.Abstract;
using ReadRssFeeds.DataProvider.Concrete;

namespace ReadRssFeeds.ConsoleControl.Infrastructure
{
    public class Bindings : NinjectModule
    {
        private static IKernel ninjectKernel;
        public IKernel Configure()
        {
            ninjectKernel = new StandardKernel();
            ninjectKernel.Load(Assembly.GetExecutingAssembly());

            return ninjectKernel;
        }

        public override void Load()
        {
            Bind<IResurseRSSRepository>().To<RSSRepository>();
        }
    }
}
