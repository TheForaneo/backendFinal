namespace apiweb.Models{
    public class MensajeriastoreDatabaseSettings : IMensajeriastoreDatabaseSettings{
        public string MensajeriaCollectionName{get;set;}
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
    public interface IMensajeriastoreDatabaseSettings{
        string MensajeriaCollectionName{get;set;}
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}