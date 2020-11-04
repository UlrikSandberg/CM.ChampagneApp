using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Foundation;

namespace CM.ChampagneApp.iOS.IOSImplementations
{
    public class iOSDevice
    {
        List<string> iphonesWithNotch = new List<string>();

        public iOSDevice()
        {
            //iPhone X (2017)
            iphonesWithNotch.Add("iPhone10,3");
            iphonesWithNotch.Add("iPhone10,6");
            iphonesWithNotch.Add("iPhone11,2");
            iphonesWithNotch.Add("iPhone11,4");
            iphonesWithNotch.Add("iPhone11,6");
            iphonesWithNotch.Add("iPhone11,8");
        }

        public bool deviceHasNotch()
        {
            var device = CheckDeviceHardware("hw.machine");

            //Simulator
            if (device == "i386" || device == "x86_64")
            {
                var simulatorDevice = NSProcessInfo.ProcessInfo.Environment["SIMULATOR_MODEL_IDENTIFIER"].Description;
                if (iphonesWithNotch.Contains(simulatorDevice))
                {
                    return true;
                }
            }
            //Actual Device
            else if (iphonesWithNotch.Contains(device))
            {
                return true;
            }

            return false;
        }

        /*
         * From XLabs
         * https://github.com/XLabs/Xamarin-Forms-Labs/blob/master/src/Platform/XLabs.Platform.iOS/Device/AppleDevice.cs#L152
         */

        [DllImport("/usr/lib/libSystem.dylib")]
        internal static extern int sysctlbyname(
           [MarshalAs(UnmanagedType.LPStr)] string property,
           IntPtr output,
           IntPtr oldLen,
           IntPtr newp,
           uint newlen);


        public string CheckDeviceHardware(string property)
        {
            var pLen = Marshal.AllocHGlobal(sizeof(int));
            sysctlbyname(property, IntPtr.Zero, pLen, IntPtr.Zero, 0);
            var length = Marshal.ReadInt32(pLen);
            var pStr = Marshal.AllocHGlobal(length);
            sysctlbyname(property, pStr, pLen, IntPtr.Zero, 0);

            var result = Marshal.PtrToStringAnsi(pStr);
            Marshal.FreeHGlobal(pLen);
            Marshal.FreeHGlobal(pStr);
            return result;
        }
    }
}
