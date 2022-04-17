using System.Text;

namespace Common.Infrastructure.Utility.EF;

public class EFConnectionStringsBuilder
{
    public static EFConnectionStringsBuilder Builder => new();

    private EFConnectionStringsBuilder()
    {
    }

    private string? Server { get; set; }
    private string? Database { get; set; }
    private string? Port { get; set; }
    private string? User { get; set; }
    private string? Password { get; set; }

    public EFConnectionStringsBuilder SetServer(string? server)
    {
        Server = server;
        return this;
    }

    public EFConnectionStringsBuilder SetDatabase(string? database)
    {
        Database = database;
        return this;
    }

    public EFConnectionStringsBuilder SetPort(string? port)
    {
        Port = port;
        return this;
    }

    public EFConnectionStringsBuilder SetUser(string? user)
    {
        User = user;
        return this;
    }

    public EFConnectionStringsBuilder SetPassword(string? password)
    {
        Password = password;
        return this;
    }

    public string Build()
    {
        StringBuilder stringBuilder = new();

        if (Server is not null)
            stringBuilder.Append($"server={Server};");

        if (Database is not null)
            stringBuilder.Append($"database={Database};");

        if (Port is not null)
            stringBuilder.Append($"port={Port};");

        if (User is not null)
            stringBuilder.Append($"user={User};");

        if (Password is not null)
            stringBuilder.Append($"password={Password};");

        return stringBuilder.ToString();
    }
}