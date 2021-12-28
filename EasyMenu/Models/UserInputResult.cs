namespace EasyMenu.Models;

public class UserInputResult
{
    internal Base _MenuSelected { get; } = null;
    internal UserInputErrorTypes? _UserInputError { get; } = null;
    internal bool _HaveSubMenus { get; set; } = false;
    internal string _TitleSelected { get; set; }
    public UserInputResult(Base MenuSelected, string TitleSelected)
    {
        _MenuSelected = MenuSelected;
        _TitleSelected = TitleSelected;
        _HaveSubMenus = _MenuSelected.ConsoleMenus != null;
    }

    public UserInputResult(UserInputErrorTypes UserInputError)
    {
        _UserInputError = UserInputError;
    }
}

public enum UserInputErrorTypes
{
    Invalid,
    Empty,
    Unknown
}