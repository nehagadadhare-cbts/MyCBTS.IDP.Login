namespace MyCBTS.IDP.Login.Models.Api
{
    public class Scope
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string DisplayName { get; set; }
        public bool Emphasize { get; set; }
        public bool Enabled { get; set; }
        public string Name { get; set; }
        public bool Required { get; set; }
        public bool ShowInDiscoveryDocument { get; set; }
        public int Type { get; set; }

        public string Secret { get; set; }
    }
}