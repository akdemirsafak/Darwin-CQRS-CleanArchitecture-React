using System.Text.Json.Serialization;

namespace Darwin.Share.Dtos;

public class DarwinResponse<T>
{
    public T Data { get; set; }
    public List<string> Errors { get; set; }
    [JsonIgnore] public int StatusCode { get; set; }

    //Static Factory Method
    public static DarwinResponse<T> Success(T Data, int statusCode = 200)
    {
        return new DarwinResponse<T> { Data = Data, StatusCode = statusCode };
    }

    public static DarwinResponse<T> Success(int statusCode = 200)
    {
        return new DarwinResponse<T> { Data = default, StatusCode = statusCode };
    }

    public static DarwinResponse<T> Fail(List<string> errors, int statusCode = 0)
    {
        return new DarwinResponse<T> { Data = default, Errors = errors, StatusCode = statusCode };
    }

    public static DarwinResponse<T> Fail(string error, int statusCode = 0)
    {
        return new DarwinResponse<T> { Data = default, Errors = new List<string> { error }, StatusCode = statusCode };
    }
}
