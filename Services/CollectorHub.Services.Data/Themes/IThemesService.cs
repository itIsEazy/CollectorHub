namespace CollectorHub.Services.Data.Themes
{
    using System.Collections.Generic;

    using CollectorHub.Web.ViewModels.Themes;

    public interface IThemesService
    {
        HotWheelsThemeViewModel GetAllHotWheelsInfo();

        IEnumerable<LegoThemeMinifigureModel> GetAllLegoFigures();
    }
}
