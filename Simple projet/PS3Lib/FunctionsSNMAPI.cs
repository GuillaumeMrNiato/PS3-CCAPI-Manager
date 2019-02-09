namespace SNMAPINetLib
{
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Security.Cryptography;
    using System.Text;

    internal static class FunctionsSNMAPI
    {
        private static delegateAutoconnectPS3 _autoconnectPS3;
        private static delegateConnectToPS3 _connecttoPS3;
        private static delegateDisconnectPS3 _disconnectPS3;
        private static delegateGameFuncs _gamefuncs;
        private static delegateGetConnectionType _getconnectiontype;
        private static delegateGetFirmware _getfirmware;
        private static delegateGetFreeMemory _getfreememory;
        private static delegateGetFreeSpace _getfreespace;
        private static delegateGetGamePID _getgamepid;
        private static delegateGetMemory _getmemory;
        private static delegateGetProcesses _getprocesses;
        private static delegateGetProcessName _getprocessname;
        private static delegateGetTemperature _getTemp;
        private static delegateGetVersion _getversion;
        private static delegateLoadSPRX _loadsprx;
        private static delegateNotify _notify;
        private static delegateNotifySmall _notifysmall;
        private static delegatePlaySound _playsound;
        private static delegateRingBuzzer _ringbuzzer;
        private static delegateSetIDPS _setidps;
        private static delegateSetMemory _setmemory;
        private static delegateShutdown _shutdown;
        private static readonly string DLLHash = "8AAB09DD2C9F4162AFEBA813E0A2CFB885527BA0";
        private static IntPtr libModule = IntPtr.Zero;

        public static int AutoconnectPS3(StringBuilder IP)
        {
            return _autoconnectPS3(IP);
        }

        public static int ConnectToPS3(string IP)
        {
            return _connecttoPS3(IP);
        }

        public static int DisconnectPS3()
        {
            return _disconnectPS3();
        }

        public static int GameFuncs(int game_id, int option_id, int value)
        {
            return _gamefuncs(game_id, option_id, value);
        }

        public static int GetConnectionType(out int type)
        {
            return _getconnectiontype(out type);
        }

        public static int GetFirmware(StringBuilder firmware)
        {
            return _getfirmware(firmware);
        }

        public static int GetFreeMemory(out uint memory)
        {
            return _getfreememory(out memory);
        }

        public static int GetFreeSpace(out uint space)
        {
            return _getfreespace(out space);
        }

        public static int GetGameProcessID(out uint processID)
        {
            return _getgamepid(out processID);
        }

        public static int GetMemory(uint processID, uint offset, uint length, byte[] buffer)
        {
            return _getmemory(processID, offset, length, buffer);
        }

        [DllImport("kernel32.dll", CharSet=CharSet.Ansi, SetLastError=true, ExactSpelling=true)]
        private static extern IntPtr GetProcAddress(IntPtr hModule, string procName);
        public static int GetProcesses(uint[] processes)
        {
            return _getprocesses(processes);
        }

        public static int GetProcessName(uint processID, StringBuilder name)
        {
            return _getprocessname(processID, name);
        }

        private static string getSNMAPIPath()
        {
            return (Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.System)) + @"\SNMAPI\SNMAPIDLL.dll");
        }

        public static int GetTemperature(byte kernel, ref int temp)
        {
            return _getTemp(kernel, ref temp);
        }

        public static int GetVersion(StringBuilder version)
        {
            return _getversion(version);
        }

        public static void InitFuncs()
        {
            if (File.Exists(getSNMAPIPath()))
            {
                if (BitConverter.ToString(new SHA1CryptoServiceProvider().ComputeHash(File.ReadAllBytes(getSNMAPIPath()))).ToUpper().Replace("-", "").Equals(DLLHash))
                {
                    if ((libModule = LoadLibrary(getSNMAPIPath())) != IntPtr.Zero)
                    {
                        _connecttoPS3 = (delegateConnectToPS3) Marshal.GetDelegateForFunctionPointer(GetProcAddress(libModule, "SNMAPIConnectToPS3"), typeof(delegateConnectToPS3));
                        _disconnectPS3 = (delegateDisconnectPS3) Marshal.GetDelegateForFunctionPointer(GetProcAddress(libModule, "SNMAPIDisconnectPS3"), typeof(delegateDisconnectPS3));
                        _autoconnectPS3 = (delegateAutoconnectPS3) Marshal.GetDelegateForFunctionPointer(GetProcAddress(libModule, "SNMAPIAutoconnectPS3"), typeof(delegateAutoconnectPS3));
                        _notify = (delegateNotify) Marshal.GetDelegateForFunctionPointer(GetProcAddress(libModule, "SNMAPINotify"), typeof(delegateNotify));
                        _getversion = (delegateGetVersion) Marshal.GetDelegateForFunctionPointer(GetProcAddress(libModule, "SNMAPIGetVersion"), typeof(delegateGetVersion));
                        _ringbuzzer = (delegateRingBuzzer) Marshal.GetDelegateForFunctionPointer(GetProcAddress(libModule, "SNMAPIRingBuzzer"), typeof(delegateRingBuzzer));
                        _getTemp = (delegateGetTemperature) Marshal.GetDelegateForFunctionPointer(GetProcAddress(libModule, "SNMAPIGetTemperature"), typeof(delegateGetTemperature));
                        _shutdown = (delegateShutdown) Marshal.GetDelegateForFunctionPointer(GetProcAddress(libModule, "SNMAPIShutdown"), typeof(delegateShutdown));
                        _getfirmware = (delegateGetFirmware) Marshal.GetDelegateForFunctionPointer(GetProcAddress(libModule, "SNMAPIGetFirmware"), typeof(delegateGetFirmware));
                        _playsound = (delegatePlaySound) Marshal.GetDelegateForFunctionPointer(GetProcAddress(libModule, "SNMAPIPlaySound"), typeof(delegatePlaySound));
                        _setidps = (delegateSetIDPS) Marshal.GetDelegateForFunctionPointer(GetProcAddress(libModule, "SNMAPISetIDPS"), typeof(delegateSetIDPS));
                        _getmemory = (delegateGetMemory) Marshal.GetDelegateForFunctionPointer(GetProcAddress(libModule, "SNMAPIGetMemory"), typeof(delegateGetMemory));
                        _setmemory = (delegateSetMemory) Marshal.GetDelegateForFunctionPointer(GetProcAddress(libModule, "SNMAPISetMemory"), typeof(delegateSetMemory));
                        _getprocesses = (delegateGetProcesses) Marshal.GetDelegateForFunctionPointer(GetProcAddress(libModule, "SNMAPIGetProcesses"), typeof(delegateGetProcesses));
                        _getprocessname = (delegateGetProcessName) Marshal.GetDelegateForFunctionPointer(GetProcAddress(libModule, "SNMAPIGetProcessName"), typeof(delegateGetProcessName));
                        _loadsprx = (delegateLoadSPRX) Marshal.GetDelegateForFunctionPointer(GetProcAddress(libModule, "SNMAPILoadSPRX"), typeof(delegateLoadSPRX));
                        _getgamepid = (delegateGetGamePID) Marshal.GetDelegateForFunctionPointer(GetProcAddress(libModule, "SNMAPIGetGamePID"), typeof(delegateGetGamePID));
                        _getfreememory = (delegateGetFreeMemory) Marshal.GetDelegateForFunctionPointer(GetProcAddress(libModule, "SNMAPIGetFreeMemory"), typeof(delegateGetFreeMemory));
                        _notifysmall = (delegateNotifySmall) Marshal.GetDelegateForFunctionPointer(GetProcAddress(libModule, "SNMAPINotifySmall"), typeof(delegateNotifySmall));
                        _getfreespace = (delegateGetFreeSpace) Marshal.GetDelegateForFunctionPointer(GetProcAddress(libModule, "SNMAPIGetFreeSpace"), typeof(delegateGetFreeSpace));
                        _getconnectiontype = (delegateGetConnectionType) Marshal.GetDelegateForFunctionPointer(GetProcAddress(libModule, "SNMAPIGetConnectionType"), typeof(delegateGetConnectionType));
                        _gamefuncs = (delegateGameFuncs) Marshal.GetDelegateForFunctionPointer(GetProcAddress(libModule, "SNMAPIGameFuncs"), typeof(delegateGameFuncs));
                    }
                    else
                    {
                        Console.WriteLine("SNMAPI - Can not load SNMAPI Library");
                    }
                }
                else
                {
                    Console.WriteLine("SNMAPI - SNMAPI Library has wrong hash");
                }
            }
            else
            {
                Console.WriteLine("SNMAPI - The SNMAPI Library file does not exist");
            }
        }

        [DllImport("kernel32.dll")]
        private static extern IntPtr LoadLibrary(string dllName);
        public static int LoadSPRX(uint processID, string path, out int prxID)
        {
            return _loadsprx(processID, path, out prxID);
        }

        public static int Notify(byte texture, string message)
        {
            return _notify(texture, message);
        }

        public static int NotifySmall(string message)
        {
            return _notifysmall(message);
        }

        public static int PlaySound(byte soundnum)
        {
            return _playsound(soundnum);
        }

        public static int RingBuzzer(byte mode)
        {
            return _ringbuzzer(mode);
        }

        public static int SetIDPS(byte[] idps)
        {
            return _setidps(idps);
        }

        public static int SetMemory(uint processID, uint offset, uint length, byte[] data)
        {
            return _setmemory(processID, offset, length, data);
        }

        public static int Shutdown(byte mode)
        {
            return _shutdown(mode);
        }

        public static byte[] StringToByteArray(string hex)
        {
            int length = hex.Length;
            byte[] buffer = new byte[length / 2];
            for (int i = 0; i < length; i += 2)
            {
                buffer[i / 2] = Convert.ToByte(hex.Substring(i, 2), 0x10);
            }
            return buffer;
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int delegateAutoconnectPS3([Out] StringBuilder IP);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int delegateConnectToPS3(string IP);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int delegateDisconnectPS3();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int delegateGameFuncs(int game_id, int option_id, int value);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int delegateGetConnectionType(out int type);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int delegateGetFirmware([Out] StringBuilder firmware);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int delegateGetFreeMemory(out uint memory);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int delegateGetFreeSpace(out uint freeSpace);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int delegateGetGamePID(out uint processID);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int delegateGetMemory(uint processID, uint offset, uint length, [Out] byte[] memory);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int delegateGetProcesses([Out] uint[] processes);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int delegateGetProcessName(uint processID, [Out] StringBuilder name);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int delegateGetTemperature(byte kernel, ref int temp);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int delegateGetVersion([Out] StringBuilder version);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int delegateLoadSPRX(uint processID, string path, out int prxID);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int delegateNotify(byte texture, string message);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int delegateNotifySmall(string message);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int delegatePlaySound(byte soundnum);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int delegateRingBuzzer(byte mode);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int delegateSetIDPS(byte[] idps);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int delegateSetMemory(uint processID, uint offset, uint length, byte[] data);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int delegateShutdown(byte mode);
    }
}

