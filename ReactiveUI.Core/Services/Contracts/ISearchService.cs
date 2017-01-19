using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactiveUI.Core.Services.Contracts
{
    public interface ISearchService
    {
        Task<List<string>> Search(string query);
    }
}
