namespace ChallengeCacibNY.Api.Responses
{
    public class Response<T> : Response
    {
        public T Content { get; set; }
    }

    public class Response
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
