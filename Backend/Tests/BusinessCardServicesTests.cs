using Moq;
using Services.Service;
using Domain.Models;
using Repository.Repository;
using Xunit;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
using System.Text;

public class BusinessCardServicesTests
{
    private readonly Mock<IRepositorySQL<BusinessCard>> _repositoryMock;
    private readonly BusinessCardServices _service;

    public BusinessCardServicesTests()
    {
        _repositoryMock = new Mock<IRepositorySQL<BusinessCard>>();
        _service = new BusinessCardServices(_repositoryMock.Object);
    }

    [Fact]
    public async Task ImportBusinessCardsAsync_ShouldImportCSV()
    {
        var csvData = "Id,Name,Gender,DateOfBirth,Email,Phone,Address,PhotoBase64\n2,John Doe,Male,01/01/1990,john@example.com,1234567890,123 Street,xxx";
        var fileMock = new Mock<IFormFile>();
        var memoryStream = new MemoryStream();
        var writer = new StreamWriter(memoryStream);
        writer.Write(csvData);
        writer.Flush();
        memoryStream.Position = 0;

        fileMock.Setup(_ => _.OpenReadStream()).Returns(memoryStream);
        fileMock.Setup(_ => _.FileName).Returns("test.csv");
        fileMock.Setup(_ => _.Length).Returns(memoryStream.Length);

        _repositoryMock.Setup(repo => repo.IntExecCommand(It.IsAny<string>(), It.IsAny<object>()))
                       .ReturnsAsync(1);

        await _service.ImportBusinessCardsAsync(fileMock.Object);

        _repositoryMock.Verify(repo => repo.IntExecCommand("CreateBusinessCard", It.IsAny<object>()), Times.Once);
    }

    [Fact]
    public async Task ImportBusinessCardsAsync_ShouldImportXML()
    {
        var xmlData = @"<ArrayOfBusinessCard>
                      <BusinessCard>
                        <Name>John Doe</Name>
                        <Gender>Male</Gender>
                        <DateOfBirth>1990-01-01T00:00:00</DateOfBirth>
                        <Email>john@example.com</Email>
                        <Phone>1234567890</Phone>
                        <Address>123 Street</Address>
                      </BusinessCard>
                    </ArrayOfBusinessCard>";
        var fileMock = new Mock<IFormFile>();
        var memoryStream = new MemoryStream();
        var writer = new StreamWriter(memoryStream);
        writer.Write(xmlData);
        writer.Flush();
        memoryStream.Position = 0;

        fileMock.Setup(_ => _.OpenReadStream()).Returns(memoryStream);
        fileMock.Setup(_ => _.FileName).Returns("test.xml");
        fileMock.Setup(_ => _.Length).Returns(memoryStream.Length);

        _repositoryMock.Setup(repo => repo.IntExecCommand(It.IsAny<string>(), It.IsAny<object>()))
                       .ReturnsAsync(1);

        await _service.ImportBusinessCardsAsync(fileMock.Object);

        _repositoryMock.Verify(repo => repo.IntExecCommand("CreateBusinessCard", It.IsAny<object>()), Times.Once);
    }
    [Fact]
    public async Task ExportBusinessCardsAsync_ShouldExportCSV()
    {
        var businessCards = new List<BusinessCard>
    {
        new BusinessCard { Name = "John Doe", Gender = "Male", DateOfBirth = new DateTime(1990, 1, 1), Email = "john@example.com", Phone = "1234567890", Address = "123 Street" }
    };

        _repositoryMock.Setup(repo => repo.ListData("GetBusinessCards", null))
                       .ReturnsAsync(businessCards);

        var result = await _service.ExportBusinessCardsAsync("csv");

        Assert.NotNull(result.Item1);
        Assert.Equal("BusinessCards.csv", result.Item2);
        Assert.Contains("John Doe", Encoding.UTF8.GetString(result.Item1));
    }
    [Fact]
    public async Task ExportBusinessCardsAsync_ShouldExportXML()
    {
        var businessCards = new List<BusinessCard>
    {
        new BusinessCard { Name = "John Doe", Gender = "Male", DateOfBirth = new DateTime(1990, 1, 1), Email = "john@example.com", Phone = "1234567890", Address = "123 Street" }
    };

        _repositoryMock.Setup(repo => repo.ListData("GetBusinessCards", null))
                       .ReturnsAsync(businessCards);

        var result = await _service.ExportBusinessCardsAsync("xml");

        Assert.NotNull(result.Item1);
        Assert.Equal("BusinessCards.xml", result.Item2);
        Assert.Contains("John Doe", Encoding.UTF8.GetString(result.Item1));
    }
    [Fact]
    public async Task ExportBusinessCardsAsync_ShouldReturnNullForEmptyData()
    {
        _repositoryMock.Setup(repo => repo.ListData("GetBusinessCards", null))
                       .ReturnsAsync(new List<BusinessCard>());

        var result = await _service.ExportBusinessCardsAsync("csv");
        Assert.Null(result.Item1);
        Assert.Null(result.Item2);
    }

}
