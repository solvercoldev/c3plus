IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'UnificarFase') AND type in (N'P', N'PC'))
DROP PROCEDURE UnificarFase
GO
-- =============================================
-- Author:		Solver
-- Create date: 00-00-0000
-- Description:
-- =============================================
CREATE PROCEDURE UnificarFase
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
		,f.Nombre = 'Fase ' + cast(f.NumeroFase as varchar(2)) + ' y ' + cast(f.NumeroFase + 1 as varchar(2))
		,f.NumFaseUnificada = f.NumeroFase + 1
		,f.DuracionMeses = datediff(mm, f.FechaInicio, @FechaFin)
from	Fases f with(nolock)
where	f.IdFase = @IdFase


---- Actualizando compromisos de fases siguiente
update	comp
set		comp.IdFase = @IdFase
from	Compromisos comp with(nolock)
		join Fases f with(nolock)
			on comp.IdFase = f.IdFase
where	f.IdContrato = @IdContrato
		and f.Grupo = @Grupo
		and f.NumeroFase = @NumFase + 1
		and f.IsActive = 1

-- Deshabilitando Fase Siguiente
update	f
set		f.IsActive = 0
from	Fases f with(nolock)
where	f.IdContrato = @IdContrato
		and f.Grupo = @Grupo
		and f.NumeroFase = @NumFase + 1
		and f.IsActive = 1