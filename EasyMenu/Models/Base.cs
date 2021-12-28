using System;
using System.Threading.Tasks;

namespace EasyMenu.Models;

public abstract class Base
{
    public async Task ExecuteActionAsync()
    {
        await MethodActionAsync.Invoke().ConfigureAwait(false);
    }

    public void ExecuteActionSync()
    {
        MethodActionSync.Invoke();
    }

    public abstract Func<Task> MethodActionAsync { get; set; }
    public abstract Action MethodActionSync { get; set; }

    public abstract Menu[] ConsoleMenus { get; set; }

    public abstract string Title { get; set; }

    public abstract bool ShowReturnOption { get; set; }
    public abstract bool BreadCrumbHeader { get; set; }
}