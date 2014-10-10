using System.Web.Mvc;

namespace HiddenMickeyProject.Areas.Editor
{
    public class EditorAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Editor";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            //context.MapRoute(
            //    "location_default",
            //    "Editor/{action}/{RegionName}/{AreaName}/{LocationName}",
            //    new { controller = "Location", action = "Detaild", locationName = UrlParameter.Optional }
            //);
            context.MapRoute(
                "region_default",
                "Editor/{action}/{RegionName}",
                new { controller="Region", action = "Index", regionName = UrlParameter.Optional }
            );
            context.MapRoute(
                "area_default",
                "Editor/{action}/{RegionName}/{AreaName}",
                new { controller = "Area", action = "Details" }
            );
        }
    }
}
