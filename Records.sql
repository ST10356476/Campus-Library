
CREATE TABLE StudentsRecords (
    ID INT PRIMARY KEY IDENTITY,
    BookTitle VARCHAR(255),
    AuthorName VARCHAR(255),
    StudentNumber VARCHAR(50) UNIQUE,
    CellNumber VARCHAR(50),
    ParentNumber VARCHAR(50),
    HomeAddress VARCHAR(255),
    CampusName VARCHAR(100)
);
