﻿namespace CollectorHub.Services.Data.Collections
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CollectorHub.Data.Models.Collections;
    using CollectorHub.Data.Models.Collections.HotWheels;
    using CollectorHub.Data.Models.User;
    using CollectorHub.Web.ViewModels.Collections;

    public interface ICollectionsService
    {
        ApplicationUser GetUser(string userId);

        CollectionsIndexViewModel GetIndexViewInformation(string categoryId);

        IEnumerable<CollectionType> GetAllCollectionTypes();

        IEnumerable<HotWheelsType> GetAllHotWheelsTypes();

        IEnumerable<CollectionIndexViewModel> GetAllTrendingCollections(string categoryId);

        Task CreateHotWheelsCollection(string userId, string hotWheelsTypeId, string description, bool isPublic, bool showPrices);

        string GetHotWheelsTypeName(string hotWheelsTypeId);

        string GetHotWheelsTypeImageUrl(string hotWheelsTypeId);

        int GetAllCollectionsCount();

        bool HotWheelsTypeExist(string typeId);

        bool CheckIfUserCanCreateHotWheelsCollection(string userId, string hotWheelsTypeId);
    }
}
