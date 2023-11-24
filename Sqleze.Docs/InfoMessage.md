InfoMessage is the textual output from query execution

The results of PRINT and RAISERROR (error level <= 10)


WithInfoMessagesTo accepts a lambda function which is called whenever an infomessage occurs.
Due to the behaviour of SQL, only ever occurs when not enumerating a rowset.


Can be stacked 


