IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'RenunciarContrato') AND type in (N'P', N'PC'))
DROP PROCEDURE RenunciarContrato
GO
-- =============================================
-- Author:		Solver
-- Create date: 00-00-0000
-- Description:
-- =============================================
CREATE PROCEDURE RenunciarContrato
(
	@IdContrato int
)
AS
/*****************************************************/
--declare	@IdContrato int, @FechaInicio datetime, @FechaFin datetime

--set @IdContrato = 1
--set	@FechaInicio = '2015-01-12'
--set	@FechaFin = '2015-12-24'
/*****************************************************/

-- Actualizando Contrato
update	c
set		c.FechaTerminacion = GETDATE()
from	Contratos c with(nolock)
where	c.IdContrato = @IdContrato

-- Actualizando Compromisos
update	comp
set		comp.Estado = 'Anulado'
from	Compromisos comp with(nolock)
		join Fases f with(nolock)
			on comp.IdFase = f.IdFase
where	f.IdContrato = @IdContrato
		and f.NumeroFase > 0