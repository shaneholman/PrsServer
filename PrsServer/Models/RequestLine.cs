using System.Text.Json.Serialization;

namespace PrsServer.Models
{
    public class RequestLine{
        public int Id { get; set; }

        public int RequestId { get; set; }

        [JsonIgnore] //needed when you read the request it will get the virtual instance of the request only happens when you have a class with two virtual keys.
        public virtual Request? Request { get; set; }

        public int ProductId { get; set; }
        public virtual Product? Product { get; set; }

        public int Quantity { get; set; } = 1;
    }
}
