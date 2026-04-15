namespace Restaurant.Application.Models.Table
{
    public class TableResponseModel
    {
        public int Id { get; set; }
        public int NumberOfSeats { get; set; }
        public decimal TotalBill { get; set; }
        public bool IsFree { get; set; }
        public bool Enabled { get; set; }
    }
}