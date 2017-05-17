using PropertyChanged;

namespace Model
{
    [ImplementPropertyChanged]
    public class Work
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public System.TimeSpan Time { get; set; }
    }

}