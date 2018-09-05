using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace DailyTasksManager.WinAPI
{
    // Availablity Request Enumerations and Constants
    public enum PowerRequestType
    {
        PowerRequestDisplayRequired = 0,
        PowerRequestSystemRequired,
        PowerRequestAwayModeRequired,
        PowerRequestMaximum
    }


    // Availablity Request Structures
    // Note:  Windows defines the POWER_REQUEST_CONTEXT structure with an
    // internal union of SimpleReasonString and Detailed information.
    // To avoid runtime interop issues, this version of 
    // POWER_REQUEST_CONTEXT only supports SimpleReasonString.  
    // To use the detailed information,
    // define the PowerCreateRequest function with the first 
    // parameter of type POWER_REQUEST_CONTEXT_DETAILED.
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct POWER_REQUEST_CONTEXT
    {
        public UInt32 Version;
        public UInt32 Flags;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string
            SimpleReasonString;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct PowerRequestContextDetailedInformation
    {
        public IntPtr LocalizedReasonModule;
        public UInt32 LocalizedReasonId;
        public UInt32 ReasonStringCount;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string[] ReasonStrings;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct POWER_REQUEST_CONTEXT_DETAILED
    {
        public UInt32 Version;
        public UInt32 Flags;
        public PowerRequestContextDetailedInformation DetailedInformation;
    }

    public enum EXECUTION_STATE : uint
    {
        ES_AWAYMODE_REQUIRED = 0x00000040,
        ES_CONTINUOUS = 0x80000000,
        ES_DISPLAY_REQUIRED = 0x00000002,
        ES_SYSTEM_REQUIRED = 0x00000001
    }

    public class PowerManager
    {
        const int POWER_REQUEST_CONTEXT_VERSION = 0;
        const int POWER_REQUEST_CONTEXT_SIMPLE_STRING = 0x1;
        const int POWER_REQUEST_CONTEXT_DETAILED_STRING = 0x2;

        POWER_REQUEST_CONTEXT _PowerRequestContext;
        IntPtr _PowerRequest; //HANDLE

        // Availability Request Functions
        [DllImport("kernel32.dll")]
        static extern IntPtr PowerCreateRequest(ref POWER_REQUEST_CONTEXT Context);

        [DllImport("kernel32.dll")]
        static extern bool PowerSetRequest(IntPtr PowerRequestHandle, PowerRequestType RequestType);

        [DllImport("kernel32.dll")]
        static extern bool PowerClearRequest(IntPtr PowerRequestHandle, PowerRequestType RequestType);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        internal static extern int CloseHandle(IntPtr hObject);


        /// <summary>
        /// Prevent screensaver, display dimming and power saving. This function wraps PInvokes on Win32 API. 
        /// </summary>
        /// <param name="enableConstantDisplayAndPower">True to get a constant display and power - False to clear the settings</param>
        public void EnableConstantDisplayAndPower(bool enableConstantDisplayAndPower)
        {
            if (enableConstantDisplayAndPower)
            {
                // Set up the diagnostic string
                _PowerRequestContext.Version = POWER_REQUEST_CONTEXT_VERSION;
                _PowerRequestContext.Flags = POWER_REQUEST_CONTEXT_SIMPLE_STRING;
                _PowerRequestContext.SimpleReasonString = "Continuous measurement"; // your reason for changing the power settings;

                // Create the request, get a handle
                _PowerRequest = PowerCreateRequest(ref _PowerRequestContext);

                // Set the request
                PowerSetRequest(_PowerRequest, PowerRequestType.PowerRequestSystemRequired);
                PowerSetRequest(_PowerRequest, PowerRequestType.PowerRequestDisplayRequired);
            }
            else
            {
                // Clear the request
                PowerClearRequest(_PowerRequest, PowerRequestType.PowerRequestSystemRequired);
                PowerClearRequest(_PowerRequest, PowerRequestType.PowerRequestDisplayRequired);

                CloseHandle(_PowerRequest);
            }
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern EXECUTION_STATE SetThreadExecutionState(EXECUTION_STATE esFlags);
    }





}
