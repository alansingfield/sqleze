
CREATE SCHEMA [test]
GO

CREATE TYPE [dbo].[tt_nvarchar_10] FROM [nvarchar](10) NULL
GO

CREATE TYPE [test].[example_udt] FROM [numeric](9, 5) NOT NULL
GO

CREATE TYPE [dbo].[tt_coords] AS TABLE(
	[x] [int] NULL,
	[y] [int] NULL
)
GO

CREATE TYPE [dbo].[tt_geometry_geography] AS TABLE(
	[geom] [geometry] NULL,
	[geog] [geography] NULL
)
GO

CREATE TYPE [dbo].[tt_geometry_vals] AS TABLE(
	[val] [geometry] NULL
)
GO

CREATE TYPE [dbo].[tt_int_foos] AS TABLE(
	[foo] [int] NULL
)
GO

CREATE TYPE [dbo].[tt_int_vals] AS TABLE(
	[val] [int] NOT NULL
)
GO

CREATE TYPE [dbo].[tt_nvarchar_vals] AS TABLE(
	[val] [nvarchar](max) NULL
)
GO

CREATE TYPE [dbo].[tt_table_type_all_types] AS TABLE(
	[col_bigint] [bigint] NOT NULL,
	[col_binary] [binary](100) NOT NULL,
	[col_bit] [bit] NOT NULL,
	[col_char] [char](1) NOT NULL,
	[col_date] [date] NOT NULL,
	[col_datetime] [datetime] NOT NULL,
	[col_datetime2] [datetime2](7) NOT NULL,
	[col_datetimeoffset_0] [datetimeoffset](0) NOT NULL,
	[col_datetimeoffset_7] [datetimeoffset](7) NOT NULL,
	[col_decimal_38_4] [decimal](38, 4) NOT NULL,
	[col_numeric_38_4] [numeric](38, 4) NOT NULL,
	[col_float] [float] NOT NULL,
	[col_image] [image] NOT NULL,
	[col_int] [int] NOT NULL,
	[col_money] [money] NOT NULL,
	[col_nchar] [nchar](1) NOT NULL,
	[col_ntext] [ntext] NOT NULL,
	[col_nvarchar_30] [nvarchar](30) NOT NULL,
	[col_nvarchar_max] [nvarchar](max) NOT NULL,
	[col_real] [real] NOT NULL,
	[col_smalldatetime] [smalldatetime] NOT NULL,
	[col_smallint] [smallint] NOT NULL,
	[col_smallmoney] [smallmoney] NOT NULL,
	[col_text] [text] NOT NULL,
	[col_time_0] [time](0) NOT NULL,
	[col_time_7] [time](7) NOT NULL,
	[col_timestamp] [timestamp] NOT NULL,
	[col_tinyint] [tinyint] NOT NULL,
	[col_uniqueidentifier] [uniqueidentifier] NOT NULL,
	[col_varbinary_100] [varbinary](100) NOT NULL,
	[col_varbinary_max] [varbinary](max) NOT NULL,
	[col_varchar_30] [varchar](30) NOT NULL,
	[col_varchar_max] [varchar](max) NOT NULL,
	[col_sql_variant] [sql_variant] NOT NULL,
	[col_xml] [xml] NOT NULL,
	[col_geography] [geography] NOT NULL,
	[col_geometry] [geometry] NOT NULL,
	[col_hierarchyid] [hierarchyid] NOT NULL
)
GO

