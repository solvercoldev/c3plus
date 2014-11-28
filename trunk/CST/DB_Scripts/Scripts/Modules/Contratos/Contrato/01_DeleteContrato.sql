IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'DeleteContrato') AND type in (N'P', N'PC'))
DROP PROCEDURE DeleteContrato
GO
-- =============================================
-- Author:		Solver
-- Create date: 00-00-0000
-- Description:
-- =============================================
CREATE PROCEDURE DeleteContrato
(
	@IdContrato int
)
AS
/*****************************************************/
--declare	@IdContrato int
--set	@IdContrato = 0
/*****************************************************/

-- Eliminando Contrato
delete LogContratos where IdContrato = @IdContrato
delete Contratos where IdContrato = @IdContrato