using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using Ninject.Activation;

namespace HiddenMickeyProject.DI
{
    public class RepositoryProvider : Provider<Data.INavigationRepository>
    {

        protected override Data.INavigationRepository CreateInstance(IContext context)
        {
            string connection = ConfigurationManager.ConnectionStrings["LocalMySqlServer"].ConnectionString;
            return new CachingRepository(new Data.MysqlSource(connection));
        }
    }
}