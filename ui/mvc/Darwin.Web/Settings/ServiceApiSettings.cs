namespace Darwin.Web.Settings;

public class ServiceApiSettings
{
    public string BaseUrl { get; set; }
    public string AuthServer { get; set; }
    public ServiceApi Category { get; set; }
    public ServiceApi Mood { get; set; }
    public ServiceApi Content { get; set; }
    public ServiceApi Plan { get; set; }
    public ServiceApi Playlist { get; set; }
    public ServiceApi Auth { get; set; }
    public ServiceApi User { get; set; }

}
public class ServiceApi
{
    public string Path { get; set; }
}