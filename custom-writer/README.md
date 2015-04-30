Custom Writer
=============

Experiment about replaceing Console standard streams with some custom TextWriter objects.

My Conclusion
=============

May be useful some day. Implementing custom TextWriter is a pain, though (you only need Write(char) method, but
every other function will the call that - which sounds very inefficient to me).
