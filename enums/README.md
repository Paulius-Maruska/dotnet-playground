Enums
=====

Testing out some `enum` casting and validation behavior.

My Conclusion
=============

When casting an integer (of any kind) to an `enum`, there will be no error even
if the `enum` does not have that particular value defined. The only way to
validate if an integer value is defined in an `enum` is to use a standard
`Enum.IsDefined` static function. Pretty simple, really.
