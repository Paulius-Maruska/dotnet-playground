Date Formatting
===============

Experimentinig with standard and custom datetime formats.

My Conclusion
=============

Quite simple really. I had no idea, that the format parameter you pass to `DateTime.ToString` can also be used inside a
`String.Format`. Example:
```
DateTime t = DateTime.Now;
string s;

// the following 2 lines produce the exact same result
s = t.ToString("o");
s = String.Format("{0:o}", t);
```
