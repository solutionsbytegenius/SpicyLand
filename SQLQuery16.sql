--EXEC sp_configure 'show advanced options', 1;
--RECONFIGURE;
--EXEC sp_configure 'Ad Hoc Distributed Queries', 1;
--RECONFIGURE;


---- Abilita la funzionalità Ad Hoc Distributed Queries (da eseguire sul server 127.0.0.1:1433)
--EXEC sp_configure 'show advanced options', 1;
--RECONFIGURE;
--EXEC sp_configure 'Ad Hoc Distributed Queries', 1;
--RECONFIGURE;

-- Importa i dati dal server localhost:27123 al server 127.0.0.1:1433
INSERT INTO [dbo].[User]
           ([UserID]
           ,[UserName]
           ,[Password]
           ,[DataCreazione]
           ,[Admin]
           ,[SuperAdmin])
SELECT *
FROM OPENROWSET('SQLNCLI', 'Server=tcp:localhost,27123;Trusted_Connection=yes;', 'SELECT TOP (1000) [UserID]
      ,[UserName]
      ,[Password]
      ,[DataCreazione]
      ,[Admin]
      ,[SuperAdmin]
  FROM [SpicyLand].[dbo].[User]
')
