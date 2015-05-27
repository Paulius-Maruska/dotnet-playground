using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms_length
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] texts = new string[] {
                String.Format("{0,140}", "asdf"),//GSM7
                String.Format("{0,160}", "asdf"),//GSM7
                String.Format("{0,180}", "asdf"),//GSM7
                String.Format("{0,70}", "ąčęė"),//UCS2
                String.Format("{0,80}", "ąčęė"),//UCS2
                String.Format("{0,90}", "ąčęė"),//UCS2
            };

            for (int i = 0; i < texts.Length; i++)
            {
                Console.WriteLine("texts[{0}](Len: {1}) -> {2}", i, texts[i].Length, GSM7.NumberOfSMS(texts[i]));
            }

        }
    }
}
