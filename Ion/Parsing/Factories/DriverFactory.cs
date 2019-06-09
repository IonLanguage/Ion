using Ion.Syntax;

namespace Ion.Parsing
{
    public static class DriverFactory
    {
        public static Driver CreateFromInput(string input)
        {
            // Create the token stream.
            TokenStream stream = TokenStreamFactory.CreateFromInput(input);

            // Create the new driver instance.
            Driver driver = new Driver(stream);

            // Return the created driver.
            return driver;
        }
    }
}
