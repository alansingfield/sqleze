Explain how non-nullable strings will always coerce NULL to empty string
No System.DBNull

Using the C# nullable type system allows you to reduce the occurrence of nulls within your
C# code.

When a query returns a SQL NULL and Sqleze tries to write this to a string, for example,
if the string is non-nullable then you will get the empty string instead.

Default values are applied for:
- string
- numeric types (0)
- byte arrays (0-length array)

