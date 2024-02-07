﻿#if WPF
using FuelEconomyWPF;
#endif
#if WINDOWS_UWP
using FuelEconomyUWP;
#endif
using System.Composition;

namespace Wrox.ProCSharp.Composition
{
    [Export(typeof(ICalculatorExtension))]
    [CalculatorExtensionMetadata(
      Title = "Fuel Economy",
      Description = "Calculate fuel economy",
      ImageUri = "Images/Fuel.png")]
    public class FuelCalculatorExtension : ICalculatorExtension
    {
        private object _control;
        public object UI => _control ?? (_control = new FuelEconomyUC());

    }
}
