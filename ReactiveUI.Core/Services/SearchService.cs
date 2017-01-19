using ReactiveUI.Core.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactiveUI.Core.Services
{
    public class SearchService : ISearchService
    {
        public async Task<List<string>> Search(string query)
        {
            return 
                await 
                Task.FromResult(
                    new List<string>() { "result 1", "result 2", "result 3" }
                    );
        }
    }
}
