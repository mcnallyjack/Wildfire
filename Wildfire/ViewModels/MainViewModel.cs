using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Wildfire.Models;
using Wildfire.Services;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace Wildfire.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ICommand UpdatePositionCommand { get; set; }

        IGoogleMapsApiService googleMapsApi = new GoogleMapsApiService();

        string _locLatitude;
        string _locLongitude;

        GooglePlaceAutoCompletePrediction _placeSelected;

        public GooglePlaceAutoCompletePrediction PlaceSelected
        {
            get
            {
                return _placeSelected;
            }
            set
            {
                _placeSelected = value;
                if (_placeSelected != null)
                    GetPlacesDetailCommand.Execute(_placeSelected);

            }

        }
        public ICommand GetPlacesCommand { get; set; }

        public ICommand GetPlacesDetailCommand { get; set; }


        public ObservableCollection<GooglePlaceAutoCompletePrediction> Places { get; set; }

        public ObservableCollection<GooglePlaceAutoCompletePrediction> RecentPlaces { get; set; } = new ObservableCollection<GooglePlaceAutoCompletePrediction>();

        public bool ShowRecentPlaces { get; set; }
        bool _isPickupFocused = true;

        string _pickupText;

        public string PickupText
        {
            get
            {
                return _pickupText;
            }
            set
            {
                _pickupText = value;
                if (!string.IsNullOrEmpty(_pickupText))
                {
                    _isPickupFocused = true;
                    GetPlacesCommand.Execute(_pickupText);
                }
            }
        }

        public ICommand GetLocationNameCommand { get; set; }

        public MainViewModel()
        {
            GetPlacesCommand = new Command<string>(async (param) => await GetPlacesByName(param));
            GetPlacesDetailCommand = new Command<GooglePlaceAutoCompletePrediction>(async (param) => GetPlacesDetail(param));
            GetLocationNameCommand = new Command<Position>(async (param) => await GetLocationName(param));
        }

        public async Task GetPlacesByName(string placeText)
        {
            var places = await googleMapsApi.GetPlaces(placeText);
            var placeResult = places.AutoCompletePlaces;
            if (placeResult != null && placeResult.Count > 0)
            {
                Places = new ObservableCollection<GooglePlaceAutoCompletePrediction>(placeResult);
            }
            ShowRecentPlaces = (placeResult == null || placeResult.Count == 0);
        }

        public async Task GetPlacesDetail(GooglePlaceAutoCompletePrediction placeA)
        {
            var place = await googleMapsApi.GetPlaceDetails(placeA.PlaceId);
            if (place != null)
            {
                if (_isPickupFocused)
                {
                    PickupText = place.Name;
                    _locLatitude = $"{place.Latitude}";
                    _locLongitude = $"{place.Longitude}";
                    _isPickupFocused = false;

                }
                else
                {
                    _locLatitude = $"{place.Latitude}";
                    _locLongitude = $"{place.Longitude}";

                    RecentPlaces.Add(placeA);

                    await App.Current.MainPage.Navigation.PopModalAsync(false);
                    CleanFields();
                }
            }
        }
        void CleanFields()
        {
            PickupText = string.Empty;
            ShowRecentPlaces = true;
            PlaceSelected = null;
        }

        public async Task GetLocationName(Position position)
        {
            try
            {
                var placemarks = await Geocoding.GetPlacemarksAsync(position.Latitude, position.Longitude);
                var placemark = placemarks?.FirstOrDefault();
                if(placemark != null)
                {
                    PickupText = placemark.FeatureName;
                }
                else
                {
                    PickupText = string.Empty;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }
      
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
