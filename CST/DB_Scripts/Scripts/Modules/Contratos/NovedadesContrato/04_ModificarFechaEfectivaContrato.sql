﻿IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'ModificarFechaEfectivaContrato') AND type in (N'P', N'PC'))
DROP PROCEDURE ModificarFechaEfectivaContrato
GO
-- =============================================
-- Author:		Solver
-- Create date: 00-00-0000
-- Description:
-- =============================================
CREATE PROCEDURE ModificarFechaEfectivaContrato
(
	@IdContrato int,
	@FechaInicio datetime
)
AS
/*****************************************************/
--declare	@IdContrato int, @FechaInicio datetime

--set @IdContrato = 1
--set	@FechaInicio = '2015-01-12'
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

select	@DiffDias = datediff(dd, c.FechaInicio, @FechaInicio)	
from	Contratos c with(nolock)
where	c.IdContrato = @IdContrato

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

select	@TotalFasesPost = max(Id)
from	@FasesPosteriores

-- Calculando Fechas Para Fases Posteriores		
set	@IndexFases = 1
set	@FechaAux = @FechaInicio

while @IndexFases <= @TotalFasesPost
begin
	select	@AuxDuracionMeses = DuracionMeses
	from	@FasesPosteriores	
	where	Id = @IndexFases
	
	update	f
	set		f.FechaInicio =	@FechaAux
			,f.FechaFinalizacion = dateadd(mm,DuracionMeses,@FechaAux)
	from	@FasesPosteriores f
	where	f.Id = @IndexFases
	
	set @FechaAux = dateadd(dd,1,dateadd(mm,@AuxDuracionMeses,@FechaAux))
	set	@IndexFases = @IndexFases + 1
	
end

--Actualizando fases
update	f
set		f.FechaInicio = fp.FechaInicio
		,f.FechaFinalizacion = fp.FechaFinalizacion
from	Fases f
		inner join @FasesPosteriores fp
			on f.IdFase = fp.IdFase

-- Actualizando Contrato
update	c
set		c.FechaInicio = @FechaInicio
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