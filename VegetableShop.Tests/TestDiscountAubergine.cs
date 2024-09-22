using System.Runtime.CompilerServices;
using VegetableShop.Factory;

namespace VegetableShop.Tests;

public class TestDiscountAubergine
{
    private string vegNameTest;
    private float vegPriceTest;
    private int vegQtyTest;


    [SetUp]
    public void Setup()
    {
        vegNameTest = "aubergine";
        vegPriceTest = 1.50f;
        vegQtyTest = 10;


    }

    [Test]
    public void TestAubergineDiscount3x2()
    {

        VegetableProduct vegAubergine = VegetableFactory.CreateProduct(vegNameTest, vegPriceTest, vegQtyTest);
        vegAubergine.SetTotal();
        vegAubergine.applyOffers();
        Assert.That(vegAubergine.TotalDiscount, Is.EqualTo(4.50f));
    }

    [Test]
    public void TestAubergineDiscountwithTomamtoOffer()
    {

        VegetableProduct vegAubergine = VegetableFactory.CreateProduct(vegNameTest, vegPriceTest, vegQtyTest);
        vegAubergine.Free = 2;
        vegAubergine.Quantity += 2;
        vegAubergine.SetTotal();
        vegAubergine.applyOffers();

        Assert.That(vegAubergine.TotalDiscount, Is.EqualTo(7.50f));
    }
    [Test]
    public void TestNoDiscount()
    {

        VegetableProduct vegAubergine = VegetableFactory.CreateProduct(vegNameTest, vegPriceTest, 1);
        vegAubergine.SetTotal();
        vegAubergine.applyOffers();
        Assert.That(vegAubergine.TotalDiscount, Is.EqualTo(0f));
    }
    [Test]
    public void TestNoAubergines()
    {

        VegetableProduct vegAubergine = VegetableFactory.CreateProduct(vegNameTest, vegPriceTest, 0);
        vegAubergine.SetTotal();
        vegAubergine.applyOffers();
        Assert.That(vegAubergine.TotalDiscount, Is.EqualTo(0f));
    }
}