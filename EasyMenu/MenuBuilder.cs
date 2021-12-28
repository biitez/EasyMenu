using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyMenu.Models;

namespace EasyMenu;

public class MenuBuilder
{
    internal List<Menu> EasyMenus { get; } = null;

    internal readonly bool breadCrumbHeader;
    internal readonly string userInputMessage;

    public string HeadNavigationSeparator { get; set; } = ">";
    public string HeadNavigationMenuSeparator { get; set; } = "---";
    public string ErrorUserInput { get; set; } = "Invalid input!";

    /// <summary>
    /// The EasyMenu constructor is initialized
    /// </summary>
    /// <param name="BreadCrumbHeader">Header navigation of pages with sub-pages (e.g. Page > SubPage / Page, SubPage)</param>
    /// <param name="UserInputMessage">The text that will be displayed for the user to choose the menu</param>
    /// <param name="PageNavigationSeparator">Separator for page navigation (<see cref="BreadCrumbHeader"/> <see cref="true"/> is required)</param>
    public MenuBuilder(bool BreadCrumbHeader = false, string UserInputMessage = "Choose your option:")
    {
        EasyMenus = new();

        breadCrumbHeader = BreadCrumbHeader;
        userInputMessage = UserInputMessage;
        //pageNavigationSeparator = PageNavigationSeparator;
    }

    /// <summary>
    /// Add a menu
    /// </summary>
    /// <param name="MenuTitle">The title of the menu</param>
    /// <param name="Execute">The asynchronous function that will run the menu</param>
    /// <returns></returns>
    public MenuBuilder WithMenu(string MenuTitle, Func<Task> Execute)
    {
        EasyMenus.Add(new Menu(MenuTitle, Execute)
        {
            BreadCrumbHeader = breadCrumbHeader
        });

        return this;
    }

    /// <summary>
    /// Add a menu
    /// </summary>
    /// <param name="MenuTitle">The title of the menu</param>
    /// <param name="Execute">The synchronous function that will run the menu</param>
    /// <returns></returns>
    public MenuBuilder WithMenu(string MenuTitle, Action Execute)
    {
        EasyMenus.Add(new Menu(MenuTitle, Execute)
        {
            BreadCrumbHeader = breadCrumbHeader
        });

        return this;
    }

    /// <summary>
    /// Add a menu
    /// </summary>
    /// <param name="MenuTitle">The title of the menu</param>
    /// <param name="SubMenus">The list of SubMenus under this menu</param>
    /// <param name="ShowReturnOption">Show the return option inside this menu</param>
    /// <returns></returns>
    public MenuBuilder WithMenu(string MenuTitle, Menu[] SubMenus/*, bool ShowReturnOption = true*/)
    {
        EasyMenus.Add(new Menu(MenuTitle, SubMenus)
        {
            BreadCrumbHeader = breadCrumbHeader,
            ShowReturnOption = true/*ShowReturnOption*/ // not yet done
        });

        return this;
    }

    /// <summary>
    /// Build MenuConsole
    /// </summary>
    /// <returns><see cref="MenuConsole"/></returns>
    public MenuConsole Build()
    {
        return new MenuConsole(this);
    }
}
