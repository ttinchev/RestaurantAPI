namespace Restaurant.Api.DTO.Request
{
    public class UpdateTableDto
    {
        public int NumberOfSeats { get; set; }
        public bool IsFree { get; set; }
        public bool Enabled { get; set; }
    }
}