Query results are mapped to C# objects in a number of ways:

- POCO (plain old CLR object with get and set accessors)
- C# record (constructor with parameters)
- Init-only accessors
- scalar types (int, string etc)




- byte array for varbinary results 
- SqlGeometry / SqlGeography / SqlHierarchyId




As a convenience feature, if your query returns a single column of data, this can be mapped
directly to a scalar type.


Multiple rowsets can be retrieved by chaining calls to ReadList&lt;T&gt;



