using System.ComponentModel;

namespace GithubAutomation.Navigation;

public enum NewDropdownItem
{
    NewRepository,
    ImportRepository,
    NewCodespace,
    NewGist,
    NewOrganization,
    NewProject
}

public static class NewDropdownItemExtensions
{
    public static string ToAttribute(this NewDropdownItem newDropdown)
    {
        switch (newDropdown)
        {
            case NewDropdownItem.NewRepository:
                return "New repository";
            default:
                throw new InvalidEnumArgumentException("Dropdown item is not recognised.");
        }
    }
}