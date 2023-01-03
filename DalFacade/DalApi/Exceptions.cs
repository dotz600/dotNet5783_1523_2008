
using System.Runtime.Serialization;

namespace DalApi;


[Serializable]
public class ObjNotFoundException : Exception
{
    public ObjNotFoundException() : base() { }
    public ObjNotFoundException(string message) : base(message) { }
    public ObjNotFoundException(string message, Exception inner) : base(message, inner) { }
    protected ObjNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}

[Serializable]
public class XMLFileSaveLoadException : Exception
{
    public XMLFileSaveLoadException() : base() { }

    public XMLFileSaveLoadException(string message) : base(message) { }

    public XMLFileSaveLoadException(string message, Exception inner) : base(message, inner) { }

    public XMLFileSaveLoadException(SerializationInfo info, StreamingContext context) : base(info, context) { }

}

[Serializable]
public class ObjExistException : Exception
{
    public ObjExistException() : base() { }
    public ObjExistException(string message) : base(message) { }
    public ObjExistException(string message, Exception inner) : base(message, inner) { }
    protected ObjExistException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}

[Serializable]
public class DalConfigException : Exception
{
    public DalConfigException(string msg) : base(msg) { }
    public DalConfigException(string msg, Exception ex) : base(msg, ex) { }
}
