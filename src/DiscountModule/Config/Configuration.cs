using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DiscountModule.Config
{
    /// <summary>
    /// 
    /// </summary>
    public class Configuration
    {
        /// <summary>
        /// The text file
        /// </summary>
        public readonly string textFile = (Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"InputData\input.txt"));

        /// <summary>
        /// The package sizes
        /// </summary>
        public readonly string[] packageSizes = new string[3] { PackageEnum.S.ToString(), PackageEnum.M.ToString(), PackageEnum.L.ToString() };
       
        /// <summary>
        /// The providers
        /// </summary>
        public readonly string[] providers = new string[2] { ProvidersEnum.LP.ToString(), ProvidersEnum.MR.ToString() };

        /// <summary>
        /// The calendar month
        /// </summary>
        public readonly string calendarMonth = "yyyy-MM";

        /// <summary>
        /// The limit
        /// </summary>
        public readonly decimal discountLimit = 10;

        /// <summary>
        /// The limit
        /// </summary>
        public readonly int purchaseSequence = 3;
    }
    /// <summary>
    /// The Providers Enum
    /// </summary>
    public enum ProvidersEnum
    {
        /// <summary>
        /// The MR= Mondial Relay, :LP= La Poste
        /// </summary>
        MR = 1, LP = 2
    }
    /// <summary>
    /// The Package Enum
    /// </summary>
    public enum PackageEnum
    {
        /// <summary>
        /// The S=> Short, M= Medium, L= Large
        /// </summary>
        S = 1, M = 2, L=3
    }
}
