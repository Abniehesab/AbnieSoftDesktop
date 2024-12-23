USE [AbnieSoftDB]
GO
/****** Object:  StoredProcedure [dbo].[GetContractData]    Script Date: 01/10/1403 02:55:48 ب.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[GetContractData]
    @BusinessId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT *
    FROM [AbnieSoftDB].[PRO].[Contract]
    WHERE BusinessId = @BusinessId AND IsDelete = 0;
END 
