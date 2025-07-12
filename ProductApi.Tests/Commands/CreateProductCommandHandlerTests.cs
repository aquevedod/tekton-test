using Xunit;
using FluentAssertions;
using Moq;
using ProductApi.Application.Products.Commands.CreateProduct;
using ProductApi.Application.Interfaces;
using ProductApi.Domain.Entities;

namespace ProductApi.Tests.Commands;

public class CreateProductCommandHandlerTests
{
    [Fact]
    public async Task Handle_ValidCommand_ShouldCreateProductAndReturnId()
    {
        // Arrange
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        var mockProductRepository = new Mock<IProductRepository>();
        
        mockUnitOfWork.Setup(x => x.Products).Returns(mockProductRepository.Object);
        mockUnitOfWork.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);
        
        var handler = new CreateProductCommandHandler(mockUnitOfWork.Object);
        var command = new CreateProductCommand
        {
            Name = "Test Product",
            Status = 1,
            Stock = 100,
            Description = "Test Description",
            Price = 99.99m
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().BeGreaterThan(0);
        mockProductRepository.Verify(x => x.AddAsync(It.IsAny<Product>()), Times.Once);
        mockUnitOfWork.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ValidCommand_ShouldSetCorrectProductProperties()
    {
        // Arrange
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        var mockProductRepository = new Mock<IProductRepository>();
        
        Product capturedProduct = null;
        mockProductRepository.Setup(x => x.AddAsync(It.IsAny<Product>()))
            .Callback<Product>(p => capturedProduct = p)
            .Returns(Task.CompletedTask);
        
        mockUnitOfWork.Setup(x => x.Products).Returns(mockProductRepository.Object);
        mockUnitOfWork.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);
        
        var handler = new CreateProductCommandHandler(mockUnitOfWork.Object);
        var command = new CreateProductCommand
        {
            Name = "Test Product",
            Status = 1,
            Stock = 100,
            Description = "Test Description",
            Price = 99.99m
        };

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        capturedProduct.Should().NotBeNull();
        capturedProduct.Name.Should().Be("Test Product");
        capturedProduct.Status.Should().Be(1);
        capturedProduct.Stock.Should().Be(100);
        capturedProduct.Description.Should().Be("Test Description");
        capturedProduct.Price.Should().Be(99.99m);
        capturedProduct.ProductId.Should().Be(0); // Debería ser 0 antes de ser guardado en la BD
    }
} 