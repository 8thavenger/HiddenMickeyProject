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

            context.MapRoute(
                "entry_default",
                "Entry/{action}/{RegionName}/{AreaName}/{LocationName}/{EntryId}",
                new { controller = "Entry", action = "Create", entryId = UrlParameter.Optional }
            );
            context.MapRoute(
                "location_default",
                "Location/{action}/{RegionName}/{AreaName}/{LocationName}",
                new { controller = "Location", action = "Create", locationName = UrlParameter.Optional }
            );
            context.MapRoute(
                "area_default",
                "Area/{action}/{RegionName}/{AreaName}",
                new { controller = "Area", action = "Create", areaName = UrlParameter.Optional }
            );
            context.MapRoute(
                "region_default",
                "Region/{action}/{RegionName}",
                new { controller="Region", action = "Index", regionName = UrlParameter.Optional }
            );
        }
    }
}
