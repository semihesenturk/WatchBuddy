using System.Text.Json.Serialization;

namespace WatchBuddy.Shared.Dtos;

public class BaseServiceResponse<T>
{
    public T Data { get; private set; }
    [JsonIgnore] public int StatusCode { get; private set; }
    [JsonIgnore] public bool IsSuccess { get; private set; }
    public List<string> Errors { get; private set; }

    //static factory method
    public static BaseServiceResponse<T> Success(T data, int statusCode)
    {
        return new BaseServiceResponse<T>
        {
            Data = data,
            StatusCode = statusCode,
            IsSuccess = true
        };
    }

    public static BaseServiceResponse<T> Success(int statusCode)
    {
        return new BaseServiceResponse<T>
        {
            Data = default(T),
            StatusCode = statusCode,
            IsSuccess = true
        };
    }

    public static BaseServiceResponse<T> Fail(List<string> errors, int statusCode)
    {
        return new BaseServiceResponse<T>
        {
            Errors = errors,
            StatusCode = statusCode,
            IsSuccess = false
        };
    }

    public static BaseServiceResponse<T> Fail(string errors, int statusCode)
    {
        return new BaseServiceResponse<T>
        {
            Errors = [errors],
            StatusCode = statusCode
        };
    }
}