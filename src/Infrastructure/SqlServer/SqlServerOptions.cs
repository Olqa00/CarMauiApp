namespace CarMauiApp.Infrastructure.SqlServer;

internal sealed class SqlServerOptions
{
    public string ConnectionString { get; set; }
    public static string StaticConnectionString { get; private set; }

    public void InitializeStaticConnection()
    {
        if (string.IsNullOrEmpty(this.ConnectionString) is false)
        {
            StaticConnectionString = this.ConnectionString;
        }
    }
}
