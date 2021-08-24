namespace CollectorHub.Services.Data.Collections
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CollectorHub.Data.Models.Collections;
    using CollectorHub.Data.Models.Collections.HotWheels;
    using CollectorHub.Data.Models.Collections.Lego;
    using CollectorHub.Data.Models.User;
    using CollectorHub.Web.ViewModels.Collections;
    using CollectorHub.Web.ViewModels.Collections.Common;
    using CollectorHub.Web.ViewModels.Collections.Hot_Wheels;
    using CollectorHub.Web.ViewModels.Collections.Lego;

    public interface ICollectionsService
    {
        ApplicationUser GetUser(string userId);

        CollectionsIndexViewModel GetIndexViewInformation(string categoryId, string searchInput, int sortingId);

        List<TrendingCollectionViewModel> GetAllCollections();

        HotWheelsCollectionViewModel GetHotWheelsCollectionViewInformation(string collectionId);

        IEnumerable<CollectionType> GetAllCollectionTypes();

        IEnumerable<HotWheelsType> GetAllHotWheelsTypes();

        IEnumerable<LegoType> GetAllLegoTypes();

        IEnumerable<CollectionIndexViewModel> GetAllTrendingCollections(string categoryId);

        IEnumerable<HotWheelsCollectionViewModel> GetHotWheelsCollections(string userId); 

        IEnumerable<MyCollectionHotWheelsCollectionViewModel> GetMyCollectionHotWheelsCollections(string userId);

        IEnumerable<MyCollectionLegoCollectionViewModel> GetMyCollectionLegoCollections(string userId);

        IEnumerable<TrendingCollectionViewModel> GetTrendingCollections(string categoryId);

        Task CreateHotWheelsCollection(string userId, string hotWheelsTypeId, string description, bool isPublic, bool showPrices);

        Task CreateLegoCollection(string userId, string legoTypeId, string description, bool isPublic, bool showPrices);

        Task AddHotWheelsCarItemToCollection(string carId, string collectionId, decimal price, string customUrl);

        void RemoveHotWheelsCarItemFromCollection(string itemId);

        void ChangePrivateOptionForHotWheelsCollection(string collectionId);

        void ChangeShowPricesOptionForHotWheelsCollection(string collectionId);

        string GetHotWheelsTypeName(string hotWheelsTypeId);

        string GetLegoTypeName(string LegoTypeId);

        string GetHotWheelsTypeImageUrl(string hotWheelsTypeId);

        string GetHotWheelsCollectionUserId(string collectionId);

        int GetAllCollectionsCount();

        bool HotWheelsTypeExist(string typeId);

        bool LegoTypeExists(string typeId);

        bool HotWheelsCollectionExists(string collectionId);

        bool HotWheelsCarExists(string carId);

        bool CheckIfUserCanCreateHotWheelsCollection(string userId, string hotWheelsTypeId);

        bool CollectionIsPublic(string collectionId);

        bool HotWheelsCarIsFromHotWHeelCollection(string collectionId, string carId);
    }
}
