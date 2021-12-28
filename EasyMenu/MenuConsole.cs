using EasyMenu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyMenu;

public class MenuConsole
{
    private MenuBuilder _Builder { get; } = null;
    private List<string> _ErrorLogs { get; } = null;
    private List<string> _BreadCrumbHeader { get; } = null;

    private string ErrorMessage { get; set; } = "Invalid input!";

    public MenuConsole(MenuBuilder Builder)
    {
        _Builder = Builder;
        _ErrorLogs = new();
        _BreadCrumbHeader = new();
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

        List<Base> MenuList = _Builder.EasyMenus;

        while (true)
        {
            if (UpdateConsole)
            {
                Console.Clear();
            }

            if (_Builder.breadCrumbHeader)
            {
                Console.WriteLine($" {string.Join(" > ", _BreadCrumbHeader)}" + Environment.NewLine);
            }

            UserInputResult UserInput = GetUserInputMenu(MenuList);

            if (UserInput._UserInputError != null)
            {
                Console.CursorVisible = false;
                PrintLineConsoleColor($"{UserInput._UserInputError} input error. Enter to try again.", ConsoleColor.Red);                
                Console.ReadLine();
                continue;
            }

            if (UserInput._HaveSubMenus)
            {
                Base MenuSelected = UserInput._MenuSelected;
                MenuList.Clear();

                foreach (var i in MenuSelected.ConsoleMenus)
                {
                    MenuList.Add(i);
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

    private UserInputResult GetUserInputMenu(List<Base> Menus)
    {
        try
        {
            for (int MenuIndex = 0; MenuIndex < Menus.Count; MenuIndex++)
            {
                Console.WriteLine($"[{MenuIndex + 1}] {Menus[MenuIndex].Title}");
            }

            Console.Write("> ");

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

            return new UserInputResult(Menus[UserInput], Menus[UserInput].Title);
        }
        catch
        {
            return new UserInputResult(UserInputErrorTypes.Unknown);
        }
    }
}