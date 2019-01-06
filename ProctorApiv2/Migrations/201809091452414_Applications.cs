namespace ProctorApiv2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Applications : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Applications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Gender = c.String(),
                        EmailAddress = c.String(),
                        Phone = c.String(),
                        School = c.String(),
                        Major = c.String(),
                        Topics = c.String(),
                        Essay = c.String(),
                        SubmitDate = c.String(),
                        FirstTimer = c.Boolean(nullable: false),
                        HowManyYears = c.Int(nullable: false),
                        AcceptedByCodemash = c.Boolean(nullable: false),
                        AcceptedByApplicant = c.Boolean(nullable: false),
                        Registered = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);

            CreateStoredProcedure("dbo.ApplicationDelete", c => new { applicationId = c.String() }, ApplicationsDeleteSql);
            CreateStoredProcedure("dbo.ApplicationGetAll", c => new { }, ApplicationsGetAllSql);
            CreateStoredProcedure("dbo.ApplicationGetById", c => new { applicationId = c.String() }, ApplicationsGetByIdSql);
            CreateStoredProcedure("dbo.ApplicationUpsert", c => new {
                Id = c.Int(),
                FirstName = c.String(),
                LastName = c.String(),
                Gender = c.String(),
                Phone = c.String(),
                EmailAddress = c.String(),
                School = c.String(),
                Major = c.String(),
                Topics = c.String(),
                Essay = c.String(),
                FirstTimer = c.Boolean(),
                HowManyYears = c.Int(),
                AcceptedByCodemash = c.Boolean(),
                AcceptedByApplicant = c.Boolean(),
                Registered = c.Boolean()
            }, ApplicationsUpsertSql);

        }

        public override void Down()
        {
            DropStoredProcedure("dbo.ApplicationDelete");
            DropStoredProcedure("dbo.ApplicationGetAll");
            DropStoredProcedure("dbo.ApplicationGetById");
            DropStoredProcedure("dbo.ApplicationUpsert");

            DropTable("dbo.Applications");
        }

        #region Applications
        const string ApplicationsGetAllSql = @" SET NOCOUNT ON;
	                                    SELECT * FROM dbo.Applications";

        const string ApplicationsGetByIdSql = @"	SET NOCOUNT ON;
	                                    SELECT * FROM dbo.Applications a
                                        WHERE a.Id = @applicationId";

        const string ApplicationsUpsertSql = @"	
IF @Id is null OR @Id = -1
    BEGIN
    --Insert
		INSERT INTO dbo.Applications
		(
		    FirstName
		  , LastName
		  , Gender
		  , EmailAddress
		  , Phone
		  , School
		  , Major
		  , Topics
		  , Essay
		  , SubmitDate
		  , FirstTimer
		  , HowManyYears
		  , AcceptedByCodemash
		  , AcceptedByApplicant
		  , Registered
		)
		VALUES
		(
		    @FirstName
		  , @LastName
		  , @Gender 
		  , @EmailAddress 
		  , @Phone 
		  , @School
		  , @Major 
		  , @Topics
		  , @Essay 
		  , GETDATE()
		  , @FirstTimer 
		  , @HowManyYears 
		  , @AcceptedByCodemash 
		  , @AcceptedByApplicant 
		  , @Registered 
		)
    END
    ELSE
    BEGIN
    --Update
			UPDATE dbo.Applications
				SET FirstName = @FirstName
		  , LastName = @LastName
		  , Gender = @Gender
		  , EmailAddress = @EmailAddress
		  , Phone = @Phone
		  , School = @School
		  , Major = @Major
		  , Topics = @Topics
		  , Essay = @Essay
		  , FirstTimer = @FirstTimer
		  , HowManyYears = @HowManyYears
		  , AcceptedByCodemash = @AcceptedByCodemash
		  , AcceptedByApplicant = @AcceptedByApplicant
		  , Registered = @Registered
		  WHERE id = @Id

    END
";

        const string ApplicationsDeleteSql = @"	SET NOCOUNT ON;
	                                    DELETE dbo.Applications 
                                        WHERE Id = @applicationId";

        #endregion
    }
}
