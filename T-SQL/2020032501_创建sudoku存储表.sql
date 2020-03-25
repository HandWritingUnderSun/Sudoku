IF EXISTS ( SELECT  *
            FROM    sys.objects
            WHERE   object_id = OBJECT_ID(N'[dbo].[FourLevelProgram]')
                    AND type IN ( N'U' ) )
    BEGIN
        DROP TABLE dbo.FourLevelProgram;
    END;
   GO
   
CREATE TABLE FourLevelProgram
    (
      Rid UNIQUEIDENTIFIER NOT NULL ,
      RowPos INT NOT NULL ,
      ColPos INT NOT NULL ,
      value INT NULL
    );