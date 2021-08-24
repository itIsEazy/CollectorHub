namespace CollectorHub.Web.ViewModels.Themes
{
    using System.Collections.Generic;

    public class LegoThemeViewModel
    {
        public LegoThemeViewModel()
        {
            this.LegoFigures = new HashSet<LegoThemeMinifigureModel>();
        }

        public IEnumerable<LegoThemeMinifigureModel> LegoFigures { get; set; }
    }
}
