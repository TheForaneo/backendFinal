namespace apiweb.Services{
     public class SolicitudstoreDatabaseSettings : ISolicitudstoreDatabaseSettings
    {
        public string SolicitudCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
    public interface ISolicitudstoreDatabaseSettings{
        string SolicitudCollectionName{get;set;}
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}