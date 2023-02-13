using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _19T1021317.DomainModels;


namespace _19T1021317.DataLayers
{
    /// <summary>
    /// Definition of country-related processing
    /// </summary>
    public interface ICountryDAL
    {
        IList<Country> List();
    }
}
