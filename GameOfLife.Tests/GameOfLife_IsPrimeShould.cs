namespace GameOfLife.UnitTests {
    public class GameOfLife_IsPrimeShould
    {
        [Fact]
        public void IsPrime_InputIs1_ReturnFalse()
        {
            World world = new World();
            bool result = world.IsPrime(1);

            Assert.False(result, "1 should not be prime");
        }
    }
}