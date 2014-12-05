IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'DeleteDocumentoRadicado') AND type in (N'P', N'PC'))
DROP PROCEDURE DeleteDocumentoRadicado
GO
-- =============================================
-- Author:		Solver
-- Create date: 00-00-0000
-- Description:
-- =============================================
CREATE PROCEDURE DeleteDocumentoRadicado
(
	@IdRadicado bigint
)
AS
/*****************************************************/

delete	DocumentosRadicado
where	IdRadicado = @IdRadicado