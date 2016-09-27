using System;
using System.Collections.Generic;
using System.Diagnostics;
using InternetCoast.DAL.Repositories;
using InternetCoast.Infrastructure.Data.EF.Context;
using InternetCoast.Model.Context;
using InternetCoast.Model.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InternetCoast.DAL.Test.Repositories
{
    [TestClass]
    public class FundRepositoryTest
    {
        [TestMethod]
        public void AddFund()
        {
            var repository = new FundRepository(new AppDbContext(new UiContext()));
            var toalFunds = repository.Count();
            var newFund = new Fund { FundTitle = "Test Fund", FundTopic = "Test Topic", Active = true, DeadLine = DateTime.Today.AddDays(20), OpenDate = DateTime.Today.AddDays(5) };
            repository.Add(newFund);
            repository.Save();
            var newToalFunds = repository.Count();
            Assert.IsTrue(newToalFunds > toalFunds);
        }
    }
}
