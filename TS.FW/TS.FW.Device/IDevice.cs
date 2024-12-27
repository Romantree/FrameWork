namespace TS.FW.Device
{
    public interface IDevice
    {
        bool IsOpen { get; }

        Response Open();

        Response Close();
    }
}
