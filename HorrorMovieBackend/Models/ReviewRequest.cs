namespace HorrorMovieBackend.Models
{
	public class ReviewRequest
	{
		public string Content { get; set; } = string.Empty;
		public int Rating { get; set; }
		public int MovieId { get; set; }
	}
}