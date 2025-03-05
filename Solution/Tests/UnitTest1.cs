using HtmlAgilityPack;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
namespace Tests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        //Assert.That(CanScrapSelenium("https://www.chedraui.com.mx/manzana?_q=manzana&map=ft"), Is.True);
        //Assert.That(CanScrapSelenium("https://www.soriana.com/buscar?q=cafe+molido"), Is.True);
        //Assert.That(CanScrapSelenium("https://www.costco.com.mx/search?text=manzana"), Is.True);

        // curl -X GET https://lacomer.buscador.amarello.com.mx/searchArtPrior?col=lacomer_2&features=&npagel=20&p=1&pasilloId=false&s=fresa&succId=287
    }
    
    public bool CanScrapePage(string url)
    {
        try
        {
            var web = new HtmlWeb();
            var doc = web.Load(url);
            Console.WriteLine(doc.DocumentNode.OuterHtml);

            // Intenta extraer el título de la página
            var titleNode = doc.DocumentNode.SelectSingleNode("//*[@data-automation-id='product-price']");
            if (titleNode != null)
            {
                Console.WriteLine($"Título de la página: {titleNode.InnerText}");
                return true;
            }

            Console.WriteLine("No se encontró el título de la página.");
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return false;
        }
    }

    public bool CanScrapSelenium(string url)
    {
        var options = new ChromeOptions();
        options.AddArgument("--no-sandbox");
        options.AddArgument("--disable-dev-shm-usage");
        options.AddArgument("--user-data-dir=" + Guid.NewGuid().ToString());
        options.AddArgument("--headless");  // Corre sin interfaz gráfica
        options.AddArgument("--disable-blink-features=AutomationControlled");  // Evita que se detecte el modo automatizado
        
        options.AddArgument("user-agent=Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36"); // Cambia el user-agent
        
        //options.AddArgument("user-agent=Googlebot-Image/1.0");
        //options.AddArgument("user-agent=AdsBot-Google (+http://www.google.com/adsbot.html)"); // Simula AdsBot-Google

        using (var driver = new ChromeDriver(options))
        {
            driver.Navigate().GoToUrl(url);

            try
            {
                // Esperar hasta 10 segundos a que cargue el elemento
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                IWebElement element = wait.Until(drv => drv.FindElement(By.XPath("//*[@data-automation-id='product-price']")));

                Console.WriteLine($"Título de la página: {element.Text}");
                return true;
            }
            catch (WebDriverTimeoutException)
            {
                Console.WriteLine("El elemento no cargó a tiempo.");
                return false;
            }
            finally
            {
                 // Captura la pantalla
                Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();

                // Guarda la captura en un archivo
                screenshot.SaveAsFile("screenshot.png");
                Console.WriteLine("Captura de pantalla guardada con éxito.");
            }
        }
    }
}