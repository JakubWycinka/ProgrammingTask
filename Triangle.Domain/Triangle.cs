namespace Triangle.Domain
{
    public class Triangle
    {
        /// <summary>
        /// To create a triangle three positive numbers must be passed that satisfies Triangle Inequality Theorem.
        /// (side1 + side2 > side3) && (side1 + side3 > side2) && (side2 + side3 > side1)
        /// The order of parameters is not important.
        /// </summary>
        public Triangle(int side1, int side2, int side3)
        {
            ThrowIfInvalidInputParameters(side1, side2, side3);

            Side1 = side1;
            Side2 = side2;
            Side3 = side3;
            KindDueToTheSides = GetTriangleKindDueToItsSides();
        }

        public int Side1 { get; }

        public int Side2 { get; }

        public int Side3 { get; }

        public TriangleKindDueToItsSides KindDueToTheSides { get; }

        private void ThrowIfInvalidInputParameters(int side1, int side2, int side3)
        {
            ThrowIfAnySideIsNotGreaterThanZero();
            ThrowIfSumOfTwoShortestSidesIsNotGreaterThenTheLongestSide();


            void ThrowIfAnySideIsNotGreaterThanZero()
            {
                if (side1 <= 0 || side2 <= 0 || side3 <= 0)
                {
                    throw new ArgumentException("All sides must be greater than 0.");
                }
            }

            void ThrowIfSumOfTwoShortestSidesIsNotGreaterThenTheLongestSide()
            {
                // Triangle Inequality Theorem
                // (a + b > c) && (a + c > b) && (b + c > a)
                // The below implementation uses subtraction in order to avoid overflow during addition of too big numbers
                // Negative numbers are not allowed, so there is no problem with numbers that are too small

                var satisfiesTriangleInequalityTheorem = side1 > side3 - side2 && side2 > side1 - side3 && side3 > side2 - side1;

                if (!satisfiesTriangleInequalityTheorem)
                {
                    throw new ArgumentException("For a triangle to be valid, the sum of the two shorter sides must be greater than the longest side.");
                }
            }
        }

        private TriangleKindDueToItsSides GetTriangleKindDueToItsSides()
        {
            if (Side1 == Side2 && Side2 == Side3)
            {
                return TriangleKindDueToItsSides.Equilateral;
            } 
            
            if (Side1 != Side2 && Side2 != Side3 && Side1 != Side3)
            {
                return TriangleKindDueToItsSides.Scalene;
            }

            return TriangleKindDueToItsSides.Isosceles;
        }
    }
}