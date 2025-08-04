namespace ProductServices.DB
{
    public class Connection
    {
        public Connection(string connectionstring) => ConnectionString = connectionstring;

        public string ConnectionString { get; set; }
    }
}
