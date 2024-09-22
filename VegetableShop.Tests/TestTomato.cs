using System.Runtime.CompilerServices;
using VegetableShop.Factory;

namespace VegetableShop.Tests;

public class TestTomato
{
    private string vegNameTest;
    private float vegPriceTest;
    private int vegQtyTest;


    [SetUp]
    public void Setup()
    {
        vegNameTest = "tomato";
        vegPriceTest = 1.50f;
        vegQtyTest = 10;

    }
    [Test]
    public void TestTomatoTotalWithoutDiscount()
    {

        VegetableProduct vegTomato = VegetableFactory.CreateProduct(vegNameTest, vegPriceTest, vegQtyTest);
        vegTomato.SetTotal();
        Assert.That(vegTomato.Total, Is.EqualTo(15.0f));
    }
    [Test]
    public void TestTomatoDiscount()
    {

        VegetableProduct vegTomato = VegetableFactory.CreateProduct(vegNameTest, vegPriceTest, vegQtyTest);
        vegTomato.SetTotal();
        vegTomato.applyOffers();
        Assert.That(vegTomato.TotalDiscount, Is.EqualTo(3f));
    }
    [Test]
    public void TestNoDiscount()
    {

        VegetableProduct vegTomato = VegetableFactory.CreateProduct(vegNameTest, vegPriceTest, 1);
        vegTomato.SetTotal();
        vegTomato.applyOffers();
        Assert.That(vegTomato.TotalDiscount, Is.EqualTo(0f));
    }
}