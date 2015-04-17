Timezones
=========

Experimentinig with timezones.

My Conclusion
=============

So converting DateTime from one timezone to another - easy. Getting a list of
all supported system timezones - easy.

What took me by surprise is that `DateTime` object does not have a timezone
field... It has `Kind` property which can either be `Local`, `Utc` or
`Unspecified`. This is somewhat lacking.

Now, speaking of pairing things up. It seems that the `DateTimeOffset` is
exactly that, however it is not exactly interchangeable with `DateTime` (if a
function expects `DateTime` - you can't give it `DateTimeOffset`). Also,
conversion is somewhat awkward. Example:
```
// Convert "UTC" DateTime to "Pacific Standard Time" DateTimeOffset
DateTime t = DateTime.UtcNow;
// first - convert DateTime to DateTimeOffset
DateTimeOffset to = new DateTimeOffset(t);
// second - convert the actual value to a different timezone
DateTimeOffset tc = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(to, "Pacific Standard Time");
```

Important note: when converting a `DateTime` to `DateTimeOffset` - everything
will work fine, as long as you have `Kind` set to `Utc` or `Local`. When `Kind`
is set to `Unspecified` - the conversion seems to behave as if `Kind` is set to
`Local`. This is important (and wrong) in cases, when you are trying to convert
a `DateTime` from one non-local and non-unicode timezone to another. Example:
```
// Convert "UTC-02" DateTime to "Pacific Standard Time" DateTimeOffset
DateTime t = DateTime.UtcNow; // Kind == Utc
DateTime w = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(t, "UTC-02"); // Kind = Unspecified
DateTimeOffset wo = new DateTimeOffset(w); // Offset set to your local offset (TimeZoneInfo.Local)
DateTimeOffset wc = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(to, "Pacific Standard Time"); // WRONG!
```
