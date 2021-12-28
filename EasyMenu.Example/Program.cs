using System;
using System.Threading.Tasks;

using EasyMenu.Models;

namespace EasyMenu.Example
{
    internal class Program
    {
        static void Main()
        {
            MenuConsole Menu = new MenuBuilder(BreadCrumbHeader: true, UserInputMessage: "Choose:", PageNavigationSeparator: ">")
                .WithMenu("MenuTitle", () => { Console.WriteLine(""); } )
                .WithMenu("MenuTitle2", async () => { await Task.Delay(1); })
                .WithMenu("MenuTitle3", new[]
                {
                    new Menu("adsasd", () => { Console.WriteLine("test"); }),
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

            Console.ReadLine();
        }
    }
}