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

-- Variables de Trabajo
declare @DiffDias int, @IdContrato int, @Grupo int, @NumFase int

select	@DiffDias = datediff(dd, f.FechaFinalizacion, @FechaFin)	
		,@IdContrato = f.IdContrato		
		,@Grupo = f.Grupo
		,@NumFase = f.NumeroFase	
from	Fases f with(nolock)
where	f.IdFase = @IdFase

-- Actualizando Fase
update	f
set		f.FechaFinalizacion = @FechaFin
		,f.DuracionMeses = datediff(mm, f.FechaInicio, @FechaFin)
from	Fases f with(nolock)
where	f.IdFase = @IdFase

-- Actualizando Fases Asociadas
update	f
set		f.FechaInicio = dateadd(dd,@DiffDias,f.FechaInicio)
		,f.FechaFinalizacion = dateadd(dd,@DiffDias,f.FechaFinalizacion)
from	Fases f with(nolock)
where	f.IdContrato = @IdContrato
		and f.Grupo = @Grupo
		and f.NumeroFase > @NumFase