using DataProvider.Model;
using System.Collections.Generic;

namespace DataProvider.ServiceAbstraction
{
    public interface IXmlService
    {
        List<Horse> GetHorsesFromXmlService();
    }
}
