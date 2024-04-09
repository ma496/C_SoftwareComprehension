using Capture;
using System.Runtime.InteropServices;

var frameGrabber = new FrameGrabber();
IValueReporter valueReporter = new ValueReporter();

using (var frameProcessor = new FrameCalculateAndStream(frameGrabber, valueReporter))
{
    // Simulate frame reception
    IntPtr framePtr = Marshal.AllocHGlobal(100 * 100); // Assuming frame size is 100x100
    frameGrabber.FrameReceived(framePtr, 100, 100);

    // Start streaming
    frameProcessor.StartStreaming();

    // Simulate receiving more frames
    // frameGrabber.FrameReceived(newFramePtr, width, height);
}