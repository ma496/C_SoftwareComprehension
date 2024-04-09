using System.Runtime.InteropServices;

namespace Capture;

public class FrameGrabber : IFrameCallback, IDisposable
{
    private byte[] _buffer;
    public delegate void FrameUpdateHandler(Frame rawFrame);
    public event FrameUpdateHandler OnFrameUpdated;

    public void FrameReceived(IntPtr frame, int width, int height)
    {
        if (_buffer == null || _buffer.Length != width * height)
            _buffer = new byte[width * height];
        Marshal.Copy(frame, _buffer, 0, width * height);
        Frame bufferedFrame = new Frame(_buffer);
        OnFrameUpdated?.Invoke(bufferedFrame);
    }

    public void Dispose()
    {
        // Dispose any resources here if needed
    }
}
