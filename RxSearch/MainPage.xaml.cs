using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using Windows.UI.Xaml.Controls;

namespace RxSearch
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();

            var api = new Api();

            var textchanges = Observable.FromEventPattern<TextChangedEventHandler, TextChangedEventArgs>(
                h => textbox.TextChanged += h,
                h => textbox.TextChanged -= h
                ).Select(x => ((TextBox)x.Sender).Text);

            textchanges
                .Throttle(TimeSpan.FromMilliseconds(300)) // result on threadpool 
                .Select(api.Search)
                .Switch()
                .ObserveOnDispatcher() // send back to dispatcher
                .Subscribe(OnSearchResult);

            api.Search("").Subscribe(OnSearchResult);
        }

        private void OnSearchResult(List<string> list)
        {
            listbox.ItemsSource = list;
        }
    }
}
