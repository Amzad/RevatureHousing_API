using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RevHousingAPI.Controllers;
using RevHousingAPI.Repositories;
using RHEntities;
using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using RevHousingAPI.Data;
using RevHousingAPI.IRepositories;


namespace RevatureHousingAPITestProject
{
    [TestClass]
    public class LocationUnitTest
    {
        // Arrange
        public DbContextOptions<ApplicationDBContext> options;
        public ApplicationDBContext testContext;
        public LocationDummyData dummyLocationsData;
        public List<Location> dummyLocations;
        public LocationRepository testLocationRepository;
        public LocationsController testLocationController;
        //Dummy constants
        public Location dummyConstantLocation;
        public LocationUnitTest()
        {
            /*// Arrange Everything We Need For Our Unit Tests
            options = new DbContextOptionsBuilder<ApplicationDBContext>()
            .UseInMemoryDatabase(databaseName: "TestRevatureHousingData")
            .Options;
            testContext = new ApplicationDBContext(options);
            dummyLocationsData = new LocationDummyData();
            dummyLocations = dummyLocationsData.LocationsList;
            testLocationRepository = new LocationRepository(testContext);
            testLocationController = new LocationsController(testLocationRepository);
            dummyConstantLocation = new Location() { LocationID = 3 };*/
        }
        public void ClearAllChanges()
        {
            // Clearing Changes
            foreach (var entity in testContext.Location)
                testContext.Location.Remove(entity);
            testContext.SaveChanges();
        }
        public void Populate()
        {
            foreach (Location locale in dummyLocations)
            {
                var postResponse = testLocationController.PostLocation(locale);
                //var result = (postResponse as StatusCodeResult);
            }
        }
        [TestMethod]
        public void CanPostLocationToDatabase()
        {
            // Arrange Everything We Need For Our Unit Tests
            options = new DbContextOptionsBuilder<ApplicationDBContext>()
            .UseInMemoryDatabase(databaseName: "TestRevatureHousingData")
            .Options;
            testContext = new ApplicationDBContext(options);
            dummyLocationsData = new LocationDummyData();
            dummyLocations = dummyLocationsData.LocationsList;
            testLocationRepository = new LocationRepository(testContext);
            testLocationController = new LocationsController(testLocationRepository);
            dummyConstantLocation = new Location() { LocationID = 3 };
            //Arrange
            Location dummyLocation = dummyConstantLocation;
            //Act
            var postResponse = testLocationController.PostLocation(dummyLocation);
            var postResult = (postResponse as StatusCodeResult);
            Assert.IsInstanceOfType(postResult,typeof(StatusCodeResult));
            Assert.AreEqual(postResult.StatusCode,201);
            ClearAllChanges();
        }
        [TestMethod]
        public void CannotAddSameLocationTwiceInDatabase()
        {
            // Arrange Everything We Need For Our Unit Tests
            options = new DbContextOptionsBuilder<ApplicationDBContext>()
            .UseInMemoryDatabase(databaseName: "TestRevatureHousingData")
            .Options;
            testContext = new ApplicationDBContext(options);
            dummyLocationsData = new LocationDummyData();
            dummyLocations = dummyLocationsData.LocationsList;
            testLocationRepository = new LocationRepository(testContext);
            testLocationController = new LocationsController(testLocationRepository);
            dummyConstantLocation = new Location() { LocationID = 3 };
            //Arrange
            Location dummyLocation = dummyConstantLocation;
            var postResponse = testLocationController.PostLocation(dummyLocation);
            //Act
            var secondPostResult = testLocationController.PostLocation(dummyLocation);
            var postStatusCode = (secondPostResult as StatusCodeResult);
            //Assert
            Assert.IsInstanceOfType(secondPostResult, typeof(StatusCodeResult));
            Assert.AreEqual(postStatusCode.StatusCode,409);
            //Clearing Changes
            ClearAllChanges();
        }
        [TestMethod]
        public void CanGetAllLocationsInDatabase()
        {
            // Arrange Everything We Need For Our Unit Tests
            options = new DbContextOptionsBuilder<ApplicationDBContext>()
            .UseInMemoryDatabase(databaseName: "TestRevatureHousingData")
            .Options;
            testContext = new ApplicationDBContext(options);
            dummyLocationsData = new LocationDummyData();
            dummyLocations = dummyLocationsData.LocationsList;
            testLocationRepository = new LocationRepository(testContext);
            testLocationController = new LocationsController(testLocationRepository);
            dummyConstantLocation = new Location() { LocationID = 3 };
            //Arrange
            Populate();
            //Act
            var allLocations = testLocationController.GetLocation().Result.ToList();
            var allDummyLocations = dummyLocations.Count;
            Assert.AreEqual(allLocations.Count, allDummyLocations);
            //Clearing Changes
            ClearAllChanges();
        }
        [TestMethod]
        public void CantGetLocationInEmptyDatabase()
        {
            // Arrange Everything We Need For Our Unit Tests
            options = new DbContextOptionsBuilder<ApplicationDBContext>()
            .UseInMemoryDatabase(databaseName: "TestRevatureHousingData")
            .Options;
            testContext = new ApplicationDBContext(options);
            dummyLocationsData = new LocationDummyData();
            dummyLocations = dummyLocationsData.LocationsList;
            testLocationRepository = new LocationRepository(testContext);
            testLocationController = new LocationsController(testLocationRepository);
            dummyConstantLocation = new Location() { LocationID = 3 };
            //Act
            var getLocationResult = testLocationController.GetLocation(8191).Result.Value;
            //Assert
            Assert.AreEqual(getLocationResult,null);
            ClearAllChanges();
        }
        [TestMethod]
        public void CantGetLocationNotInDatabase()
        {
            // Arrange Everything We Need For Our Unit Tests
            options = new DbContextOptionsBuilder<ApplicationDBContext>()
            .UseInMemoryDatabase(databaseName: "TestRevatureHousingData")
            .Options;
            testContext = new ApplicationDBContext(options);
            dummyLocationsData = new LocationDummyData();
            dummyLocations = dummyLocationsData.LocationsList;
            testLocationRepository = new LocationRepository(testContext);
            testLocationController = new LocationsController(testLocationRepository);
            dummyConstantLocation = new Location() { LocationID = 3 };
            //Arrange
            Populate();
            //Act
            var getLocationResult = testLocationController.GetLocation(8191).Result.Value;
            //Assert
            Assert.AreEqual(getLocationResult, null);
            ClearAllChanges();
        }
        [TestMethod]
        public void CanGetLocationInDatabase()
        {
            // Arrange Everything We Need For Our Unit Tests
            options = new DbContextOptionsBuilder<ApplicationDBContext>()
            .UseInMemoryDatabase(databaseName: "TestRevatureHousingData")
            .Options;
            testContext = new ApplicationDBContext(options);
            dummyLocationsData = new LocationDummyData();
            dummyLocations = dummyLocationsData.LocationsList;
            testLocationRepository = new LocationRepository(testContext);
            testLocationController = new LocationsController(testLocationRepository);
            dummyConstantLocation = new Location() { LocationID = 3 };
            //Arrange
            Populate();
            //Act
            var getLocationResult = testLocationController.GetLocation(5).Result.Value;
            //Assert
            Assert.IsInstanceOfType(getLocationResult, typeof(Location));
            ClearAllChanges();
        }
        [TestMethod]
        public void CannotUpdateLocationWithWrongSignature()
        {
            // Arrange Everything We Need For Our Unit Tests
            options = new DbContextOptionsBuilder<ApplicationDBContext>()
            .UseInMemoryDatabase(databaseName: "TestRevatureHousingData")
            .Options;
            testContext = new ApplicationDBContext(options);
            dummyLocationsData = new LocationDummyData();
            dummyLocations = dummyLocationsData.LocationsList;
            testLocationRepository = new LocationRepository(testContext);
            testLocationController = new LocationsController(testLocationRepository);
            dummyConstantLocation = new Location() { LocationID = 3 };
            //Arrange
            Location locale = new Location() { LocationID = 5 };
            var postResponse = testLocationController.PostLocation(locale);
            //Act
            var putResponse = testLocationController.PutLocation(128923, locale).Result;
            //Assert
            Assert.IsInstanceOfType(putResponse, typeof(BadRequestObjectResult));
            // Clearing Changes
            ClearAllChanges();
        }
        [TestMethod]
        public void CanDeleteLocationInDatabase()
        {
            // Arrange Everything We Need For Our Unit Tests
            options = new DbContextOptionsBuilder<ApplicationDBContext>()
            .UseInMemoryDatabase(databaseName: "TestRevatureHousingData")
            .Options;
            testContext = new ApplicationDBContext(options);
            dummyLocationsData = new LocationDummyData();
            dummyLocations = dummyLocationsData.LocationsList;
            testLocationRepository = new LocationRepository(testContext);
            testLocationController = new LocationsController(testLocationRepository);
            dummyConstantLocation = new Location() { LocationID = 3 };
            //Arrange
            Location locale = new Location() { LocationID = 5 };
            var postResponse = testLocationController.PostLocation(locale);
            //Act
            var deleteResult = testLocationController.DeleteLocation(5).Result.Result;
            var deleteStatusCode = (deleteResult as StatusCodeResult);
            //Assert
            Assert.IsInstanceOfType(deleteResult, typeof(OkResult));
            Assert.AreEqual(deleteStatusCode.StatusCode, 200);
            // Clearing Changes
            ClearAllChanges();
        }
        [TestMethod]
        public void CannotDeleteLocationInEmptyDatabase()
        {
            // Arrange Everything We Need For Our Unit Tests
            options = new DbContextOptionsBuilder<ApplicationDBContext>()
            .UseInMemoryDatabase(databaseName: "TestRevatureHousingData")
            .Options;
            testContext = new ApplicationDBContext(options);
            dummyLocationsData = new LocationDummyData();
            dummyLocations = dummyLocationsData.LocationsList;
            testLocationRepository = new LocationRepository(testContext);
            testLocationController = new LocationsController(testLocationRepository);
            dummyConstantLocation = new Location() { LocationID = 3 };
            //Act
            var deleteResult = testLocationController.DeleteLocation(19292).Result.Result;
            var deleteStatusCode = (deleteResult as StatusCodeResult);
            //Assert
            Assert.IsInstanceOfType(deleteResult, typeof(NotFoundResult));
            Assert.AreEqual(deleteStatusCode.StatusCode, 404);
            // Clearing Changes
            ClearAllChanges();
        }
        [TestMethod]
        public void CannotDeleteLocationNotInDatabase()
        {
            // Arrange Everything We Need For Our Unit Tests
            options = new DbContextOptionsBuilder<ApplicationDBContext>()
            .UseInMemoryDatabase(databaseName: "TestRevatureHousingData")
            .Options;
            testContext = new ApplicationDBContext(options);
            dummyLocationsData = new LocationDummyData();
            dummyLocations = dummyLocationsData.LocationsList;
            testLocationRepository = new LocationRepository(testContext);
            testLocationController = new LocationsController(testLocationRepository);
            dummyConstantLocation = new Location() { LocationID = 3 };
            //Arrange
            Populate();
            //Act
            var deleteResult = testLocationController.DeleteLocation(12728).Result.Result;
            var deleteStatusCode = (deleteResult as StatusCodeResult);
            //Assert
            Assert.IsInstanceOfType(deleteResult, typeof(NotFoundResult));
            Assert.AreEqual(deleteStatusCode.StatusCode, 404);
            // Clearing Changes
            ClearAllChanges();
        }
        [TestMethod]
        public void CanGetLocationWithProviderInDatabase()
        {
            // Arrange Everything We Need For Our Unit Tests
            options = new DbContextOptionsBuilder<ApplicationDBContext>()
            .UseInMemoryDatabase(databaseName: "TestRevatureHousingData")
            .Options;
            testContext = new ApplicationDBContext(options);
            dummyLocationsData = new LocationDummyData();
            dummyLocations = dummyLocationsData.LocationsList;
            testLocationRepository = new LocationRepository(testContext);
            testLocationController = new LocationsController(testLocationRepository);
            dummyConstantLocation = new Location() { LocationID = 3 };
            //Arrange
            Populate();
            //Act
            var GetLocationWithProviderResult = testLocationController.GetLocationByProvider(12.ToString()).Result.Value.ToList()[0];
            //Assert
            Assert.IsInstanceOfType(GetLocationWithProviderResult, typeof(Location));
            //Clearing Changes
            ClearAllChanges();
        }
        [TestMethod]
        public void CantGetLocationWithProviderInEmptyDatabase()
        {
            // Arrange Everything We Need For Our Unit Tests
            options = new DbContextOptionsBuilder<ApplicationDBContext>()
            .UseInMemoryDatabase(databaseName: "TestRevatureHousingData")
            .Options;
            testContext = new ApplicationDBContext(options);
            dummyLocationsData = new LocationDummyData();
            dummyLocations = dummyLocationsData.LocationsList;
            testLocationRepository = new LocationRepository(testContext);
            testLocationController = new LocationsController(testLocationRepository);
            dummyConstantLocation = new Location() { LocationID = 3 };
            //Act
            var GetLocationWithProviderResult = testLocationController.GetLocationByProvider(12.ToString()).Result.Value.ToList();
            //Assert
            Assert.AreEqual(GetLocationWithProviderResult.Count(), 0);
            ClearAllChanges();
        }
        [TestMethod]
        public void CantGetLocationWithProviderNotInDatabase()
        {
            // Arrange Everything We Need For Our Unit Tests
            options = new DbContextOptionsBuilder<ApplicationDBContext>()
            .UseInMemoryDatabase(databaseName: "TestRevatureHousingData")
            .Options;
            testContext = new ApplicationDBContext(options);
            dummyLocationsData = new LocationDummyData();
            dummyLocations = dummyLocationsData.LocationsList;
            testLocationRepository = new LocationRepository(testContext);
            testLocationController = new LocationsController(testLocationRepository);
            dummyConstantLocation = new Location() { LocationID = 3 };
            //Arrange
            Populate();
            string inValidProvider = "InValidProviderName";
            //Act
            var GetLocationWithProviderResult = testLocationController.GetLocationByProvider(inValidProvider).Result.Value.ToList();
            //Assert
            Assert.AreEqual(GetLocationWithProviderResult.Count(), 0);
            ClearAllChanges();
        }
        [TestMethod]
        public void CanGetLocationByTrainingCenterInDatabase()
        {
            // Arrange Everything We Need For Our Unit Tests
            options = new DbContextOptionsBuilder<ApplicationDBContext>()
            .UseInMemoryDatabase(databaseName: "TestRevatureHousingData")
            .Options;
            testContext = new ApplicationDBContext(options);
            dummyLocationsData = new LocationDummyData();
            dummyLocations = dummyLocationsData.LocationsList;
            testLocationRepository = new LocationRepository(testContext);
            testLocationController = new LocationsController(testLocationRepository);
            dummyConstantLocation = new Location() { LocationID = 3 };
            //Arrange
            Populate();
            //Act
            var GetLocationWithProviderResult = testLocationController.GetLocationByTrainingCenter(14.ToString()).Result.Value.ToList()[0];
            //Assert
            Assert.IsInstanceOfType(GetLocationWithProviderResult, typeof(Location));
            //Clearing Changes
            ClearAllChanges();
        }
        [TestMethod]
        public void CantGetLocationByTrainingCenterInEmptyDatabase()
        {
            // Arrange Everything We Need For Our Unit Tests
            options = new DbContextOptionsBuilder<ApplicationDBContext>()
            .UseInMemoryDatabase(databaseName: "TestRevatureHousingData")
            .Options;
            testContext = new ApplicationDBContext(options);
            dummyLocationsData = new LocationDummyData();
            dummyLocations = dummyLocationsData.LocationsList;
            testLocationRepository = new LocationRepository(testContext);
            testLocationController = new LocationsController(testLocationRepository);
            dummyConstantLocation = new Location() { LocationID = 3 };
            //Act
            var GetLocationWithProviderResult = testLocationController.GetLocationByTrainingCenter(14.ToString()).Result.Value.ToList();
            //Assert
            Assert.AreEqual(GetLocationWithProviderResult.Count(), 0);
            ClearAllChanges();
        }
        [TestMethod]
        public void CantGetLocationByTrainingCenterNotInDatabase()
        {
            // Arrange Everything We Need For Our Unit Tests
            options = new DbContextOptionsBuilder<ApplicationDBContext>()
            .UseInMemoryDatabase(databaseName: "TestRevatureHousingData")
            .Options;
            testContext = new ApplicationDBContext(options);
            dummyLocationsData = new LocationDummyData();
            dummyLocations = dummyLocationsData.LocationsList;
            testLocationRepository = new LocationRepository(testContext);
            testLocationController = new LocationsController(testLocationRepository);
            dummyConstantLocation = new Location() { LocationID = 3 };
            //Arrange
            Populate();
            string inValidTrainingCenter = "InValidTrainingCenter";
            //Act
            var GetLocationWithProviderResult = testLocationController.GetLocationByTrainingCenter(inValidTrainingCenter).Result.Value.ToList();
            //Assert
            Assert.AreEqual(GetLocationWithProviderResult.Count(), 0);
            ClearAllChanges();
        }


    }
}
