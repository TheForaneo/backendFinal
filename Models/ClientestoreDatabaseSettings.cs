namespace apiweb.Models{
    public class ClientestoreDatabaseSettings : IClientestoreDatabaseSettings
    {
        public string ClienteCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
    public interface IClientestoreDatabaseSettings{
        string ClienteCollectionName{get;set;}
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}