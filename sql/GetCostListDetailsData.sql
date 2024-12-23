USE [AbnieSoftDB]
GO
/****** Object:  StoredProcedure [dbo].[GetCostListDetailsData]    Script Date: 01/10/1403 02:56:32 ب.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[GetCostListDetailsData]
    @BusinessId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT *
    FROM [AbnieSoftDB].[PRO].[CostListDetails]
    WHERE BusinessId = @BusinessId AND IsDelete = 0 AND IsUpdate = 0;
END
