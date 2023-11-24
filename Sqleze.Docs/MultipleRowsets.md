# Multiple rowsets

SQL Server can return more than one rowset from a single batch of execution.

Typically this occurs in a master-detail scenario within a stored procedure; e.g. selecting
an order with the associated order lines.

Sqleze can read these either into output parameters:

```
using var conn = Connect();

conn.Sql(@"

    SELECT 1 UNION SELECT 2;

    SELECT 'A' UNION SELECT 'B';

")
    .ReadList<int>(out var l1)
    .ReadList<string>(out var l2);

l1.ShouldBe(new List<int> { 1, 2 });
l2.ShouldBe(new List<string> { "A", "B" });

```

... or populate a model

```

private class ReadChainModel
{
    public int[] L1 { get; set; } = new int[0];
    public string[] L2 { get; set; } = new string[0];
}

using var conn = Connect();

var model = new ReadChainModel();

conn.Sql(@"

    SELECT 1 UNION SELECT 2;

    SELECT 'A' UNION SELECT 'B';

")
    .ReadArray(() => model.L1)
    .ReadArray(() => model.L2);

model.L1.ShouldBe(new int[] { 1, 2 });
model.L2.ShouldBe(new string[] { "A", "B" });

```


... or populate a variable/property via a lambda expression

```
using var conn = Connect();

int[]? l1 = null;
string[]? l2 = null;

conn.Sql(@"

    SELECT 1 UNION SELECT 2;

    SELECT 'A' UNION SELECT 'B';

")
    .ReadArray(() => l1!)
    .ReadArray(() => l2!);

l1.ShouldBe(new int[] { 1, 2 });
l2.ShouldBe(new string[] { "A", "B" });
```

