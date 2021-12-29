using System;

using EasyMenu.Models;

namespace EasyMenu.Example
{
    internal class Program
    {
        static void Main()
        {
            Console.Title = "https://github.com/biitez/EasyMenu";

            // BreadCrumbHeader (boolean - default: false) = Enable/disable display of navigation between pages
            // UserInputMessage (string - default: "Choose your option") = The message that the user will be prompted to type the option

            MenuBuilder MenuSettings = new MenuBuilder(BreadCrumbHeader: true, UserInputMessage: "Choose:")

                // You can add lambda expressions
                .WithMenu("Page A", () => { Console.WriteLine("Hi from Page A!"); })

                // or.. directly call an (a)synchronous method
                .WithMenu("Page B", MyMethod)

                // or.. make a sub menus
                .WithMenu("Page C", new[]
                {
                    // Inside you can do exactly the same as in .WithMenu
                    new Menu("SubPage A", () => { Console.WriteLine("Hi from SubPage A!"); }),

                    // Also you can create all the SubMenus you want within others
                    new Menu("SubPage B", new[]
                    {
                        new Menu("SubPage BA", () => { Console.WriteLine("Hi from SubPage BA!"); }),
                        new Menu("SubPage BB", () => { Console.WriteLine("Hi from SubPage BB!"); }),
                        // (...)
                    })
                });

            // Page > Page2 > Page3
            MenuSettings.HeadNavigationSeparator = ">"; // Optional

            // Page > Page2 > Page3
            // --- <- this
            // [1] (...)
            MenuSettings.HeadNavigationMenuSeparator = "---"; // Optional

            // The error message input
            MenuSettings.ErrorUserInput = "Invalid Input!"; // Optional

            // Build to MenuConsole
            MenuConsole consoleMenu = MenuSettings.Build();

            // Display menu - UpdateConsole (Optional): Refresh the console after there is an error
            consoleMenu.Show(UpdateConsole: true);

            Console.ReadLine();
        }

        static void MyMethod()
        {
            Console.WriteLine("Hi from MyMethod!");
        }
    }
}