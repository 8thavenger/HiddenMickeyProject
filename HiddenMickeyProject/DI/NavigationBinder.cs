using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
            Models.Navigator navigator = new Models.Navigator();
            navigator.RegionName = controllerContext.RouteData.Values["RegionName"] as string;
            navigator.AreaName = controllerContext.RouteData.Values["AreaName"] as string;
            navigator.LocationName = controllerContext.RouteData.Values["LocationName"] as string;

            navigator.Regions.AddRange(this.repository.Regions());
            int regionId = navigator.Regions.DefaultIfEmpty(new Data.Region()).FirstOrDefault(r => r.RegionName == navigator.RegionName).RegionId;
            
            navigator.Areas.AddRange(this.repository.GetAreasByRegionId(regionId));
            int areaId = 0;
            if(!String.IsNullOrEmpty(navigator.AreaName))                
                areaId = navigator.Areas.DefaultIfEmpty(new Data.Area()).FirstOrDefault(a => a.AreaName == navigator.AreaName).AreaId;

            //locatons

            //entities

            return navigator;
        }
    }
}