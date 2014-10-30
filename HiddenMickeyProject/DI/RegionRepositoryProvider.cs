using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using Ninject.Activation;
using System.Reflection;
using System.IO;
using System.Xml;

namespace HiddenMickeyProject.DI
{
    public class RepositoryProvider : Provider<Data.INavigationRepository>
    {

        protected override Data.INavigationRepository CreateInstance(IContext context)
        {
            return Utilities.ObjectFactory.GetRepository();
        }
    }
}