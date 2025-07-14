using Xunit;
using FluentAssertions;
using ProductApi.Application.Products.Commands.UpdateProduct;

namespace ProductApi.Tests.Validators;

public class UpdateProductCommandValidatorTests
{
    private readonly UpdateProductCommandValidator _validator = new();

    [Fact]
    public void Validate_ValidCommand_ShouldPass()
    {
        var command = new UpdateProductCommand
        {
            ProductId = 1,
            Name = "Producto válido",
            Status = 1,
            Stock = 10,
            Description = "Descripción válida",
            Price = 10m
        };
        var result = _validator.Validate(command);
        result.IsValid.Should().BeTrue();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Validate_InvalidProductId_ShouldFail(int id)
    {
        var command = new UpdateProductCommand
        {
            ProductId = id,
            Name = "X",
            Status = 1,
            Stock = 1,
            Description = "X",
            Price = 1
        };
        var result = _validator.Validate(command);
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == nameof(UpdateProductCommand.ProductId));
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("   ")]
    public void Validate_EmptyName_ShouldFail(string name)
    {
        var command = new UpdateProductCommand
        {
            ProductId = 1,
            Name = name,
            Status = 1,
            Stock = 1,
            Description = "X",
            Price = 1
        };
        var result = _validator.Validate(command);
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == nameof(UpdateProductCommand.Name));
    }

    [Fact]
    public void Validate_NameTooLong_ShouldFail()
    {
        var command = new UpdateProductCommand
        {
            ProductId = 1,
            Name = new string('A', 101),
            Status = 1,
            Stock = 1,
            Description = "X",
            Price = 1
        };
        var result = _validator.Validate(command);
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == nameof(UpdateProductCommand.Name));
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(2)]
    public void Validate_InvalidStatus_ShouldFail(int status)
    {
        var command = new UpdateProductCommand
        {
            ProductId = 1,
            Name = "X",
            Status = status,
            Stock = 1,
            Description = "X",
            Price = 1
        };
        var result = _validator.Validate(command);
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == nameof(UpdateProductCommand.Status));
    }

    [Fact]
    public void Validate_NegativeStock_ShouldFail()
    {
        var command = new UpdateProductCommand
        {
            ProductId = 1,
            Name = "X",
            Status = 1,
            Stock = -1,
            Description = "X",
            Price = 1
        };
        var result = _validator.Validate(command);
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == nameof(UpdateProductCommand.Stock));
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("   ")]
    public void Validate_EmptyDescription_ShouldFail(string desc)
    {
        var command = new UpdateProductCommand
        {
            ProductId = 1,
            Name = "X",
            Status = 1,
            Stock = 1,
            Description = desc,
            Price = 1
        };
        var result = _validator.Validate(command);
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == nameof(UpdateProductCommand.Description));
    }

    [Fact]
    public void Validate_DescriptionTooLong_ShouldFail()
    {
        var command = new UpdateProductCommand
        {
            ProductId = 1,
            Name = "X",
            Status = 1,
            Stock = 1,
            Description = new string('A', 301), // 301 caracteres
            Price = 1
        };
        var result = _validator.Validate(command);
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == nameof(UpdateProductCommand.Description));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-99.99)]
    public void Validate_InvalidPrice_ShouldFail(decimal price)
    {
        var command = new UpdateProductCommand
        {
            ProductId = 1,
            Name = "X",
            Status = 1,
            Stock = 1,
            Description = "X",
            Price = price
        };
        var result = _validator.Validate(command);
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == nameof(UpdateProductCommand.Price));
    }
} 