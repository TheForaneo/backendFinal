namespace apiweb.Models{
    public class AdminstoreDatabaseSettings : IAdminstoreDatabaseSettings
    {
        public string AdminCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
    public interface IAdminstoreDatabaseSettings{
        string AdminCollectionName{get;set;}
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}