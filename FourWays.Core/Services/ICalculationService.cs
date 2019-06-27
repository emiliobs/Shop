namespace FourWays.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface ICalculationService
    {
        decimal TipAmount(decimal subTotal, double generosity);
    }
}
