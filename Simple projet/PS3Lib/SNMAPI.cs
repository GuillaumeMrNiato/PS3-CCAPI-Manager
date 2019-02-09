namespace SNMAPINetLib
{
    using System;
    using System.Text;

    public class SNMAPI
    {
        private Extension _extension;
        private uint _freeSpace;
        private string _gamename;
        private string _ip;
        private uint _processID;
        private int _prxid;

        public SNMAPI()
        {
            FunctionsSNMAPI.InitFuncs();
            _extension = new SNMAPINetLib.Extension(this);
        }

        public bool Attach()
        {
            uint gameProcessID = GetGameProcessID();
            if (GetProcessName(gameProcessID).Contains("/game/"))
            {
                _processID = gameProcessID;
                return true;
            }
            return false;
        }

        public bool AutoconnectPS3()
        {
            FunctionsSNMAPI.DisconnectPS3();
            StringBuilder iP = new StringBuilder(20);
            if (success(FunctionsSNMAPI.AutoconnectPS3(iP)))
            {
                _ip = iP.ToString();
                return true;
            }
            return false;
        }

        public bool ConnectPS3(string IP)
        {
            FunctionsSNMAPI.DisconnectPS3();
            if (success(FunctionsSNMAPI.ConnectToPS3(IP + "\0")))
            {
                _ip = IP;
                return true;
            }
            return false;
        }

        public bool DisconnectPS3()
        {
            return success(FunctionsSNMAPI.DisconnectPS3());
        }

        ~SNMAPI()
        {
            FunctionsSNMAPI.DisconnectPS3();
        }

        public string GetConnectionType()
        {
            int type = 0;
            if (success(FunctionsSNMAPI.GetConnectionType(out type)))
            {
                if (type == 0)
                {
                    return "LAN";
                }
                if (type == 1)
                {
                    return "WLAN";
                }
            }
            return null;
        }

        public string GetFirmware()
        {
            StringBuilder firmware = new StringBuilder(15);
            if (!success(FunctionsSNMAPI.GetFirmware(firmware)))
            {
                return null;
            }
            return firmware.ToString();
        }

        public uint GetFreeMemory()
        {
            uint memory = 0;
            if (success(FunctionsSNMAPI.GetFreeMemory(out memory)))
            {
                return memory;
            }
            return 0;
        }

        public uint GetFreeSpace()
        {
            uint space = 0;
            if (success(FunctionsSNMAPI.GetFreeSpace(out space)))
            {
                return (_freeSpace = space);
            }
            return 0;
        }

        public uint GetGameProcessID()
        {
            uint processID = 0;
            if (success(FunctionsSNMAPI.GetGameProcessID(out processID)))
            {
                return processID;
            }
            return 0;
        }

        public bool GetMemory(uint offset, byte[] buffer)
        {
            return success(FunctionsSNMAPI.GetMemory(_processID, offset, (uint) buffer.Length, buffer));
        }

        public uint[] GetProcesses()
        {
            uint[] processes = new uint[0x10];
            if (success(FunctionsSNMAPI.GetProcesses(processes)))
            {
                return processes;
            }
            return new uint[0x10];
        }

        public string GetProcessName(uint processID)
        {
            StringBuilder name = new StringBuilder(0x200);
            if (success(FunctionsSNMAPI.GetProcessName(processID, name)))
            {
                return (_gamename = name.ToString());
            }
            return null;
        }
        public string GetVersion()
        {
            StringBuilder version = new StringBuilder(10);
            if (success(FunctionsSNMAPI.GetVersion(version)))
            {
                return version.ToString();
            }
            return "";
        }

        public bool LoadSPRX(string path)
        {
            return ((_processID != 0) && success(FunctionsSNMAPI.LoadSPRX(_processID, path, out _prxid)));
        }

        public bool NotifySmall(string message)
        {
            return success(FunctionsSNMAPI.NotifySmall(message + "\0"));
        }
        public bool SetIDPS(string idps)
        {
            return SetIDPS(FunctionsSNMAPI.StringToByteArray(idps));
        }

        public bool SetIDPS(byte[] idps)
        {
            return success(FunctionsSNMAPI.SetIDPS(idps));
        }

        public bool SetMemory(uint offset, byte[] buffer)
        {
            return success(FunctionsSNMAPI.SetMemory(_processID, offset, (uint) buffer.Length, buffer));
        }


        private bool success(int value)
        {
            return (value >= 0);
        }

        public SNMAPINetLib.Extension Extension
        {
            get
            {
                return _extension;
            }
        }

        public uint FreeSpace
        {
            get
            {
                return _freeSpace;
            }
        }

        public string Gamename
        {
            get
            {
                return _gamename;
            }
        }

        public string IP
        {
            get
            {
                return _ip;
            }
        }

        public uint ProcessID
        {
            get
            {
                return _processID;
            }
        }
    }
}

