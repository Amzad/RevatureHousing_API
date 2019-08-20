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
    public class RoomUnitTest
    {
        // Arrange
        public DbContextOptions<ApplicationDBContext> options;
        public ApplicationDBContext testContext;
        public RoomDummyData dummyRoomsData;
        public List<Room> dummyRooms;
        public RoomRepository testRoomRepository;
        public RoomsController testRoomController;
        //Dummy constants
        public Room dummyConstantRoom;
        public RoomUnitTest()
        {
            /*// Arrange Everything We Need For Our Unit Tests
            options = new DbContextOptionsBuilder<ApplicationDBContext>()
            .UseInMemoryDatabase(databaseName: "TestRevatureHousingData")
            .Options;
            testContext = new ApplicationDBContext(options);
            dummyRoomsData = new RoomDummyData();
            dummyRooms = dummyRoomsData.RoomsList;
            testRoomRepository = new RoomRepository(testContext);
            testRoomController = new RoomsController(testRoomRepository);
            dummyConstantRoom = new Room() { RoomID = 12728 };*/
        }
        public void ClearAllChanges()
        {
            // Clearing Changes
            foreach (var entity in testContext.Room)
                testContext.Room.Remove(entity);
            testContext.SaveChanges();
        }
        public void Populate()
        {
            foreach (Room room in dummyRooms)
            {
                var postResponse = testRoomController.PostRoom(room);
                var result = postResponse.Result;
            }
        }
        [TestMethod]
        public void CanPostRoomToDatabase()
        {
            // Arrange Everything We Need For Our Unit Tests
            options = new DbContextOptionsBuilder<ApplicationDBContext>()
            .UseInMemoryDatabase(databaseName: "TestRevatureHousingData")
            .Options;
            testContext = new ApplicationDBContext(options);
            dummyRoomsData = new RoomDummyData();
            dummyRooms = dummyRoomsData.RoomsList;
            testRoomRepository = new RoomRepository(testContext);
            testRoomController = new RoomsController(testRoomRepository);
            dummyConstantRoom = new Room() { RoomID = 12728 };
            //Arrange
            Room dummyRoom = dummyConstantRoom;
            //Act
            var postResponse = testRoomController.PostRoom(dummyRoom);
            var postResult = postResponse.Result.Result;
            var postStatusCode = (postResult as StatusCodeResult);
            //Assert
            Assert.IsInstanceOfType(postResult, typeof(StatusCodeResult));
            Assert.AreEqual(postStatusCode.StatusCode, 201);
            // Clearing Changes
            ClearAllChanges();
        }
        [TestMethod]
        public void CannotAddSameRoomTwiceInDatabase()
        {
            // Arrange Everything We Need For Our Unit Tests
            options = new DbContextOptionsBuilder<ApplicationDBContext>()
            .UseInMemoryDatabase(databaseName: "TestRevatureHousingData")
            .Options;
            testContext = new ApplicationDBContext(options);
            dummyRoomsData = new RoomDummyData();
            dummyRooms = dummyRoomsData.RoomsList;
            testRoomRepository = new RoomRepository(testContext);
            testRoomController = new RoomsController(testRoomRepository);
            dummyConstantRoom = new Room() { RoomID = 12728 };
            //Arrange
            Room dummyRoom = dummyConstantRoom;
            var postResponse = testRoomController.PostRoom(dummyRoom);
            //Act
            var secondPostResponse = testRoomController.PostRoom(dummyRoom);
            var secondPostResult = secondPostResponse.Result.Result;
            var postStatusCode = (secondPostResult as StatusCodeResult);
            //Assert
            Assert.IsInstanceOfType(secondPostResult, typeof(StatusCodeResult));
            Assert.AreEqual(postStatusCode.StatusCode, 409);
            // Clearing Changes
            ClearAllChanges();
        }
        [TestMethod]
        public void CanGetAllRoomsInDatabase()
        {
            // Arrange Everything We Need For Our Unit Tests
            options = new DbContextOptionsBuilder<ApplicationDBContext>()
            .UseInMemoryDatabase(databaseName: "TestRevatureHousingData")
            .Options;
            testContext = new ApplicationDBContext(options);
            dummyRoomsData = new RoomDummyData();
            dummyRooms = dummyRoomsData.RoomsList;
            testRoomRepository = new RoomRepository(testContext);
            testRoomController = new RoomsController(testRoomRepository);
            dummyConstantRoom = new Room() { RoomID = 12728 };
            // Arrange 
            // Adding all rooms to database
            Populate();
            //Act
            var allRooms = testRoomController.GetRoom().Result.ToList();
            var allDummyRooms = dummyRooms.Count;
            Assert.AreEqual(allRooms.Count, allDummyRooms);
            // Clearing Changes
            ClearAllChanges();
        }
        [TestMethod]
        public void CantGetRoomInEmptyDatabase()
        {
            // Arrange Everything We Need For Our Unit Tests
            options = new DbContextOptionsBuilder<ApplicationDBContext>()
            .UseInMemoryDatabase(databaseName: "TestRevatureHousingData")
            .Options;
            testContext = new ApplicationDBContext(options);
            dummyRoomsData = new RoomDummyData();
            dummyRooms = dummyRoomsData.RoomsList;
            testRoomRepository = new RoomRepository(testContext);
            testRoomController = new RoomsController(testRoomRepository);
            dummyConstantRoom = new Room() { RoomID = 12728 };
            // Act
            var getRoomResult = testRoomController.GetRoom(8191).Result;
            // Assert
            Assert.IsInstanceOfType(getRoomResult,typeof(NotFoundResult));
            ClearAllChanges();
        }
        [TestMethod]
        public void CantGetRoomNotInDatabase()
        {
            // Arrange Everything We Need For Our Unit Tests
            options = new DbContextOptionsBuilder<ApplicationDBContext>()
            .UseInMemoryDatabase(databaseName: "TestRevatureHousingData")
            .Options;
            testContext = new ApplicationDBContext(options);
            dummyRoomsData = new RoomDummyData();
            dummyRooms = dummyRoomsData.RoomsList;
            testRoomRepository = new RoomRepository(testContext);
            testRoomController = new RoomsController(testRoomRepository);
            dummyConstantRoom = new Room() { RoomID = 12728 };
            //Arrange
            Populate();
            // Act
            var getRoomResult = testRoomController.GetRoom(8191).Result;
            //Assert
            Assert.IsInstanceOfType(getRoomResult, typeof(NotFoundResult));
            // Clearing Changes
            ClearAllChanges();
        }
        [TestMethod]
        public void CanGetRoomInDatabase()
        {
            // Arrange Everything We Need For Our Unit Tests
            options = new DbContextOptionsBuilder<ApplicationDBContext>()
            .UseInMemoryDatabase(databaseName: "TestRevatureHousingData")
            .Options;
            testContext = new ApplicationDBContext(options);
            dummyRoomsData = new RoomDummyData();
            dummyRooms = dummyRoomsData.RoomsList;
            testRoomRepository = new RoomRepository(testContext);
            testRoomController = new RoomsController(testRoomRepository);
            dummyConstantRoom = new Room() { RoomID = 12728 };
            //Arrange
            Populate();
            // Act
            var getRoomResult = testRoomController.GetRoom(5).Value;
            //Assert
            Assert.IsInstanceOfType(getRoomResult, typeof(Room));
            // Clearing Changes
            ClearAllChanges();
        }
        [TestMethod]
        public void CannotUpdateRoomWithWrongSignature()
        {
            // Arrange Everything We Need For Our Unit Tests
            options = new DbContextOptionsBuilder<ApplicationDBContext>()
            .UseInMemoryDatabase(databaseName: "TestRevatureHousingData")
            .Options;
            testContext = new ApplicationDBContext(options);
            dummyRoomsData = new RoomDummyData();
            dummyRooms = dummyRoomsData.RoomsList;
            testRoomRepository = new RoomRepository(testContext);
            testRoomController = new RoomsController(testRoomRepository);
            dummyConstantRoom = new Room() { RoomID = 12728 };
            //Arrange
            Room room = new Room() { RoomID = 5 };
            var postResponse = testRoomController.PostRoom(room);
            //Act
            var putResponse = testRoomController.PutRoom(128923, room).Result;
            //Assert
            Assert.IsInstanceOfType(putResponse, typeof(BadRequestResult));
            // Clearing Changes
            ClearAllChanges();
        }
        [TestMethod]
        public void CanDeleteRoomInDatabase()
        {
            // Arrange Everything We Need For Our Unit Tests
            options = new DbContextOptionsBuilder<ApplicationDBContext>()
            .UseInMemoryDatabase(databaseName: "TestRevatureHousingData")
            .Options;
            testContext = new ApplicationDBContext(options);
            dummyRoomsData = new RoomDummyData();
            dummyRooms = dummyRoomsData.RoomsList;
            testRoomRepository = new RoomRepository(testContext);
            testRoomController = new RoomsController(testRoomRepository);
            dummyConstantRoom = new Room() { RoomID = 12728 };
            //Arrange
            Room room = new Room() { RoomID = 5 };
            var postResponse = testRoomController.PostRoom(room);
            //Act
            var deleteResult = testRoomController.DeleteRoom(5).Result.Result;
            var deleteStatusCode = (deleteResult as StatusCodeResult);
            //Assert
            Assert.AreEqual(deleteStatusCode.StatusCode, 202);
            // Clearing Changes
            ClearAllChanges();
        }
        [TestMethod]
        public void CannotDeleteRoomInEmptyDatabase()
        {
            // Arrange Everything We Need For Our Unit Tests
            options = new DbContextOptionsBuilder<ApplicationDBContext>()
            .UseInMemoryDatabase(databaseName: "TestRevatureHousingData")
            .Options;
            testContext = new ApplicationDBContext(options);
            dummyRoomsData = new RoomDummyData();
            dummyRooms = dummyRoomsData.RoomsList;
            testRoomRepository = new RoomRepository(testContext);
            testRoomController = new RoomsController(testRoomRepository);
            dummyConstantRoom = new Room() { RoomID = 12728 };
            //Act
            var deleteResult = testRoomController.DeleteRoom(5).Result.Result;
            //Assert
            Assert.IsInstanceOfType(deleteResult, typeof(NotFoundResult));
            // Clearing Changes
            ClearAllChanges();
        }
        [TestMethod]
        public void CannotDeleteRoomNotInDatabase()
        {
            // Arrange Everything We Need For Our Unit Tests
            options = new DbContextOptionsBuilder<ApplicationDBContext>()
            .UseInMemoryDatabase(databaseName: "TestRevatureHousingData")
            .Options;
            testContext = new ApplicationDBContext(options);
            dummyRoomsData = new RoomDummyData();
            dummyRooms = dummyRoomsData.RoomsList;
            testRoomRepository = new RoomRepository(testContext);
            testRoomController = new RoomsController(testRoomRepository);
            dummyConstantRoom = new Room() { RoomID = 12728 };
            //Arrange
            Populate();
            //Act
            var deleteResult = testRoomController.DeleteRoom(12728).Result.Result;
            //Assert
            Assert.IsInstanceOfType(deleteResult, typeof(NotFoundResult));
            // Clearing Changes
            ClearAllChanges();
        }
        [TestMethod]
        public void CanGetRoomWithLocationIDInDatabase()
        {
            // Arrange Everything We Need For Our Unit Tests
            options = new DbContextOptionsBuilder<ApplicationDBContext>()
            .UseInMemoryDatabase(databaseName: "TestRevatureHousingData")
            .Options;
            testContext = new ApplicationDBContext(options);
            dummyRoomsData = new RoomDummyData();
            dummyRooms = dummyRoomsData.RoomsList;
            testRoomRepository = new RoomRepository(testContext);
            testRoomController = new RoomsController(testRoomRepository);
            dummyConstantRoom = new Room() { RoomID = 12728 };
            //Arrange
            Populate();
            //Act
            var GetRoomWithLocationIDResult = testRoomController.GetRoomWithLocationID(12).Result.Value.ToList();
            bool greaterOrEqualToOne = false;
            if (GetRoomWithLocationIDResult.Count() > 0)
                greaterOrEqualToOne = true;
            //assert
            Assert.AreEqual(greaterOrEqualToOne, true);
            //Clearing Changes
            ClearAllChanges();
        }
        [TestMethod]
        public void CantGetRoomWithLocationIDInEmptyDatabase()
        {
            // Arrange Everything We Need For Our Unit Tests
            options = new DbContextOptionsBuilder<ApplicationDBContext>()
            .UseInMemoryDatabase(databaseName: "TestRevatureHousingData")
            .Options;
            testContext = new ApplicationDBContext(options);
            dummyRoomsData = new RoomDummyData();
            dummyRooms = dummyRoomsData.RoomsList;
            testRoomRepository = new RoomRepository(testContext);
            testRoomController = new RoomsController(testRoomRepository);
            dummyConstantRoom = new Room() { RoomID = 12728 };
            //Act
            var GetRoomWithLocationIDResult = testRoomController.GetRoomWithLocationID(6).Result.Value.ToList();
            //assert
            Assert.AreEqual(GetRoomWithLocationIDResult.Count(), 0);
            ClearAllChanges();
        }
        [TestMethod]
        public void CantGetRoomWithLocationIDNotInDatabase()
        {
            // Arrange Everything We Need For Our Unit Tests
            options = new DbContextOptionsBuilder<ApplicationDBContext>()
            .UseInMemoryDatabase(databaseName: "TestRevatureHousingData")
            .Options;
            testContext = new ApplicationDBContext(options);
            dummyRoomsData = new RoomDummyData();
            dummyRooms = dummyRoomsData.RoomsList;
            testRoomRepository = new RoomRepository(testContext);
            testRoomController = new RoomsController(testRoomRepository);
            dummyConstantRoom = new Room() { RoomID = 12728 };
            //Arrange
            Populate();
            //Act
            var GetRoomWithLocationIDResult = testRoomController.GetRoomWithLocationID(7).Result.Value.ToList();
            //assert
            Assert.AreEqual(GetRoomWithLocationIDResult.Count(), 0);
            //Clearing Changes
            ClearAllChanges();
        }
        [TestMethod]
        public void CanGetInactiveRoomWithIDInDatabase()
        {
            // Arrange Everything We Need For Our Unit Tests
            options = new DbContextOptionsBuilder<ApplicationDBContext>()
            .UseInMemoryDatabase(databaseName: "TestRevatureHousingData")
            .Options;
            testContext = new ApplicationDBContext(options);
            dummyRoomsData = new RoomDummyData();
            dummyRooms = dummyRoomsData.RoomsList;
            testRoomRepository = new RoomRepository(testContext);
            testRoomController = new RoomsController(testRoomRepository);
            dummyConstantRoom = new Room() { RoomID = 12728 };
            //Arrange
            Populate();
            //Act
            var GetRoomWithLocationIDResult = testRoomController.GetInactiveRoomByID(18).Result.Value.ToList()[0];
            //assert
            Assert.IsInstanceOfType(GetRoomWithLocationIDResult, typeof(Room));
            //Clearing Changes
            ClearAllChanges();
        }
        [TestMethod]
        public void CantGetInactiveRoomWithIDInEmptyDatabase()
        {
            // Arrange Everything We Need For Our Unit Tests
            options = new DbContextOptionsBuilder<ApplicationDBContext>()
            .UseInMemoryDatabase(databaseName: "TestRevatureHousingData")
            .Options;
            testContext = new ApplicationDBContext(options);
            dummyRoomsData = new RoomDummyData();
            dummyRooms = dummyRoomsData.RoomsList;
            testRoomRepository = new RoomRepository(testContext);
            testRoomController = new RoomsController(testRoomRepository);
            dummyConstantRoom = new Room() { RoomID = 12728 };
            //Act
            var GetRoomWithLocationIDResult = testRoomController.GetInactiveRoomByID(18).Result.Value.ToList();
            //assert
            Assert.AreEqual(GetRoomWithLocationIDResult.Count(), 0);
            ClearAllChanges();
        }
        [TestMethod]
        public void CantGetInacitveRoomWithIDNotInDatabase()
        {
            // Arrange Everything We Need For Our Unit Tests
            options = new DbContextOptionsBuilder<ApplicationDBContext>()
            .UseInMemoryDatabase(databaseName: "TestRevatureHousingData")
            .Options;
            testContext = new ApplicationDBContext(options);
            dummyRoomsData = new RoomDummyData();
            dummyRooms = dummyRoomsData.RoomsList;
            testRoomRepository = new RoomRepository(testContext);
            testRoomController = new RoomsController(testRoomRepository);
            dummyConstantRoom = new Room() { RoomID = 12728 };
            //Arrange
            Populate();
            //Act
            var GetRoomWithLocationIDResult = testRoomController.GetInactiveRoomByID(19).Result.Value.ToList();
            //assert
            Assert.AreEqual(GetRoomWithLocationIDResult.Count(), 0);
            //Clearing Changes
            ClearAllChanges();
        }
    }
}
