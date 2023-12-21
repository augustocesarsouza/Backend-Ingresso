namespace Ingresso.Application.DTOs
{
    public class MovieDTO
    {
        public Guid? Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Gender { get; set; }
        public string? Duration { get; set; }
        public int? MovieRating { get; set; }
        public string? PublicId { get; set; }
        public string? ImgUrl { get; set; }
        public string? Base64Img { get; set; }
        public string? StatusMovie { get; set; }
        public string? ImgUrlBackground { get; set; }
        public string? PublicIdImgBackgound { get; set; }
    }
}
