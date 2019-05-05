create database COMP1640_DB

go
use COMP1640_DB
go

create table [Role]
(
[RoleID] uniqueidentifier primary key,
[RoleName] varchar(200),
[Description] varchar(500)
);

insert into [Role] values
((select NEWID()), 'Marketing Manager', 'Marketing Manager'),
((select NEWID()), 'Marketing Coordinator', 'Marketing Coordinator'),
((select NEWID()), 'Student', 'Student'),
((select NEWID()), 'Administrator', 'Administrator'),
((select NEWID()), 'Guest Account', 'Guest Account');

select * from [Role]
delete from [Role] where [RoleID] = '074E3178-7361-466F-AA60-132AD7104EDC'

create table [Faculty]
(
[FacultyID] uniqueidentifier primary key,
[FacultyName] varchar(200),
[Description] varchar(200)
);

select * from [Faculty]

create table [User]
(
[UserID] uniqueidentifier primary key,
[Email] varchar(200),
[Password] varchar(200),
[Name] varchar(200),
[Address] varchar(200),
[Phone] varchar(100),
[Role] uniqueidentifier foreign key references [Role]([RoleID]),
[Status] int
);

insert into [User] values
((select NEWID()), 'longnvgc00982@fpt.edu.vn', '12345678', 'LongNV', 'Ha Noi', '1234567890', '559C2837-E749-44AE-B9DA-9CF2135E33BB', 1),
((select NEWID()), 'tuann@fpt.aptech.ac.vn', '12345678', 'TuanN', 'Ha Noi', '1234567890', '559C2837-E749-44AE-B9DA-9CF2135E33BB', 1),
((select NEWID()), 'tungndgc00952@fpt.edu.vn', '12345678', 'TungND', 'Ha Noi', '0123456789', '4C42D248-EAC8-4E75-9541-42541245B667', 1),
((select NEWID()), 'trungntch18001@fpt.edu.vn', '12345678', 'TrungNT', 'Ha Noi', '0987654321', '559C2837-E749-44AE-B9DA-9CF2135E33BB', 1),
((select NEWID()), 'thanhldgc00959@fpt.edu.vn', '12345678', 'ThanhLD', 'Ha Noi', '1234567890', '559C2837-E749-44AE-B9DA-9CF2135E33BB', 1),
((select NEWID()), 'alwaysrememberyouth@gmail.com', '12345678', 'TungND1', 'Ha Noi', '5678998765', '4C42D248-EAC8-4E75-9541-42541245B667', 1),
((select NEWID()), 'canhndgt00545@fpt.edu.vn', '12345678', 'CanhND', 'Ha Noi', '2345665432', 'E29D3FF8-4CB9-4C64-83B2-B15DFF5EB895', 1),
((select NEWID()), 'thangpdgch15325@fpt.edu.vn', '12345678', 'ThangPD', 'Ha Noi', '3456776543', '4C42D248-EAC8-4E75-9541-42541245B667', 1),
((select NEWID()), 'hannn@fe.edu.vn', '12345678', 'HanNN', 'Ha Noi', '0965826780', '4C42D248-EAC8-4E75-9541-42541245B667', 1),
((select NEWID()), 'duongpt@fe.edu.vn', '12345678', 'DuongPT', 'Ha Noi', '4567887654', 'A7B58D6F-BF1F-469A-8075-E177CA53B757', 1);

select * from [Role]
select * from [User]

Update [User] set [Password] = '37213902101311706410244100199109113607173' where [UserID] = 'DA26E35D-B2F1-4500-BBB1-16F0127D35B0'

create table [Faculty_Detail]
(
[Faculty_DetailID] uniqueidentifier primary key,
[FacultyID] uniqueidentifier foreign key references [Faculty]([FacultyID]),
[UserCoordinator] uniqueidentifier foreign key references [User]([UserID])
);

select * from [Faculty_Detail]
select * from [Faculty]
select * from [User]

delete from [Faculty_Detail] where [Faculty_DetailID] = 'DD655864-CF46-E911-9D2D-E4115BF5193D'

--create table [Faculty_Detail_Student]
--(
--[Faculty_Detail_StudentID] uniqueidentifier primary key,
--[Faculty_DetailID] uniqueidentifier foreign key references [Faculty_Detail]([Faculty_DetailID]),
--[UserStudent] uniqueidentifier foreign key references [User]([UserID])
--);

