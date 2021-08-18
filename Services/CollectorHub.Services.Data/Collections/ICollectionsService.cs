namespace CollectorHub.Services.Data.Collections
{
    using System.Collections.Generic;

    using CollectorHub.Web.ViewModels.Collections;

    public interface ICollectionsService
    {
        CollectionsIndexViewModel GetIndexViewInformation(string categoryId);

        IEnumerable<CollectionIndexViewModel> GetAllTrendingCollections();

        int GetAllCollectionsCount();
    }
}
