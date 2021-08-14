namespace CollectorHub.Services.Data.Common
{
    using System.Collections.Generic;

    using CollectorHub.Web.ViewModels.Common;

    public interface ICommonService
    {
        IEnumerable<SortingIndexViewModel> GetAllSortings();
    }
}
