There are three levels of metadata available on the rowset

Name - just the names of the fields
FieldType - name, column ordinal, SQL data type without length or precision
FieldSchema - all the above plus the length and precision

GetFieldNames()
GetFieldTypes()
GetFieldSchemas()

These are listed in the order of resource requirement


To access these extension methods, you need to avoid the shortcut methods like ExecuteList<T>()

Instead use:

var rdr = command.ExecuteReader();

var rowset = rdr.OpenRowset<T>();

rowset.GetFieldSchemas();

ExecuteList<T> et al give you a shortcut through the ExecuteReader / OpenRowset methods but
with the downside that you don't see the OpenRowset<> method.


