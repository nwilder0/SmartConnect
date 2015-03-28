using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartConnect
{
    class SCUtility
    {
        public static byte[] String2Bytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        public static string Bytes2String(byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char)];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }

        public static string String2HexStr(string str)
        {
            string hex="";
            char[] letters = str.ToCharArray();
            foreach (char letter in letters)
            {
                long val = Convert.ToInt64(letter);
                hex += String.Format("{0:X}", val);
            }
            return hex;
        }

        public static string HexStr2String(string hex)
        {
            string str="";
            hex = hex.Replace("-", "");
            byte[] bytes = new byte[hex.Length / 2];
            for (int i = 0; i < hex.Length; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }
            str = SCUtility.Bytes2String(bytes);
            return str;
        }

        public static string Bytes2MAC(byte[] bytes)
        {
            string mac = "";
            if (bytes.Length == 6)
            {
                foreach (byte value in bytes)
                {
                    string tmp = SCUtility.String2HexStr(Convert.ToChar(value).ToString());
                    if (tmp.Length == 1) tmp = "0" + tmp;
                    if (tmp.Length == 0) tmp = "00" + tmp;
                    mac += tmp + ":";
                }
                mac = mac.Substring(0, mac.Length - 1);
            }
            return mac;
        }

        public static byte[] MAC2Bytes(string mac)
        {
            byte[] bytes = new byte[6];
            string[] strBytes = mac.Split(':');
            for(int i=0; i<bytes.Length; i++)
            {
                bytes[i] = Convert.ToByte(strBytes[i], 16);
            }
            return bytes;
        }

        public static int RSSI2SignalPercent(int signalStrength)
        {
            int percent = 0;
            if (signalStrength > -100 && signalStrength < -50) percent = (int)((signalStrength + 100.0) / 50.0) * 100;
            if (signalStrength >= -50) percent = 100;
            return percent;
        }
    }
}
