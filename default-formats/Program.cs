using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Reflection;

namespace default_formats
{
    class Program
    {
        public static string SolutionDir
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                path = Path.GetDirectoryName(path);

                if (path.EndsWith("bin\\Debug") || path.EndsWith("bin\\Release"))
                    path = Path.GetDirectoryName(Path.GetDirectoryName(path));

                return path;
            }
        }

        static FormattedValues values = new FormattedValues();

        static void Val(string code, object obj, string comment)
        {
            FormattedValue val = new FormattedValue(code, obj.GetType(), obj, comment);
            values.Add(val);
            Console.WriteLine("==== ==== ==== ==== ==== ==== ==== ====");
            Console.WriteLine("Code:    {0}", val.Code);
            Console.WriteLine("Type:    {0}", val.Type);
            Console.WriteLine("Value:   {0}", val.Value);
            Console.WriteLine("Comment: {0}", val.Comment);
        }

        static void UpdateFile(string filename)
        {
            string content = "";
            try
            {
                using (var f = File.OpenText(filename))
                {
                    content = f.ReadToEnd();
                }
            }
            catch (Exception) { }

            string startTag = "<!-- AUTO GEN -->\n";
            string endTag = "<!-- /AUTO GEN -->\n";

            int startTagPos = content.IndexOf(startTag);
            int endTagPos = content.IndexOf(endTag);

            if (startTagPos == -1)
            {
                content = string.Concat(content, startTag, values.ToString(), "\n", endTag);
            }
            else
            {
                content = content.Remove(startTagPos + startTag.Length, endTagPos - startTagPos - startTag.Length - 1);
                content = content.Insert(startTagPos + startTag.Length, values.ToString());
            }

            using (var f = File.CreateText(filename))
            {
                f.Write(content);
            }
        }

        static void Main(string[] args)
        {
            string ae = "as expected";

            Val("DateTime.Now", DateTime.Now, ae);
            Val("new IPAddress(new byte[] { 127, 0, 0, 1 })",
                new IPAddress(new byte[] { 127, 0, 0, 1 }),
                ae);
            Val("new IPEndPoint(new IPAddress(new byte[] { 127, 0, 0, 1 }), 80)",
                new IPEndPoint(new IPAddress(new byte[] { 127, 0, 0, 1 }), 80),
                ae);

            string fn = Path.Combine(SolutionDir, "default-formats\\README.md");
            Console.WriteLine("==== ==== ==== ==== ==== ==== ==== ====");
            Console.WriteLine("Updating File: {0}", fn);
            UpdateFile(fn);
        }
    }
}
