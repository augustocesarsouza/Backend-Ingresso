namespace Ingresso.Domain.Entities
{
    public class AdditionalFoodMovie
    {
        public Guid Id { get; private set; }
        public string? Title { get; private set; }
        public string? Price { get; private set; }
        public string? Fee { get; private set; }
        public string? ImgUrl { get; private set; }
        public string? PublicId { get; private set; }

        public Guid MovieId { get; private set; }
        public Movie? Movie { get; private set; }

        public AdditionalFoodMovie()
        {
        }

        public AdditionalFoodMovie(string? title, string? price, string? fee, string? imgUrl)
        {
            Title = title;
            Price = price;
            Fee = fee;
            ImgUrl = imgUrl;
        }
    }
}
