namespace YourCorporation.Shared.Abstractions.Messaging
{
    public class RabbitMQOptions
    {
        public const string SectionName = "RabbitMQ";

        public string Username { get; set; }

        public string Password { get; set; }

        public string HostName { get; set; }

        public string VirtualHost { get; set; }
    }
}
