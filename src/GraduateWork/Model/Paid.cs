using PropertyChanged;

namespace Model
{
    [ImplementPropertyChanged]
    public class Paid
    {
        public int Id { get; set; }
        public System.DateTime DatePaid { get; set; }
        public int UserId { get; set; }
        public double Salary { get; set; }
    }
}