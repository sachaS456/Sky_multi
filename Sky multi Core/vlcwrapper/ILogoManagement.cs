namespace Sky_multi_Core.VlcWrapper
{
    public interface ILogoManagement
    {
        bool Enabled { get; set; }
        string File { set; }
        int X { get; set; }
        int Y { get; set; }
        int Delay { get; set; }
        int Opacity { get; set; }
        int Position { get; set; }
    }
}
