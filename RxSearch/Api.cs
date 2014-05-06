using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

namespace RxSearch
{
    public class Api
    {
        public IObservable<List<string>> Search(string filter)
        {
            var list = new List<string> { "Vera", "Chuck", "Dave", "John", "Paul", "Ringo", "George" };
            var filteredList = list.Where(x => x.ToLower().Contains(filter.ToLower()));
            return Observable.Return(filteredList.ToList());
        }
    }
}
