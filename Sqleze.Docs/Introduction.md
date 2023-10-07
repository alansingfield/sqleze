# Introduction

Sqleze (pronounced SQL-easy) is an Object-Relational Mapper (ORM) for developers who do not like ORMs.

The key design goal is to allow simple mapping between C# objects and SQL queries,
without any interference with the SQL queries themselves.

Simple example:

```
string connStr = "Server=YOURSERVER;Database=YOURDB;...";

using conn = SqlezeRoot.Connect(connStr);
    
DateTime now =  conn.Sql("SELECT GETDATE()")
    .ReadSingle<DateTime>();
```

Here we connect to the database, run some SQL and read the result as a scalar value.

Object-based example:

```
class Foo
{
    int Number { get; set; }
    string Text { get; set; }
}

List<Foo> foos = conn.Sql(@"
    SELECT	number = 1,
            text = 'hello'
    UNION
    SELECT	number = 2,
            text = 'bye'
    ").ReadList<Foo>();

foos.Count.ShouldBe(2);
foos[0].Number.shouldBe(1);
foos[0].Text.Shouldbe("hello");
foos[1].Number.shouldBe(2);
foos[1].Text.Shouldbe("bye");

```

TODO - pass parameters by name using lambda

You can pass parameters into the query directly from object properties, like this:

```
class Foo
{
    int Number { get; set; }
    string Text { get; set; }
}

var fooParam = new Foo() { Number = 3, Text = "banana" };

string result = conn
    .Sql("SELECT CONVERT(varchar, @number) + @text")
    .Parameters.Set(fooParam)
    .ReadSingle<string>();

result.ShouldBe("3banana");

```



Now; here's Sqleze's party trick. You can pass lists of objects as table-valued parameters.

```
CREATE TYPE dbo.tt_my_type AS TABLE
(
    number int,
    message nvarchar(200)
);

class Foo
{
    int Number { get; set; }
    string Message { get; set; }
}

var arg = new List<Foo>()
{
    new()
    {
        Number = 1,
        Mesage = "hello"
    },
    new()
    {
        Number = 2,
        message = "bye"
    }
};

List<string> result = conn.Sql(@"
    SELECT  result = CONVERT(varchar, number) + message
    FROM	@arg
    ")
    .Parameters.Set(() => arg).AsTableType("dbo.tt_my_type")
    .ReadList<string>();

result.Count.ShouldBe(2);

result[0].ShouldBe("1hello");
result[1].ShouldBe("2bye");



```
This allows a bulk dataload without needing repeated INSERT statements, temp tables,
CSV, XML or other workarounds.

The table-valued parameters are streamed from your code, so given careful design with
IEnumerable you can bulk-load large amounts of data without it all having to be in
memory at once.