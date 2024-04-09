namespace Capture;

public class Frame : IDisposable
{
    private bool _disposed;
    private byte[] _rawBuffer;

    public Frame(byte[] raw)
    {
        _rawBuffer = raw;
    }

    public byte[] GetRawData()
    {
        if (_disposed)
            throw new ObjectDisposedException("underlying buffer has changed, should not be used anymore");
        return _rawBuffer;
    }

    public void Dispose()
    {
        if (!_disposed)
        {
            _disposed = true;
            _rawBuffer = null;
        }
    }
}
