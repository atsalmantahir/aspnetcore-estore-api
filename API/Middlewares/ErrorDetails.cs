using Newtonsoft.Json;

namespace API.Middlewares
{
    internal class ErrorDetails
    {
        public ErrorDetails()
        {
        }

        public string Message { get; set; }
        public int StatusCode { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}