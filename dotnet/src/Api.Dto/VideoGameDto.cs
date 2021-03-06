namespace KeepTrack.Api.Dto
{
    /// <summary>
    /// Video Game data transfer object.
    /// </summary>
    public class VideoGameDto
    {
        /// <summary>
        /// Video Game ID.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Video Game title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Plaform.
        /// </summary>
        public string Platform { get; set; }

        /// <summary>
        /// Year (of the released date).
        /// </summary>
        public int Year { get; set; }
    }
}
