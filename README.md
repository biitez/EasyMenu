## EasyMenu

Library created in .NET Standard to make interactive menus in the console in a very simple way

### Preview:

### Usage:

```cs

MenuConsole Menu = new MenuBuilder(BreadCrumbHeader: true, UserInputMessage: "Choose:", PageNavigationSeparator: ">")
    // create a menu with function synchronous
    .WithMenu("Menu A", () => { Console.WriteLine("Example"); } )
    
    // create a menu with function Asynchronous
    .WithMenu("Menu B", async () => { await Task.Delay(1); })
    
    // create a menu with SubMenus
    .WithMenu("Menu C", new[]
    {
        // Here you can create as many menus as you want for 'Menu C'
        new Menu("SubMenu CA", () => { Console.WriteLine("Hello from SubMenu CA!"); }),
        new Menu("SubMenu CB", new[]
        {
            new Menu("SubSubMenu CB-A", () => { Console.WriteLine("Hi from SubSubMenu!"); })
            // (...)
        })
    })
    .Build();

Menu.Show(); // Print the Menu

/*
 * Output:
 *
 * Main
 * -----------
 * [1]. Menu A
 * [2]. Menu B
 * [3]. Menu C
 *
 * Choose: 
 */

```

