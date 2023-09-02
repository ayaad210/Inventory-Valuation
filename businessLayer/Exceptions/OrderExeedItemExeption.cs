using System.Runtime.Serialization;

namespace businessLayer.exeptions;

[Serializable]
public class OrderExceedItemExeption : Exception
{

    public OrderExceedItemExeption()
    {
    }

  

    public OrderExceedItemExeption(string? message) : base(message)
    {
    }

    public OrderExceedItemExeption(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected OrderExceedItemExeption(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}