using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace byte_convert
{
    class Program
    {
        static string getHex(byte[] buf)
        {
            StringBuilder hex = new StringBuilder(buf.Length * 2);
            foreach (byte b in buf)
                hex.AppendFormat("{0:x2} ", b);
            return hex.ToString().Trim();
        }

        static void prtInt(byte[] buf)
        {
            byte[] b = new byte[buf.Length];
            Array.Copy(buf, b, buf.Length);

            if (BitConverter.IsLittleEndian)
                Array.Reverse(b); 

            switch (buf.Length)
            {
                case 1:
                    Console.WriteLine("{0} => {1}", getHex(buf), (int)(b[0]));
                    break;
                case 2:
                    Console.WriteLine("{0} => {1}", getHex(buf), BitConverter.ToUInt16(b, 0));
                    break;
                case 4:
                    Console.WriteLine("{0} => {1}", getHex(buf), BitConverter.ToUInt32(b, 0));
                    break;
                case 8:
                    Console.WriteLine("{0} => {1}", getHex(buf), BitConverter.ToUInt64(b, 0));
                    break;
                default:
                    Console.WriteLine("Invalid buf '{1}' length: {0}", buf.Length, getHex(buf));
                    break;
            }

        }

        static void Main(string[] args)
        {
            Console.WriteLine("System is {0}-endian", BitConverter.IsLittleEndian ? "Little" : "Big");
            prtInt(new byte[] { 0x01 });
            prtInt(new byte[] { 0x01, 0x23 });
            prtInt(new byte[] { 0x01, 0x23, 0x45 });
            prtInt(new byte[] { 0x01, 0x23, 0x45, 0x67 });
            prtInt(new byte[] { 0x01, 0x23, 0x45, 0x67, 0x89 });
            prtInt(new byte[] { 0x01, 0x23, 0x45, 0x67, 0x89, 0xAB });
            prtInt(new byte[] { 0x01, 0x23, 0x45, 0x67, 0x89, 0xAB, 0xCD });
            prtInt(new byte[] { 0x01, 0x23, 0x45, 0x67, 0x89, 0xAB, 0xCD, 0xEF });
        }
    }
}
