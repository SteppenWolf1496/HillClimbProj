using UnityEngine;

#if UNITY_IPHONE
using UnityEngine.iOS;
#endif

public abstract class ISystemInfo
{
    private static ISystemInfo _instance = null;

    public static ISystemInfo Get
    {
        get
        {
            if (_instance != null) {
                return _instance;
            }

#if UNITY_EDITOR || UNITY_STANDALONE
            _instance = new DesctopSystemInfo();
#elif UNITY_ANDROID
            _instance = new AndroidSystemInfo();
#elif UNITY_IPHONE
            _instance = new IOSSystemInfo();
#endif
            return _instance;
        }
    }

    public abstract bool IsLowDevice { get; }
}

public sealed class DesctopSystemInfo : ISystemInfo
{
    public override bool IsLowDevice { get { return false; } }
}

#if UNITY_ANDROID

public sealed class AndroidSystemInfo : ISystemInfo
{
    private bool _isLowDevice = false;

    public override bool IsLowDevice { get { return _isLowDevice; } }

    public AndroidSystemInfo()
    {
        SetRangeDevice();
    }

    private void SetRangeDevice()
    {
        int processorCount = SystemInfo.processorCount;
        int processorFrequencyMhz = SystemInfo.processorFrequency;
        int systemMemorySize = SystemInfo.systemMemorySize;

        
        Log.Temp("[AndroidSystemInfo::SetRangeDevice] Device info: {{ 'processorCount':{0}, 'processorFrequency (MHz)':{1}, 'systemMemorySize':{2} }}", processorCount, processorFrequencyMhz, systemMemorySize);

        if (processorCount == 1) {
            _isLowDevice = true;
            return;
        }

        if (processorCount > 4) {
            _isLowDevice = false;
            return;
        }

        bool isLowMemorySizeOrFrequencyCPU = systemMemorySize <= 1024 || processorFrequencyMhz <= 1200;
        bool isLowScreenSize = Screen.width < 720 || Screen.height < 1280;
        if (processorCount == 2) {
            _isLowDevice = isLowMemorySizeOrFrequencyCPU == true && isLowScreenSize == false;
            return;
        }

        _isLowDevice = isLowMemorySizeOrFrequencyCPU == true;

        Log.Temp("[AndroidSystemInfo::SetRangeDevice] Device is low: {0}", _isLowDevice);
    }
}

#endif

#if UNITY_IPHONE

public sealed class IOSSystemInfo : ISystemInfo
{
    public override bool IsLowDevice { get { return Device.generation < DeviceGeneration.iPad4Gen; } }
}

#endif


