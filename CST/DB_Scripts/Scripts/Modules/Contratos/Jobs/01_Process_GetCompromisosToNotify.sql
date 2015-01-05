IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'Process_GetCompromisosToNotify') AND type in (N'P', N'PC'))
DROP PROCEDURE Process_GetCompromisosToNotify
GO
-- =============================================
-- Author:		Solver
-- Create date: 00-00-0000
-- Description:
-- =============================================
CREATE PROCEDURE Process_GetCompromisosToNotify

AS

declare @DiasDiff int

select	@DiasDiff = cast(Value as varchar(50))
from	TBL_Admin_OptionList
where	[Key] = 'Notificacion_DiasDiferencia'

select	comp.IdCompromiso			
from	Compromisos comp with(nolock)
where	(datediff(dd,getdate(),comp.FechaCumplimiento) <= @DiasDiff
		or dateadd(dd,comp.DiasAlarma * (-1),comp.FechaCumplimiento) <= getdate())
		and comp.Estado not in ('Realizado','Anulado')