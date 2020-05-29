namespace apiweb.Models{
    public class ComentariostoreDatabaseSettings : IComentariostoreDatabaseSettings
    {
        public string ComentariosCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
    public interface IComentariostoreDatabaseSettings{
        string ComentariosCollectionName{get;set;}
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}