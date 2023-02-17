global using DryIoc;


// In this project we use the MOCK SqlClient.
// All the classes in Microsoft.Data.SqlClient are sealed without
// public constructors so this seems to be the only way to make
// them testable.
global using MS = Sqleze.Mock.MockSqlClient;
global using MSS = Sqleze.Mock.MockSqlClientServer;
