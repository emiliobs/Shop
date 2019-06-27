namespace FourWays.FormsTraditional.ViewModels
{
    using FourWays.Core.Services;
    using System;
    using System.Collections.Generic;
    using System.Text;
    public class MainViewModel : BaseViewModel
    {
        #region Atributtes

        private ICalculationService calculationService;
        private decimal amount;
        private double generosity;
        private decimal tip;

        #endregion

        #region Properties

        public decimal Amount
        {
            get => amount;
            set
            {
                if (this.amount != value)
                {
                    this.amount = value;
                    NotifyPropertyChanged();
                    this.Recalculate();
                }

            }
        }


        public double Generosity
        {
            get => generosity;
            set
            {
                if (this.generosity != value)
                {
                    this.generosity = value;
                    NotifyPropertyChanged();
                    this.Recalculate();

                }

            }
        }

        public decimal Tips
        {
            get => tip;
            set
            {
                if (this.tip != value)
                {
                    this.tip = value;
                    NotifyPropertyChanged();
                 
                }

            }
        }

        #endregion

        #region Constructs
        public MainViewModel()
        {
            this.calculationService = new CalculationService();
            this.Amount = 100;
            this.Generosity = 10;
        }
        #endregion

        #region Methods


        private void Recalculate()
        {
            this.Tips = this.calculationService.TipAmount(amount, generosity);
        }

        #endregion

    }
}