CREATE TYPE [dbo].[tt_table_type_all_types_nullable] AS TABLE(
	[col_bigint] [bigint] NULL,
	[col_binary] [binary](100) NULL,
	[col_bit] [bit] NULL,
	[col_char] [char](1) NULL,
	[col_date] [date] NULL,
	[col_datetime] [datetime] NULL,
	[col_datetime2] [datetime2](7) NULL,
	[col_datetimeoffset_0] [datetimeoffset](0) NULL,
	[col_datetimeoffset_7] [datetimeoffset](7) NULL,
	[col_decimal_38_4] [decimal](38, 4) NULL,
	[col_numeric_38_4] [numeric](38, 4) NULL,
	[col_float] [float] NULL,
	[col_image] [image] NULL,
	[col_int] [int] NULL,
	[col_money] [money] NULL,
	[col_nchar] [nchar](1) NULL,
	[col_ntext] [ntext] NULL,
	[col_nvarchar_30] [nvarchar](30) NULL,
	[col_nvarchar_max] [nvarchar](max) NULL,
	[col_real] [real] NULL,
	[col_smalldatetime] [smalldatetime] NULL,
	[col_smallint] [smallint] NULL,
	[col_smallmoney] [smallmoney] NULL,
	[col_text] [text] NULL,
	[col_time_0] [time](0) NULL,
	[col_time_7] [time](7) NULL,
	[col_timestamp] [timestamp] NULL,
	[col_tinyint] [tinyint] NULL,
	[col_uniqueidentifier] [uniqueidentifier] NULL,
	[col_varbinary_100] [varbinary](100) NULL,
	[col_varbinary_max] [varbinary](max) NULL,
	[col_varchar_30] [varchar](30) NULL,
	[col_varchar_max] [varchar](max) NULL,
	[col_sql_variant] [sql_variant] NULL,
	[col_xml] [xml] NULL,
	[col_geography] [geography] NULL,
	[col_geometry] [geometry] NULL,
	[col_hierarchyid] [hierarchyid] NULL
)
GO

CREATE TYPE [dbo].[tt_varbinary_vals] AS TABLE(
	[val] [varbinary](max) NULL
)
GO

CREATE TYPE [dbo].[tt_varchar_20_utf8_vals] AS TABLE(
	[val] [varchar](20) NULL
)
GO

CREATE TYPE [dbo].[tt_varchar_utf8_vals] AS TABLE(
	[val] [varchar](max) NULL
)
GO

CREATE TYPE [dbo].[tt_varchar_vals] AS TABLE(
	[val] [varchar](max) NULL
)
GO

CREATE TYPE [dbo].[tt_varchar2_vals] AS TABLE(
	[val] [varchar](max) NULL
)
GO

CREATE TYPE [test].[tt_p_check_parameter_table_entities_foos] AS TABLE(
	[foo_id] [int] NOT NULL,
	[foo_name] [nvarchar](50) NOT NULL
)
GO

CREATE TYPE [test].[udt_table] AS TABLE(
	[column_1] [test].[example_udt] NOT NULL
)
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[fn_return_nvarchar](@arg nvarchar(20) = 'DEFAULT')
RETURNS nvarchar(20)
AS
BEGIN
    RETURN @arg;
END

GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[fn_table_valued_func]
(
    @ids dbo.tt_int_vals READONLY
)
RETURNS TABLE 
AS
RETURN 
(
	-- Add the SELECT statement with parameter references here
	SELECT  col_a = val,
            col_b = val + 1
    FROM    @ids
)
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TABLE_01](
	[table_01_id] [int] IDENTITY(1,1) NOT NULL,
	[column_3] [int] NULL,
	[column_1] [int] NOT NULL
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TABLE_02](
	[table_02_id] [int] IDENTITY(1,1) NOT NULL,
	[varchar_utf8_col] [varchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[table_02_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[p_datetime_scales]
(
    @dt_0 datetime2(0),
    @dt_1 datetime2(1),
    @dt_7 datetime2(7)
)
AS
BEGIN
    SELECT dt_0 = @dt_0,
           dt_1 = @dt_1,
           dt_7 = @dt_7;
END
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[p_geo]
    @x geography,
    @y geometry,
    @z hierarchyid
AS
BEGIN
    SELECT geog = @x, geom = @y, hier = @z
END

GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[p_nvarchar_10]
    @arg dbo.tt_nvarchar_10
AS
BEGIN
    SELECT result = @arg
END
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[p_read_tempdb_column_type]
    @objname nvarchar(128),
    @colname nvarchar(128),
    @sqltype nvarchar(128) OUTPUT
AS
BEGIN
    SELECT  @sqltype = stype.name +
            CASE 
                WHEN stype.name IN ('numeric', 'decimal')
                    THEN '(' + CONVERT(varchar, sycol.precision)
                        + ',' + CONVERT(varchar, sycol.scale)
                        + ')'
                WHEN stype.name IN ('time', 'datetime2', 'datetimeoffset')
                    THEN '(' + CONVERT(varchar, sycol.scale)
                        + ')'
                WHEN stype.name IN ('varbinary','varchar','binary','char')
                    THEN '(' +
                        CASE WHEN sycol.max_length = -1 THEN 'max'
                        ELSE CONVERT(varchar, sycol.max_length)
                        END
                        + ')'
                WHEN stype.name IN ('nvarchar','nchar')
                    THEN '(' +
                        CASE WHEN sycol.max_length = -1 THEN 'max'
                        ELSE CONVERT(varchar, sycol.max_length / 2)
                        END
                        + ')'
                ELSE ''
            END
    FROM    tempdb.sys.columns sycol
        INNER JOIN
            tempdb.sys.types stype
        ON  sycol.system_type_id = stype.system_type_id
        AND sycol.user_type_id = stype.user_type_id
    WHERE   sycol.object_id = OBJECT_ID('tempdb..' + @objname)
    AND     sycol.name = @colname
END
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[p_return_geography]
    @arg geography
AS
BEGIN
    SELECT result = @arg
END
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[p_return_geometry]
    @arg geometry
AS
BEGIN
    SELECT result = @arg
END
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[p_return_hierarchyid]
    @arg hierarchyid
AS
BEGIN
    SELECT result = @arg
END
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[p_return_int]
AS
    RETURN 123
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[p_return_nvarchar_10_arg]
    @arg nvarchar(10)
AS
    SELECT @arg
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[p_select_default_value_input]
(
    @arg int = 9999
)
AS
BEGIN

    SELECT val = @arg;

END

GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_select_default_value_input_output]
(
    @arg int = 9999 OUTPUT
)
AS
BEGIN

    -- Reflect what input we got
    SELECT val = @arg;

    -- Change the output value
    SET @arg = 1234;

END

GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [test].[p_check_execute_list_02]
AS
BEGIN
    SELECT  foo_id = 123,
            foo_name = 'FooName1'
    UNION ALL
    SELECT  foo_id = 456,
            foo_name = 'FooName2'
    ;

END
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [test].[p_check_execute_lists_03]
AS
BEGIN
    SELECT  foo_id = 123,
            foo_name = 'FooName1'
    UNION ALL
    SELECT  foo_id = 456,
            foo_name = 'FooName2'
    ;

    SELECT  bar_id = 789,
            bar_name = 'BarName1'
    UNION ALL
    SELECT  bar_id = 553,
            bar_name = 'BarName2'
    ;

END
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [test].[p_check_parameter_table_entities]
(
    @foos test.tt_p_check_parameter_table_entities_foos READONLY
)
AS
BEGIN

    SELECT  foo_id = foo_id + 100,
            foo_name = foo_name + 'X'
    FROM    @foos
    ;

END
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [test].[p_check_parameter_table_scalar]
(
    @numbers dbo.tt_int_vals READONLY
)
AS
BEGIN

    SELECT  result = SUM(val)
    FROM    @numbers;


END
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [test].[p_check_set_parameter_values]
(
    @first_number int,
    @second_number int
)
AS
BEGIN

    SELECT result = @first_number + @second_number;

END
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [test].[p_get_args_01]
(
    @arg1 int
)
AS
BEGIN
    SELECT 12345;
END
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [test].[p_query_dataset_typed_1]
AS
BEGIN

    SELECT  foo_id = 123,
            foo_name = 'FooName1'
    UNION ALL
    SELECT  foo_id = 456,
            foo_name = 'FooName2'
    ;

END
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [test].[p_query_dataset_untyped_1]
AS
BEGIN

    SELECT  foo_id = 123,
            foo_name = 'FooName1'
    UNION ALL
    SELECT  foo_id = 456,
            foo_name = 'FooName2'
    ;

END
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [test].[p_set_output_param_01]
    @arg int OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT at_input = @arg;
    SET @arg = 12345;

END
GO
