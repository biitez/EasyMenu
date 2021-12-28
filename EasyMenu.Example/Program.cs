using EasyMenu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EasyMenu.Example
{
    internal class Program
    {
        static void Main(string[] args)
        {

            MenuConsole Menu = new MenuBuilder(BreadCrumbHeader: true)
                .WithMenu("MenuTitle", () => { System.Console.WriteLine("Hey!"); })
                .WithMenu("MenuTitle2", async () => { await Task.Delay(1); })
                .WithMenu("MenuTitle3", new[]
                {
                    new Menu("adsasd", () => {System.Console.WriteLine("test"); }),
                    new Menu("Ll", new[]
                    {
                        new Menu("other", () => { Console.WriteLine("adssda"); }),
                        new Menu("other", () => { Console.WriteLine("adssda"); }),
                        new Menu("other", () => { Console.WriteLine("adssda"); }),
                        new Menu("other", () => { Console.WriteLine("adssda"); }),
                        new Menu("other", () => { Console.WriteLine("adssda"); }),
                    })
                })
                .Build();

            Menu.Show();

            /*
             * var ConsoleMenu = new MenuBuilder(Title = "Choose your option")
             *                          .WithMenu(Title = "MyMenuAsync!", async () => {})
             *                          .WithMenu(Title = "MyMenuSync!", () => {} )
             *                          .WithMenu(Title = "MyMenuWithSubMenus", 
             *                               new Menu("SubMenuTitle", ShowReturnOption = true, 
             *                               new Menu("SubSubMenuTitle, ShowReturnOption = true),
             *                               breadcrumbHeader = true)
             *                                                   
             */

            System.Console.ReadLine();

        }
    }
}