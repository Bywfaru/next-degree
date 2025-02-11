namespace DegreePlanner.Services;

public class Response<T>
{
    public Response(T data)
    {
        Data = data;
        Error = null;
    }

    public Response(Exception ex)
    {
        Data = default;
        Error = ex.Message;
    }

    public T? Data { get; set; }
    public string? Error { get; set; }
}