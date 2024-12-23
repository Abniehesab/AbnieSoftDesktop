USE [AbnieSoftDB]
GO
/****** Object:  StoredProcedure [dbo].[GetMoeinTafziliGroupData]    Script Date: 01/10/1403 02:58:32 ب.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[GetMoeinTafziliGroupData]
    @BusinessId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT *
    FROM [AbnieSoftDB].[ACC].[MoeinTafziliGroup]
    WHERE BusinessId = @BusinessId AND IsDelete = 0;
END
