IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'Vistas_MisCompromisosPendientes') AND type in (N'P', N'PC'))
DROP PROCEDURE Vistas_MisCompromisosPendientes
GO
-- =============================================
-- Author:		Solver
-- Create date: 00-00-0000
-- Description:
-- =============================================
CREATE PROCEDURE Vistas_MisCompromisosPendientes
(
	@IdUsuario	int
)
AS
/*************************** Variables De Prueba **********************/
--declare @IdUsuario	varchar(256)
--set @IdUsuario = 'Administrador'
/**********************************************************************/
select	distinct
		comp.IdCompromiso				as IdCompromiso
		,ctr.IdContrato					as IdContrato
		,ctr.Nombre						as Contrato
		,f.Nombre						as Fase
		,blq.Descripcion				as Bloque
		,comp.Nombre					as Compromiso
		,cmp.Descripcion				as Campo
		,pz.Descripcion					as Pozo
		,comp.NombreResponsable			as Responsable
		,comp.Estado					as Estado
		,comp.FechaCumplimiento			as FechaCumplimiento
		,comp.TipoCompromiso			as TipoCompromiso
		,comp.Importancia				as Importancia
		,comp.TipoAsociacion			as TipoAsociacionCompromiso
		,ter.Nombre						as Entidad
		,tpo.Descripcion				as TipoPagoCompromiso
		,pago.Valor						as ValorPago
		,mnd.Nombre						as Moneda
from	Compromisos comp with(nolock)
		inner join	Fases f with(nolock)
			on comp.IdFase = f.IdFase
		join Contratos ctr with(nolock)
			on f.IdContrato = ctr.IdContrato
		join Bloques blq with(nolock)
			on ctr.IdBloque = blq.IdBloque		
		left join Campos cmp with(nolock)
			on comp.IdCampo = cmp.IdCampo
		left join Pozos pz with(nolock)
			on comp.IdPozo = pz.IdPozo
		left join PagosObligaciones pago with(nolock)
			on comp.IdCompromiso = pago.IdCompromiso
		left join Terceros ter with(nolock)
			on pago.IdTercero = ter.IdTercero
		left join TiposPagoObligacion tpo with(nolock)
			on pago.IdTipoPagoObligacion = tpo.IdTipoPagoObligacion
		left join Monedas mnd with(nolock)
			on pago.IdMoneda = mnd.IdMoneda
where	comp.IdResponsable = @IdUsuario
		and comp.Estado not in ('Realizado','Anulado')
order	by
		comp.FechaCumplimiento desc