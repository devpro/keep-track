namespace KeepTrack.BlazorWebAssemblyApp.Models
{
    public class UserModel
    {
        public required string EmailAddress { get; set; }

        public required string DisplayName { get; set; }

        public required string Token { get; set; }
    }
}
