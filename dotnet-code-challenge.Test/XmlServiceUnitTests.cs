using DataProvider;
using DataProvider.Service;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace dotnet_code_challenge.Test
{
    public class XmlServiceUnitTests
    {
        [Fact]
        public void XmlService_Throws_Exception_With_Wrong_File_Test()
        {
            var appSettings = new AppSettings
            {
                XmlDataFile = "TestData/Caulfield_Race.xml" //wrong file name
            };

            var mockConfig = new Mock<IOptions<AppSettings>>();
            mockConfig.Setup(c => c.Value)
                .Returns(appSettings);

            var xmlDataService = new XmlService(mockConfig.Object);

            Assert.Throws<FileNotFoundException>(() => xmlDataService.GetHorsesFromXmlService());
        }

        [Fact]
        public void XmlService_Throws_Exception_With_Missing_Required_Argument_Test()
        {
            var appSettings = new AppSettings
            {
                XmlDataFile = "TestData/Caulfield_MissingArgument.xml" //missing horse's name
            };

            var mockConfig = new Mock<IOptions<AppSettings>>();
            mockConfig.Setup(c => c.Value)
                .Returns(appSettings);

            var xmlDataService = new XmlService(mockConfig.Object);

            Assert.Throws< NullReferenceException>(()=>xmlDataService.GetHorsesFromXmlService());
        }

        [Fact]
        public void XmlService_Returns_Null_With_Missing_Required_Node_Test()
        {
            var appSettings = new AppSettings
            {
                XmlDataFile = "TestData/Caulfield_MissingNode.xml" //missing horses node
            };

            var mockConfig = new Mock<IOptions<AppSettings>>();
            mockConfig.Setup(c => c.Value)
                .Returns(appSettings);

            var xmlDataService = new XmlService(mockConfig.Object);

            Assert.True(null == xmlDataService.GetHorsesFromXmlService());
        }

        [Fact]
        public void XmlService_Returns_A_List_of_Horses_With_Valid_Input_Test()
        {
            var appSettings = new AppSettings
            {
                XmlDataFile = "TestData/Caulfield_Race1.xml" //valid, should return a list of 2 horses
            };

            var mockConfig = new Mock<IOptions<AppSettings>>();
            mockConfig.Setup(c => c.Value)
                .Returns(appSettings);

            var xmlDataService = new XmlService(mockConfig.Object);

            Assert.True(xmlDataService.GetHorsesFromXmlService().Count == 2);
        }
    }
}
