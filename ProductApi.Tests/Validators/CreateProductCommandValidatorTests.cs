using Xunit;
using FluentAssertions;
using ProductApi.Application.Products.Commands.CreateProduct;

namespace ProductApi.Tests.Validators;

public class CreateProductCommandValidatorTests
{
    private readonly CreateProductCommandValidator _validator;

    public CreateProductCommandValidatorTests()
    {
        _validator = new CreateProductCommandValidator();
    }

    [Fact]
    public void Validate_ValidCommand_ShouldPass()
    {
        var command = new CreateProductCommand
        {
            Name = "Valid Product",
            Status = 1,
            Stock = 100,
            Description = "Valid Description",
            Price = 99.99m
        };

        var result = _validator.Validate(command);

        result.IsValid.Should().BeTrue();
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("   ")]
    public void Validate_EmptyName_ShouldFail(string name)
    {
        var command = new CreateProductCommand
        {
            Name = name,
            Status = 1,
            Stock = 100,
            Description = "Valid Description",
            Price = 99.99m
        };

        var result = _validator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle(e => e.PropertyName == nameof(CreateProductCommand.Name));
    }

    [Fact]
    public void Validate_NameTooLong_ShouldFail()
    {
        var command = new CreateProductCommand
        {
            Name = new string('A', 101),
            Status = 1,
            Stock = 100,
            Description = "Valid Description",
            Price = 99.99m
        };

        var result = _validator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle(e => e.PropertyName == nameof(CreateProductCommand.Name));
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(2)]
    [InlineData(10)]
    public void Validate_InvalidStatus_ShouldFail(int status)
    {
        var command = new CreateProductCommand
        {
            Name = "Valid Product",
            Status = status,
            Stock = 100,
            Description = "Valid Description",
            Price = 99.99m
        };

        var result = _validator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle(e => e.PropertyName == nameof(CreateProductCommand.Status));
    }

    [Fact]
    public void Validate_NegativeStock_ShouldFail()
    {
        var command = new CreateProductCommand
        {
            Name = "Valid Product",
            Status = 1,
            Stock = -1,
            Description = "Valid Description",
            Price = 99.99m
        };

        var result = _validator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle(e => e.PropertyName == nameof(CreateProductCommand.Stock));
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("   ")]
    public void Validate_EmptyDescription_ShouldFail(string description)
    {
        var command = new CreateProductCommand
        {
            Name = "Valid Product",
            Status = 1,
            Stock = 100,
            Description = description,
            Price = 99.99m
        };

        var result = _validator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle(e => e.PropertyName == nameof(CreateProductCommand.Description));
    }

    [Fact]
    public void Validate_DescriptionTooLong_ShouldFail()
    {
        var command = new CreateProductCommand
        {
            Name = "Valid Product",
            Status = 1,
            Stock = 100,
            Description = new string('A', 501),
            Price = 99.99m
        };

        var result = _validator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle(e => e.PropertyName == nameof(CreateProductCommand.Description)
            && e.ErrorMessage.Contains("500"));
    }

    [Fact]
    public void Validate_DescriptionMaxLength_ShouldPass()
    {
        var command = new CreateProductCommand
        {
            Name = "Valid Product",
            Status = 1,
            Stock = 100,
            Description = new string('A', 500),
            Price = 99.99m
        };

        var result = _validator.Validate(command);

        result.IsValid.Should().BeTrue();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-99.99)]
    public void Validate_InvalidPrice_ShouldFail(decimal price)
    {
        var command = new CreateProductCommand
        {
            Name = "Valid Product",
            Status = 1,
            Stock = 100,
            Description = "Valid Description",
            Price = price
        };

        var result = _validator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle(e => e.PropertyName == nameof(CreateProductCommand.Price));
    }
} 