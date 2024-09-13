using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WeatherApp.Models.Model;
using WeatherApp.Services.Service;
using WeatherApp.ViewModel.Commands;

namespace WeatherApp.ViewModel
{
    public partial class WeatherVM : BaseVM
    {
        [ObservableProperty]
        private string? query;

        public SearchCommand SearchCommand { get; set; }
		public ObservableCollection<City> Cities { get; set; }

        private City selectedCity;

        public City SelectedCity
        {
            get { return selectedCity; }
            set 
            { 
                selectedCity = value;
                if (selectedCity != null)
                {
                    OnPropertyChanged("SelectedCity");
                    GetCurrentConditons();
                }
            }
        }


        [ObservableProperty]
        private CurrentCondition? currentCondition;

		public async void MakeQuery()
		{
			var cities = await AccuWeatherService.GetCities(Query);
			Cities.Clear();
			foreach(var city in cities)
			{
				Cities.Add(city);
			}
		}

        private async Task GetCurrentConditons()
		{
			Query = string.Empty;
			Cities.Clear();
            CurrentCondition = await AccuWeatherService.GetCurrentCondition(selectedCity.Key);
		}

        public WeatherVM()
        {
			if(DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()))
			{
                SelectedCity = new City()
                {
                    LocalizedName = "Lille"
                };
                CurrentCondition = new CurrentCondition()
                {
                    WeatherText = "Raining",
                    Temperature = new Temperature()
                    {
                        Metric = new Units()
                        {
                            Value = "21"
                        }
                    }
                };
            }

			//init a command by create a new instance and pass vm because it's needed
			SearchCommand = new SearchCommand(this);
			//init observablecollection once to keep a binding
			Cities = new ObservableCollection<City>();
			
        }

    }
}
