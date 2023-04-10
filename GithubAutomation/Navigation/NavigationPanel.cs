using GithubAutomation.Selenium;
using OpenQA.Selenium;

namespace GithubAutomation.Navigation
{
    public class NavigationPanel
    {
        public static void SelectNewDropdownItem(NewDropdownItem newDropdownItem)
        {
            ExpandNewDropMenu();
            Driver.Instance.FindElement(By.XPath($"//a[contains(text(), '{newDropdownItem.ToAttribute()}')]")).Click();
        }

        public static void SelectProfileDropdownItem(ProfileDropdownItem profileDropdownItem)
        {
            ExpandProfileDropMenu();
            Driver.Instance.FindElement(By.XPath($"//a[contains(text(), '{profileDropdownItem.ToAttribute()}')]")).Click();
        }

        public static string GetUsername()
        {
            ExpandProfileDropMenu();
            return Driver.Instance.FindElement(By.XPath("//a['header-nav-current-user']/strong")).Text;
        }

        private static void ExpandNewDropMenu()
        {
            var addButton = Driver.Instance.FindElement(By.ClassName("octicon-plus"));
            addButton.Click();
        }

        private static void ExpandProfileDropMenu()
        {
            Driver.Instance.FindElement(By.ClassName("avatar-small")).Click();
        }
    }
}