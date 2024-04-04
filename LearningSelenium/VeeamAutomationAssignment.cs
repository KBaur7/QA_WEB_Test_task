using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Drawing;
using OpenQA.Selenium.Interactions;
using System.Collections.ObjectModel;

namespace QA_WEB_Test_task
{
    public class Tests
    {
        public IWebDriver driver;
        public string USAXpath = "//*[@id=\"root\"]/div/div[1]/header/div/div/div[3]/div/div/div/div/div[1]/a[25]";
        public string TexasXpath = "//*[@id=\"root\"]/div/div[1]/header/div/div/div[4]/div[1]/div/div/div/div/div[1]/a[12]";

        public string RomaniaXpath = "//*[@id=\"root\"]/div/div[1]/header/div/div/div[3]/div/div/div/div/div[1]/a[20]";
        public string BucharestXpath = "//*[@id=\"root\"]/div/div[1]/header/div/div/div[3]/div[2]/div/div/div/div/a[2]";

        public string DepartmentXpath = "//*[@id=\"root\"]/div/div[1]/header/div/div/div[2]/div/div/div/div/a[2]";  



        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
        }

        [Test]
        public void VacancyCount()
        {
            NavigateToVeeamURL();

            //USA, Texas
            // For some Reason Austin city not available in the "All cities" dropdown, so I used only Texas ¯\_(ツ)_/¯

            SelectDepartment(DepartmentXpath);

            SelectCountry(USAXpath);

            SelectState(TexasXpath);

            FindACareer();

            int vacanciesCount = VacancisCount();

            Console.WriteLine($"In USA, Texas {vacanciesCount} Sales vacancies");

            Assertion(vacanciesCount, 2);

            driver.Navigate().Refresh();

            //Romania, Bucharest

            
            SelectDepartment(DepartmentXpath);

            SelectCountry(RomaniaXpath);

            SelectCity(BucharestXpath);

            FindACareer();

            vacanciesCount = VacancisCount();

            Assertion(vacanciesCount, 32);

            Console.WriteLine($"In Romania, Bucharest {vacanciesCount} Sales vacancies");



            driver.Quit();
        }

        public void NavigateToVeeamURL()
        {
            Uri veeamURL = new Uri("https://careers.veeam.com/vacancies");

            driver.Navigate().GoToUrl(veeamURL);
        }

        public void ScrollToElement(IWebElement element)
        {
            Actions actions = new Actions(driver);
            actions.MoveToElement(element);
            actions.Build().Perform();
        }

        public void SelectDepartment(string jobValueXpath)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            var departmentDropDown = driver.FindElement(By.XPath("//*[@id=\"department-toggler\"]"));
            departmentDropDown.Click();
            wait.Until(driver => departmentDropDown.GetAttribute("aria-expanded") == "true");

            //Selecting Job Value
            IWebElement jobValue = driver.FindElement(By.XPath(jobValueXpath));
            jobValue.Click();
        }

        public void SelectCountry(string countryValueXpath)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            var countryDropdown = driver.FindElement(By.XPath("//*[@id=\"city-toggler\"]"));
            countryDropdown.Click();
            wait.Until(driver => countryDropdown.GetAttribute("aria-expanded") == "true");

            //Selecting Country Value
            IWebElement countryValue = driver.FindElement(By.XPath(countryValueXpath));

            ScrollToElement(countryValue);
            countryValue.Click();
        }

        public void SelectState(string stateValueXpath)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            var stateDropdown = driver.FindElement(By.XPath("//button[text()='All states']"));
            stateDropdown.Click();
            wait.Until(driver => stateDropdown.GetAttribute("aria-expanded") == "true");

            //Selecting State Value

            IWebElement stateValue = driver.FindElement(By.XPath(stateValueXpath));

            ScrollToElement(stateValue);
            stateValue.Click();

        }

        public void SelectCity(string stateValueXpath)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            var stateDropdown = driver.FindElement(By.XPath("//button[text()='All cities']"));
            stateDropdown.Click();
            wait.Until(driver => stateDropdown.GetAttribute("aria-expanded") == "true");

            //Selecting State Value

            IWebElement stateValue = driver.FindElement(By.XPath(stateValueXpath));

            ScrollToElement(stateValue);
            stateValue.Click();

        }

        public int VacancisCount()
        {
            IList<IWebElement> vacancy = driver.FindElements(By.XPath("//*[@id=\"root\"]/div/div[1]/div/div/div[1]/div[1]/div/div[1]/a[*]"));
            int vacanciesCount = vacancy.Count;

            return vacanciesCount;
        }

        public void FindACareer()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            IWebElement searchButton = driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[1]/header/div/div/div[*]/button"));

            searchButton.Click();
        }

        public void Assertion(int actual, int expected)
        {
            Assert.AreEqual(expected, actual, "Expected number of vacancies does not match");
        }

        
        
    }
}
