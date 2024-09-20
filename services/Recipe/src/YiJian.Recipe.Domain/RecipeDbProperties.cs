namespace YiJian.Recipe
{
    public static class RecipeDbProperties
    {
        public static string DbTablePrefix { get; set; } = "RC_";

        public static string DbSchema { get; set; } = null;

        public const string ConnectionStringName = "Recipe";
    }
}
