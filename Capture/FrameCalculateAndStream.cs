using System.Timers;
using Timer = System.Timers.Timer;

namespace Capture;

public class FrameCalculateAndStream : IDisposable
{
    private IValueReporter _reporter;
    private Queue<Frame> _receivedFrames = new Queue<Frame>();
    private Timer _timer;

    public FrameCalculateAndStream(FrameGrabber fg, IValueReporter vr)
    {
        fg.OnFrameUpdated += HandleFrameUpdated;
        _timer = new Timer(1000 / 30);
        _timer.Elapsed += OnTimerElapsed;
        _reporter = vr;
    }

    private void HandleFrameUpdated(Frame frame)
    {
        lock (_receivedFrames)
        {
            _receivedFrames.Enqueue(frame);
        }
    }

    private void OnTimerElapsed(object sender, ElapsedEventArgs e)
    {
        Frame frame = null;
        lock (_receivedFrames)
        {
            if (_receivedFrames.Count > 0)
                frame = _receivedFrames.Dequeue();
        }
        if (frame != null)
        {
            byte[] raw = frame.GetRawData();
            double average = CalculateAverage(raw);
            _reporter.Report(average);
            frame.Dispose();
        }
    }

    private double CalculateAverage(byte[] data)
    {
        int sum = 0;
        foreach (byte value in data)
        {
            sum += value;
        }
        return (double)sum / data.Length;
    }

    public void StartStreaming()
    {
        _timer.Enabled = true;
    }

    public void Dispose()
    {
        _timer.Dispose();
        lock (_receivedFrames)
        {
            foreach (var frame in _receivedFrames)
            {
                frame.Dispose();
            }
            _receivedFrames.Clear();
        }
    }
}
