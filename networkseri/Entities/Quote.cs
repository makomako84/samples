using System.Text;

namespace Entities;

public class Quote : Entities.ISerializable<Quote>
{
    private int _id;
    private string _message;
    private int _length;
    private int _height;
    

    public int Id => _id;
    public string Message => _message;
    public int Length => _length;
    public int Height => _height;

    public Quote()
    {

    }

    public Quote(int id, string message, int length, int height)
    {
        _id = id;
        _message = message;
        _length = length;
        _height = height;
    }

    public override string ToString()
    {
        return $"{_id}: {_message}; {_height}*{_length}";
    }

    ISerializer<Quote> ISerializable<Quote>.GetSerializer()
    {
        return new QuoteSerializer();
    }
}

public class QuoteSerializer : ISerializer<Quote>
{
    public Quote Deserialize(byte[] data, int bytesRead)
    {
        System.Console.WriteLine($"Length:{data.Length}, Read: {bytesRead}");
        byte[] idBytes = new byte[sizeof(int)];
        int offset = 0;
        Array.Copy(data, 0, idBytes, offset, sizeof(int));
        int id = BitConverter.ToInt32(idBytes, 0);
        offset += sizeof(int);

        int nameBytesCount = bytesRead - offset - sizeof(int) - sizeof(int);
        string message = Encoding.UTF8.GetString(data, offset, nameBytesCount);
        offset += message.Length;

        byte[] lengthBytes = new byte[sizeof(int)];
        Array.Copy(data, offset, lengthBytes, 0, sizeof(int));
        int length = BitConverter.ToInt32(lengthBytes, 0);
        offset += lengthBytes.Length;

        byte[] heightBytes = new byte[sizeof(int)];
        Array.Copy(data, offset, heightBytes, 0, sizeof(int));
        int height = BitConverter.ToInt32(heightBytes, 0);

        return new Quote(id, message, length, height);
    }

    public byte[] Serialize(Quote obj)
    {
        byte[] idBytes = BitConverter.GetBytes(obj.Id);
        byte[] messageBytes = Encoding.UTF8.GetBytes(obj.Message);
        byte[] lengthBytes = BitConverter.GetBytes(obj.Length);
        byte[] heightBytes = BitConverter.GetBytes(obj.Height);


        int resLength = messageBytes.Length + idBytes.Length + lengthBytes.Length + heightBytes.Length;
        byte[] result = new byte[resLength];

        int offset = 0;
        Array.Copy(idBytes, 0, result, offset, idBytes.Length);
        offset += idBytes.Length;

        try
        {
            Array.Copy(messageBytes, 0, result, offset, messageBytes.Length);
            offset += messageBytes.Length;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error, when copying bytes of string message: {e}");
        }


        Array.Copy(lengthBytes, 0, result, offset, lengthBytes.Length);
        offset += lengthBytes.Length;

        Array.Copy(heightBytes, 0, result, offset, heightBytes.Length);
        return result;
    }
}


public interface ISerializer<T>
{
    public byte[] Serialize(T obj);
    public T Deserialize(byte[] data, int bytesRead);
}

public interface ISerializable<T>
{
    public ISerializer<T> GetSerializer();
}

