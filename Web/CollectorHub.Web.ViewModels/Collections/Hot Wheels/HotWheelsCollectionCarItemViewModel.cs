﻿namespace CollectorHub.Web.ViewModels.Collections.Hot_Wheels
{
    public class HotWheelsCollectionCarItemViewModel
    {
        public HotWheelsCollectionCarItemViewModel()
        {
        }

        public string Id { get; set; }

        public decimal PriceBoughted { get; set; }

        public virtual HotWheelsCollectionCarViewModel Car { get; set; }
    }
}
