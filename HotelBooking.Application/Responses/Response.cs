namespace HotelBooking.Application.Responses;

public class Response<T>
{
    public bool Success { get; set; } = true;
    public string? Message { get; set; }
    public T? Data { get; set; }
    public Pagination? Pagination { get; set; }

    public static Response<T> Ok(T data) => new()
    {
        Data = data
    };

    public static Response<T> Ok(T data, Pagination pagination) => new()
    {
        Data = data,
        Pagination = pagination
    };
    public static Response<T> Fail(string message) => new()
    {
        Success = false,
        Message = message
    };
}
