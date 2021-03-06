use SchoolUtilsProject
go

-- calculator tests
create table test_calculator ( calculator dbo.Calculator);
insert into test_calculator (calculator) values ('14,20/Casio/9/10/8');
insert into test_calculator (calculator) values ('11,20/Erno/6/11/8');
--insert into test_calculator (calculator) values ('-14,20/Razzio/-9/10/8')
select Calculator.ToString() as ok_calculators from test_calculator;
drop table test_calculator;

-- glue tests
create table test_glue ( glue dbo.Glue);
--insert into test_glue (glue) values ('4,59/-2/10')
insert into test_glue (glue) values ('7,59/3/8')
--insert into test_glue (glue) values ('7,59/3/-8')
--insert into test_glue (glue) values ('-4,59/2/10')
select Glue.ToString() as ok_glues from test_glue;
drop table test_glue;


-- notebook tests
create table test_notebook ( notebook dbo.Notebook);
insert into test_notebook (notebook) values ('5,99/60/in lines/B5/yes')
insert into test_notebook (notebook) values ('1,99/90/clear/A4/no')
insert into test_notebook (notebook) values ('3,99/90/checkered/A4/bla')
--insert into test_notebook (notebook) values ('3,99/90/checkered/A4/no')
insert into test_notebook (notebook) values ('2,99/90/in lines/B5/yes')
--insert into test_notebook (notebook) values ('-3,99/90/checkered/A4/yes')
select Notebook.ToString() as ok_notebooks from test_notebook;
drop table test_notebook;



-- pen tests
create table test_pen ( pen dbo.Pen);
insert into test_pen (pen) values ('3,29/red/12/no')
insert into test_pen (pen) values ('2,29/black/14/yes')
insert into test_pen (pen) values ('1,99/black/12/xaxa')
--insert into test_pen (pen) values ('-1,49/blue/11/yes')
select Pen.ToString() as ok_pens from test_pen;
drop table test_pen;



-- rubber tests
create table test_rubber ( rubber dbo.Rubber);
--insert into Rubber (rubber) values ('-1,19/3/21/-3')
insert into test_rubber (rubber) values ('2,44/2/1/4')
insert into test_rubber (rubber) values ('2,54/2/2/2')
--insert into Rubber (rubber) values ('3,19/3/21/-3')
--insert into Rubber (rubber) values ('3,19/3/-21/3')
select Rubber.ToString() as ok_rubbers from test_rubber;
drop table test_rubber;


-- ruler tests
create table test_ruler ( ruler dbo.Ruler);
insert into test_ruler (ruler) values ('3,19/20/blue/0,1')
insert into test_ruler (ruler) values ('4,19/30/pink/0,1')
--insert into test_ruler (ruler) values ('7,19/5/blue/0,5')
--insert into test_ruler (ruler) values ('-7,19/50/blue/0,5')
select Ruler.ToString() as ok_rulers from test_ruler;
drop table test_ruler;




create table test_scissors( scissors dbo.Scissors);
insert into test_scissors (scissors) values ('3,49/yellow/good')
--insert into Scissors (scissors) values ('-4,39/black/good')
insert into test_scissors (scissors) values ('5,49/green/amazing')
insert into test_scissors (scissors) values ('3,99/blue/excellent')
insert into test_scissors (scissors) values ('2,99/blue/medium')
--insert into test_scissors (scissors) values ('2,99/blue/sdsum')
select Scissors.ToString() as ok_scissors from test_scissors;
drop table test_scissors;

-- all methods tests
select * from get_all_prices() order by price;
select * from get_calculator_prices() order by price;
select * from get_glue_prices() order by price;
select * from get_notebook_prices() order by price;
select * from get_pen_prices() order by price;
select * from get_rubber_prices() order by price;
select * from get_ruler_prices() order by price;
select * from get_scissors_prices() order by price;