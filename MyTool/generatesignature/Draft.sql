DROP PROCEDURE IF EXISTS UpdateTenant;
-- delimiter $$
create procedure UpdateTenant()
begin
    declare TableName varchar(64);  
      
    DECLARE cur_FountTable CURSOR FOR SELECT TABLE_NAME FROM information_schema.TABLES WHERE table_schema = 'scc_0710';
    DECLARE EXIT HANDLER FOR not found CLOSE cur_FountTable;
    #打开游标
    OPEN cur_FountTable;
    REPEAT
     FETCH cur_FountTable INTO TableName;
    -- SET @SQLSTR1 = CONCAT('select * from ',TableName);
    -- PREPARE STMT1 FROM @SQLSTR1;
    --  EXECUTE STMT1;
       IF ( EXISTS(SELECT * FROM information_schema.columns WHERE table_schema = DATABASE()
                            AND table_name = CONCAT(TableName,'')  AND column_name = 'tenant'))  THEN
              -- update CONCAT(TableName,'')  set tenant = 'hrone6';
                    SET @SQLSTR1 = CONCAT('update  ',TableName ,' set tenant = ''hrone6''');
                   PREPARE STMT1 FROM @SQLSTR1;
          EXECUTE STMT1;
         end if;
    -- DEALLOCATE PREPARE STMT1;   
        
     UNTIL 0 END REPEAT;
  #关闭游标
  CLOSE cur_FountTable;
   
END ;
-- DELIMITER ;
   
call UpdateTenant();
DROP PROCEDURE IF EXISTS UpdateTenant;