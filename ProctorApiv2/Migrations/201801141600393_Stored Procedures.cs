namespace ProctorApiv2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StoredProcedures : DbMigration
    {
        public override void Up()
        {
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

            CreateStoredProcedure("dbo.RoleDelete", c => new { roleId = c.String() }, RoleDeleteSql);
            CreateStoredProcedure("dbo.RoleGetAll", c => new { }, RoleGetAllSql);
            CreateStoredProcedure("dbo.RoleGetById", c => new { roleId = c.String() }, RoleGetByIdSql);
            CreateStoredProcedure("dbo.RoleGetByName", c => new { roleName = c.String() }, RoleGetByNameSql);
            CreateStoredProcedure("dbo.RoleGetUsersById", c => new { roleId = c.String() }, RoleGetUsersByIdSql);
            CreateStoredProcedure("dbo.RoleGetUsersByName", c => new { roleName = c.String() }, RoleGetUsersByNameSql);
            CreateStoredProcedure("dbo.RoleAddUser", c => new { userId = c.String(), roleId = c.String() }, RoleAddUserSql);
            CreateStoredProcedure("dbo.RoleRemoveUser", c => new { userId = c.String(), roleId = c.String() }, RoleRemoveUserSql);

            CreateStoredProcedure("dbo.RoomGetAll", c => new { }, RoomGetAllSql);

            CreateStoredProcedure("dbo.SpeakerUpsert", c => new {   Biography = c.String(),
                                                                    BlogUrl = c.String(),
                                                                    FirstName = c.String(),
                                                                    GitHubLink = c.String(),
                                                                    GravatarUrl = c.String(),
                                                                    Id = c.String(),
                                                                    LastName = c.String(),
                                                                    LinkedInProfile = c.String(),
                                                                    TwitterLink = c.String()}, SpeakerUpsertSql);

            CreateStoredProcedure("dbo.SessionUpsert", c => new {   Abstract = c.String(),
                                                                    Category = c.String(),
                                                                    Id = c.Int(),
                                                                    SessionEndTime = c.DateTime(),
                                                                    SessionStartTime = c.DateTime(),
                                                                    SessionTime = c.DateTime(),
                                                                    SessionType = c.String(),
                                                                    Title = c.String(),
                                                                    Rooms = c.String(),
                                                                    Tags = c.String(),
                                                                    Speakers = c.String()}, SessionUpsertSql);


            CreateStoredProcedure("dbo.AutoAssignUsersToSessions", c => new { }, AutoAssignUsersToSessionsSql);
        }
        
        public override void Down()
        {
            DropStoredProcedure("dbo.UserDelete");
            DropStoredProcedure("dbo.UserGetAll");
            DropStoredProcedure("dbo.UserGetById");
            DropStoredProcedure("dbo.UserUpdate");

            DropStoredProcedure("dbo.RoleDelete");
            DropStoredProcedure("dbo.RoleGetAll");
            DropStoredProcedure("dbo.RoleGetById");
            DropStoredProcedure("dbo.RoleGetByName");
            DropStoredProcedure("dbo.RoleGetUsersById");
            DropStoredProcedure("dbo.RoleGetUsersByName");
            DropStoredProcedure("dbo.RoleAddUser");
            DropStoredProcedure("dbo.RoleRemoveUser");

            DropStoredProcedure("dbo.RoomGetAll");

            DropStoredProcedure("dbo.SpeakerUpsert");

            DropStoredProcedure("dbo.SessionUpsert");

            DropStoredProcedure("dbo.AutoAssignUsersToSessions");
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
        #endregion

        #region Role
        const string RoleDeleteSql = @"	SET NOCOUNT ON;
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
        #endregion

        #region Speaker
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
        const string SessionUpsertSql = @"SET NOCOUNT ON;
	
	                                        DECLARE @SessionId INT = NULL
	                                        DECLARE @SessionTypeId INT

	                                        SELECT @SessionTypeId = Id FROM dbo.SessionTypes st WHERE st.Name = @SessionType
	                                        SELECT @SessionId = Id FROM dbo.Sessions s WHERE s.FeedSessionId = @Id

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
		                                         WHERE FeedSessionId = @Id
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
		                                        )
		                                        VALUES
		                                        (
		                                            @Id         
		                                          , @SessionTime
		                                          , @SessionStartTime
		                                          , @SessionEndTime
		                                          , @Title
		                                          , @Abstract
		                                          , @Category
		                                          , 1
		                                          , @SessionTypeId
		                                        )
		                                        SET @SessionId = @@identity
	                                        END

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
		                                        AND sr.Speaker_Id NOT IN (SELECT r.Id FROM dbo.CSVtoTable(@Speakers,',') cvt INNER JOIN dbo.Speakers r ON r.Id = cvt.RESULT))";

        #endregion

        #region Helpers
        const string AutoAssignUsersToSessionsSql = @"
                                        SET NOCOUNT ON;

                                        DECLARE @SessionId INT
	                                    DECLARE @UserId VARCHAR(128)
	                                    DECLARE @Msg VARCHAR(1000)
	                                    DECLARE @UnableToAssign TABLE(SessionId INT)
 
	                                    SELECT @SessionId = s.Id FROM dbo.Sessions s
	                                    WHERE s.SessionType IN ('General Session', 'Static Session', 'Pre-Compiler', 'Sponsor Session')  
	                                    AND isnull(s.VolunteersRequired,1) > (SELECT count(*) FROM dbo.SessionUsers su WHERE su.Session_Id = s.Id)
	                                    AND s.Id NOT IN (SELECT uta.SessionId FROM @UnableToAssign uta)
	                                    ORDER BY s.SessionStartTime DESC

	                                    WHILE @SessionId IS NOT NULL
	                                    BEGIN
	                                        SET @UserId = NULL 
	                                        SELECT TOP 1 @UserId = anu.Id
		                                    FROM dbo.AspNetUsers anu
		                                    INNER JOIN dbo.AspNetUserRoles anur
			                                    ON anur.UserId = anu.Id
		                                    INNER JOIN dbo.AspNetRoles anr
			                                    ON anr.Id = anur.RoleId
		                                    LEFT JOIN dbo.SessionUsers su
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
			                                    INSERT INTO dbo.SessionUsers
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
			                                    WHERE s.SessionType IN ('General Session', 'Static Session', 'Pre-Compiler', 'Sponsor Session')  
			                                    AND isnull(s.VolunteersRequired,1) > (SELECT count(*) FROM dbo.SessionUsers su WHERE su.Session_Id = s.Id)
			                                    AND s.Id NOT IN (SELECT uta.SessionId FROM @UnableToAssign uta)
			                                    ORDER BY s.SessionStartTime DESC
	                                    END";
        #endregion
    }
}
