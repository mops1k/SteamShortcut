using System.Text;

namespace VDFMapper.VDF;

public class VDFStream
{
    private readonly BinaryReader reader;

    public VDFStream(string path) => reader = new BinaryReader(new FileStream(path, FileMode.Open));

    public void Close() => reader.Close();

    public string? ReadString()
    {
        List<byte> text = new();
        while (true)
        {
            byte c = ReadByte();
            if (c == 0)
            {
                break;
            }

            text.Add(c);
        }

        return Encoding.UTF8.GetString(text.ToArray());
    }

    public uint ReadInteger() => reader.ReadUInt32();

    public byte ReadByte() => reader.ReadByte();
}
