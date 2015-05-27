Default Formats
===============

Testing what the default formats of some standard .NET objects are. Basically, just seeing
what is returned by ToString().

My Conclusion
=============

Auto-generated output table:
<!-- AUTO GEN -->
<table>
  <tr>
    <td>DateTime.Now</td>
    <td>System.DateTime</td>
    <td>2015-05-27 10:56:39</td>
    <td>as expected</td>
  </tr>
  <tr>
    <td>DateTime.UtcNow</td>
    <td>System.DateTime</td>
    <td>2015-05-27 07:56:39</td>
    <td>as expected</td>
  </tr>
  <tr>
    <td>new IPAddress(new byte[] { 127, 0, 0, 1 })</td>
    <td>System.Net.IPAddress</td>
    <td>127.0.0.1</td>
    <td>as expected</td>
  </tr>
  <tr>
    <td>new IPEndPoint(new IPAddress(new byte[] { 127, 0, 0, 1 }), 80)</td>
    <td>System.Net.IPEndPoint</td>
    <td>127.0.0.1:80</td>
    <td>as expected</td>
  </tr>
</table>
<!-- /AUTO GEN -->
