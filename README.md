### EasyMenu 
[![Nuget](https://img.shields.io/nuget/v/EasyMenu?style=flat-square)](https://www.nuget.org/packages/EasyMenu) [![GitHub Latest Release](https://img.shields.io/github/v/release/biitez/EasyMenu.svg?style=flat-square)](https://github.com/biitez/EasyMenu/releases)

 
Library created in .NET Standard to make menus in C# console in a very simple way

#### Features:
- Menus with asynchronous/synchronous function
- Possibility of making unlimited Sub-Menus
- Return and navigation header
- Configurable options

#### Preview:
![Preview Gif](https://i.imgur.com/HObfexj.gif)

#### [Example](https://github.com/biitez/EasyMenu/blob/master/EasyMenu.Example/Program.cs):

```cs
MenuBuilder MenuSettings = new MenuBuilder(BreadCrumbHeader: true, UserInputMessage: "Choose:")

    // You can add lambda expressions
    .WithMenu("Menu A", () => { Console.WriteLine("Hi from Menu A!"); })

    // or.. directly call an (a)synchronous method
    .WithMenu("Menu MyMethod", MyMethod)

    // You can also make a menu with sub menus
    .WithMenu("Menu with SubMenus", new[]
    {
        // Inside you can do exactly the same as in .WithMenu
        new Menu("SubMenu A", () => { Console.WriteLine("Hi from SubMenu A!"); }),

        // Also you can create all the SubMenus you want within others
        new Menu("SubMenu B with Sub sub menus", new[]
        {
            new Menu("SubSubMenu BA", () => { Console.WriteLine("Hi from SubSubMenu BA!"); }),
            new Menu("SubSubMenu BB", () => { Console.WriteLine("Hi from SubSubMenu BB!"); }),
            (...)
        })
    });

// e.g. Page > Page2 > Page3
MenuSettings.HeadNavigationSeparator = ">"; // Optional

// Page > Page2 > Page3
// --- <- this
// 1. (...)
MenuSettings.HeadNavigationMenuSeparator = "---"; // Optional

// The error message input
MenuSettings.ErrorUserInput = "Invalid Input!"; // Optional

// Build to MenuConsole
MenuConsole consoleMenu = MenuSettings.Build();

// Display menu - UpdateConsole (Optional): Refresh the console after there is an error
consoleMenu.Show(UpdateConsole: true);

static void MyMethod()
{
    Console.WriteLine("Hi from MyMethod!");
}
```

### Contributions, reports or suggestions
If you find a problem or have a suggestion inside this library, please let me know by [clicking here](https://github.com/biitez/EasyMenu/issues), if you want to improve the code, make it cleaner or more secure, create a [pull request](https://github.com/biitez/EasyMenu/pulls). 

In case you will contribute in the code, please follow the same code base

### Credits

- `Telegram: https://t.me/biitez`
- `Bitcoin Addy: bc1qzz4rghmt6zg0wl6shzaekd59af5znqhr3nxmms`
