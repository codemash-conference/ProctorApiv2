namespace ProctorApiv2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StoredProcedures : DbMigration
    {
        public override void Up()
        {
            //User
            CreateStoredProcedure("dbo.UserDelete", c => new { userId = c.String() }, UserDeleteSql);
            CreateStoredProcedure("dbo.UserGetAll", c => new { }, UserGetAllSql);
            CreateStoredProcedure("dbo.UserGetById", c => new { userId = c.String() }, UserGetByIdSql);
            CreateStoredProcedure("dbo.UserUpdate", c => new { userId = c.String(),
                                                                FirstName = c.String(),
                                                                LastName = c.String(),
                                                                Gravatar = c.String(),
                                                                CellNumber = c.String(),
                                                                Email = c.String(),
                                                                UserName = c.String()}, UserUpdateSql);
            CreateStoredProcedure("dbo.UserGetBySessionId", c => new { SessionId = c.Int() }, UserGetBySessionIdSql);

            //Role
            CreateStoredProcedure("dbo.RoleDelete", c => new { roleId = c.String() }, RoleDeleteSql);
            CreateStoredProcedure("dbo.RoleGetAll", c => new { }, RoleGetAllSql);
            CreateStoredProcedure("dbo.RoleGetById", c => new { roleId = c.String() }, RoleGetByIdSql);
            CreateStoredProcedure("dbo.RoleGetByName", c => new { roleName = c.String() }, RoleGetByNameSql);
            CreateStoredProcedure("dbo.RoleGetUsersById", c => new { roleId = c.String() }, RoleGetUsersByIdSql);
            CreateStoredProcedure("dbo.RoleGetUsersByName", c => new { roleName = c.String() }, RoleGetUsersByNameSql);
            CreateStoredProcedure("dbo.RoleAddUser", c => new { userId = c.String(), roleId = c.String() }, RoleAddUserSql);
            CreateStoredProcedure("dbo.RoleRemoveUser", c => new { userId = c.String(), roleId = c.String() }, RoleRemoveUserSql);

            //Room
            CreateStoredProcedure("dbo.RoomGetAll", c => new { }, RoomGetAllSql);
            CreateStoredProcedure("dbo.RoomGetBySessionId", c => new { SessionId = c.Int() }, RoomGetBySessionIdSql);

            //Speaker
            CreateStoredProcedure("dbo.SpeakerUpsert", c => new {   Biography = c.String(defaultValue:"null"),
                                                                    BlogUrl = c.String(defaultValue: "null"),
                                                                    FirstName = c.String(defaultValue: "null"),
                                                                    GitHubLink = c.String(defaultValue: "null"),
                                                                    GravatarUrl = c.String(defaultValue: "null"),
                                                                    Id = c.String(),
                                                                    LastName = c.String(defaultValue: "null"),
                                                                    LinkedInProfile = c.String(defaultValue: "null"),
                                                                    TwitterLink = c.String(defaultValue: "null")
            }, SpeakerUpsertSql);
            CreateStoredProcedure("dbo.SpeakerGetBySessionId", c => new { SessionId = c.Int() }, SpeakerGetBySessionIdSql);

            //Session
            //TODO: Set ActualSessionStartTime and ActualSessionEndTime to default to NULL
            CreateStoredProcedure("dbo.SessionUpsert", c => new {   Abstract = c.String(defaultValue: null),
                                                                    Category = c.String(defaultValue: null),
                                                                    Id = c.Int(),
                                                                    FeedSessionId = c.Int(defaultValue: null),
                                                                    SessionEndTime = c.DateTime(defaultValue: null),
                                                                    SessionStartTime = c.DateTime(defaultValue: null),
                                                                    SessionTime = c.DateTime(defaultValue: null),
                                                                    SessionType = c.String(defaultValue: null),
                                                                    Title = c.String(defaultValue: null),
                                                                    Rooms = c.String(defaultValue: null),
                                                                    Tags = c.String(defaultValue: null),
                                                                    Speakers = c.String(defaultValue: null),
                                                                    ActualSessionStartTime = c.DateTime(defaultValue: null),
                                                                    ActualSessionEndTime = c.DateTime(defaultValue: null),
                                                                    Attendees10 = c.Int(defaultValue: 0),
                                                                    Attendees50 = c.Int(defaultValue: 0),
                                                                    Notes = c.String(defaultValue: null),
                                                                    VolunteersRequired = c.Int(defaultValue: null)
            }, SessionUpsertSql);
            CreateStoredProcedure("dbo.SessionGetAll", c => new { }, SessionGetAllSql);
            CreateStoredProcedure("dbo.SessionGetAllForUser", c => new { UserId = c.String() }, SessionGetAllForUserSql);
            CreateStoredProcedure("dbo.SessionGetById", c => new { SessionId = c.Int() }, SessionGetByIdSql);
            CreateStoredProcedure("dbo.SessionGetAllInfo", c => new { }, SessionGetAllInfoSql);
            CreateStoredProcedure("dbo.SessionAssignUser", c => new { UserId = c.String(), SessionId = c.Int() }, SessionAssignUserSql);
            CreateStoredProcedure("dbo.SessionUnassignUser", c => new { UserId = c.String(), SessionId = c.Int() }, SessionUnassignUserSql);
            CreateStoredProcedure("dbo.SessionGetResults", c => new { }, SessionGetResultsSql);

            //Session Type
            CreateStoredProcedure("dbo.SessionTypeGetBySessionId", c => new { SessionTypeId = c.Int() }, SessionTypeGetBySessionIdSql);

            //Tag
            CreateStoredProcedure("dbo.TagGetBySessionId", c => new { SessionId = c.Int() }, TagGetBySessionIdSql);

            //UserCheckIn
            CreateStoredProcedure("dbo.UserCheckInGetBySessionId", c => new { SessionId = c.Int() }, UserCheckInGetBySessionIdSql);
            CreateStoredProcedure("dbo.UserCheckInUpsert", c => new { SessionId = c.Int(), UserId = c.String(), CheckInTime = c.DateTime(defaultValue: null) }, UserCheckInUpsertSql);

            //Helper
            CreateStoredProcedure("dbo.AutoAssignUsersToSessions", c => new { }, AutoAssignUsersToSessionsSql);

            //ScheduleExceptions
            CreateStoredProcedure("dbo.ScheduleExceptionGetAll", c => new { }, ScheduleExceptionGetAllSql);
            CreateStoredProcedure("dbo.AddSessionSwitch", c => new {
                FromUserId = c.String(),
                ToUserId = c.String(),
                SessionId = c.Int(),
                ForSessionId = c.Int(),
                Type = c.Int()
            }, AddSessionSwitchSql);
        }
        
        public override void Down()
        {
            //User
            DropStoredProcedure("dbo.UserDelete");
            DropStoredProcedure("dbo.UserGetAll");
            DropStoredProcedure("dbo.UserGetById");
            DropStoredProcedure("dbo.UserUpdate");
            DropStoredProcedure("dbo.UserGetBySessionId");

            //Role
            DropStoredProcedure("dbo.RoleDelete");
            DropStoredProcedure("dbo.RoleGetAll");
            DropStoredProcedure("dbo.RoleGetById");
            DropStoredProcedure("dbo.RoleGetByName");
            DropStoredProcedure("dbo.RoleGetUsersById");
            DropStoredProcedure("dbo.RoleGetUsersByName");
            DropStoredProcedure("dbo.RoleAddUser");
            DropStoredProcedure("dbo.RoleRemoveUser");

            //Room
            DropStoredProcedure("dbo.RoomGetAll");
            DropStoredProcedure("dbo.RoomGetBySessionId");

            //Speaker
            DropStoredProcedure("dbo.SpeakerUpsert");
            DropStoredProcedure("dbo.SpeakerGetBySessionId");

            //Session
            DropStoredProcedure("dbo.SessionUpsert");
            DropStoredProcedure("dbo.SessionGetAll");
            DropStoredProcedure("dbo.SessionGetAllForUser");
            DropStoredProcedure("dbo.SessionGetById");
            DropStoredProcedure("dbo.SessionGetAllInfo");
            DropStoredProcedure("dbo.SessionAssignUser");
            DropStoredProcedure("dbo.SessionUnassignUser");

            //SessionType
            DropStoredProcedure("dbo.SessionTypeGetBySessionId");
            
            //Tag
            DropStoredProcedure("dbo.TagGetBySessionId");

            //UserCheckIn
            DropStoredProcedure("dbo.UserCheckInGetBySessionId");
            DropStoredProcedure("dbo.UserCheckInUpsert");

            //Helper
            DropStoredProcedure("dbo.AutoAssignUsersToSessions");

            //ScheduleExceptions
            DropStoredProcedure("dbo.ScheduleExceptionGetAll");
        }
        
        #region User
        const string UserGetAllSql = @"	SET NOCOUNT ON;
	                                    SELECT * FROM dbo.AspNetUsers anu";

        const string UserGetByIdSql = @"SET NOCOUNT ON;
	                                    SELECT * FROM dbo.AspNetUsers anu WHERE Id = @userId";

        const string UserUpdateSql = @"SET NOCOUNT ON;
	                                    UPDATE dbo.AspNetUsers 
		                                    SET FirstName = @FirstName,
			                                    LastName = @LastName,
			                                    Gravatar = @Gravatar,
			                                    CellNumber = @CellNumber,
			                                    Email = @Email
			                                    WHERE Id = @userId";


        const string UserDeleteSql = @"
                                        SET NOCOUNT ON;
	                                    SELECT * INTO #user FROM dbo.AspNetUsers anu WHERE Id = @userId
	                                    DELETE FROM dbo.AspNetUsers WHERE Id = @userId
	                                    SELECT * FROM #user";

        const string UserGetBySessionIdSql = @"		SET NOCOUNT ON;
	                                                IF (SELECT s.VolunteersRequired FROM dbo.Sessions s WHERE id = @SessionId) = 99
	                                                BEGIN
		                                                SELECT * FROM dbo.AspNetUsers anu
	                                                END
	                                                ELSE
	                                                BEGIN
		                                                SELECT * FROM dbo.AspNetUsers anu
    		                                                INNER JOIN dbo.UserSessions us
    			                                                ON us.User_Id = anu.Id
		                                                WHERE us.Session_Id = @SessionId    
	                                                END  ";
        #endregion

        #region Role
        const string RoleDeleteSql = @"    SET NOCOUNT ON;
                                    	DELETE FROM dbo.AspNetRoles WHERE Id = @roleId";

        const string RoleGetAllSql = @"	SET NOCOUNT ON;
	                                    SELECT * FROM dbo.AspNetRoles";

        const string RoleGetByIdSql = @"SET NOCOUNT ON;
	                                    SELECT * FROM dbo.AspNetRoles WHERE Id = @roleId";

        const string RoleGetByNameSql = @"	SET NOCOUNT ON;
	                                        SELECT * FROM dbo.AspNetRoles WHERE Name = @roleName";

        const string RoleGetUsersByIdSql = @"	SET NOCOUNT ON;
	                                            SELECT * FROM dbo.AspNetUsers anu 
		                                            INNER JOIN dbo.AspNetUserRoles anur
		                                            ON anur.UserId = anu.Id
		                                            WHERE anur.RoleId = @roleId";

        const string RoleGetUsersByNameSql = @"	SET NOCOUNT ON;
	                                            SELECT * FROM dbo.AspNetUsers anu 
		                                            INNER JOIN dbo.AspNetUserRoles anur
			                                            ON anur.UserId = anu.Id
		                                            INNER JOIN dbo.AspNetRoles anr
			                                            ON anr.Id = anur.RoleId
		                                            WHERE anr.Name = @roleName";

        const string RoleAddUserSql = @"	SET NOCOUNT ON;
	                                        INSERT INTO dbo.AspNetUserRoles
	                                            (UserId, RoleId)
	                                        VALUES
                                                (@userId, @roleId)";

        const string RoleRemoveUserSql = @"	SET NOCOUNT ON;
	                                        DELETE dbo.AspNetUserRoles WHERE UserId = @userId AND RoleId = @roleId";

        #endregion

        #region Room
        const string RoomGetAllSql = @" SET NOCOUNT ON;
	                                    SELECT * FROM dbo.Rooms Order By Name";

        const string RoomGetBySessionIdSql = @"	SET NOCOUNT ON;
	                                    SELECT * FROM dbo.Rooms r
		                                    INNER JOIN dbo.SessionRooms sr
			                                    ON sr.Room_Id = r.Id
	                                    WHERE sr.Session_Id = @SessionId";

        #endregion

        #region Speaker
        const string SpeakerGetBySessionIdSql = @"  SET NOCOUNT ON;
	                                        SELECT * FROM dbo.Speakers s
		                                        INNER JOIN dbo.SpeakerSessions ss
			                                        ON ss.Speaker_Id = s.Id
	                                        WHERE ss.Session_Id = @SessionId";

        const string SpeakerUpsertSql = @"SET NOCOUNT ON;
	                                        IF (SELECT 1 FROM Speakers WHERE Id = @Id) = 1
	                                        BEGIN
		                                        --UPDATE
		                                        UPDATE [dbo].[Speakers]
		                                           SET [FirstName] = @FirstName
			                                          ,[LastName] = @LastName
			                                          ,[Biography] = @Biography
			                                          ,[GravatarUrl] = @GravatarUrl
			                                          ,[TwitterLink] = @TwitterLink
			                                          ,[GitHubLink] = @GitHubLink
			                                          ,[LinkedInProfile] = @LinkedInProfile
			                                          ,[BlogUrl] = @BlogUrl
		                                         WHERE Id = @Id
	                                        END
	                                        ELSE
	                                        BEGIN
		                                        --INSERT
		                                        INSERT INTO dbo.Speakers
		                                        (
		                                            Id
		                                          , FirstName
		                                          , LastName
		                                          , Biography
		                                          , GravatarUrl
		                                          , TwitterLink
		                                          , GitHubLink
		                                          , LinkedInProfile
		                                          , BlogUrl
		                                        )
		                                        VALUES
		                                        (
		                                            @Id
		                                          , @FirstName
		                                          , @LastName
		                                          , @Biography
		                                          , @GravatarUrl
		                                          , @TwitterLink
		                                          , @GitHubLink
		                                          , @LinkedInProfile
		                                          , @BlogUrl
		                                        )
	                                        END";

        #endregion

        #region Session
        const string SessionGetAllSql = @"SET NOCOUNT ON;
	                                    SELECT
		                                    s.Id
                                          , s.FeedSessionId
                                          , s.SessionTime
                                          , s.SessionStartTime
                                          , s.SessionEndTime
                                          , s.Title
                                          , s.Abstract
                                          , s.Category
                                          , s.VolunteersRequired
                                          , s.ActualSessionStartTime
                                          , s.ActualSessionEndTime
                                          , s.Attendees10
                                          , s.Attendees50
                                          , s.Notes
                                          , s.SessionType_Id
	                                    FROM dbo.Sessions s";

        const string SessionGetByIdSql = @"
SET NOCOUNT ON;
	SELECT
		s.Id
      , s.FeedSessionId
      , s.SessionTime
      , s.SessionStartTime
      , s.SessionEndTime
      , s.Title
      , s.Abstract
      , s.Category
      , s.VolunteersRequired
      , s.ActualSessionStartTime
      , s.ActualSessionEndTime
      , s.Attendees10
      , s.Attendees50
      , s.Notes
	  , s.SessionType_Id
	FROM dbo.Sessions s
	WHERE Id = @SessionId
";

        const string SessionGetAllForUserSql = @"
SET NOCOUNT ON;
	SELECT
		s.Id
      , s.FeedSessionId
      , s.SessionTime
      , s.SessionStartTime
      , s.SessionEndTime
      , s.Title
      , s.Abstract
      , s.Category
      , s.VolunteersRequired
      , s.ActualSessionStartTime
      , s.ActualSessionEndTime
      , s.Attendees10
      , s.Attendees50
      , s.Notes
	  , s.SessionType_Id
	FROM dbo.Sessions s
		LEFT JOIN dbo.UserSessions us
		ON us.Session_Id = s.Id
	WHERE us.User_Id = @UserId OR s.VolunteersRequired = 99
";

        const string SessionGetAllInfoSql = @"	    SET NOCOUNT ON;
    
    SELECT * FROM dbo.SessionTypes st
    
    SELECT r.*, sr.Session_Id FROM dbo.Rooms r
    INNER JOIN dbo.SessionRooms sr
    	ON sr.Room_Id = r.Id
    	
    
    SELECT s.*, ss.Session_Id FROM dbo.Speakers s
    	INNER JOIN dbo.SpeakerSessions ss
    		ON ss.Speaker_Id = s.Id
    	
    
    SELECT t.*, ts.Session_Id FROM dbo.Tags t
    	INNER JOIN dbo.TagSessions ts
    		ON ts.Tag_Id = t.Id
    	
    
    SELECT * FROM dbo.UserCheckIns uci
    	
    
    SELECT anu.*, us.Session_Id FROM dbo.AspNetUsers anu
    	INNER JOIN dbo.UserSessions us
    		ON us.User_Id = anu.Id
	UNION
	SELECT anu.*, us.Id AS Session_Id FROM dbo.AspNetUsers anu
    	CROSS JOIN dbo.Sessions us
    		WHERE us.VolunteersRequired = 99";

        const string SessionAssignUserSql = @"SET NOCOUNT ON;
	                                    INSERT INTO dbo.UserSessions
	                                    (User_Id, Session_Id)
                                        VALUES
	                                    (@UserId,@SessionId)";

        const string SessionUnassignUserSql = @"SET NOCOUNT ON;
	                                    DELETE dbo.UserSessions WHERE Session_Id = @SessionId AND User_Id = @UserId";

        const string SessionGetResultsSql = @"SET NOCOUNT ON;

	SELECT s.[Id]
		  ,[FeedSessionId] AS SessionAlternateId
		  ,[SessionStartTime]
		  ,[SessionEndTime]
		  , r.Name AS Rooms
		  ,[Title]
		  , st.Name AS SessionType
		  , uci.CheckInTime AS ProctorCheckInTime
		  ,[ActualSessionStartTime]
		  ,[ActualSessionEndTime]
		  ,[Attendees10]
		  ,[Attendees50]
		  ,[Notes]
	  FROM [Sessions] s
		INNER JOIN dbo.SessionTypes st
			ON st.Id = s.SessionType_Id
		LEFT JOIN dbo.UserCheckIns uci
			ON uci.SessionId = s.Id AND uci.CheckInTime IS NOT NULL
		LEFT JOIN dbo.SessionRooms sr
			ON sr.Session_Id = s.Id
		LEFT JOIN dbo.Rooms r
			ON r.Id = sr.Room_Id
			WHERE st.Name IN ('General Session', 'Pre-Compiler', 'PreCompiler', 'Sponsor Session')
			AND s.FeedSessionId IS NOT NULL";

        const string SessionUpsertSql = @"
    SET NOCOUNT ON;
    	
    	DECLARE @SessionId INT = NULL
    	DECLARE @SessionTypeId INT
    
    	SELECT @SessionTypeId = Id FROM dbo.SessionTypes st WHERE st.Name = @SessionType
		IF @SessionTypeId IS NULL
		BEGIN
		    INSERT INTO dbo.SessionTypes ( Name ) VALUES ( @SessionType )
			SET @SessionTypeId = scope_identity()
		END

    
    	IF @FeedSessionId <> 0 AND @FeedSessionId IS NOT NULL
    	BEGIN
    		SELECT @SessionId = Id FROM dbo.Sessions s WHERE s.FeedSessionId = @FeedSessionId		
    	END
    	ELSE
    	BEGIN
    		IF(@Id = 0 OR @Id = NULL)
    		BEGIN
    			SELECT @SessionId = NULL
    		END
    		ELSE
    		BEGIN
    			SELECT @SessionId = @Id
    		END
    	END 
    
    	--Add session type if it does not exist
    	IF @SessionTypeId IS NULL
    	BEGIN
    		INSERT INTO dbo.SessionTypes (Name)
    		VALUES (@SessionType)
    		SET @SessionTypeId = @@identity
    	END 
    
    	IF @SessionId IS NOT NULL
    	BEGIN
    		--UPDATE
    		UPDATE [dbo].[Sessions]
    		   SET Abstract = @Abstract
    			  ,Category = @Category
    			  ,SessionEndTime = @SessionEndTime
    			  ,SessionStartTime = @SessionStartTime
    			  ,SessionTime = @SessionTime
    			  ,SessionType_Id = @SessionTypeId
    			  ,Title = @Title			
    			  ,ActualSessionStartTime = @ActualSessionStartTime
    			  ,ActualSessionEndTime = @ActualSessionEndTime
    			  ,Attendees10 = @Attendees10
    			  ,Attendees50 = @Attendees50  
    			  ,Notes =@Notes
    			  ,VolunteersRequired = @VolunteersRequired
    		 WHERE Id = @Id
    	END
    	ELSE
    	BEGIN
    		--INSERT
    		INSERT INTO dbo.Sessions
    		(
    		    FeedSessionId
    		  , SessionTime
    		  , SessionStartTime
    		  , SessionEndTime
    		  , Title
    		  , Abstract
    		  , Category
    		  , VolunteersRequired
    		  , SessionType_Id
    		  , Attendees10
    		  , Attendees50		  
    		)
    		VALUES
    		(
    		    @FeedSessionId         
    		  , @SessionTime
    		  , @SessionStartTime
    		  , @SessionEndTime
    		  , @Title
    		  , @Abstract
    		  , @Category
    		  , @VolunteersRequired
    		  , @SessionTypeId
    		  , 0
    		  , 0
    		)
    		SET @SessionId = @@identity
    	END
    
    	--Add room if it doesn't exist
    	INSERT INTO dbo.Rooms (Name)
    	SELECT r.RESULT FROM dbo.CSVtoTable(@Rooms,',') r
    	WHERE r.RESULT NOT IN (SELECT Name FROM dbo.Rooms)
    
    	--INSERT ROOMS
    	INSERT INTO dbo.SessionRooms
    	( Session_Id, Room_Id)
    	SELECT @SessionId, r.Id FROM dbo.CSVtoTable(@Rooms,',') cvt
    			INNER JOIN dbo.Rooms r ON r.Name = cvt.RESULT
    		WHERE r.Id NOT IN (SELECT sr.Room_Id FROM dbo.SessionRooms sr WHERE sr.Session_Id = @SessionId)
    
    	--DELETE ROOMS
    	DELETE dbo.SessionRooms WHERE Session_Id = @SessionId AND Room_Id IN (
    	SELECT sr.Room_Id FROM dbo.SessionRooms sr WHERE sr.Session_Id = @SessionId
    		AND sr.Room_Id NOT IN (SELECT r.Id FROM dbo.CSVtoTable(@Rooms,',') cvt INNER JOIN dbo.Rooms r ON r.Name = cvt.RESULT))
    
    
    	--Add tags if it doesn't exist
    	INSERT INTO dbo.Tags (Name)
    	SELECT t.RESULT FROM dbo.CSVtoTable(@Tags,',') t
    	WHERE t.RESULT NOT IN (SELECT Name FROM dbo.Tags)
    
    	--INSERT Tags
    	INSERT INTO dbo.TagSessions
    	( Session_Id, Tag_Id)
    	SELECT @SessionId, r.Id FROM dbo.CSVtoTable(@Tags,',') cvt
    			INNER JOIN dbo.Tags r ON r.Name = cvt.RESULT
    		WHERE r.Id NOT IN (SELECT sr.Tag_Id FROM dbo.TagSessions sr WHERE sr.Session_Id = @SessionId)
    
    	--DELETE Tags
    	DELETE dbo.TagSessions WHERE Session_Id = @SessionId AND Tag_Id IN (
    	SELECT sr.Tag_Id FROM dbo.TagSessions sr WHERE sr.Session_Id = @SessionId
    		AND sr.Tag_Id NOT IN (SELECT r.Id FROM dbo.CSVtoTable(@Tags,',') cvt INNER JOIN dbo.Tags r ON r.Name = cvt.RESULT))
    
    	--INSERT Speakers
    	INSERT INTO dbo.SpeakerSessions	
    	( Session_Id, Speaker_Id)
    	SELECT @SessionId, r.Id FROM dbo.CSVtoTable(@Speakers,',') cvt
    			INNER JOIN dbo.Speakers r ON r.Id = cvt.RESULT
    		WHERE r.Id NOT IN (SELECT sr.Speaker_Id FROM dbo.SpeakerSessions sr WHERE sr.Session_Id = @SessionId)
    
    	--DELETE Speakers
    	DELETE dbo.SpeakerSessions WHERE Session_Id = @SessionId AND Speaker_Id IN (
    	SELECT sr.Speaker_Id FROM dbo.SpeakerSessions sr WHERE sr.Session_Id = @SessionId
    		AND sr.Speaker_Id NOT IN (SELECT r.Id FROM dbo.CSVtoTable(@Speakers,',') cvt INNER JOIN dbo.Speakers r ON r.Id = cvt.RESULT))
    
    	RETURN @SessionId
";

        #endregion

        #region SessionType
        const string SessionTypeGetBySessionIdSql = @"
	                            SET NOCOUNT ON;
	                            SELECT * FROM dbo.SessionTypes st
	                            WHERE st.Id = @SessionTypeId";
        #endregion

        #region Tag
        const string TagGetBySessionIdSql = @"	SET NOCOUNT ON;
	                            SELECT * FROM dbo.Rooms r
		                            INNER JOIN dbo.SessionRooms sr
			                            ON sr.Room_Id = r.Id
	                            WHERE sr.Session_Id = @SessionId";
        #endregion

        #region UserCheckIn
        const string UserCheckInGetBySessionIdSql = @"	SET NOCOUNT ON;
	                            SELECT * FROM dbo.UserCheckIns uci
	                            WHERE uci.SessionId = @SessionId";

        const string UserCheckInUpsertSql = @"
    SET NOCOUNT ON
	IF (SELECT count(*) FROM dbo.UserCheckIns uci WHERE uci.UserId = @UserId AND uci.SessionId = @SessionId) > 0
	BEGIN
		--UPDATE
		UPDATE dbo.UserCheckIns
			SET CheckInTime = @CheckInTime
			WHERE UserId = @UserId AND SessionId = @SessionId
	END
	ELSE
	BEGIN
		--INSERT
		INSERT dbo.UserCheckIns
		(
			SessionId
		  , UserId
		  , CheckInTime
		)
		VALUES
		(
			@SessionId         -- SessionId - int
		  , @UserId       -- UserId - nvarchar(128)
		  , @CheckInTime -- CheckInTime - datetime
		)

	END
";
        #endregion

        #region Helpers
        const string AutoAssignUsersToSessionsSql = @"
    SET NOCOUNT ON;
    
    DECLARE @SessionId INT
    DECLARE @UserId VARCHAR(128)
    DECLARE @Msg VARCHAR(1000)
    DECLARE @UnableToAssign TABLE(SessionId INT)
    
    SELECT @SessionId = s.Id FROM dbo.Sessions s
		INNER JOIN dbo.SessionTypes st 
		ON st.Id = s.SessionType_Id
    WHERE s.SessionType_Id IN (SELECT Id FROM dbo.SessionTypes st WHERE Name IN ('General Session', 'Static Session', 'Pre-Compiler', 'PreCompiler', 'Sponsor Session'))  
    AND isnull(s.VolunteersRequired,1) > (SELECT count(*) FROM dbo.UserSessions su WHERE su.Session_Id = s.Id)
    AND s.Id NOT IN (SELECT uta.SessionId FROM @UnableToAssign uta)
	AND s.VolunteersRequired <> 99
    ORDER BY st.Priority, s.SessionStartTime DESC
    
    WHILE @SessionId IS NOT NULL
    BEGIN
    	SET @UserId = NULL 
    	SELECT TOP 1 @UserId = anu.Id
    	FROM dbo.AspNetUsers anu
    	INNER JOIN dbo.AspNetUserRoles anur
    		ON anur.UserId = anu.Id
    	INNER JOIN dbo.AspNetRoles anr
    		ON anr.Id = anur.RoleId
    	LEFT JOIN dbo.UserSessions su
    		ON su.User_Id = anu.Id
    	LEFT JOIN dbo.Sessions s
    		ON s.Id = su.Session_Id
    	WHERE anr.Name = 'Volunteers'
    	AND dbo.HasCollision(@SessionId, anu.Id) = 0
    		AND dbo.HasException(@SessionId, anu.Id) = 0
    	GROUP BY anu.Id
    	ORDER BY sum(isnull(datediff(SECOND, s.SessionStartTime, s.SessionEndTime),0))
    
    	
    	IF @UserId IS NOT NULL
    	BEGIN
    		INSERT INTO dbo.UserSessions
    		(
    			Session_Id
    			, User_Id
    		)
    		VALUES
    		(
    			@SessionId   -- Session_Id - int
    			, @UserId -- User_Id - nvarchar(128)
    		)	
    		SET @Msg = 'Assigned session ' + CAST(@SessionId AS VARCHAR(20)) + ' to user ' + @UserId
    		RAISERROR (@Msg, 0, 0)
    	END
    	ELSE
    	BEGIN
    		INSERT INTO  @UnableToAssign
    		(
    			SessionId
    		)
    		VALUES
    		(
    			@SessionId -- SessionId - int
    		)
    		SET @Msg = 'Could not assign session ' + CAST(@SessionId AS VARCHAR(20)) + ' to any user.'
    		RAISERROR (@Msg, 0, 0)
    	END
    	SET @SessionId = NULL 
    	SELECT @SessionId = s.Id FROM dbo.Sessions s
		        INNER JOIN dbo.SessionTypes st 
		        ON st.Id = s.SessionType_Id
            WHERE s.SessionType_Id IN (SELECT Id FROM dbo.SessionTypes st WHERE Name IN ('General Session', 'Static Session', 'Pre-Compiler', 'PreCompiler', 'Sponsor Session'))  
            AND isnull(s.VolunteersRequired,1) > (SELECT count(*) FROM dbo.UserSessions su WHERE su.Session_Id = s.Id)
            AND s.Id NOT IN (SELECT uta.SessionId FROM @UnableToAssign uta)
	        AND s.VolunteersRequired <> 99
            ORDER BY st.Priority, s.SessionStartTime DESC
    END
";
        #endregion

        #region ScheduleExceptions
        const string ScheduleExceptionGetAllSql = @"
 SET NOCOUNT ON

    SELECT se.Id
         , se.StartTime
         , se.EndTime
         , se.User_Id
		  FROM dbo.ScheduleExceptions se
";
        const string AddSessionSwitchSql = @"
SET NOCOUNT ON

	INSERT INTO dbo.SessionSwitches
	(
	    FromUserId
	  , ToUserId
	  , SessionId
	  , CreatedTime
	  , Status
	)
	VALUES
	(
	    @FromUserId
	  , @ToUserId
	  , @SessionId
	  , getdate()
	  , N'Pending'       
	)
    
	IF @Type = 2
	BEGIN
	    INSERT INTO dbo.SessionSwitches
	    (
	        FromUserId
	      , ToUserId
	      , SessionId
	      , CreatedTime
	      , Status
	      , RelatedSessionSwitchId
	    )
	    VALUES
	    (
	        @FromUserId
	      , @ToUserId 
	      , @ForSessionId
	      , getdate() 
	      , N'Pending'
	      , scope_identity()
	    )
	END";
        #endregion
    }
}
