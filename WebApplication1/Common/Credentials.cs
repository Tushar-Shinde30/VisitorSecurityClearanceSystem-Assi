namespace WebApplication1.Common
{
    public class Credentials
    {
        public static readonly string DatabaseName = Environment.GetEnvironmentVariable("databaseName");
        public static readonly string ContainerName = Environment.GetEnvironmentVariable("containerName");
        public static readonly string CosmosEndpoint = Environment.GetEnvironmentVariable("cosmosUrl");
        public static readonly string PrimaryKey = Environment.GetEnvironmentVariable("primaryKey");
        public static string VisitorDocumenttype = "visitor";

        public static string ApiKey ="";
       
        
      
    }
}
