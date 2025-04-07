using VDFMapper.VDF;

namespace VDFMapper.ShortcutMap;

public class ShortcutRoot
{
    public ShortcutRoot(VDFMap root) => this.root = root;

    public VDFMap root { get; }
    private VDFMap GetShortcutMap() => root.GetValue("shortcuts").ToMap();

    public int GetSize() => GetShortcutMap().GetSize();

    public ShortcutEntry? GetEntry(int entry) => new(GetShortcutMap().ToMap().GetValue(entry.ToString()).ToMap());

    public ShortcutEntry? AddEntry()
    {
        VDFMap entry = new();
        entry.FillWithDefaultShortcutEntry();

        GetShortcutMap().Map.Add(GetSize().ToString(), entry);
        return new ShortcutEntry(entry);
    }

    public void RemoveEntry(int idx) => GetShortcutMap().RemoveFromArray(idx);
}
