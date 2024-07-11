use imsdb

--Users

--user table
create table users
         (
         usr_id int not null identity primary key,
         usr_name varchar(40) not null ,
         usr_username varchar(30) not null,
         usr_password nvarchar (30) not null,
         usr_phone varchar(15) not null,
         usr_email varchar(50) not null,
         usr_status tinyint not null default 1
         )
--insert user in data_base
  
    create procedure st_insertUsers
    @name varchar(40),
    @username varchar(30),
    @pwd nvarchar(30),
    @phone varchar(15),
    @email varchar(50)
    as 
    insert into users (usr_name, usr_username, usr_password, usr_phone, usr_email)
    values (@name,@username, @pwd, @phone,@email)

	--update data
  
   create procedure st_updateUsers
   @name varchar(40),
   @username varchar(30),
   @pwd nvarchar (30),
   @phone varchar(15),
   @email varchar(50),
   @id int
   as
   update users
   set
   usr_name = @name,
   usr_username = @username,
   usr_password = @pwd,
   usr_phone= @phone,
   usr_email = @email
   where
   usr_id = @id

   --delete user

   create procedure st_deleteUser
   @id int
   as
   delete from users where usr_id = @id

   --get User Data
   
   create procedure st_getUsersData
   as
   select
   u.usr_id as 'ID',
   u.usr_name as 'Name',
   u.usr_username as 'Username',
   u.usr_password as 'Password',
   u.usr_phone as 'Phone',
   u.usr_email as 'Email',
   case when (usr_status = 1) then 'Active' else 'In-active' end as 'Status'
   from users u
   order by u.usr_name asc 

   
   --Products


--Update Products
create procedure st_updateProducts
	@id int,
	@name varchar(50),
	@barcode nvarchar(100),
	@price money,
	@expiry date,
	@catID int
as
update products
set 
	pro_name=@name,
	pro_barcode=@barcode,
	pro_price=@price,
	pro_expiry=@expiry,
	pro_catID=@catID
where
pro_id = @id

--delete product
create procedure deleteProducts
@id int
as
delete from products where pro_id=@id


--Get All Products
create procedure getAllProducts
as
select 
products.pro_id as 'ID',
products.pro_name as 'Name',
products.pro_barcode as 'Barcode',
products.pro_catID as 'Category',
products.pro_price as 'Price',
products.pro_expiry as 'Expiry'
from products
order by pro_name asc

exec getAllProducts


									--Stock/Supplier
create table supplier
(
	sup_id int not null identity primary key,
	sup_company varchar(100) not null unique,
	sup_contactPerson varchar(50) not null,
	sup_phone1 varchar(15) not null,
	sup_phone2 varchar(15),
	sup_address nvarchar(100) not null,
	sup_ntn varchar (25) not null,
	sup_status tinyint not null
)


--Insert New Supplier
create procedure st_insertSupplier
@company varchar(100),
@conPerson varchar(50),
@phone1 varchar(15),
@phone2 varchar(15),
@address nvarchar(100),
@ntn varchar(25),
@status tinyint
as
insert into supplier values (@company,@conPerson,@phone1, @phone2, @address, @ntn,@status)

--Update Existing Supplier

create procedure st_updateSupplier
@company varchar(100),
@conPerson varchar(50),
@phone1 varchar(15),
@phone2 varchar(15),
@address nvarchar(100),
@ntn varchar(25),
@status tinyint,
@suppID int
as
update supplier
set
sup_company= @company,
sup_contactPerson = @conPerson,
sup_phone1 =@phone1,
sup_phone2 = @phone2,
sup_address=@address,
sup_ntn=@ntn,
sup_status=@status
where
sup_id = @suppID

--Delete Supplier
create procedure st_deleteSupplier
@suppID int
as
delete from supplier where sup_id=@suppID

--Get Supplier List
create procedure st_getSupplierList
as
select s.sup_id as 'ID',s.sup_company as 'Company' from supplier s where s.sup_status = 1 order by s.sup_company asc


create procedure st_getSupplierData
as
select
supplier.sup_id as 'ID',
supplier.sup_company as 'Company',
supplier.sup_contactPerson as 'Contact Person',
supplier.sup_phone1 as 'Phone 1',
supplier.sup_phone2 as 'Phone 21',
supplier.sup_ntn as 'NTN #',
supplier.sup_address as 'Address',
case when (supplier.sup_status= 1) then 'Active' else 'In-active' end as 'Status'
from supplier order by supplier.sup_company asc

--	Purchase Invoice

create table purchaseInvoice
(
pi_id bigint not null identity primary key,
pi_date date not null,
pi_doneBy int not null,
pi_suppID int not null
) 

--insert Purchase Invoice

create procedure st_insertPurchaseInvoice
@date date,
@doneBy int,
@suppID int
as
insert into purchaseInvoice values (@date, @doneBy, @suppID)

--Last Purchase Invoice

create procedure st_getLastPurchaseID
as
select top 1 purchaseInvoice.pi_id from purchaseInvoice order by purchaseInvoice.pi_id desc


--Purchase Invoice Detail Table
create table purchaseInvoiceDetails
(
pid_id bigint not null identity primary key,
pid_purchaseID bigint not null foreign key references purchaseInvoice(pi_id),
pid_proID int not null,
pid_proquan int not null,
pid_totprice money not null
)

--Insert Purchase Invoice Detail
create procedure st_insertPurchaseInvoiceDetails
@purchaseID bigint,
@proID int,
@quan int, 
@totPrice money
as
insert into purchaseInvoiceDetails values (@purchaseID, @proID, @quan,@totPrice)

