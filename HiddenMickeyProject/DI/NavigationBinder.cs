using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HiddenMickeyProject.Data;

namespace HiddenMickeyProject.DI
{
    public class NavigationBinder : DefaultModelBinder
    {
        private Data.INavigationRepository repository = null;

        public NavigationBinder(Data.INavigationRepository repository)
        {
            this.repository = repository;
        }

        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            HttpRequestBase request = controllerContext.HttpContext.Request;

            string regionName = controllerContext.RouteData.Values["RegionName"] as string;
            string areaName = controllerContext.RouteData.Values["AreaName"] as string;
            string locationName = controllerContext.RouteData.Values["LocationName"] as string;
            string value = controllerContext.RouteData.Values["EntryId"] as string;
            int entryId = 0;

            if (!String.IsNullOrEmpty(value))
                Int32.TryParse(value, out entryId);

            Models.Navigator navigator = Utilities.ObjectFactory.GetNavigator(regionName, areaName, locationName, entryId);

            return navigator;
        }
    }
}