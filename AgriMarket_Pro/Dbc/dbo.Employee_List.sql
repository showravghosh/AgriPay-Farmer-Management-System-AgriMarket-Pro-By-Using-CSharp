CREATE TABLE [dbo].[Employee List] (
    [Id]            INT          IDENTITY (1, 1) NOT NULL,
    [Employee Name] VARCHAR (50) NOT NULL,
    [Salary]        INT          NOT NULL,
    [Phone Number]  VARCHAR(50)   NOT NULL,
    [Gender]        VARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

