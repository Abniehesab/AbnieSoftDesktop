USE [AbnieSoftDB]
GO
/****** Object:  StoredProcedure [dbo].[GetReceiveChequeData]    Script Date: 01/10/1403 02:59:16 ب.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[GetReceiveChequeData]
    @BusinessId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT *
    FROM [AbnieSoftDB].[FIN].[ReceiveCheque]
    WHERE BusinessId = @BusinessId AND IsDelete = 0 AND IsUpdate = 0;
END
