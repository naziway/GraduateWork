using PropertyChanged;

namespace Model
{
    [ImplementPropertyChanged]
    public class PartModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Model { get; set; }
        public string Marka { get; set; }
        public double Price { get; set; }

        [DependsOn(nameof(ChooseCount), nameof(Price))]
        public double SumPrice => Price * ChooseCount;
        public int AvailableCount { get; set; }
        public int ChooseCount { get; set; }

    }
}