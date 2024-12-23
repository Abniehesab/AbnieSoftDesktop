USE [AbnieSoftDB]
GO
/****** Object:  StoredProcedure [dbo].[GetAccDocmentDetailsData]    Script Date: 01/10/1403 02:54:45 ب.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[GetAccDocmentDetailsData]
    @BusinessId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT *
    FROM [AbnieSoftDB].[ACC].[AccDocmentDetails]
    WHERE BusinessId = @BusinessId AND IsDelete = 0 AND IsUpdate = 0;
END 
