using System.Collections.Generic;
using Domain.MainModules.Entities;

namespace Application.MainModule.DTO
{
    public class OpcionesMenu
    {

        public List<TBL_Admin_OpcionesMenu> GetMenuPrincipal(IEnumerable<TBL_Admin_OpcionesMenu> menuResult, string moduleId)
        {

            var itemsMenu = new List<TBL_Admin_OpcionesMenu>();
            foreach (var menu in menuResult)
            {
                if (menu.IdopcionPadre != null )continue;
                if (!menu.Activo) continue;
                if (!menu.ShowSecondMenu) continue;
                var oMenu = NewMenu();
                if (!string.IsNullOrEmpty(menu.LinkUrl))
                    oMenu.LinkUrl = string.Format("{0}?ModuleId={1}", menu.LinkUrl,moduleId);

                oMenu.TituloOpcion = menu.TituloOpcion;
                oMenu.IdOpcionMenu = menu.IdOpcionMenu;
                oMenu.Icono = menu.Icono;
                itemsMenu.Add(oMenu);
                AddMenuItem(menuResult, oMenu, moduleId);
            }

            return itemsMenu;
        }

        private static void AddMenuItem(IEnumerable<TBL_Admin_OpcionesMenu> menuResult, TBL_Admin_OpcionesMenu itemMenu, string moduleId)
        {
            foreach (var menu in menuResult)
            {
                if(itemMenu.IdOpcionMenu != menu.IdopcionPadre)continue;
                if (!menu.Activo || !menu.ShowSecondMenu) continue;

                var oMenu = NewMenu();
                oMenu.LinkUrl = string.Format("{0}?ModuleId={1}", menu.LinkUrl, moduleId);
                oMenu.TituloOpcion = menu.TituloOpcion;
                oMenu.IdOpcionMenu = menu.IdOpcionMenu;
                oMenu.IdopcionPadre = menu.IdopcionPadre;
                oMenu.Icono = menu.Icono;
                itemMenu.TBL_Admin_OpcionesMenu1.Add(oMenu);
                AddMenuItem(menuResult, oMenu, moduleId);
            }
        }


        private static TBL_Admin_OpcionesMenu NewMenu()
        {
            return new TBL_Admin_OpcionesMenu();
        }

    }
}