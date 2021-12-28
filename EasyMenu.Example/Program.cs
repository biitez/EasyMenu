using System;

using EasyMenu.Models;

namespace EasyMenu.Example
{
    internal class Program
    {
        static void Main()
        {
            Console.Title = "https://github.com/biitez/EasyMenu";

            MenuBuilder MenuSettings = new MenuBuilder(BreadCrumbHeader: true, UserInputMessage: "Choose:")

                // You can add lambda expressions
                .WithMenu("Menu A", () => { Console.WriteLine("Hi from Menu A!"); })

                // or.. directly call an (a)synchronous method
                .WithMenu("Menu MyMethod", MyMethod)

                // o.. make subMenus
                .WithMenu("Menu with SubMenus", new[]
                {
                    // Dentro de los SubMenus puede hacer exactamente lo mismo que en .WithMenu
                    new Menu("SubMenu A", () => { Console.WriteLine("Hi from SubMenu A!"); }),

                    // Also you can create all the SubMenus you want within others
                    new Menu("SubMenu B with SubSubMenus", new[]
                    {
                        new Menu("SubSubMenu BA", () => { Console.WriteLine("Hi from SubSubMenu BA!"); }),
                        new Menu("SubSubMenu BB", () => { Console.WriteLine("Hi from SubSubMenu BB!"); }),
                    })
                });

            // e.g. Page > Page2 > Page3
            MenuSettings.HeadNavigationSeparator = ">";

            // Page > Page2 > Page3
            // --- <- this
            // [1] (...)
            MenuSettings.HeadNavigationMenuSeparator = "---";

            // The error message input
            MenuSettings.ErrorUserInput = "Invalid Input!";

            // Build to MenuConsole
            MenuConsole consoleMenu = MenuSettings.Build();

            // Display menu - UpdateConsole: Refresh the console after there is an error
            consoleMenu.Show(UpdateConsole: true);

            Console.ReadLine();
        }

        static void MyMethod()
        {
            Console.WriteLine("Hi from MyMethod!");
        }
    }
}