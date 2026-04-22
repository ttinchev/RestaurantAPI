namespace Restaurant.Api.DTO.Request
{
    public class CreateTableDto
    {
        public int NumberOfSeats { get; set; }
        public bool IsFree { get; set; }
        public bool Enabled { get; set; }
    }
}