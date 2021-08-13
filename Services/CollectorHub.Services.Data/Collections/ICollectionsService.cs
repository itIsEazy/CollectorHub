namespace CollectorHub.Services.Data.Collections
{
    using CollectorHub.Web.ViewModels.Collections;

    public interface ICollectionsService
    {
        CollectionsIndexViewModel GetIndexViewInformation(string categoryId);

        int GetAllCollectionsCount();
    }
}
