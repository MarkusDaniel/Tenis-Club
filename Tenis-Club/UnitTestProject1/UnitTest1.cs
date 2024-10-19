using Microsoft.VisualStudio.TestTools.UnitTesting;
using _3.Beadando;
using System;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void UjPaja_ShouldAddNewCourt()
        {
            // Arrange
            var teniszklub = new TeniszKlub();
            var expectedPalyakCount = 1; // Mivel csak egy új pályát adunk hozzá

            // Act
            teniszklub.UjPaja(1, PalyaTipus.Fu, true); // Példa adatokkal

            // Assert
            Assert.AreEqual(expectedPalyakCount, teniszklub.OsszesPalya().Count);
        }

        [TestMethod]
        public void PalyaFelszamolasa_ShouldRemoveCourt()
        {
            // Arrange
            var teniszklub = new TeniszKlub();
            teniszklub.UjPaja(1, PalyaTipus.Fu, true); // Először hozzáadunk egy pályát
            var expectedPalyakCount = 0; // A pálya felszámolása után nem lesz pálya

            // Act
            teniszklub.PalyaFelszamolasa(1); // Példa adattal

            // Assert
            Assert.AreEqual(expectedPalyakCount, teniszklub.OsszesPalya().Count);
        }

        [TestMethod]
        public void UjTag_ShouldAddNewMember()
        {
            // Arrange
            var teniszklub = new TeniszKlub();
            var expectedTagokCount = 1; // Mivel csak egy új tagot adunk hozzá

            // Act
            teniszklub.UjTag("John Doe"); // Példa névvel

            // Assert
            Assert.AreEqual(expectedTagokCount, teniszklub.OsszesTag().Count);
        }

        [TestMethod]
        public void TagKilep_ShouldRemoveMember()
        {
            // Arrange
            var teniszklub = new TeniszKlub();
            teniszklub.UjTag("John Doe"); // Először hozzáadunk egy tagot
            var expectedTagokCount = 0; // A tag kilépése után nem lesz tag

            // Act
            teniszklub.TagKilep("John Doe"); // Példa névvel

            // Assert
            Assert.AreEqual(expectedTagokCount, teniszklub.OsszesTag().Count);
        }

        [TestMethod]
        public void UjFoglalas_ShouldCreateReservation()
        {
            // Arrange
            var teniszklub = new TeniszKlub();
            teniszklub.UjTag("John Doe"); // Először hozzáadunk egy tagot
            teniszklub.UjPaja(1, PalyaTipus.Fu, true); // Majd létrehozunk egy pályát
            var expectedFoglalasokCount = 1; // Mivel csak egy foglalást készítünk

            // Act
            teniszklub.UjFoglalás("John Doe", 1, DateTime.Now.Date, 10); // Példa adatokkal

            // Assert
            Assert.AreEqual(expectedFoglalasokCount, teniszklub.OsszesFoglalas(DateTime.Now.Date, 1).Count);
        }

        [TestMethod]
        public void FoglalasLemondas_ShouldCancelReservation()
        {
            // Arrange
            var teniszklub = new TeniszKlub();
            teniszklub.UjTag("John Doe"); // Először hozzáadunk egy tagot
            teniszklub.UjPaja(1, PalyaTipus.Fu, true); // Majd létrehozunk egy pályát
            teniszklub.UjFoglalás("John Doe", 1, DateTime.Now.Date, 10); // Először létrehozunk egy foglalást
            var expectedFoglalasokCount = 0; // Mivel a foglalást töröljük

            // Act
            teniszklub.FoglalasLemondas("John Doe", 1, DateTime.Now.Date, 10); // Példa adatokkal

            // Assert
            Assert.AreEqual(expectedFoglalasokCount, teniszklub.OsszesFoglalas(DateTime.Now.Date, 1).Count);
        }

        [TestMethod]
        public void NapiOsszbevetel_ShouldCalculateTotalRevenueForSpecificDate()
        {
            // Arrange
            var teniszklub = new TeniszKlub();
            teniszklub.UjTag("John Doe"); // Először hozzáadunk egy tagot
            teniszklub.UjPaja(1, PalyaTipus.Fu, true); // Majd létrehozunk egy pályát
            teniszklub.UjFoglalás("John Doe", 1, DateTime.Now.Date, 10); // Először létrehozunk egy foglalást
            teniszklub.UjFoglalás("John Doe", 1, DateTime.Now.Date, 15); // Második foglalás
            var expectedRevenue = 12000.0; // Mivel 2 foglalást készítettünk

            // Act
            var revenue = teniszklub.NapiOsszbevetel(DateTime.Now.Date); // Példa adattal

            // Assert
            Assert.AreEqual(expectedRevenue, revenue);
        }
    }
}
