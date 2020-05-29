namespace apiweb.Services{
    public class TallerstoreDatabaseSettings : ITallerstoreDatabaseSettings{
        public string TallerCollectionName{get;set;}
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface ITallerstoreDatabaseSettings{
        string TallerCollectionName{get;set;}
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}