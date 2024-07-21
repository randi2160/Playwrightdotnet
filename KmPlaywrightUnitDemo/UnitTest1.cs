using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KmPlaywrightUnitDemo
{
    public class NunitPlaywright
    {
        private IBrowser _browser;
        private IPage _page;
        private IPlaywright _playwright;

        [SetUp]
        public async Task Setup()
        {


        }

        [TearDown]
        public async Task Teardown()
        {

        }


        [Test]
        public async Task Test1()
        {
            //Playwright
            using var playwright = await Playwright.CreateAsync();
            //Browser
            await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false
            });
            //Page
            var page = await browser.NewPageAsync();
            await page.GotoAsync("http://www.publix.com");
            await page.ClickAsync("text=Log in");
            await page.ScreenshotAsync(new PageScreenshotOptions
            {
                Path = "EAApp.jpg"
            });
            // Fill in the email and password fields
            await page.FillAsync("#signInName", "secret@secret.com");
            await page.FillAsync("#password", "secret");
            await page.ClickAsync("button#next");

            //await page.FillAsync("#UserName", "admin");
            //await page.FillAsync("#Password", "password");
            // await page.ClickAsync("text=Log in");
            // await page.ClickAsync("button#next");
            // Wait for the error message to appear
            var errorMessageLocator = _page.Locator("div.error.pageLevel[role='alert']");
            await errorMessageLocator.WaitForAsync(new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Visible
            });

            // Get the error message text
            var errorMessage = await errorMessageLocator.InnerTextAsync();

            // Assert the error message
            Assert.AreEqual("Incorrect email address or password. Enter your Publix account email address and password, and try again.", errorMessage);
        }
    }



}

