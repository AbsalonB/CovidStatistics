using CovidCasesStatics.ViewModels;
using dotMorten.Xamarin.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CovidCasesStatics.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPageViewModel viewModel => BindingContext as MainPageViewModel;
        private List<string> countries;
        public MainPage()
        {
            InitializeComponent(); 
        }
        private void Initialize()
        {
            countries = viewModel.Regions.Select(x => x.Name).ToList();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (!IsBusy)
            { 
                await viewModel.LoadReport(); 
                Initialize();
            } 
        }
        private async void ViewCell_Appearing(object sender, EventArgs e)
        {
            var cell = sender as ViewCell;
            var view = cell.View;

            view.TranslationX = -100;
            view.Opacity = 0;

            await Task.WhenAny<bool>(
                     view.TranslateTo(0, 0, 250, Easing.SinIn),
                     view.FadeTo(1, 500, Easing.SinIn)
                );

        }

        private void AutoSuggestBox_TextChanged(object sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            // Only get results when it was a user typing, 
            // otherwise assume the value got filled in by TextMemberPath 
            // or the handler for SuggestionChosen. AutoSuggestBox
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                //Set the ItemsSource to be your filtered dataset 
                AutoSuggestBox box = (AutoSuggestBox)sender;
                // Filter the list based on text input
                if(string.IsNullOrEmpty(box.Text))
                {
                    Task.Run(async () => {
                        if (!IsBusy)
                        {
                            await viewModel.LoadReport();
                        }
                    });
                }
                else
                    box.ItemsSource = GetSuggestions(box.Text);
            } 
        }
        private List<string> GetSuggestions(string text)
        {
            return string.IsNullOrWhiteSpace(text) ? null : countries.Where(s => s.StartsWith(text, StringComparison.InvariantCultureIgnoreCase)).ToList();
        }


        private void AutoSuggestBox_SuggestionChosen(object sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            // Set sender.Text. You can use args.SelectedItem to build your text string. AutoSuggestBox
            if (args.SelectedItem != null)
            {
                Task.Run(async () => {
                    if (!IsBusy)
                    {
                        await viewModel.LoadReport(viewModel.Regions.FirstOrDefault(x => x.Name == args.SelectedItem.ToString()).Iso);
                    }
                });
            }
        }

    }
}
