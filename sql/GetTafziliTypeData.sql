USE [AbnieSoftDB]
GO
/****** Object:  StoredProcedure [dbo].[GetTafziliTypeData]    Script Date: 01/10/1403 03:00:33 ب.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[GetTafziliTypeData]
    @BusinessId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT *
    FROM [AbnieSoftDB].[ACC].[TafziliType];
END
