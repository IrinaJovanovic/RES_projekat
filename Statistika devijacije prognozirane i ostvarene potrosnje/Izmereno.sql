CREATE TABLE [dbo].[Izmereno]
(
	[Region] CHAR(10) NOT NULL , 
    [Sat] INT NOT NULL, 
    [Izmereno] INT NOT NULL, 
    PRIMARY KEY ([Region, Sat]), 
    
)
