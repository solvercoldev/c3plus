IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'SuspenderContrato') AND type in (N'P', N'PC'))
DROP PROCEDURE SuspenderContrato
GO
-- =============================================
-- Author:		Solver
-- Create date: 00-00-0000
-- Description:
-- =============================================
CREATE PROCEDURE SuspenderContrato
(
	@IdContrato int,
	@FechaInicio datetime,
	@FechaFin datetime
)
AS
/*****************************************************/
--declare	@IdContrato int, @FechaInicio datetime, @FechaFin datetime

--set @IdContrato = 1
--set	@FechaInicio = '2015-01-12'
--set	@FechaFin = '2015-12-24'
/*****************************************************/

-- Definiendo Variables de trabajo
declare	@FechaAux datetime, @DiffDias int, @TotalFasesPost int, @IndexFases int, @AuxDuracionMeses int
declare	@FasesPosteriores table
		(
			Id					int
			,IdContrato			int
			,IdFase				bigint
			,NumeroFase			int
			,DuracionMeses		int
			,FechaInicio		datetime
			,FechaFinalizacion	datetime
			
		)	

set	@DiffDias = datediff(dd, @FechaInicio, @FechaFin)	

-- Modificando fecha final a fase solo la que se encuentra dentro de la novedad
select	top 1
		@FechaAux = dateadd(dd,@DiffDias, f.FechaFinalizacion)
from	Fases f with(nolock)
where	f.IdContrato = @IdContrato
		and f.NumeroFase > 0	
		and (f.FechaInicio >= @FechaInicio
		or f.FechaFinalizacion >= @FechaInicio)	
		and f.IdFase in
			(
				select	f2.IdFase
				from	Fases f2 with(nolock)
				where	f2.IdContrato = @IdContrato
						and GETDATE() >= f2.FechaInicio
						and GETDATE() <= f2.FechaFinalizacion
			)
			
update	f
set		f.FechaFinalizacion = dateadd(dd,@DiffDias, f.FechaFinalizacion)
		,f.DuracionMeses = datediff(mm, f.FechaInicio, dateadd(dd,@DiffDias, f.FechaFinalizacion))	
from	Fases f with(nolock)
where	f.IdContrato = @IdContrato
		and f.NumeroFase > 0	
		and (f.FechaInicio >= @FechaInicio
		or f.FechaFinalizacion >= @FechaInicio)	
		and f.IdFase in
			(
				select	f2.IdFase
				from	Fases f2 with(nolock)
				where	f2.IdContrato = @IdContrato
						and GETDATE() >= f2.FechaInicio
						and GETDATE() <= f2.FechaFinalizacion
			)
			
-- Actualizando Fases Posteriores
-- Seleccionando fases a trabajar
insert	into
		@FasesPosteriores
		(
			Id
			,IdContrato
			,IdFase
			,NumeroFase
			,DuracionMeses
			,FechaInicio
			,FechaFinalizacion
		)
select	row_number() over(order by f.NumeroFase asc) as Id
		,f.IdContrato
		,f.IdFase
		,f.NumeroFase
		,f.DuracionMeses
		,f.FechaInicio
		,f.FechaFinalizacion
from	Fases f with(nolock)
where	f.IdContrato = @IdContrato
		and f.NumeroFase > 0
		and (f.FechaInicio >= @FechaInicio
			or f.FechaFinalizacion >= @FechaInicio)
		and f.IdFase not in
			(
				select	f2.IdFase
				from	Fases f2 with(nolock)
				where	f2.IdContrato = @IdContrato
						and GETDATE() >= f2.FechaInicio
						and GETDATE() <= f2.FechaFinalizacion
			)

select	@TotalFasesPost = max(Id)
from	@FasesPosteriores

-- Calculando Fechas Para Fases Posteriores		
set	@IndexFases = 1
while @IndexFases <= @TotalFasesPost
begin
	select	@AuxDuracionMeses = DuracionMeses
	from	@FasesPosteriores	
	where	Id = @IndexFases
	
	update	f
	set		f.FechaInicio =	dateadd(dd,1,@FechaAux)
			,f.FechaFinalizacion = dateadd(mm,DuracionMeses,@FechaAux)
	from	@FasesPosteriores f
	where	f.Id = @IndexFases
	
	set @FechaAux = dateadd(mm,@AuxDuracionMeses,@FechaAux)
	set	@IndexFases = @IndexFases + 1
	
end

--Actualizando fases
update	f
set		f.FechaInicio = fp.FechaInicio
		,f.FechaFinalizacion = fp.FechaFinalizacion
		,f.DuracionMeses = datediff(mm, fp.FechaInicio, fp.FechaFinalizacion)	
from	Fases f
		inner join @FasesPosteriores fp
			on f.IdFase = fp.IdFase

-- Actualizando Contrato
update	c
set		c.FechaInicioSuspension = @FechaInicio
		,c.DiasSuspension = @DiffDias
		,c.FechaTerminacionSuspension = @FechaFin
		,c.FechaTerminacion = @FechaAux
from	Contratos c with(nolock)
where	c.IdContrato = @IdContrato

-- Actualizando Compromisos
update	comp
set		comp.FechaCumplimiento = dateadd(dd,@DiffDias,comp.FechaCumplimiento)
from	Compromisos comp with(nolock)
		join Fases f with(nolock)
			on comp.IdFase = f.IdFase
where	f.IdContrato = @IdContrato
		and f.NumeroFase > 0
		and comp.FechaCumplimiento >= @FechaInicio