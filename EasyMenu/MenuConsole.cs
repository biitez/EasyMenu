using EasyMenu.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EasyMenu;

public class MenuConsole
{
    private MenuBuilder _Builder { get; } = null;
    private List<string> _ErrorLogs { get; } = null;
    private List<string> _BreadCrumbHeader { get; } = null;
    private List<List<Menu>> _MenuLogs { get; set; } = null;

    private string ErrorMessage { get; set; } = "Invalid input!";

    public MenuConsole(MenuBuilder Builder)
    {
        _Builder = Builder;
        _ErrorLogs = new();
        _BreadCrumbHeader = new();
        _MenuLogs = new();
    }

    public MenuConsole WithCustomErrorMessage(string UserInvalidInput = "Invalid input!")
    {
        ErrorMessage = UserInvalidInput;

        return this;
    }

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
                Console.WriteLine($"{string.Join($"{_Builder.pageNavigationSeparator} ", _BreadCrumbHeader)}" + Environment.NewLine);
            }

            UserInputResult UserInput = GetUserInputMenu(MenuList);

            Console.Clear();

            if (UserInput._UserInputError is not null)
            {
                Console.CursorVisible = false;

                PrintLineConsoleColor($"{UserInput._UserInputError} input error. Enter to try again.", ConsoleColor.Red);
                
                Console.ReadLine();

                continue;
            }

            if (_Builder.breadCrumbHeader)
            {
                Console.WriteLine($"{string.Join("{_Builder.pageNavigationSeparator} ", _BreadCrumbHeader)}" + Environment.NewLine);
            }

            if (UserInput._HaveSubMenus)
            {

                Base MenuSelected = UserInput._MenuSelected;

                MenuList.Clear();

                foreach (var i in MenuSelected.ConsoleMenus)
                {
                    MenuList.Add(i);
                }

                if (MenuSelected.ShowReturnOption)
                {
                    if (MenuSelected.Title.Equals("Return"))
                    {
                        MenuList = _MenuLogs.Last();
                        _MenuLogs.RemoveAt(_MenuLogs.Count - 1);
                    }

                }

                continue;
            }
            else
            {
                Console.Clear();

                if (UserInput._MenuSelected.MethodActionSync != null)
                {
                    UserInput._MenuSelected.MethodActionSync.Invoke();
                }

                if (UserInput._MenuSelected.MethodActionAsync != null)
                {
                    UserInput._MenuSelected.MethodActionAsync.Invoke();
                }

                break;
            }
        }

        return this;
    }

    private void PrintLineConsoleColor(string Text, ConsoleColor Color)
    {
        Console.ForegroundColor = Color;
        Console.WriteLine(Text);
        Console.ResetColor();
    }

    private UserInputResult GetUserInputMenu(List<Menu> Menus)
    {
        try
        {
            for (int MenuIndex = 0; MenuIndex < Menus.Count; MenuIndex++)
            {
                Console.WriteLine($"[{MenuIndex + 1}] {Menus[MenuIndex].Title}");
            }

            Console.Write($"{_Builder.userInputMessage} ");

            string UserInputString = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(UserInputString) || !int.TryParse(UserInputString, out var UserInput))
            {
                return new UserInputResult(UserInputErrorTypes.Empty);
            }

            UserInput--;

            if (_Builder.EasyMenus.ElementAtOrDefault(UserInput) is null)
            {
                return new UserInputResult(UserInputErrorTypes.Invalid);
            }

            _MenuLogs.Add(Menus);

            var CurrentMenu = Menus[UserInput];

            if (!Menus[UserInput].Title.Equals("Return"))
                _BreadCrumbHeader.Add(Menus[UserInput].Title);
            else
            {
                _MenuLogs.RemoveAt(_MenuLogs.Count - 1);

                var lastBreadCrumb = _BreadCrumbHeader.Last();
                _BreadCrumbHeader.Remove(lastBreadCrumb);
            }

            //if (UserInput._MenuSelected.Title.Equals("Return"))
            //{
            //    _MenuLogs.RemoveAt(_MenuLogs.Count - 1);

            //    var lastBreadCrumb = _BreadCrumbHeader.Last();
            //    _BreadCrumbHeader.Remove(lastBreadCrumb);
            //}

            if (CurrentMenu.ConsoleMenus != null)
            {
                var ConsoleList = CurrentMenu.ConsoleMenus.ToList();

                if (!ConsoleList.Any(c => c.Title.Equals("Return")) && _BreadCrumbHeader.Count != 1)
                {
                    ConsoleList.Add(new Menu("Return", Menus.ToArray()));
                    CurrentMenu.ConsoleMenus = ConsoleList.ToArray();
                }
            }

            //_BreadCrumbHeader.Add(Menus[UserInput].Title);

            return new UserInputResult(CurrentMenu, Menus[UserInput].Title);
        }
        catch
        {
            return new UserInputResult(UserInputErrorTypes.Unknown);
        }
    }
}