namespace Capture;

public interface IFrameCallback
{
    void FrameReceived(IntPtr pFrame, int pixelWidth, int pixelHeight);
}
