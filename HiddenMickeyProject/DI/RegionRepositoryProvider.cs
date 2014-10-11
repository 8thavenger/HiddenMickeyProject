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
#if DEBUG
            Assembly assembly = Assembly.GetExecutingAssembly();
            Stream data = assembly.GetManifestResourceStream("HiddenMickeyProject.MockData.xml");
            Data.INavigationRepository repository;
            using (XmlReader reader = XmlReader.Create(data))
            {
                repository = new Data.XmlSource(reader);
            }
            return new CachingRepository(repository);
#else
            string connection = ConfigurationManager.ConnectionStrings["LocalMySqlServer"].ConnectionString;
            return new CachingRepository(new Data.MysqlSource(connection));
#endif
        }
    }
}