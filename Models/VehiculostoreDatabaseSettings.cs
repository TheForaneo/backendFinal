namespace apiweb.Models{
    public class VehiculostoreDatabaseSettings : IVehiculostoreDatabaseSettings
    {
        public string VehiculoCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
    public interface IVehiculostoreDatabaseSettings{
        string VehiculoCollectionName{get;set;}
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}