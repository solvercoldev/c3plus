IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'ModificarFechaEfectivaContrato') AND type in (N'P', N'PC'))
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

-- Variables de Trabajo
declare @DiffDias int

select	@DiffDias = datediff(dd, c.FechaInicio, @FechaInicio)	
from	Contratos c with(nolock)
where	c.IdContrato = @IdContrato

-- Actualizando Contrato
update	c
set		c.FechaInicio = @FechaInicio
from	Contratos c with(nolock)
where	c.IdContrato = @IdContrato

-- Actualizando Fases
update	f
set		f.FechaInicio = dateadd(dd,@DiffDias, f.FechaInicio)
		,f.FechaFinalizacion = dateadd(dd,@DiffDias, f.FechaFinalizacion)
from	Fases f with(nolock)
where	f.IdContrato = @IdContrato
		and f.NumeroFase > 0

-- Actualizando Compromisos
update	comp
set		comp.FechaCumplimiento = dateadd(dd,@DiffDias,comp.FechaCumplimiento)
from	Compromisos comp with(nolock)
		join Fases f with(nolock)
			on comp.IdFase = f.IdFase
where	f.IdContrato = @IdContrato
		and f.NumeroFase > 0