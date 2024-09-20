using FluentAssertions;

namespace Triangle.Domain.UnitTests
{
    public class TriangleTests
    {
        [Theory]
        [InlineData(5, 5, 5, TriangleKindDueToItsSides.Equilateral)]
        [InlineData(5, 5, 3, TriangleKindDueToItsSides.Isosceles)]
        [InlineData(5, 4, 3, TriangleKindDueToItsSides.Scalene)]
        [InlineData(int.MaxValue, int.MaxValue, int.MaxValue, TriangleKindDueToItsSides.Equilateral)]
        [InlineData(1, int.MaxValue, int.MaxValue, TriangleKindDueToItsSides.Isosceles)]
        public void GivenValidSidesLengths_WhenConstructingTriangle_ThenCreatesTriangle(
            int side1,
            int side2,
            int side3,
            TriangleKindDueToItsSides expectedTriangleKindDueToItsSides)
        {
            // Act
            var triangle = new Triangle(side1, side2, side3);

            // Assert
            triangle.Side1.Should().Be(side1);
            triangle.Side2.Should().Be(side2);
            triangle.Side3.Should().Be(side3);
            triangle.KindDueToTheSides.Should().Be(expectedTriangleKindDueToItsSides);
        }

        [Theory]
        [InlineData(-1, 2,3)]
        [InlineData(3, 1, 1)]
        [InlineData(2, 1, 1)]
        [InlineData(int.MinValue, int.MinValue, int.MinValue)]
        public void GivenInvalidSidesLengths_WhenConstructingTriangle_ThenThrowsException(int side1, int side2, int side3)
        {
            // Act
            var triangleCreation = () => new Triangle(side1, side2, side3);

            // Assert
            triangleCreation.Should().Throw<Exception>();
        }
    }
}