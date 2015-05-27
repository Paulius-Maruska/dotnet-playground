/*
 * EasySMPP - SMPP protocol library for fast and easy
 * SMSC(Short Message Service Centre) client development
 * even for non-telecom guys.
 *
 * Easy to use classes covers all needed functionality
 * for SMS applications developers and Content Providers.
 *
 * Written for .NET 2.0 in C#
 *
 * Copyright (C) 2006 Balan Andrei, http://balan.name
 *
 * Licensed under the terms of the GNU Lesser General Public License:
 * 		http://www.opensource.org/licenses/lgpl-license.php
 *
 * For further information visit:
 * 		http://easysmpp.sf.net/
 *
 *
 * "Support Open Source software. What about a donation today?"
 *
 *
 * File Name: Tools.cs
 *
 * File Authors:
 * 		Balan Name, http://balan.name
 */

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace sms_length
{
    public class GSM7
    {
        // The index of the character in the string represents the index
        // of the character in the respective character set

        // Basic Character Set
        private const string BASIC_SET =
                "@£$¥èéùìòÇ\nØø\rÅåΔ_ΦΓΛΩΠΨΣΘΞ\x1bÆæßÉ !\"#¤%&'()*+,-./0123456789:;<=>?" +
                "¡ABCDEFGHIJKLMNOPQRSTUVWXYZÄÖÑÜ`¿abcdefghijklmnopqrstuvwxyzäöñüà";

        // Basic Character Set Extension
        private const string EXTENSION_SET =
                "````````````````````^```````````````````{}`````\\````````````[~]`" +
                "|````````````````````````````````````€``````````````````````````";

        // If the character is in the extension set, it must be preceded
        // with an 'ESC' character whose index is '27' in the Basic Character Set
        private const int ESC_INDEX = 27;

        public static int GSM7Length(string text)
        {
            int len = 0;
            for (int i = 0; i < text.Length; i++)
            {
                int index = BASIC_SET.IndexOf(text[i]);
                if (index >= 0)
                    len += 1;
                else
                {
                    index = EXTENSION_SET.IndexOf(text[i]);
                    if (index >= 0)
                        len += 2;
                    else
                        throw new ArgumentOutOfRangeException("Text is not GSM7 compatible.");
                }
            }
            return len;
        }

        public static int NumberOfSMS(string text)
        {
            int len = text.Length * 2;
            int maxLen = 140;
            int div = 134;
            try
            {
                len = GSM7Length(text);
                maxLen = 160;
                div = 153;
            }
            catch (ArgumentOutOfRangeException) { }

            int msgnum = 1;

            if (len > maxLen)
            {
                msgnum = len / div;
                if (div * msgnum < len)
                    msgnum += 1;
            }

            return msgnum;
        }
    }
}
