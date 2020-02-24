using System;

namespace KeepTrack.Api.IntegrationTests.Firebase
{
    public class FirebaseConfiguration
    {
        public string ApplicationKey => Environment.GetEnvironmentVariable("Authentication__Application__Key");

        public string Username => Environment.GetEnvironmentVariable("Authentication__Username");

        public string Password => Environment.GetEnvironmentVariable("Authentication__Password");
    }
}
