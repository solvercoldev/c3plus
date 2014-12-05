IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'GetBloquesSinContratoIncluyeBloque') AND type in (N'P', N'PC'))
DROP PROCEDURE GetBloquesSinContratoIncluyeBloque
GO
-- =============================================
-- Author:		Solver
-- Create date: 00-00-0000
-- Description:
-- =============================================
CREATE PROCEDURE GetBloquesSinContratoIncluyeBloque
(
	@IdBloque varchar(10)
)
AS

/*****************************************************/
--declare	@IdBloque varchar(10)

--set	@IdBloque = 'B01'
/*****************************************************/

	select	blq.IdBloque
			,blq.Descripcion
	from	Bloques blq with(nolock)
	where	blq.IdBloque = @IdBloque
union
	select	blq.IdBloque
			,blq.Descripcion
	from	Bloques blq with(nolock)
	where	not exists
			(
				select	1
				from	Contratos c with(nolock)
				where	blq.IdBloque = c.IdBloque
			)