﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyMenu.Models;

namespace EasyMenu;

public class MenuBuilder
{
    internal readonly string MainTitle;
    internal readonly bool breadCrumbHeader;
    internal List<Base> EasyMenus { get; } = null;

    public MenuBuilder(string Title = "Choose your option", bool BreadCrumbHeader = false)
    {
        EasyMenus = new();

        MainTitle = Title;
        breadCrumbHeader = BreadCrumbHeader;
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
