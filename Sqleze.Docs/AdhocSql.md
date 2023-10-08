Sqleze can be used to run either ad-hoc SQL statements or stored procedures.

Advantages of ad-hoc SQL

Flow of execution is inline with the C# code (great for unit tests and tutorial code)
Simple to deploy

Disadvantages of ad-hoc SQL

Parameter size has to be computed based on the actual value of the arguments passed.
If the SQL text is not fully parameterised, SQL Server's execution cache will be littered with single-run
queries which only differ by the non-parameterised values.

Table-type parameter types need to be explicitly stated

Mitigations

Parameter sizing can be quantized - that is, Sqleze is able to choose an appropriate size for an nvarchar
field rather than 

Table-type parameter types can be pre-registered 

Parameterisation




