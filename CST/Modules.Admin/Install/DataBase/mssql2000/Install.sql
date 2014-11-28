declare @id int
select  @id  = IdModuletype from TBL_Admin_ModuleType
where Nombre = 'Admin'

insert into TBL_Admin_ModuleRepository(
IdModuleType,
Repositorykey,
RepositoryType,
classtype,
IsActive)
values(
@id,
'Admin.Ciudades',
'Domain.MainModule.Contracts.ITBL_Admin_CiudadesRepository,Domain.MainModule',
'Infrastructure.Data.MainModule.Repositories.TBL_Admin_CiudadesRepository,Infrastructure.Data.MainModule',
1)

insert into TBL_Admin_ModuleRepository(
IdModuleType,
Repositorykey,
RepositoryType,
classtype,
IsActive)
values(
@id,
'Admin.Departamentos',
'Domain.MainModule.Contracts.ITBL_Admin_DepartamentosRepository,Domain.MainModule',
'Infrastructure.Data.MainModule.Repositories.TBL_Admin_DepartamentosRepository,Infrastructure.Data.MainModule',
1)

insert into TBL_Admin_ModuleRepository(
IdModuleType,
Repositorykey,
RepositoryType,
classtype,
IsActive)
values(
@id,
'Admin.EstadosProceso',
'Domain.MainModule.Contracts.ITBL_Admin_EstadosProcesoRepository,Domain.MainModule',
'Infrastructure.Data.MainModule.Repositories.TBL_Admin_EstadosProcesoRepository,Infrastructure.Data.MainModule',
1)

insert into TBL_Admin_ModuleRepository(
IdModuleType,
Repositorykey,
RepositoryType,
classtype,
IsActive)
values(
@id,
'Admin.Monedas',
'Domain.MainModule.Contracts.ITBL_Admin_MonedasRepository,Domain.MainModule',
'Infrastructure.Data.MainModule.Repositories.TBL_Admin_MonedasRepository,Infrastructure.Data.MainModule',
1)

insert into TBL_Admin_ModuleRepository(
IdModuleType,
Repositorykey,
RepositoryType,
classtype,
IsActive)
values(
@id,
'Admin.opcionesMenu',
'Domain.MainModule.Contracts.ITBL_Admin_OpcionesMenuRepository,Domain.MainModule',
'Infrastructure.Data.MainModule.Repositories.TBL_Admin_OpcionesMenuRepository,Infrastructure.Data.MainModule',
1)

insert into TBL_Admin_ModuleRepository(
IdModuleType,
Repositorykey,
RepositoryType,
classtype,
IsActive)
values(
@id,
'Admin.OptionList',
'Domain.MainModule.Contracts.ITBL_Admin_OptionListRepository,Domain.MainModule',
'Infrastructure.Data.MainModule.Repositories.TBL_Admin_OptionListRepository,Infrastructure.Data.MainModule',
1)

insert into TBL_Admin_ModuleRepository(
IdModuleType,
Repositorykey,
RepositoryType,
classtype,
IsActive)
values(
@id,
'Admin.Paises',
'Domain.MainModule.Contracts.ITBL_Admin_PaisesRepository,Domain.MainModule',
'Infrastructure.Data.MainModule.Repositories.TBL_Admin_PaisesRepository,Infrastructure.Data.MainModule',
1)


insert into TBL_Admin_ModuleRepository(
IdModuleType,
Repositorykey,
RepositoryType,
classtype,
IsActive)
values(
@id,
'Admin.PaisMoneda',
'Domain.MainModule.Contracts.ITBL_Admin_PaisMonedaRepository,Domain.MainModule',
'Infrastructure.Data.MainModule.Repositories.TBL_Admin_PaisMonedaRepository,Infrastructure.Data.MainModule',
1)

insert into TBL_Admin_ModuleRepository(
IdModuleType,
Repositorykey,
RepositoryType,
classtype,
IsActive)
values(
@id,
'Admin.Plantillas',
'Domain.MainModule.Contracts.ITBL_Admin_PlantillasRepository,Domain.MainModule',
'Infrastructure.Data.MainModule.Repositories.TBL_Admin_PlantillasRepository,Infrastructure.Data.MainModule',
1)

insert into TBL_Admin_ModuleRepository(
IdModuleType,
Repositorykey,
RepositoryType,
classtype,
IsActive)
values(
@id,
'Admin.ConfServer',
'Domain.MainModule.Contracts.ITBL_Admin_ConfiguracionServidoresRepository,Domain.MainModule',
'Infrastructure.Data.MainModule.Repositories.TBL_Admin_ConfiguracionServidoresRepository,Infrastructure.Data.MainModule',
1)

insert into TBL_Admin_ModuleRepository(
IdModuleType,
Repositorykey,
RepositoryType,
classtype,
IsActive)
values(
@id,
'Admin.Vendedores',
'Domain.MainModule.Contracts.ITBL_Admin_VendedoresRepository,Domain.MainModule',
'Infrastructure.Data.MainModule.Repositories.TBL_Admin_VendedoresRepository,Infrastructure.Data.MainModule',
1)

insert into TBL_Admin_ModuleRepository(
IdModuleType,
Repositorykey,
RepositoryType,
classtype,
IsActive)
values(
@id,
'Admin.DescTipoProducto',
'Domain.MainModule.Contracts.ITBL_Admin_DescripcionesTipoProductoRepository,Domain.MainModule',
'Infrastructure.Data.MainModule.Repositories.TBL_Admin_DescripcionesTipoProductoRepository,Infrastructure.Data.MainModule',
1)

insert into TBL_Admin_ModuleRepository(
IdModuleType,
Repositorykey,
RepositoryType,
classtype,
IsActive)
values(
@id,
'Admin.Secciones',
'Domain.MainModule.Contracts.ITBL_Admin_SeccionesRepository,Domain.MainModule',
'Infrastructure.Data.MainModule.Repositories.TBL_Admin_SeccionesRepository,Infrastructure.Data.MainModule',
1)

insert into TBL_Admin_ModuleRepository(
IdModuleType,
Repositorykey,
RepositoryType,
classtype,
IsActive)
values(
@id,
'Admin.DiasAnticipados',
'Domain.MainModule.Contracts.ITBL_Admin_DiasAnticipadosRepository,Domain.MainModule',
'Infrastructure.Data.MainModule.Repositories.TBL_Admin_DiasAnticipadosRepository,Infrastructure.Data.MainModule',
1)

