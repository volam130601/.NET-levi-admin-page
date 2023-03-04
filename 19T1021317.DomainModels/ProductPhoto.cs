using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _19T1021317.DomainModels
{
    /// <summary>
    /// Photo of product
    /// </summary>
    public class ProductPhoto
    {
        ///<summary>
        ///
        ///</summary>
        public long PhotoID { get; set; }
        ///<summary>
        ///
        ///</summary>
        public int ProductID { get; set; }
        ///<summary>
        ///
        ///</summary>
        public string Photo { get; set; }
        ///<summary>
        ///
        ///</summary>
        public string Description { get; set; }
        ///<summary>
        ///
        ///</summary>
        public int DisplayOrder { get; set; }
        ///<summary>
        ///
        ///</summary>
        public bool IsHidden { get; set; }
    }

}
