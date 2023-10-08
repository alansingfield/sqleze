# Introduction

Sqleze (pronounced SQL-easy) is an Object-Relational Mapper (ORM) for developers who do not like ORMs.

The key design goal is to allow simple mapping between C# objects and SQL queries,
without any interference with the SQL queries themselves.

## Simple example:

```
string connStr = "Server=YOURSERVER;Database=YOURDB;...";

using conn = SqlezeRoot.Connect(connStr);
    
DateTime now =  conn.Sql("SELECT GETDATE()")
    .ReadSingle<DateTime>();
```

Here we connect to the database, run some SQL and read the result as a scalar value.

## Object population

You can populate objects directly from the query result:

```
class Foo
{
    int Number { get; set; }
    string Text { get; set; }
}

List<Foo> foos = conn.Sql(@"

    SELECT  number = 1,  text = 'hello'
    UNION
    SELECT  number = 2,  text = 'bye'

    ")
    .ReadList<Foo>();

foos.Count.ShouldBe(2);
foos[0].Number.shouldBe(1);
foos[0].Text.Shouldbe("hello");
foos[1].Number.shouldBe(2);
foos[1].Text.Shouldbe("bye");

```

## Parameters

Parameters can be passed into the query directly from object properties, like this:

```
class Foo
{
    int Number { get; set; }
    string Text { get; set; }
}

var fooParam = new Foo() { Number = 999, Text = "banana" };

string result = conn.Sql(@"

    SELECT CONVERT(varchar, @number) + @text

    ")
    .Parameters.Set(fooParam)
    .ReadSingle<string>();

result.ShouldBe("999banana");

```

Now; here's Sqleze's party trick. You can pass lists of objects INTO the SQL using table-valued parameters.

First create the table type on your database:

```
CREATE TYPE dbo.tt_my_type AS TABLE
(
    x int,
    y int
);
```

Then set the parameter value as a list of objects. It will appear within SQL as a table you can SELECT from.

```
class Foo
{
    int X { get; set; }
    int Y { get; set; }
}

var arg = new List<Foo>()
{
    new() { X = 1, Y = 30 },
    new() { X = 2, Y = 40 }
};

List<int> result = conn.Sql(@"

    SELECT result = x + y
    FROM   @arg

    ")
    .Parameters.Set("@arg", arg).AsTableType("dbo.tt_my_type")
    .ReadList<int>();

result.Count.ShouldBe(2);

result[0].ShouldBe(31);
result[1].ShouldBe(42);
```

This allows a bulk dataload without needing repeated INSERT statements, temp tables,
CSV, XML or other workarounds.

The table-valued parameters are streamed from your code, so given careful design with
IEnumerable you can bulk-load large amounts of data without it all having to be in
memory at once.


