IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'CorregirFechaFinFase') AND type in (N'P', N'PC'))
DROP PROCEDURE CorregirFechaFinFase
GO
-- =============================================
-- Author:		Solver
-- Create date: 00-00-0000
-- Description:
-- =============================================
CREATE PROCEDURE CorregirFechaFinFase
(
	@IdFase bigint
	, @FechaFin datetime
)
AS
/*****************************************************/
--declare	@IdFase bigint, @FechaFin datetime

--set @IdFase = 8
--set	@FechaFin = '2015-12-24'
/*****************************************************/

-- Actualizando Fase
update	f
set		f.FechaFinalizacion = @FechaFin
from	Fases f with(nolock)
where	f.IdFase = @IdFase