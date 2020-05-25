use SchoolUtilsProject
go

if OBJECT_ID('get_all_prices') IS NOT NULL
	drop function get_all_prices
go
create function get_all_prices() 
returns @TempTable table ( obj varchar(3000), price float)
AS
BEGIN
	insert into @TempTable select Calculator.ToString(), Calculator.GetPrice() from calculator
	insert into @TempTable select Glue.ToString(), Glue.GetPrice() from glue
	insert into @TempTable select Notebook.ToString(), Notebook.GetPrice() from notebook
	insert into @TempTable select Pen.ToString() , Pen.GetPrice() from pen
	insert into @TempTable select Rubber.ToString(), Rubber.GetPrice() from rubber
	insert into @TempTable select Ruler.ToString(), Ruler.GetPrice()  from ruler
	insert into @TempTable select Scissors.ToString(), Scissors.GetPrice() from scissors
	return  
END
GO

if OBJECT_ID('get_calculator_prices') IS NOT NULL
	drop function get_calculator_prices
go
create function get_calculator_prices() 
returns @TempTable table ( obj varchar(3000), price float)
AS
BEGIN
	insert into @TempTable select Calculator.ToString(), Calculator.GetPrice() from calculator
	return  
END
GO


if OBJECT_ID('get_glue_prices') IS NOT NULL
	drop function get_glue_prices
go
create function get_glue_prices() 
returns @TempTable table ( obj varchar(3000), price float)
AS
BEGIN
	insert into @TempTable select Glue.ToString(), Glue.GetPrice() from glue
	return  
END
GO


if OBJECT_ID('get_notebook_prices') IS NOT NULL
	drop function get_notebook_prices
go
create function get_notebook_prices() 
returns @TempTable table ( obj varchar(3000), price float)
AS
BEGIN
	insert into @TempTable select Notebook.ToString(), Notebook.GetPrice() from notebook
	return  
END
GO

if OBJECT_ID('get_pen_prices') IS NOT NULL
	drop function get_pen_prices
go
create function get_pen_prices() 
returns @TempTable table ( obj varchar(3000), price float)
AS
BEGIN
	insert into @TempTable select Pen.ToString() , Pen.GetPrice() from pen
	return  
END
GO


if OBJECT_ID('get_rubber_prices') IS NOT NULL
	drop function get_rubber_prices
go
create function get_rubber_prices() 
returns @TempTable table ( obj varchar(3000), price float)
AS
BEGIN
	insert into @TempTable select Rubber.ToString(), Rubber.GetPrice() from rubber
	return  
END
GO

if OBJECT_ID('get_ruler_prices') IS NOT NULL
	drop function get_ruler_prices
go
create function get_ruler_prices() 
returns @TempTable table ( obj varchar(3000), price float)
AS
BEGIN
	insert into @TempTable select Ruler.ToString(), Ruler.GetPrice()  from ruler
	return  
END
GO

if OBJECT_ID('get_scissors_prices') IS NOT NULL
	drop function get_scissors_prices
go
create function get_scissors_prices() 
returns @TempTable table ( obj varchar(3000), price float)
AS
BEGIN
	insert into @TempTable select Scissors.ToString(), Scissors.GetPrice() from scissors
	return  
END
GO

