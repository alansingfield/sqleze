Stored procedure execution

Applies to:
- SQL stored procedures
- SQL scalar functions

Does not apply to:
- SQL table-valued functions

Stored procedures and SQL
Parameter types are read from the SQL server metadata and set up to match.

This means that you don't have to explictly specify the datatype

Advantages for
table valued parameters
datetime2


stored procedure execution cache

## Return values

SQL scalar functions can return any scalar type
SQL stored procedures can only return int.

```
CREATE PROC [dbo].[p_return_int]
AS
    RETURN 123
GO
```

The ReturnTo&lt;&gt;() method on the Parameters collection collects the return value
and populates your C# variable either through a lambda function or a property setter.

By property setter:

```
using var conn = connect();

int retval = 0;
conn.StoredProc("dbo.p_return_int")
    .Parameters.ReturnTo(() => retval)
    .ExecuteNonQuery();

retval.ShouldBe(123);

```
By lambda (though note, you have to explicitly state the return type)

```
using var conn = connect();

int retval = 0;
conn.StoredProc("dbo.p_return_int")
    .Parameters.ReturnTo<int>(x => retval = x)
    .ExecuteNonQuery();

retval.ShouldBe(123);

```



