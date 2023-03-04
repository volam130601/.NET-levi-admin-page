using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _19T1021317.DomainModels
{
    /// <summary>
    /// Product
    /// </summary>
    public class Product
    {
        ///<summary>
        ///
        ///</summary>
        public int ProductID { get; set; }
        ///<summary>
        ///
        ///</summary>
        public string ProductName { get; set; }
        ///<summary>
        ///
        ///</summary>
        public int SupplierID { get; set; }
        ///<summary>
        ///
        ///</summary>
        public int CategoryID { get; set; }
        ///<summary>
        ///
        ///</summary>
        public string Unit { get; set; }
        ///<summary>
        ///
        ///</summary>
        public decimal Price { get; set; }
        ///<summary>
        ///
        ///</summary>
        public string Photo { get; set; }
    }
}
