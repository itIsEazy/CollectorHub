﻿namespace CollectorHub.Data.Models.Interfaces
{
    using System.Collections.Generic;

    using CollectorHub.Data.Models.User;

    public interface ICollection
    {
        public string Name { get; set; }

        public int ViewsCount { get; set; }

        public bool IsPublic { get; set; }

        public string Description { get; set; }

        // public void AddItem(Item currItem)
        // {
        //     this.Items.Add(currItem);
        // }
        //
        // public decimal CalculateProfit()
        // {
        //     decimal sum = 0.0m;
        //
        //     foreach (var item in Items)
        //     {
        //         sum += item.Profit;
        //     }
        //
        //     return sum;
        // }
    }
}
