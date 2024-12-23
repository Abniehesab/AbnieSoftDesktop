USE [AbnieSoftDB]
GO
/****** Object:  StoredProcedure [dbo].[GetTafziliGroupData]    Script Date: 01/10/1403 03:00:22 ب.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[GetTafziliGroupData]
    @BusinessId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT *
    FROM [AbnieSoftDB].[ACC].[TafziliGroup]
    WHERE BusinessId = @BusinessId AND IsDelete = 0;
END
