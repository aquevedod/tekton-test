using Xunit;
using FluentAssertions;
using Moq;
using ProductApi.Application.Products.Commands.UpdateProduct;
using ProductApi.Application.Interfaces;
using ProductApi.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace ProductApi.Tests.Commands;

public class UpdateProductCommandHandlerTests
{
    [Fact]
    public async Task Handle_ValidCommand_ShouldUpdateProduct()
    {
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        var mockProductRepository = new Mock<IProductRepository>();
        var existingProduct = new Product
        {
            ProductId = 1,
            Name = "Old Name",
            Status = 0,
            Stock = 10,
            Description = "Old Desc",
            Price = 10m
        };
        mockProductRepository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(existingProduct);
        mockUnitOfWork.Setup(x => x.Products).Returns(mockProductRepository.Object);
        mockUnitOfWork.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);
        var handler = new UpdateProductCommandHandler(mockUnitOfWork.Object);
        var command = new UpdateProductCommand
        {
            ProductId = 1,
            Name = "New Name",
            Status = 1,
            Stock = 20,
            Description = "New Desc",
            Price = 20m
        };
        await handler.Handle(command, CancellationToken.None);
        existingProduct.Name.Should().Be("New Name");
        existingProduct.Status.Should().Be(1);
        existingProduct.Stock.Should().Be(20);
        existingProduct.Description.Should().Be("New Desc");
        existingProduct.Price.Should().Be(20m);
        mockProductRepository.Verify(x => x.UpdateAsync(existingProduct), Times.Once);
        mockUnitOfWork.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ProductNotFound_ShouldThrowException()
    {
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        var mockProductRepository = new Mock<IProductRepository>();
        mockProductRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Product)null);
        mockUnitOfWork.Setup(x => x.Products).Returns(mockProductRepository.Object);
        var handler = new UpdateProductCommandHandler(mockUnitOfWork.Object);
        var command = new UpdateProductCommand { ProductId = 99, Name = "X", Status = 1, Stock = 1, Description = "X", Price = 1 };
        await Assert.ThrowsAsync<KeyNotFoundException>(() => handler.Handle(command, CancellationToken.None));
    }
} 