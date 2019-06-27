namespace FourWays.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    public class CalculationService : ICalculationService
    {
        public decimal TipAmount(decimal subTotal, double generosity)
        {
            return subTotal * (decimal)(generosity / 100);
        }
    }
}
