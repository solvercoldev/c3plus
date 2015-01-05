IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'Process_GetRadicadosToNotify') AND type in (N'P', N'PC'))
DROP PROCEDURE Process_GetRadicadosToNotify
GO
-- =============================================
-- Author:		Solver
-- Create date: 00-00-0000
-- Description:
-- =============================================
CREATE PROCEDURE Process_GetRadicadosToNotify

AS

declare @DiasDiff int

select	@DiasDiff = cast(Value as varchar(50))
from	TBL_Admin_OptionList
where	[Key] = 'Notificacion_DiasDiferencia'

select	rad.IdRadicado
from	Radicados rad with(nolock)
where	(datediff(dd,getdate(),rad.FechaRespuesta) <= @DiasDiff
		or dateadd(dd,rad.DiasAlarma * (-1),rad.FechaRespuesta) <= getdate())
		and rad.EstadoRadicado not in ('Realizado','Anulado')
		and rad.RespuestaPendiente = 1