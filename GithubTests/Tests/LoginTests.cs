﻿using GithubAutomation.Navigation;
using GithubAutomation.Pages;
using GithubAutomation.Selenium;
using GithubTests.Utilities;
using NUnit.Framework;

namespace GithubTests.Tests
{
    [TestFixture]
    public class LoginTests : BaseSetup
    {
        /// <summary>
        /// Checks that user can login.
        /// </summary>
        [Test]
        public void User_Can_Login()
        {
            Assert.That(NavigationPanel.GetUsername(), Is.EqualTo(Driver.Username), "Failed to login");
        }
        
        // Negative test should be added.
    }
}