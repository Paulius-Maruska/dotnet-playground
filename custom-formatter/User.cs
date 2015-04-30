using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace custom_formatter
{
    public class User : IFormattable
    {
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get { return String.Concat(FirstName, " \"", Name, "\" ", LastName); } }
        public DateTime Birthday { get; set; }
        public int Age { get { return (int)Math.Floor((DateTime.Now - Birthday).TotalDays / 365); } }
        public string Location { get; set; }
        public User(string name, string fname, string lname, DateTime birthday, string location)
        {
            Name = name;
            FirstName = fname;
            LastName = lname;
            Birthday = birthday;
            Location = location;
        }
        public override string ToString()
        {
            return ToString(null);
        }

        public string ToString(string format)
        {
            return ToString(format, CultureInfo.CurrentCulture);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (string.IsNullOrEmpty(format))
                format = "FullName";
            if (formatProvider == null)
                formatProvider = CultureInfo.CurrentCulture;

            string[] fields = format.Split('|');

            string result = "";
            foreach (string field in fields)
            {
                if ((field[0] == '"' && field[field.Length - 1] == '"') || (field[0] == '\'' && field[field.Length - 1] == '\''))
                {
                    result = string.Concat(result, field.Substring(1, field.Length - 2));
                }
                else
                {
                    string name = field;
                    string fmt = null;
                    int pos = field.IndexOf(',');
                    if (pos > -1)
                    {
                        name = field.Substring(0, pos);
                        fmt = field.Substring(pos + 1);
                    }
                    string value = "";

                    switch (name)
                    {
                        case "Name":
                            value = Name.ToString(formatProvider);
                            break;
                        case "FirstName":
                            value = FirstName.ToString(formatProvider);
                            break;
                        case "LastName":
                            value = LastName.ToString(formatProvider);
                            break;
                        case "FullName":
                            value = FullName.ToString(formatProvider);
                            break;
                        case "Location":
                            value = Location.ToString(formatProvider);
                            break;
                        case "Age":
                            value = Age.ToString(fmt, formatProvider);
                            break;
                        case "Birthday":
                            value = Birthday.ToString(fmt, formatProvider);
                            break;
                        default:
                            throw new FormatException(String.Format("Unknown field name \"{0}\".", name));
                    }
                    result = string.Concat(result, value);
                }
            }
            return result;
        }
    }
}
