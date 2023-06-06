using Api_Properties.Controllers;
using Api_Properties.Interfaces;
using Api_Properties.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace TestApiProperty
{
    public class Tests
    {        
        private PropertyController _propertyController;
        private Mock<IDatabaseService> _databaseServiceMock;


        [SetUp]
        public void Setup()
        {
            _databaseServiceMock = new Mock<IDatabaseService>();
            _propertyController = new PropertyController(_databaseServiceMock.Object);
        }

        [Test]
        public async Task CreatePropertyTest()
        {            
            var property = new Property
            {
                PkProperty = "PROP7",
                PropertyName = "House42",
                PropertyAddress = "7TH Avenue, 74th Floor",
                PropertyPrice = 350000000,
                PropertyCodeInternal = "COD7",
                PropertyYear = 1998,
                FkOwner = "OW1"
            };           

            var result = await _propertyController.CreateProperty(property) as ObjectResult;
                        
            Assert.AreEqual(StatusCodes.Status201Created, result.StatusCode);

            _databaseServiceMock.Verify(db => db.InsertProperty(property), Times.Once);
        }

        [Test]
        public async Task InsertImagePropertyTest()
        {
            var propertyImage = new PropertyImage
            {
                
                IdPropertyImage = "PROPIM4",
                IdProperty = "PROP2",
                File = "/9j/4AAQSkZJRgABAQAAAQABAAD/",
                Enabled = false
            };            

            var result = await _propertyController.CreateImageProperty(propertyImage) as ObjectResult;
            
            Assert.AreEqual(StatusCodes.Status201Created, result.StatusCode);

            _databaseServiceMock.Verify(db => db.InsertImageProperty(propertyImage), Times.Once);
        }        

        [Test]
        public async Task PricePropertyTest()
        {            
            var property = new UpdatePrice
            {
                idProperty = "PROP3",
                price = 450000000
            };

            _databaseServiceMock.Setup(d => d.ExistsProperty(property.idProperty)).ReturnsAsync(true);
            
            var result = await _propertyController.PriceProperty(property) as ObjectResult;            
            
            Assert.AreEqual(StatusCodes.Status201Created, result.StatusCode);    
            
            _databaseServiceMock.Verify(db => db.UpdatePriceProperty(property), Times.Once);
        }        

        [Test]
        public async Task UpdatePropertyAllTest()
        {            
            var property = new Property
            {
                PkProperty = "PROP1",
                PropertyName = "House77",
                PropertyAddress = "1TH Avenue, 44th Floor",
                PropertyPrice = 0,
                PropertyCodeInternal = "",
                PropertyYear = 2001,
                FkOwner = "OW1"
            };

            _databaseServiceMock.Setup(d => d.ExistsProperty(property.PkProperty)).ReturnsAsync(true);
                        
            var result = await _propertyController.UpdatePropertyAll(property) as ObjectResult;                        
            
            Assert.AreEqual(StatusCodes.Status201Created, result.StatusCode);

            _databaseServiceMock.Verify(db => db.UpdateAllProperty(property), Times.Once);
        }

        [Test]
        public async Task ListPropertyTest()
        {            
            string name = "House77";

            List<Property> expectedResult = new List<Property>()
            {
                new Property()
                {
                    PkProperty = "PROP1",
                    PropertyName = "House77",
                    PropertyAddress = "1TH Avenue, 44th Floor",
                    PropertyPrice = 400000000,
                    PropertyCodeInternal = "COD1",
                    PropertyYear = 2001,
                    FkOwner = "OW1"
                }
            };

            _databaseServiceMock.Setup(db => db.GetProperty(name)).ReturnsAsync(expectedResult);
            
            var result = await _propertyController.ListProperty(name) as ObjectResult;            
            
            Assert.AreEqual(StatusCodes.Status200OK, result.StatusCode);

            Assert.AreEqual(expectedResult, result.Value);
        }
    }
}