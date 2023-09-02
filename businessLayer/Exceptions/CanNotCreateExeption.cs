using System.Runtime.Serialization;

namespace businessLayer.exeptions;

[Serializable]
public class CanNotCreateExeption : Exception
{
    public string DependentEntityName { get; }

    public CanNotCreateExeption()
    {
    }

    public CanNotCreateExeption(string? message, string DependentEntityName) : base(message)
    {
        this.DependentEntityName=DependentEntityName;
    }

    public CanNotCreateExeption(string? message) : base(message)
    {
    }

    public CanNotCreateExeption(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected CanNotCreateExeption(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}