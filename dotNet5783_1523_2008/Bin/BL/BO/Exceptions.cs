using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BO;



[Serializable]
internal class NegativeID : Exception
{
    public NegativeID() : base() { }
    public NegativeID(string message) : base(message) { }
    public NegativeID(string message, Exception inner) : base(message, inner) { }
    protected NegativeID(SerializationInfo info, StreamingContext context) : base(info, context) { }
}




[Serializable]
internal class EmptyName : Exception
{
    public EmptyName() : base() { }
    public EmptyName(string message) : base(message) { }
    public EmptyName(string message, Exception inner) : base(message, inner) { }
    protected EmptyName(SerializationInfo info, StreamingContext context) : base(info, context) { }
}




[Serializable]
internal class NegativePrice : Exception
{
    public NegativePrice() : base() { }
    public NegativePrice(string message) : base(message) { }
    public NegativePrice(string message, Exception inner) : base(message, inner) { }
    protected NegativePrice(SerializationInfo info, StreamingContext context) : base(info, context) { }
}




[Serializable]
internal class NegativeAmount : Exception
{
    public NegativeAmount() : base() { }
    public NegativeAmount(string message) : base(message) { }
    public NegativeAmount(string message, Exception inner) : base(message, inner) { }
    protected NegativeAmount(SerializationInfo info, StreamingContext context) : base(info, context) { }
}




[Serializable]
internal class ReadObjectFailed : Exception
{
    public ReadObjectFailed() : base() { }
    public ReadObjectFailed(string message) : base(message) { }
    public ReadObjectFailed(string message, Exception inner) : base(message, inner) { }
    protected ReadObjectFailed(SerializationInfo info, StreamingContext context) : base(info, context) { }
}




[Serializable]
internal class CreateObjectFailed : Exception
{
    public CreateObjectFailed() : base() { }
    public CreateObjectFailed(string message) : base(message) { }
    public CreateObjectFailed(string message, Exception inner) : base(message, inner) { }
    protected CreateObjectFailed(SerializationInfo info, StreamingContext context) : base(info, context) { }
}



[Serializable]
internal class UpdateObjectFailed : Exception
{
    public UpdateObjectFailed() : base() { }
    public UpdateObjectFailed(string message) : base(message) { }
    public UpdateObjectFailed(string message, Exception inner) : base(message, inner) { }
    protected UpdateObjectFailed(SerializationInfo info, StreamingContext context) : base(info, context) { }
}



[Serializable]
internal class ObjectNotExist : Exception
{
    public ObjectNotExist() : base() { }
    public ObjectNotExist(string message) : base(message) { }
    public ObjectNotExist(string message, Exception inner) : base(message, inner) { }
    protected ObjectNotExist(SerializationInfo info, StreamingContext context) : base(info, context) { }
}





[Serializable]
internal class ProductFoundInOrder : Exception
{
    public ProductFoundInOrder() : base() { }
    public ProductFoundInOrder(string message) : base(message) { }
    public ProductFoundInOrder(string message, Exception inner) : base(message, inner) { }
    protected ProductFoundInOrder(SerializationInfo info, StreamingContext context) : base(info, context) { }
}