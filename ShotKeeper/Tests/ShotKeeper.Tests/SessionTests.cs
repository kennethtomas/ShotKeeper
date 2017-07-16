using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShotKeeper.Models;

namespace ShotKeeper.Tests
{
    [TestClass]
    public class SessionTests
    {
        [TestMethod]
        public void CreateNewSession()
        {
            ShootingSession sessh = new ShootingSession();
            Assert.IsNotNull(sessh);
        }

        [TestMethod]
        public void AddFreeThrow()
        {
            ShootingSession session = new ShootingSession();
            session.NumberOfFreeThrows++;
            Assert.AreEqual(session.NumberOfFreeThrows, 1);
        }

        [TestMethod]
        public void RemoveFreeThrow()
        {
            ShootingSession session = new ShootingSession();
            session.NumberOfFreeThrows++;
            Assert.AreEqual(session.NumberOfFreeThrows, 1);
            session.NumberOfFreeThrows--;
            Assert.AreEqual(session.NumberOfFreeThrows, 0);
        }

        [TestMethod]
        public void AddFreeThrowCounted()
        {
            ShootingSession session = new ShootingSession();
            session.NumberOfFreeThrowsCounted++;
            Assert.AreEqual(session.NumberOfFreeThrowsCounted, 1);
        }

        [TestMethod]
        public void RemoveFreeThrowCounted()
        {
            ShootingSession session = new ShootingSession();
            session.NumberOfFreeThrowsCounted++;
            Assert.AreEqual(session.NumberOfFreeThrowsCounted, 1);
            session.NumberOfFreeThrowsCounted--;
            Assert.AreEqual(session.NumberOfFreeThrowsCounted, 0);
        }
    }
}
