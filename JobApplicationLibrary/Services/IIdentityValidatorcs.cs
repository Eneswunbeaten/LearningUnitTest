using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobApplicationLibrary.Services
{
    public interface IIdentityValidator
    {
        bool IsValid(string identityNumber);

        //bool CheckConnectionToRemoteServer();

        ICountryDataProvider CountryDataProvider { get; }

        //public ValidationMode ValidationMode { get; set; }
    }

    //public enum ValidationMode
    //{
    //    None,
    //    Quick,
    //    Detailed
    //}

    public interface ICountryData
    {
        string Country { get; }
    }

    public interface ICountryDataProvider
    {
        ICountryData CountryData { get; }
    }
}
