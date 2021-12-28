using EasyMenu.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EasyMenu;

public class MenuConsole
{
    private MenuBuilder _Builder { get; } = null;
    private List<string> _BreadCrumbHeader { get; } = null;
    private List<List<Menu>> _MenuLogs { get; set; } = null;

    private object _Lock = new();

    /// <summary>
    /// initialize <see cref="MenuConsole"/>
    /// </summary>
    /// <param name="Builder"><see cref="MenuBuilder"/></param>
    public MenuConsole(MenuBuilder Builder)
    {
        _Builder = Builder;

        _BreadCrumbHeader = new();
        _MenuLogs = new();
    }

    /// <summary>
    /// Display the Menu in the console
    /// </summary>
    /// <param name="UpdateConsole"></param>
    /// <returns><see cref="MenuConsole"/></returns>
    /// <exception cref="InvalidOperationException"></exception>
    /// <exception cref="NotImplementedException"></exception>
    public MenuConsole Show(bool UpdateConsole = true)
    {
        if (_Builder is null)
        {
            throw new InvalidOperationException(nameof(MenuBuilder));
        }

        if (!_Builder.EasyMenus.Any())
        {
            throw new NotImplementedException(nameof(Menu));
        }

        // Main is added as first navigation
        _BreadCrumbHeader.Add("Main");

        List<Menu> MenuList = _Builder.EasyMenus;

        while (true)
        {
            if (UpdateConsole)
            {
                Console.Clear();
            }

            if (_Builder.breadCrumbHeader)
            {
                Console.WriteLine($"{string.Join($" {_Builder.pageNavigationSeparator} ", _BreadCrumbHeader)}");
                Console.WriteLine("---");
            }

            UserInputResult UserInput = GetUserInputMenu(MenuList);

            if (UserInput._UserInputError is not null)
            {
                Console.ForegroundColor = ConsoleColor.Red;

                Console.WriteLine("{_Builder.ErrorUserInput} - Enter to try again.");

                Console.ResetColor();
                
                Console.ReadLine();

                if (UpdateConsole)
                {
                    Console.Clear();
                }
                else
                {
                    Console.WriteLine();
                }

                continue;
            }

            Console.Clear();

            if (UserInput._HaveSubMenus)
            {

                Base MenuSelected = UserInput._MenuSelected;

                MenuList.Clear();

                lock (_Lock)
                {
                    foreach (var i in MenuSelected.ConsoleMenus)
                    {
                        MenuList.Add(i);
                    }
                }

                if (MenuSelected.ShowReturnOption && MenuSelected.Title.Equals("Return"))
                {
                    lock (_Lock)
                    {
                        MenuList = _MenuLogs.Last();
                        _MenuLogs.RemoveAt(_MenuLogs.Count - 1);
                    }
                }

                continue;
            }

            Console.Clear();

            bool IsSynchronous = UserInput._MenuSelected.MethodActionSync != null;
            bool IsAsynchronous = UserInput._MenuSelected.MethodActionAsync != null;

            if (!IsSynchronous && !IsAsynchronous)
            {
                throw new InvalidOperationException("Invalid function");
            }

            if (_Builder.breadCrumbHeader)
            {
                Console.WriteLine($"{string.Join($" {_Builder.pageNavigationSeparator} ", _BreadCrumbHeader)}");
                Console.WriteLine($"----");
            }            

            if (IsSynchronous)
            {
                UserInput._MenuSelected.MethodActionSync.Invoke();
            }

            if (IsAsynchronous)
            {
                UserInput._MenuSelected.MethodActionAsync.Invoke().GetAwaiter().GetResult();
            }

            Console.ReadLine();
            break;
            
        }

        return this;
    }

    /// <summary>
    /// Displays the list of menus to the user and retrieves <see cref="UserInputResult"/>
    /// </summary>
    /// <param name="Menus">Menu List</param>
    /// <returns><see cref="UserInputResult"/></returns>
    private UserInputResult GetUserInputMenu(List<Menu> Menus)
    {
        try 
        {
            // Displays the menus of the list
            for (int MenuIndex = 0; MenuIndex < Menus.Count; MenuIndex++)
            {
                Console.WriteLine($"[{MenuIndex + 1}] {Menus[MenuIndex].Title}");
            }

            // Prompts the user to select one of the following
            Console.Write($"{Environment.NewLine}{_Builder.userInputMessage} ");

            string UserInputString = Console.ReadLine();

            // If the ReadLine is not valid
            if (string.IsNullOrWhiteSpace(UserInputString) || !int.TryParse(UserInputString, out var UserInput))
            {
                return new UserInputResult(UserInputErrorTypes.Empty);
            }

            // 1 is subtracted because the index in the lists starts from 0, so if the user selects 1, the 0 item in the list is requested
            UserInput--;

            // If the index is not found
            if (_Builder.EasyMenus.ElementAtOrDefault(UserInput) is null)
            {
                return new UserInputResult(UserInputErrorTypes.Invalid);
            }

            // Menu list is added to the logs (this is to obtain them from the returns)
            _MenuLogs.Add(Menus);

            // Get the Menu
            var CurrentMenu = Menus[UserInput];

            // If the user did NOT select a Return
            if (!Menus[UserInput].Title.Equals("Return"))
                // The title of the selected menu is added to the navigation
                _BreadCrumbHeader.Add(Menus[UserInput].Title);
            else // Otherwise
            {
                // The last LOG is removed from the list
                /*
                 * When the user enters a menu with subMenus, the list of 
                 * the main menu is added to the Logs, then if the user wants 
                 * to return, the last item in the list is retrieved and deleted
                 */
                _MenuLogs.RemoveAt(_MenuLogs.Count - 1);

                var lastBreadCrumb = _BreadCrumbHeader.Last();
                _BreadCrumbHeader.Remove(lastBreadCrumb);
            }

            // If the selected menu has subMenus
            if (CurrentMenu.ConsoleMenus != null)
            {
                // Contains the submenus to a list
                var ConsoleList = CurrentMenu.ConsoleMenus.ToList();

                // If any of the subMenu does not contain a title with the name 'Return' and the navigation is NOT equal to 1
                // Note: If the navigation list is 1, it means that it is in the Main, if it is not 1, it is in a subMenu
                if (!ConsoleList.Any(c => c.Title.Equals("Return")) && _BreadCrumbHeader.Count != 1)
                {
                    lock (_Lock)
                    {
                        // Added to the subMenu the return option
                        ConsoleList.Add(new Menu("Return", Menus.ToArray()));
                    }

                    // Converts to an array
                    CurrentMenu.ConsoleMenus = ConsoleList.ToArray();
                }
            }

            // Return
            return new UserInputResult(CurrentMenu, Menus[UserInput].Title);
        }
        catch // For unhandled errors
        {
            return new UserInputResult(UserInputErrorTypes.Unknown);
        }
    }
}