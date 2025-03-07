using Scraper.Enums;

namespace Tests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [TestCase("manzana")]
    public void SearchProduct(string name)
    {
        StepsFactory(Provider.Soriana).SearchProduct(name);
        string header = StepsFactory(Provider.Soriana).GetHeader();
        Assert.That(header, Contains(name));
    }
}