namespace CollectorHub.Web.ViewModels.Themes
{
    public class LegoThemeMinifigureModel
    {
        public LegoThemeMinifigureModel()
        {
        }

        public string SwNumber { get; set; } // maybe not needed sw0002 => only 0002

        public string Name { get; set; }

        public int ProductionYear { get; set; }

        public string LegoTypeName { get; set; }

        public string ImageUrl { get; set; }
    }
}
