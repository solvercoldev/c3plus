IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'ExtenderFase') AND type in (N'P', N'PC'))
DROP PROCEDURE ExtenderFase
GO
-- =============================================
-- Author:		Solver
-- Create date: 00-00-0000
-- Description:
-- =============================================
CREATE PROCEDURE ExtenderFase
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

-- Variables de Trabajo
declare @DiffDias int

select	@DiffDias = datediff(dd, f.FechaFinalizacion, @FechaFin)		
from	Fases f with(nolock)
where	f.IdFase = @IdFase

-- Actualizando Fase
update	f
set		f.FechaFinalizacion = @FechaFin
		,f.DuracionMeses = datediff(mm, f.FechaInicio, @FechaFin)
from	Fases f with(nolock)
where	f.IdFase = @IdFase

-- Actualizando Compromisos
update	comp
set		comp.FechaCumplimiento = dateadd(dd,@DiffDias,comp.FechaCumplimiento)
from	Compromisos comp with(nolock)
where	IdFase = @IdFase