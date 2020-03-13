using System;

namespace KeepTrack.Api.IntegrationTests.Firebase
{
    public class FirebaseConfiguration
    {
        public string ApplicationKey => Environment.GetEnvironmentVariable("Firebase__Application__Key");

        public string Username => Environment.GetEnvironmentVariable("Firebase__Username");

        public string Password => Environment.GetEnvironmentVariable("Firebase__Password");
    }
}
