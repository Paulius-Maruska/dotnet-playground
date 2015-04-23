using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace stack.trace
{
    class Program
    {
        class Subclass
        {
            public StackTrace wub1(string a, int b)
            {
                Console.WriteLine("wub1 - {0} - {1}", a, b);
                return new StackTrace(true);
            }
            public StackTrace wub2(string a, int b)
            {
                Console.WriteLine("wub2 - {0} - {1}", a, b);
                return new StackTrace(1, false);
            }
        }

        static StackTrace sub1(string a, int b)
        {
            Console.WriteLine("sub1 - {0} - {1}", a, b);
            return new StackTrace(true);
        }

        static StackTrace sub2(string a, int b)
        {
            Console.WriteLine("sub2 - {0} - {1}", a, b);
            return new StackTrace(1, true);
        }

        static void func(string a, int b)
        {
            Subclass s = new Subclass();
            StackTrace[] st = new StackTrace[4] {
                sub1(a, b),
                sub2(a, b),
                s.wub1(a, b),
                s.wub2(a, b),
            };
            Console.WriteLine(" ==== ==== ==== ==== ==== ==== ==== ==== ==== ==== ==== ====");
            for (int i = 0; i < st.Length; i++)
            {
                Console.WriteLine("StackTrace #{0}:\n{1}", i, st[i]);
                Console.WriteLine(" ==== ==== ==== ==== ==== ==== ==== ==== ==== ==== ==== ====");
            }
        }

        static void analyze(StackTrace st)
        {
            Console.WriteLine(st.ToString());
            Console.WriteLine("Frames: {0}", st.FrameCount);
            for (int i = 0; i < st.FrameCount; i++)
            {
                StackFrame fr = st.GetFrame(i);
                var m = fr.GetMethod();
                Console.WriteLine("Frame #{0}:", i);
                Console.Write("    --: {0}", fr.ToString());
                Console.WriteLine("    File: {0}", fr.GetFileName());
                Console.WriteLine("    Line: {0}", fr.GetFileLineNumber());
                //Console.WriteLine("    Column: {0}", fr.GetFileColumnNumber());
                //Console.WriteLine("    ILOffset: {0}", fr.GetILOffset());
                //Console.WriteLine("    NativeOffset: {0}", fr.GetNativeOffset());
                Console.WriteLine("    Method:");
                Console.WriteLine("        --: {0}", m.ToString());
                Console.WriteLine("        Name: {0}", m.Name);
                //Console.WriteLine("        Module: {0}", m.Module);
                Console.WriteLine("        Class:");
                Console.WriteLine("            Name: {0}", m.DeclaringType.Name);
                Console.WriteLine("            Assembly: {0}", m.DeclaringType.Assembly);
                Console.WriteLine("            AssemblyName: {0}", m.DeclaringType.AssemblyQualifiedName);
                Console.WriteLine("            FullName: {0}", m.DeclaringType.FullName);
                Console.WriteLine("            Namespace: {0}", m.DeclaringType.Namespace);
                Console.WriteLine("            IsNested: {0}", m.DeclaringType.IsNested);
                if (m.DeclaringType.IsNested)
                {
                    Console.WriteLine("            DeclaringType: {0}", m.DeclaringType.DeclaringType.FullName);
                }
            }
        }

        static void Main(string[] args)
        {
            Subclass s = new Subclass();
            func("foo", 0x0f00);
            analyze(s.wub1("bar", 0x0ba8));
        }
    }
}
