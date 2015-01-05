IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'Vistas_AllRadicados') AND type in (N'P', N'PC'))
DROP PROCEDURE Vistas_AllRadicados
GO
-- =============================================
-- Author:		Solver
-- Create date: 00-00-0000
-- Description:
-- =============================================
CREATE PROCEDURE Vistas_AllRadicados

AS
/*************************** Variables De Prueba **********************/
select	distinct
		rad.IdContrato					as IdContrato
		,ctr.Nombre						as Contrato
		,blq.Descripcion				as Bloque
		,rad.IdRadicado					as IdRadicado
		,rad.TipoRadicado				as TipoRadicado
		,rad.Numero						as Numero
		,rad.FechaReciboSalida			as FechaReciboSalida
		,rad.Asunto						as Asunto
		,rad.Resumen					as Resumen
		,usuarioFrom.Nombres			as UsuarioFrom
		,rad.IdFromExterno				as FromExterno
		,usuarioTo.Nombres				as UsuarioTo
		,rad.IdToExterno				as ToExterno
		,rad.RespuestaPendiente			as RespuestaPendiente
		,rad.EstadoRadicado				as Estado
		,rad.FechaRespuesta				as FechaRespuesta		
		,responsableRespuesta.Nombres	as Responsable	
		,dependencia.Descripcion		as Dependencia
from	Radicados rad with(nolock)
		inner join Contratos ctr with(nolock)
			on rad.IdContrato = ctr.IdContrato
		inner join Bloques blq with(nolock)
			on ctr.IdBloque = blq.IdBloque	
		inner join TBL_Admin_Usuarios responsableRespuesta with(nolock)
			on rad.ResponsableRespuesta = responsableRespuesta.IdUser
		left join Dependencias dependencia with(nolock)
			on responsableRespuesta.IdDependencia = dependencia.IdDependencia
		left join TBL_Admin_Usuarios usuarioFrom with(nolock)
			on rad.IdFrom = usuarioFrom.IdUser
		left join TBL_Admin_Usuarios usuarioTo with(nolock)
			on rad.IdTo = usuarioTo.IdUser
order	by
		rad.FechaReciboSalida desc