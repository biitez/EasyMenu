using System;
using System.Linq;
using System.Threading.Tasks;

namespace EasyMenu.Models;

public class Menu : Base
{
    public Menu(string MenuTitle)
    {
        Title = MenuTitle;
    }

    public Menu(string MenuTitle, Func<Task> MenuAsyncExecute) :
        this(MenuTitle)
    {
        MethodActionAsync = MenuAsyncExecute;
    }

    public Menu(string MenuTitle, Menu[] Menus) : 
        this(MenuTitle)
    {
        ConsoleMenus = Menus;
    }

    public Menu(string MenuTitle, Action MenuSyncExecute) : this(MenuTitle)
    {
        MethodActionSync = MenuSyncExecute;
    }

    public override Func<Task> MethodActionAsync { get; set; } = null;
    public override Action MethodActionSync { get; set; } = null;
    public override Menu[] ConsoleMenus { get; set; } = null;

    public int IdentifierMenu { get; set; }
    public override string Title { get; set; }    
    public override bool ShowReturnOption { get; set; }
    public override bool BreadCrumbHeader { get; set; }    
}