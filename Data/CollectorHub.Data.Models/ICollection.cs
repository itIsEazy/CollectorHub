namespace CollectorHub.Data.Models
{
    using System.Collections.Generic;

    using CollectorHub.Data.Common.Models;

    public interface ICollection
    {
        public abstract List<IItem> Items { get; set; }

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
