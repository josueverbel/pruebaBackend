
CREATE TABLE Author
   (
      Id int IDENTITY(1,1) PRIMARY KEY,
      Dob DATE not null,
			FirstName varchar(150) not null,
			LastName varchar(150) not null,
			Email varchar(150) not null,
			City varchar(150) not null,
			CreatedAt datetime default getdate() not null,
			UpdatedAt datetime default getdate() not null,
			DeletedAt datetime NULL,
   );
CREATE TABLE Editorial
   (
      Id int IDENTITY(1,1) PRIMARY KEY,
			Name varchar(150) not null,
			Address varchar(150) not null,
			Email varchar(150) not null,
			Phone varchar(150) not null,
			MaxCount int  not NULL,
			CreatedAt datetime default getdate() not null,
			UpdatedAt datetime default getdate() not null,
			DeletedAt datetime NULL,
			
   );
   CREATE TABLE Book
   (
      Id int IDENTITY(1,1) PRIMARY KEY,
			Title varchar(150) not null,
			Year CHAR(4) not null,
		
			PageCount int not NULL,
			EditorialId int not NULL,
			AuthorId int not NULL,
			CreatedAt datetime default getdate() not null,
			UpdatedAt datetime default getdate() not null,
			DeletedAt datetime NULL,
			
			CONSTRAINT BookEditorialFk FOREIGN KEY (EditorialId) REFERENCES Editorial(Id),
			CONSTRAINT BookAuthorFk FOREIGN KEY (AuthorId) REFERENCES Author(Id),
   );