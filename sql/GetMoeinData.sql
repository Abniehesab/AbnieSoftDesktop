USE [AbnieSoftDB]
GO
/****** Object:  StoredProcedure [dbo].[GetMoeinData]    Script Date: 01/10/1403 02:58:20 ب.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[GetMoeinData]
    @BusinessId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT *
    FROM [AbnieSoftDB].[ACC].[Moein]
    WHERE BusinessId = @BusinessId AND IsDelete = 0;
END
