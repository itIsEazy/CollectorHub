namespace CollectorHub.Data.Models.Collections.HotWheels.Interfaces
{
    public interface ICar
    {
        public string Col { get; set; }

        public string ToyId { get; set; }

        public string Name { get; set; }

        public string Color { get; set; }

        public string Tampos { get; set; }

        public string WheelType { get; set; }

        public string Movie { get; set; }

        public string Notes { get; set; }

        public string PhotoLooseLink { get; set; }

        public string PhotoCardLink { get; set; }

        public string SerieId { get; set; }

        public string HotWheelsTypeId { get; set; }
    }
}
