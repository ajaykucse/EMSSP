--fetching all employees

create procedure GetAllEmployees
As
Begin
select * from Employees with(nolock)
End 
-- get single record
create procedure GetSingleEmployeeRecord
@Id int 
as
Begin
select * from Employees where EmployeeId =@id
end
-- update employee
create procedure UpdateEmployeeRecord
@Id int,
@Name varchar(50),
@Email nvarchar(max),
@PhoneNo nvarchar(max),
@JobTitle nvarchar(max)
as
Begin
update Employees set Name=@Name,Email=@Email,PhoneNo=@PhoneNo,JobTitle=@JobTitle where EmployeeId=@Id
end
alter proc insertEmployee
(
@Name varchar(50),
@Email nvarchar(max),
@PhoneNo nvarchar(max),
@JobTitle nvarchar(max)
)
as
Begin
begin try
begin tran
insert into Employees(Name, Email, PhoneNo, JobTitle)
values
	(
		@Name
		,@Email
		,@PhoneNo
		,@JobTitle
	)
commit tran
end try
begin catch 
	rollback tran
end catch
end

create proc sp_getEmployeeById
(
	@id INT
)
as
begin
select EmployeeId, Name, Email, PhoneNo, JobTitle from Employees with (nolock)
where EmployeeId = @Id
end 

create proc sp_employee_delete
(
	@id int 
)
as
begin
declare @rowCount int = 0
begin try
set @rowCount = (select count(1) from Employees with (nolock) where EmployeeId = @id)

if (@rowCount>0)
begin 
begin tran 
delete from Employees where EmployeeId = @id
commit tran
end
end try
begin catch
rollback tran 
end catch
end

--InsertOrUpdate
CREATE PROC [DBO].[usp_InsertOrUpdateEmployee]
(
	@id int,
	@Name varchar(50),
	@Email nvarchar(max),
	@PhoneNo nvarchar(max),
	@JobTitle nvarchar(max)
)
as
begin
	begin try
	if(@id=0)
	begin
	--INSERT
	begin tran
		insert into dbo.Employees
		(
			Name
			,Email
			,PhoneNo
			,JobTitle
		)
		values
		(
			@Name
			,@Email
			,@PhoneNo
			,@JobTitle
		)
	commit tran
	end
	else
	begin
	 --update
	 declare @RowCount int = 0
	 set @RowCount = (select count(1) from dbo.Employees where EmployeeId = @id)
	 if(@RowCount > 0)
	 begin
	 begin tran
	   update dbo.Employees
		set
			Name = @Name
			,Email = @Email
			,PhoneNo = @PhoneNo
			,JobTitle = @JobTitle
			where EmployeeId = @id
	 commit tran
	 end 
	end
	end try
	begin catch
	rollback tran
	end catch
end
