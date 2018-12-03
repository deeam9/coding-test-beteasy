using DataProvider;
using DataProvider.Service;
using Microsoft.Extensions.Options;
using Moq;
using Newtonsoft.Json;
using System;
using System.IO;
using Xunit;

namespace dotnet_code_challenge.Test
{
    public class JsonServiceUnitTests
    {
        [Fact]
        public void JsonService_Throws_Exception_With_Wrong_File_Test()
        {
            var appSettings = new AppSettings
            {
                JsonDataFile = "TestData/Wolferhampton_Race.json" //wrong file name
            };

            var mockConfig = new Mock<IOptions<AppSettings>>();
            mockConfig.Setup(c => c.Value)
                .Returns(appSettings);

            var jsonDataService = new JsonService(mockConfig.Object);

            Assert.Throws<FileNotFoundException>(() => jsonDataService.GetHorsesFromJsonService());
        }

        [Fact]
        public void JsonService_Throws_Exception_With_Empty_File_Test()
        {
            var appSettings = new AppSettings
            {
                JsonDataFile = "TestData/Wolferhampton_Empty.json" //empty json file
            };

            var mockConfig = new Mock<IOptions<AppSettings>>();
            mockConfig.Setup(c => c.Value)
                .Returns(appSettings);

            var jsonDataService = new JsonService(mockConfig.Object);

            Assert.Throws<JsonReaderException>(()=>jsonDataService.GetHorsesFromJsonService());
        }

        [Fact]
        public void JsonService_Throws_Exception_On_Missing_Required_Node_Test()
        {
            var appSettings = new AppSettings
            {
                JsonDataFile = "TestData/Wolferhampton_MissingNode.json" //without RawData node
            };

            var mockConfig = new Mock<IOptions<AppSettings>>();
            mockConfig.Setup(c => c.Value)
                .Returns(appSettings);

            var jsonDataService = new JsonService(mockConfig.Object);

            Assert.Throws<NullReferenceException>(()=>jsonDataService.GetHorsesFromJsonService() );
        }

        [Fact]
        public void JsonService_Throws_Exception_On_Missing_Required_Argument_Test()
        {
            var appSettings = new AppSettings
            {
                JsonDataFile = "TestData/Wolferhampton_MissingArgument.json" //without Price argument under node Selections
            };

            var mockConfig = new Mock<IOptions<AppSettings>>();
            mockConfig.Setup(c => c.Value)
                .Returns(appSettings);

            var jsonDataService = new JsonService(mockConfig.Object);

            Assert.Throws<ArgumentNullException>(() => jsonDataService.GetHorsesFromJsonService());
        }

        [Fact]
        public void JsonService_Returns_List_Of_Horses_With_Valid_Input_Test()
        {
            var appSettings = new AppSettings
            {
                JsonDataFile = "TestData/Wolferhampton_Race1.json" //real data, should return a list of two horses
            };

            var mockConfig = new Mock<IOptions<AppSettings>>();
            mockConfig.Setup(c => c.Value)
                .Returns(appSettings);

            var jsonDataService = new JsonService(mockConfig.Object);

            Assert.True(jsonDataService.GetHorsesFromJsonService().Count == 2);
        }
    }
}
