

using System.ComponentModel;

namespace MetroFibre.RecipeApp.Services.Helpers
{
    public static class RecipeEnums
    {
        public enum Recipe 
        {
            [Description("Burger")]
            Burger,
            [Description("Pasta")]
            Pasta,
            [Description("Pizza")]
            Pizza,
            [Description("Salad")]
            Salad,
            [Description("Pie")]
            Pie,
            [Description("Sandwich")]
            Sandwich
        
        }
        public enum RecipeServings
        {
            [Description("1")]
            Burger,
            [Description("2")]
            Pasta,
            [Description("4")]
            Pizza,
            [Description("3")]
            Salad,
            [Description("1")]
            Pie,
            [Description("1")]
            Sandwich

        }

        public enum Incredients 
        {
            [Description("Cucumber")]
            Cucumber,
            [Description("Olives")]
            Olives,
            [Description("Lettuce")]
            Lettuce,
            [Description("Meat")]
            Meat,
            [Description("Tomato")]
            Tomato,
            [Description("Cheese")]
            Cheese,
            [Description("Dough")]
            Dough
        
        }

        public static string GetEnumDescription(this Enum enumValue)
        {
            var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());

            var descriptionAttributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return descriptionAttributes.Length > 0 ? descriptionAttributes[0].Description : enumValue.ToString();
        }
    }
}
