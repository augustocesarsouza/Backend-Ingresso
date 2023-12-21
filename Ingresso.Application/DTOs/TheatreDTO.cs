namespace Ingresso.Application.DTOs
{
    public class TheatreDTO
    {
        public Guid? Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime Data { get; set; }
        public string? DataString { get; set; }
        public string? Location { get; set; }
        public string? TypeOfAttraction { get; set; }
        public string? Category { get; set; }
        public string? PublicId { get; set; }
        public string? ImgUrl { get; set; }
        public string? Base64Img { get; set; }
    }
}
