namespace Infrastructure.CrossCutting.NetFramework.Enums
{
    public enum EstadosAplicacion
    {
        //ESTADOS RECLAMOS
        /// <summary>
        /// RECLAMOS : 1
        /// </summary>
        Draft = 1,
        /// <summary>
        /// RECLAMOS : 2
        /// </summary>
        EnRevisiónCc = 2,
        /// <summary>
        /// RECLAMOS : 3
        /// </summary>
        EnProcesoReclamo = 3,
        /// <summary>
        /// RECLAMOS : 4
        /// </summary>
        RevisionPlanAccion = 4,
        /// <summary>
        /// RECLAMOS : 5
        /// </summary>
        RespuestaCliente = 5,
        /// <summary>
        /// RECLAMOS : 6
        /// </summary>
        CierrePlanAccion = 6,
        /// <summary>
        /// RECLAMOS : 7
        /// </summary>
        Cierre = 7,
        /// <summary>
        /// RECLAMOS : 8
        /// </summary>
        Cerrado = 8,
        /// <summary>
        /// RECLAMOS : 10
        /// </summary>
        Cancelado = 10,

        //ESTADOS Acciones Preventivas Correctivas
        /// <summary>
        /// ACCIONES CP : 11
        /// </summary>
        Registro = 11,
        /// <summary>
        /// ACCIONES CP : 12
        /// </summary>
        PendienteVoBo = 12,
        /// <summary>
        /// ACCIONES CP : 13
        /// </summary>
        EnAprobación = 13,
        /// <summary>
        /// ACCIONES CP : 14 
        /// </summary>
        EnProceso= 14,
        /// <summary>
        /// ACCIONES CP : 15
        /// </summary>
        AccionRespondida=15,
        /// <summary>
        /// ACCIONES CP : 16
        /// </summary>
        AccionCerrada=16,
        /// <summary>
        /// ACCIONES CP : 17
        /// </summary>
        Rechazada=17
    }
}