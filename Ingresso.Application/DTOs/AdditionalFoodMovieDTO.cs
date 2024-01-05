namespace Ingresso.Application.DTOs
{
    public class AdditionalFoodMovieDTO
    {
        public Guid? Id { get; set; }
        public string? Title { get; set; }
        public string? Price { get; set; }
        public string? Fee { get; set; }
        public string? ImgUrl { get; set; }
        public string? PublicId { get; set; }
        public string? Base64Img { get; set; }

        public Guid? MovieId { get; set; }
        public MovieDTO? MovieDTO { get; set; }
    }
}
