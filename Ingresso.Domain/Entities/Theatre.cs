namespace Ingresso.Domain.Entities
{
    public class Theatre
    {
        public Guid Id { get; private set; }
        public string? Title { get; private set; }
        public string? Description { get; private set; }
        public DateTime Data { get; private set; }
        public string? Location { get; private set; }
        public string? TypeOfAttraction { get; private set; }
        public string? Category { get; private set; }
        public string? PublicId { get; private set; }
        public string? ImgUrl { get; private set; }

        public Theatre()
        { 
        }

        public Theatre(Guid id, string? title)
        {
            Id = id;
            Title = title;
        }

        public Theatre(Guid id, string? title, DateTime data, string? location, string? imgUrl) : this(id, title)
        {
            Data = data;
            Location = location;
            ImgUrl = imgUrl;
        }

        public void SetImgUrlPublicId(string imgUrl, string publicId)
        {
            ImgUrl = imgUrl;
            PublicId = publicId;
        }
    }
}
