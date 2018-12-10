Use graywolf
go

 DECLARE @stmt VARCHAR(300);
 
  -- Cursor to generate ALTER TABLE DROP CONSTRAINT statements  
  DECLARE cur CURSOR FOR
     SELECT 'ALTER TABLE ' + OBJECT_SCHEMA_NAME(parent_object_id) + '.[' + OBJECT_NAME(parent_object_id) +
                    '] DROP CONSTRAINT [' + name + ']'
     FROM sys.foreign_keys 
     WHERE OBJECT_SCHEMA_NAME(referenced_object_id) = 'dbo' 

   OPEN cur;
   FETCH cur INTO @stmt;
 
   -- Drop each found foreign key constraint 
   WHILE @@FETCH_STATUS = 0
     BEGIN
       EXEC (@stmt);
       FETCH cur INTO @stmt;
     END
 
  CLOSE cur;
  DEALLOCATE cur;
   
EXEC sp_MSforeachtable @command1 = "DROP TABLE ?"
GO
