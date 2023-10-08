Sqleze has support for the geometry, geography and hierarchyid datatypes.

These are included in Microsoft's package Microsoft.SqlServer.Types

The support for these types has been placed in a separate DLL as we would otherwise
cause a dependency on that package for users who never use those types.

Note that Microsoft.SqlServer.Types is a versioned package. If you try to use raw
ADO calls

You can declare a C# object with a property of type SqlGeography / SqlGeometry / SqlHierarchyId
and this will be instantiated by the ExecuteList() call.



Explain usage of geography, geometry and hierarchyid
Passed into ADO using binary representation to avoid DLL version clashes
