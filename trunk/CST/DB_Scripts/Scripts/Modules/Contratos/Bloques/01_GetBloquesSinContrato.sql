IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'GetBloquesSinContrato') AND type in (N'P', N'PC'))
DROP PROCEDURE GetBloquesSinContrato
GO
-- =============================================
-- Author:		Solver
-- Create date: 00-00-0000
-- Description:
-- =============================================
CREATE PROCEDURE GetBloquesSinContrato

AS
/*****************************************************/
select	blq.IdBloque
		,blq.Descripcion
from	Bloques blq with(nolock)
where	not exists
		(
			select	1
			from	Contratos c with(nolock)
			where	blq.IdBloque = c.IdBloque
		)