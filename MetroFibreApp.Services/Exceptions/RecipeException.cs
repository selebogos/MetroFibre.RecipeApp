using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroFibre.RecipeApp.Services.Exceptions
{
    public sealed class RecipeException : Exception
    {
        public RecipeException() : base() { }

        public RecipeException(string message) : base(message) { }

        public RecipeException(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
