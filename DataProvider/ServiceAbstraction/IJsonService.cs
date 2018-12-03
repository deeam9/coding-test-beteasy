using DataProvider.Model;
using System.Collections.Generic;

namespace DataProvider.ServiceAbstraction
{
    public interface IJsonService
    {
        List<Horse> GetHorsesFromJsonService();
    }
}
