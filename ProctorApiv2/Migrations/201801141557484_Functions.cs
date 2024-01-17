namespace ProctorApiv2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Functions : DbMigration
    {
        public override void Up()
        {
            Sql(_createHasCollisionFunctionScript);
            Sql(_createHasExceptionFunctionScript);
            Sql(_createCSVToTableFunctionScript);
        }
        
        public override void Down()
        {
            Sql(DropHasCollisionFunctionScript);
            Sql(DropHasExceptionFunctionScript);
            Sql(DropCSVToTableFunctionScript);
        }

        private const string DropHasCollisionFunctionScript = "DROP FUNCTION [dbo].[HasCollision]";
        private const string DropHasExceptionFunctionScript = "DROP FUNCTION [dbo].[HasException]";
        private const string DropCSVToTableFunctionScript = "DROP FUNCTION [dbo].[CSVtoTable]";

        private readonly string _createHasCollisionFunctionScript = @"
                    CREATE FUNCTION [dbo].[HasCollision] 
                    (
	                    -- Add the parameters for the function here
	                    @SessionId INT,
	                    @UserId VARCHAR(128)
                    )
                    RETURNS bit
                    AS
                    BEGIN
	                    -- Declare the return variable here
	                    DECLARE @Result BIT = 0

	                    -- Add the T-SQL statements to compute the return value here
	                    DECLARE @CollisionCount INT = 0

	                    SELECT @CollisionCount = count(*) FROM
	                    (SELECT s.Id , s.SessionStartTime, s.SessionEndTime FROM dbo.UserSessions su 
		                    INNER JOIN (SELECT * FROM dbo.Sessions WHERE VolunteersRequired <> 99) s 
			                    ON s.Id = su.Session_Id
		                    WHERE su.User_Id = @UserId) a,
	
	                    (SELECT * FROM (SELECT * FROM dbo.Sessions WHERE VolunteersRequired <> 99) s 
		                    WHERE s.Id = @SessionId) b
		                    WHERE b.SessionStartTime BETWEEN a.SessionStartTime AND a.SessionEndTime
                                OR b.SessionEndTime BETWEEN a.SessionStartTime AND a.SessionEndTime
                                OR (b.SessionStartTime <= a.SessionStartTime AND b.SessionEndTime >= a.SessionEndTime)

	                    IF @CollisionCount > 0
	                    BEGIN
		                    SET @Result = 1
	                    END

	                    -- Return the result of the function
	                    RETURN @Result

                    END
                    ";
        private readonly string _createHasExceptionFunctionScript = @"
                    CREATE FUNCTION [dbo].[HasException] 
                    (
	                    -- Add the parameters for the function here
	                    @SessionId INT,
	                    @UserId VARCHAR(128)
                    )
                    RETURNS bit
                    AS
                    BEGIN
	                    -- Declare the return variable here
	                    DECLARE @Result BIT = 0

	                    -- Add the T-SQL statements to compute the return value here
	                    DECLARE @ExceptionCount INT = 0

	                    SELECT @ExceptionCount = count(*) FROM
	                    (SELECT se.Id , se.StartTime, se.EndTime FROM dbo.ScheduleExceptions se		                    
		                    WHERE se.User_Id = @UserId) a,
	
	                    (SELECT * FROM dbo.Sessions s 
		                    WHERE s.Id = @SessionId) b
		                    WHERE b.SessionStartTime BETWEEN a.StartTime AND a.EndTime
                                OR b.SessionEndTime BETWEEN a.StartTime AND a.EndTime

	                    IF @ExceptionCount > 0
	                    BEGIN
		                    SET @Result = 1
	                    END

	                    -- Return the result of the function
	                    RETURN @Result

                    END";

        private readonly string _createCSVToTableFunctionScript = @"
                    CREATE FUNCTION [dbo].[CSVtoTable]
                    (
                        @LIST varchar(MAX),
                        @Delimeter varchar(10)
                    )
                    RETURNS @RET1 TABLE (RESULT VARCHAR(50))
                    AS
                    BEGIN
                        DECLARE @RET TABLE(RESULT VARCHAR(50))
    
                        IF LTRIM(RTRIM(@LIST))='' RETURN  

                        DECLARE @START BIGINT
                        DECLARE @LASTSTART BIGINT
                        SET @LASTSTART=0
                        SET @START=CHARINDEX(@Delimeter,@LIST,0)

                        IF @START=0
                        INSERT INTO @RET VALUES(LTRIM(RTRIM(SUBSTRING(@LIST,0,LEN(@LIST)+1))))

                        WHILE(@START >0)
                        BEGIN
                            INSERT INTO @RET VALUES(LTRIM(RTRIM(SUBSTRING(@LIST,@LASTSTART,@START-@LASTSTART))))
                            SET @LASTSTART=@START+1
                            SET @START=CHARINDEX(@Delimeter,@LIST,@START+1)
                            IF(@START=0)
                            INSERT INTO @RET VALUES(LTRIM(RTRIM(SUBSTRING(@LIST,@LASTSTART,LEN(@LIST)+1))))
                        END
    
                        INSERT INTO @RET1 SELECT * FROM @RET
                        RETURN 
                    END";
    }
}
