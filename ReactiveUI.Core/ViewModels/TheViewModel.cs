using ReactiveUI.Core.Services;
using ReactiveUI.Core.Services.Contracts;
using Splat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;


namespace ReactiveUI.Core.ViewModels
{
    public class TheViewModel : ReactiveObject
    {
        public TheViewModel(ISearchService searchService = null)
        {
            //SearchService = searchService ?? Locator.Current.GetService<ISearchService>();

            SearchService = new SearchService();

            var canSearch = this
                .WhenAny(x => x.SearchQuery, x => !String.IsNullOrWhiteSpace(x.Value));

            Search = ReactiveCommand.CreateFromTask<string, List<string>>(async _ =>
            {
                var result = await SearchService.Search(this.SearchQuery);
                return result;
            }, canSearch);

            Search.Subscribe(result => 
            {
                SearchResults.Clear();
                SearchResults.AddRange(result);
            }
            );

            Search.ThrownExceptions
                .Subscribe(Exception => {
                    throw Exception;
                });

            this.WhenAnyValue(x => x.SearchQuery)
                .Throttle(TimeSpan.FromSeconds(1), RxApp.MainThreadScheduler)
                .InvokeCommand(Search);
        }

        public ReactiveList<string> SearchResults {
            get;
            set;
        } = new ReactiveList<string>();

        private string _searchQuery;

        public string SearchQuery
        {
            get { return this._searchQuery; }
            set { this.RaiseAndSetIfChanged(ref this._searchQuery, value); }
        }

        public ReactiveCommand<string, List<string>> Search { get; set; }

        public ISearchService SearchService { get; set; }
    }
}
