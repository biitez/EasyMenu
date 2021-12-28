﻿using System;

using EasyMenu.Models;

namespace EasyMenu.Example
{
    internal class Program
    {
        static void Main()
        {
            MenuConsole Menu = new MenuBuilder(BreadCrumbHeader: true, UserInputMessage: "Choose:", PageNavigationSeparator: "-")

                // You can add lambda expressions
                .WithMenu("Menu A", () => { Console.WriteLine("Hi from Menu A!"); } )

                // or.. directly call an (a)synchronous method
                .WithMenu("Menu MyMethod", MyMethod )

                // o.. make subMenus
                .WithMenu("Menu with SubMenus", new[]
                {
                    // Dentro de los SubMenus puede hacer exactamente lo mismo que en .WithMenu
                    new Menu("SubMenu A", () => { Console.WriteLine("Hi from SubMenu A!"); }),

                    // Also you can create all the SubMenus you want within others
                    new Menu("SubMenu B", new[]
                    {
                        new Menu("SubSubMenu BA", () => { Console.WriteLine("Hi from SubSubMenu BA!"); }),
                        new Menu("SubSubMenu BB", () => { Console.WriteLine("Hi from SubSubMenu BB!"); }),
                    })
                }, false)
                // If the user does not enter a numerical option by calling one of the Menus, this error message will appear
                .WithCustomErrorMessageInvalidInput("Invalid Input!")
                .Build();

            // Display menu - UpdateConsole: Refresh the console after there is an error
            Menu.Show(UpdateConsole: true);

            Console.ReadLine();
        }

        static void MyMethod()
        {
            Console.WriteLine("Hi from MyMethod!");
        }
    }
}