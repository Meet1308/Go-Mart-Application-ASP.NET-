select * from tblAdmin

select * from tblSeller

create table tblCategory
(
CatID int identity(1,1) primary key not null,
CategoryName nvarchar(50),
CategoryDesc nvarchar(50)
)

select * from tblCategory


insert into tblCategory (CategoryName,CategoryDesc) values(@CategoryName,@CategoryDesc)


create procedure spCatInsert
(
@CategoryName nvarchar(50),
@CategoryDesc nvarchar(50)
)
as
begin
insert into tblCategory (CategoryName,CategoryDesc) values(@CategoryName,@CategoryDesc)
end

------

select CategoryName from tblCategory where CategoryName=@CategoryName

select  CatID as CategoryID,CategoryName,CategoryDesc as CategoryDescription from tblCategory

------

create procedure spCatUpdate
(
@CatID int,
@CategoryName nvarchar(50),
@CategoryDesc nvarchar(50)
)
as
begin
update tblCategory set CategoryName=@CategoryName,CategoryDesc=@CategoryDesc where CatID=@CatID
end
-----

create procedure spCatDelete
(
@CatID int 
)
as
begin
Delete from tblCategory where CatID=@CatID
end

--  catch ->  MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);