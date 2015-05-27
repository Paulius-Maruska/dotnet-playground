using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace default_formats
{
    class FormattedValue
    {
        public string Code { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public string Comment { get; set; }
        public FormattedValue(string code, Type type, object obj, string comment)
        {
            Code = code;
            Type = type.FullName;
            Value = obj.ToString();
            Comment = comment;
        }
        public FormattedValue(string code, Type type, object obj)
            :this(code, type, obj, "-")
        { }
        public string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("  <tr>\n    <td>");
            sb.Append(Code);
            sb.Append("</td>\n    <td>");
            sb.Append(Type);
            sb.Append("</td>\n    <td>");
            sb.Append(Value);
            sb.Append("</td>\n    <td>");
            sb.Append(Comment);
            sb.Append("</td>\n  </tr>");
            return sb.ToString();
        }
    }

    class FormattedValues : List<FormattedValue>
    {
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table>\n");
            foreach (FormattedValue fv in this)
            {
                sb.Append(fv.ToString());
                sb.Append("\n");
            }
            sb.Append("</table>");
            return sb.ToString();
        }
    }
}
