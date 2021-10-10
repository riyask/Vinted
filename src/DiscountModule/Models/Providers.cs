using System;
using System.Collections.Generic;
using System.Text;

namespace DiscountModule.Models
{
    /// <summary>
    /// Model for providers
    /// </summary>
    public class Providers
    {
        /// <summary>
        /// Gets or sets the provider.
        /// </summary>
        /// <value>
        /// The provider.
        /// </value>
        public string Provider { get; set; }
        /// <summary>
        /// Gets or sets the size of the package.
        /// </summary>
        /// <value>
        /// The size of the package.
        /// </value>
        public string PackageSize { get; set; }
        /// <summary>
        /// Gets or sets the price.
        /// </summary>
        /// <value>
        /// The price.
        /// </value>
        public decimal Price { get; set; }
    }
}
