namespace CollectorHub.Data.Models.Interfaces
{
    public interface ICollection
    {
        public string Name { get; set; }

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
