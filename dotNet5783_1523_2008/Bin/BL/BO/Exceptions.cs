using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BO;



[Serializable]
internal class NegativeIDException : Exception
{
    public NegativeIDException() : base() { }
    public NegativeIDException(string message) : base(message) { }
    public NegativeIDException(string message, Exception inner) : base(message, inner) { }
    protected NegativeIDException(SerializationInfo info, StreamingContext context) : base(info, context) { }

    override public string ToString() =>
    "Negative ID Exception:" + " overloaded\n" + Message;
}




[Serializable]
internal class EmptyNameException : Exception
{
    public EmptyNameException() : base() { }
    public EmptyNameException(string message) : base(message) { }
    public EmptyNameException(string message, Exception inner) : base(message, inner) { }
    protected EmptyNameException(SerializationInfo info, StreamingContext context) : base(info, context) { }
   
    override public string ToString() =>
    "EmptyName Exception:"  + " overloaded\n" + Message;
}




[Serializable]
internal class NegativePriceException : Exception
{
    public NegativePriceException() : base() { }
    public NegativePriceException(string message) : base(message) { }
    public NegativePriceException(string message, Exception inner) : base(message, inner) { }
    protected NegativePriceException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    override public string ToString() =>
    "Negative Price Exception:" + " overloaded\n" + Message;
}




[Serializable]
internal class NegativeAmountException : Exception
{
    public NegativeAmountException() : base() { }
    public NegativeAmountException(string message) : base(message) { }
    public NegativeAmountException(string message, Exception inner) : base(message, inner) { }
    protected NegativeAmountException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    override public string ToString() =>
    "Negative Amount Exception:" + " overloaded\n" + Message;
}




[Serializable]
internal class ReadObjectFailedException : Exception
{
    public ReadObjectFailedException() : base() { }
    public ReadObjectFailedException(string message) : base(message) { }
    public ReadObjectFailedException(string message, Exception inner) : base(message, inner) { }
    protected ReadObjectFailedException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    override public string ToString() =>
    "Read Object Failed Exception:" + " overloaded\n" + Message;
}




[Serializable]
internal class CreateObjectFailedException : Exception
{
    public CreateObjectFailedException() : base() { }
    public CreateObjectFailedException(string message) : base(message) { }
    public CreateObjectFailedException(string message, Exception inner) : base(message, inner) { }
    protected CreateObjectFailedException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    override public string ToString() =>
    "Create Object Failed Exception:" + " overloaded\n" + Message;
}



[Serializable]
internal class UpdateObjectFailedException : Exception
{
    public UpdateObjectFailedException() : base() { }
    public UpdateObjectFailedException(string message) : base(message) { }
    public UpdateObjectFailedException(string message, Exception inner) : base(message, inner) { }
    protected UpdateObjectFailedException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    override public string ToString() =>
    "Update Object Failed Exception:" + " overloaded\n" + Message;
}



[Serializable]
internal class ObjectNotExistException : Exception
{
    public ObjectNotExistException() : base() { }
    public ObjectNotExistException(string message) : base(message) { }
    public ObjectNotExistException(string message, Exception inner) : base(message, inner) { }
    protected ObjectNotExistException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    override public string ToString() =>
    "Object Not Exist Exception:" + " overloaded\n" + Message;
}





[Serializable]
internal class ProductFoundInOrderException : Exception
{
    public ProductFoundInOrderException() : base() { }
    public ProductFoundInOrderException(string message) : base(message) { }
    public ProductFoundInOrderException(string message, Exception inner) : base(message, inner) { }
    protected ProductFoundInOrderException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    override public string ToString() =>
    "Product Found In Order Exception:" + " overloaded\n" + Message;
}