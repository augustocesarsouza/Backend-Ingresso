namespace Ingresso.Domain.Entities
{
    public class Movie
    {
        public Guid Id { get; private set; }
        public string? Title { get; private set; }
        public string? Description { get; private set; }
        public string? Gender { get; private set; }
        public string? Duration { get; private set; }
        public int MovieRating { get; private set; }
        public string? PublicId { get; private set; }
        public string? ImgUrl { get; private set; }
        public string? StatusMovie { get; private set; }
        public string? ImgUrlBackground { get; private set; }
        public string? PublicIdImgBackgound { get; private set; }

        public Movie()
        { 
        }

        public Movie(Guid id, string? title)
        {
            Id = id;
            Title = title;
        }

        public Movie(Guid id, string? title, string? imgUrl, int movieRating) : this(id, title)
        {
            ImgUrl = imgUrl;
            MovieRating = movieRating;
        }

        //public Movie(Guid id, string? title, string? description, string? gender, string? duration, int movieRating, string? imgUrl, string? statusMovie) : this(id, title)
        //{
        //    Description = description;
        //    Gender = gender;
        //    Duration = duration;
        //    MovieRating = movieRating;
        //    ImgUrl = imgUrl;
        //    StatusMovie = statusMovie;
        //}

        public Movie(Guid id, string? title, string? description, string? gender, string? duration, int movieRating, string? imgUrl, string? imgUrlBackground)
        {
            Id = id;
            Title = title;
            Description = description;
            Gender = gender;
            Duration = duration;
            MovieRating = movieRating;
            ImgUrl = imgUrl;
            ImgUrlBackground = imgUrlBackground;
        }

        public void SetImgUrlPublicId(string imgUrl, string publicId)
        {
            ImgUrl = imgUrl;
            PublicId = publicId;
        }

        public void SetImgBackgroundPublicId(string imgUrlBackground, string publicIdImgBackgound)
        {
            ImgUrlBackground = imgUrlBackground;
            PublicIdImgBackgound = publicIdImgBackgound;
        }
    }
}