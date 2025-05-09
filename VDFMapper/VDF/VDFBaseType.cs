﻿namespace VDFMapper.VDF;

public abstract class VDFBaseType
{
    public VDFType Type { get; set; }
    public uint Integer { get; set; }
    public string? Text { get; set; }
    public Dictionary<string?, VDFBaseType> Map { get; protected set; }

    public abstract void Write(BinaryWriter writer, string? key);

    public VDFMap ToMap()
    {
        if (GetType() == typeof(VDFMap))
        {
            return (VDFMap)this;
        }

        return null;
    }
}
