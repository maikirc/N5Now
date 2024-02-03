USE [N5Now]
GO

INSERT INTO [dbo].[Employee]
    ([FirstName]
    ,[LastName]
    ,[Age]
    ,[Company]
    ,[Department]
    ,[State]
    ,[CreationDate]
    ,[CreationUser]
    ,[LastModificationDate]
    ,[LastModificationUser])
VALUES
    ('MIGUEL ANGEL'
    ,'REGALADO CEDEÑO'
    ,42
    ,'N5Now'
    ,'IT'
    ,1
    ,GETDATE()
    ,'MREGALADO'
    ,GETDATE()
    ,'MREGALADO')
GO

INSERT INTO [dbo].[TypePermission]
    ([Description]
    ,[State]
    ,[CreationDate]
    ,[CreationUser]
    ,[LastModificationDate]
    ,[LastModificationUser])
VALUES
    ('VACACIONES'
    ,1
    ,GETDATE()
    ,'MREGALADO'
    ,GETDATE()
    ,'MREGALADO')
GO

INSERT INTO [dbo].[Permission]
    ([IdEmployee]
    ,[IdTypePermission]
    ,[DateFrom]
    ,[DateUntil]
    ,[Observation]
    ,[State]
    ,[CreationDate]
    ,[CreationUser]
    ,[LastModificationDate]
    ,[LastModificationUser])
VALUES
    (1
    ,1
    ,GETDATE()
    ,GETDATE()
    ,'VACACIONES'
    ,1
    ,GETDATE()
    ,'MREGALADO'
    ,GETDATE()
    ,'MREGALADO')
GO
