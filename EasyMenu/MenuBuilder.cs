using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyMenu.Models;

namespace EasyMenu;

public class MenuBuilder
{
    internal readonly bool breadCrumbHeader;
    internal List<Menu> EasyMenus { get; } = null;

    internal readonly string userInputMessage;
    internal readonly string pageNavigationSeparator;

    public MenuBuilder(bool BreadCrumbHeader = false, string UserInputMessage = "Choose your option:", string PageNavigationSeparator = ",")
    {
        EasyMenus = new();

        breadCrumbHeader = BreadCrumbHeader;
        userInputMessage = UserInputMessage;
        pageNavigationSeparator = PageNavigationSeparator;
    }

    public MenuBuilder WithMenu(string MenuTitle, Func<Task> Execute)
    {
        EasyMenus.Add(new Menu(MenuTitle, Execute)
        {
            BreadCrumbHeader = breadCrumbHeader
        });

        return this;
    }

    public MenuBuilder WithMenu(string MenuTitle, Action Execute)
    {
        EasyMenus.Add(new Menu(MenuTitle, Execute)
        {
            BreadCrumbHeader = breadCrumbHeader
        });

        return this;
    }

    public MenuBuilder WithMenu(string MenuTitle, Menu[] SubMenus, bool ShowReturnOption = true)
    {
        EasyMenus.Add(new Menu(MenuTitle, SubMenus)
        {
            BreadCrumbHeader = breadCrumbHeader,
            ShowReturnOption = ShowReturnOption
        });

        return this;
    }

    public MenuConsole Build()
    {
        return new MenuConsole(this);
    }
}