create table [Category]
(
[CategoryID] uniqueidentifier primary key,
[CategoryName] varchar(200),
[ClosureDate] datetime,
[FinalClosureDate] datetime,
[Faculty_DetailID] uniqueidentifier foreign key references [Faculty_Detail]([Faculty_DetailID]),
[Description] varchar(500),
[Status] int
);

select * from [Category]
select * from [Faculty_Detail]
select * from [Faculty]
select * from [User]
delete from [Category] where [CategoryID] = 'ED522758-B762-E911-9D78-E4115BF5193D'

create table [Contribution]
(
[ContributionID] uniqueidentifier primary key,
[Title] varchar(200),
[Description] varchar(500),
[CreationDate] datetime,
[Category] uniqueidentifier foreign key references [Category]([CategoryID]),
[Creator] uniqueidentifier foreign key references [User]([UserID]),
[Status] int
);

create table [Comment]
(
[CommentID] uniqueidentifier primary key,
[Content] varchar(max),
[Creator] uniqueidentifier foreign key references [User]([UserID]),
[Contribution] uniqueidentifier foreign key references [Contribution]([ContributionID]),
[CommentDate] datetime,
[Status] int
);

create table [Document]
(
[DocumentID] uniqueidentifier primary key,
[DocumentName] varchar(200),
[Path] varchar(300),
[UploadDate] datetime,
[Contribution] uniqueidentifier foreign key references [Contribution]([ContributionID])
);

select * from [Contribution]
select * from [Document]

delete from [Contribution] where [ContributionID] = '8CB2292F-F04E-E911-9D41-E4115BF5193D'
delete from [Document] where [DocumentID] = '8DB2292F-F04E-E911-9D41-E4115BF5193D'

create table [Image]
(
[ImageID] uniqueidentifier primary key,
[ImageName] varchar(200),
[Description] varchar(500),
[Path] varchar(300),
[UploadDate] datetime,
[Contribution] uniqueidentifier foreign key references [Contribution]([ContributionID])
);

select * from [Comment]
select * from [Image]
select * from [Document]
select * from [Contribution]
select * from [Category]
select * from [Faculty_Detail]
select * from [Faculty]
select * from [User]


select cmm.* from [Comment] cmm where cmm.Contribution = '37648D6B-4B47-E911-9D2F-E4115BF5193D' ORDER BY [CommentDate] ASC;
select cmm.* from [Comment] cmm, [Contribution] ctb where ctb.ContributionID = cmm.Contribution ORDER BY [CommentDate] ASC;

delete  from [Image] where ImageID = '82954F04-634C-E911-9D3A-E4115BF5193D'
delete  from [Comment] where [CommentID] = '6DB9719E-D748-E911-9D31-E4115BF5193D'

-- Get number document and image of faculty
select * from [Contribution]
select * from [Category]
select * from [Faculty_Detail]
select * from [Faculty]

select ct.* from [Contribution] ct where ct.Status = 2

select Count(ContributionID) from [Category] ct, [Faculty_Detail] fd, [Contribution] ctb 
where ct.CategoryID = ctb.Category and ct.Faculty_DetailID = fd.Faculty_DetailID and fd.UserCoordinator = '4D0EEC35-F497-4E1B-AE0E-5239CBA7DFD1' and fd.Faculty_DetailID= '688CECC8-CE46-E911-9D2D-E4115BF5193D';
select ct.* from [Category] ct, [Faculty_Detail] fd where ct.Faculty_DetailID = fd.Faculty_DetailID and fd.UserCoordinator = '4D0EEC35-F497-4E1B-AE0E-5239CBA7DFD1';
select Count(ContributionID) from [Category] ct, [Contribution] ctb where ct.CategoryID = ctb.Category 
and ct.CategoryID in (select la.CategoryID from (select CategoryID from [Category] ct, [Faculty_Detail] fd where ct.Faculty_DetailID = fd.Faculty_DetailID and fd.UserCoordinator = '4D0EEC35-F497-4E1B-AE0E-5239CBA7DFD1') la);

select ctb.* from [Category] ct, [Faculty_Detail] fd, [Contribution] ctb 
where ct.CategoryID = ctb.Category and ct.Faculty_DetailID = fd.Faculty_DetailID and fd.UserCoordinator = '4D0EEC35-F497-4E1B-AE0E-5239CBA7DFD1' and fd.Faculty_DetailID= '9D58D5A5-8146-E911-9D2C-E4115BF5193D';