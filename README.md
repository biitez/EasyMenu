### EasyMenu 
[![Nuget](https://img.shields.io/nuget/v/EasyMenu?style=flat-square)](https://www.nuget.org/packages/EasyMenu) [![GitHub Latest Release](https://img.shields.io/github/v/release/biitez/EasyMenu.svg?style=flat-square)](https://github.com/biitez/EasyMenu/releases)

Simple library built on .NET standard to create a console menu interface

#### Features:
- Asynchronous/synchronous function
- Possibility of making unlimited Sub-Pages
- Return and navigation header
- Configurable options
- It is extremely easy-to-use

#### Preview:
![Preview Gif](https://share.biitez.dev/i/3v7oh.gif)

#### [Example](https://github.com/biitez/EasyMenu/blob/master/EasyMenu.Example/Program.cs):

```cs
// BreadCrumbHeader (boolean - default: false) = Enable/disable display of navigation between pages
// UserInputMessage (string - default: "Choose your option") = The message that the user will be prompted to type the option

MenuBuilder MenuSettings = new MenuBuilder(BreadCrumbHeader: true, UserInputMessage: "Choose:")

    // You can add lambda expressions
    .WithMenu("Page A", () => { Console.WriteLine("Hi from Page A!"); })

    // or.. directly call an (a)synchronous method
    .WithMenu("Page B", MyMethod)

    // or.. make a sub pages
    .WithMenu("Page C", new[]
    {
        // Inside you can do exactly the same as in .WithMenu
        new Menu("SubPage A", () => { Console.WriteLine("Hi from SubPage A!"); }),

        // Also you can create all the SubPages you want within others
        new Menu("SubPage B", new[]
        {
            new Menu("SubPage BA", () => { Console.WriteLine("Hi from SubPage BA!"); }),
            new Menu("SubPage BB", () => { Console.WriteLine("Hi from SubPage BB!"); }),
            // (...)
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
