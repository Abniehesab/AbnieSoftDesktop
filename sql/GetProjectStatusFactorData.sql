USE [AbnieSoftDB]
GO
/****** Object:  StoredProcedure [dbo].[GetProjectStatusFactorData]    Script Date: 01/10/1403 02:59:02 ب.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[GetProjectStatusFactorData]
    @BusinessId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT *
    FROM [AbnieSoftDB].[PRO].[ProjectStatusFactor]
    WHERE BusinessId = @BusinessId AND IsDelete = 0;
END
