using System.ComponentModel;

namespace GithubAutomation.Navigation;

public enum ProfileDropdownItem
{
    YourProfile,
    YourRepositories,
    YourProjects,
    Settings,
    SignOut
}

public static class ProfileDropdownItemExtensions
{
    public static string ToAttribute(this ProfileDropdownItem profileDropdownItem)
    {
        switch (profileDropdownItem)
        {
            case ProfileDropdownItem.YourProfile:
                return "Your profile";
            case ProfileDropdownItem.YourRepositories:
                return "Your repositories";
            case ProfileDropdownItem.YourProjects:
                return "Your projects";
            case ProfileDropdownItem.Settings:
                return "Settings";
            case ProfileDropdownItem.SignOut:
                return "Sign out";
            default:
                throw new InvalidEnumArgumentException("Dropdown item is not recognised.");
        }
    }
}