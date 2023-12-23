
create table tblAdmin
(
AdminID nvarchar(50) primary key,
[Password] nvarchar(50),
FullName nvarchar(50)
)

select * from tblAdmin

select top 1 AdminID,Password,FullName from tblAdmin where AdminID=@AdminID and Password=@Password

select * from tblSeller

select SellerName from tblSeller where SellerName=@SellerName

create procedure spSellerInsert
(
@SellerName nvarchar(50),
@SellerAge int, 
@SellerPhone nvarchar(50),
@SellerPass nvarchar(50)
)
as
begin
insert into tblSeller(SellerName,SellerAge,SellerPhone,SellerPass) values(@SellerName,@SellerAge,@SellerPhone,@SellerPass)
end
go
-------------

create procedure spSellerUpadte
(
@SellerID int,
@SellerName nvarchar(50),
@SellerAge int, 
@SellerPhone nvarchar(50),
@SellerPass nvarchar(50)
)
as
begin
update tblSeller set SellerName=@SellerName,SellerAge=@SellerAge,SellerPhone=@SellerPhone,SellerPass=@SellerPass where SellerID=@SellerID
end
go

----

create procedure spSellerDelete
(
@SellerID int 
)
as
begin
Delete from tblSeller where SellerID=@SellerID
end

go

select * from tblAdmin

select AdminID from tblAdmin where AdminID='Coder'

create procedure spAddAdmin
(
@AdminID nvarchar(50),
@Password nvarchar(50),
@FullName nvarchar(50)
)
as
begin
Insert into tblAdmin(AdminID,[Password],FullName) values(@AdminID,@Password,@FullName)
end
go

----

create procedure spUpdateAdmin
(
@AdminID nvarchar(50),
@Password nvarchar(50),
@FullName nvarchar(50)
)
as
begin
update tblAdmin set [Password]=@Password,FullName=@FullName where AdminID=@AdminID
end
go

select * from tblAdmin
---
create procedure spDeleteAdmin
(
@AdminID nvarchar(50)
)
as
begin
delete tblAdmin where AdminID=@AdminID
end
go

--


create table tblProduct
(
ProdID int identity(1,1) primary key not null,
ProdName nvarchar(50),
ProdCatID int,
ProdPrice decimal(10,2),
ProdQty int,
)

select * from tblProduct

create procedure spGetCategory
as
begin
set nocount on;
select CatID,CategoryName from tblCategory order by CategoryName asc
end
go

--

create procedure spCheckDuplicateProduct
(
@ProdName nvarchar(50),
@ProdCatID int
)
as
begin
set nocount on;
select ProdName from tblProduct where ProdName=@ProdName and ProdCatID=@ProdCatID
end
go
----

create procedure spInsertProduct
(
@ProdName nvarchar(50),
@ProdCatID int,
@ProdPrice decimal(10,2),
@ProdQty int
)
as
begin
 
Insert into tblProduct(ProdName,ProdCatID,ProdPrice,ProdQty) values(@ProdName,@ProdCatID,@ProdPrice,@ProdQty)
end
go

----

select * from tblProduct

select * from tblCategory

create procedure spGetAllProductList
as
begin
set nocount on;
select t1.ProdID,t1.ProdName,t2.CategoryName,t1.ProdCatID as CategoryID,t1.ProdPrice,t1.ProdQty from tblProduct as t1
inner join tblCategory as t2 on t1.ProdCatID=t2.CatID
order by t1.ProdName,t2.CategoryName asc
end
go

---


create procedure spUpdateProduct
(
@ProdID int,
@ProdName nvarchar(50),
@ProdCatID int,
@ProdPrice decimal(10,2),
@ProdQty int
)
as
begin
 
update tblProduct set ProdName=@ProdName,ProdCatID=@ProdCatID,ProdPrice=@ProdPrice,ProdQty=@ProdQty where ProdID=@ProdID
end
go

---

create procedure spDeleteProduct
(
@ProdID Int
)
as
begin
 
delete from tblProduct where ProdID=@ProdID
end
go


select * from tblProduct
--

create procedure spGetAllProductList_SearchByCat
(
@ProdCatID int
)
as
begin
set nocount on;
select t1.ProdID,t1.ProdName,t2.CategoryName,t1.ProdCatID as CategoryID,t1.ProdPrice,t1.ProdQty from tblProduct as t1
inner join tblCategory as t2 on t1.ProdCatID=t2.CatID
where t1.ProdCatID=@ProdCatID
order by t1.ProdName,t2.CategoryName asc
end
go

----------------

create table tblBill
(
Bill_ID int primary key,
SellerID nvarchar(50),
SellDate nvarchar(50),
TotalAmt decimal(18,2)
)


create procedure spInsertBill
(
@Bill_ID int,
@SellerID nvarchar(50),
@SellDate nvarchar(50),
@TotalAmt decimal(18,2)
)
as
begin
insert into tblBill (Bill_ID,SellerID,SellDate,TotalAmt) values(@Bill_ID,@SellerID,@SellDate,@TotalAmt)
end
go
---

create procedure spGetBillList
as
begin
set nocount on;
select Bill_ID,SellerID,SellDate,TotalAmt from tblBill order by Bill_ID desc 
end
go