using System.Runtime.CompilerServices;
using VegetableShop.Factory;

namespace VegetableShop.Tests;

public class TestFactoryMethod
{
    [Test]
    public void TestNoProductException()
    {

        Assert.Throws<ArgumentException>(
                () => VegetableFactory.CreateProduct("Onion", 1.50f, 8));

    }

}