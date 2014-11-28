IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'ProrrogarFase') AND type in (N'P', N'PC'))
DROP PROCEDURE ProrrogarFase
GO
-- =============================================
-- Author:		Solver
-- Create date: 00-00-0000
-- Description:
-- =============================================
CREATE PROCEDURE ProrrogarFase
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
from	Fases f with(nolock)
where	f.IdFase = @IdFase

-- Actualizando Compromisos
update	comp
set		comp.FechaCumplimiento = dateadd(dd,@DiffDias,comp.FechaCumplimiento)
from	Compromisos comp with(nolock)
where	IdFase = @IdFase

-- Actualizando Fases Asociadas
update	f
set		f.FechaInicio = dateadd(dd,@DiffDias,f.FechaInicio)
		,f.FechaFinalizacion = dateadd(dd,@DiffDias,f.FechaFinalizacion)
from	Fases f with(nolock)
where	f.IdContrato = @IdContrato
		and f.Grupo = @Grupo
		and f.NumeroFase > @NumFase

-- Actualizando compromisos de fases asociadas
update	comp
set		comp.FechaCumplimiento = dateadd(dd,@DiffDias,comp.FechaCumplimiento)
from	Compromisos comp with(nolock)
		join Fases f with(nolock)
			on comp.IdFase = f.IdFase
where	f.IdContrato = @IdContrato
		and f.Grupo = @Grupo
		and f.NumeroFase > @NumFase