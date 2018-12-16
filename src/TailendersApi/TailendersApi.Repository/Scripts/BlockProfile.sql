BEGIN TRY

    BEGIN TRAN

    USE [tailendersuat]

    DECLARE @ProfileId VARCHAR
    DECLARE @ReasonCode INT
    DECLARE @Description VARCHAR(MAX)
    DECLARE @BlockedAt DATETIME

    SET @ProfileId = ''
    SET @ReasonCode = 99
    SET @Description = ''
    SET @BlockedAt = GETDATE()

    UPDATE [dbo].[Profiles]
    SET IsBlocked = 1, BlockedAt = @BlockedAt
    WHERE Id = @ProfileId

    INSERT INTO [dbo].[BlockedProfiles] (ProfileId, BlockedAt, ReasonCode, Description)
    VALUES (@ProfileId, @BlockedAt, @ReasonCode, @Description)

    -- COMMIT TRAN
    -- ROLLBACK TRAN

END TRY
BEGIN CATCH

    SELECT 	ERROR_NUMBER() AS ErrorNumber,
            ERROR_SEVERITY() AS ErrorSeverity,
            ERROR_STATE() AS ErrorState,
            ERROR_PROCEDURE() AS ErrorProcedure,
            ERROR_LINE() AS ErrorLine,
            ERROR_MESSAGE() AS ErrorMessage;

    IF @@TRANCOUNT > 0
        ROLLBACK;

END CATCH;