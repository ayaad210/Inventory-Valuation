using System.Runtime.Serialization;

namespace AbusinessLayer.exeptions;

[Serializable]
public class CanNotEvaluateExeption : Exception
{
   

    public CanNotEvaluateExeption()
    {
    }

    public CanNotEvaluateExeption(string? message) : base(message)
    {
    }

    public CanNotEvaluateExeption(List<int> ids, string type)
    {
      
    }

    public CanNotEvaluateExeption(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected CanNotEvaluateExeption(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}