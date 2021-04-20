using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Wildfire.Views;
using Xamarin.Essentials;
using Wildfire.Helper;
using Xamarin.Forms.Internals;
using Wildfire.Services;
using Wildfire.Models;
using System.Windows.Input;

namespace Wildfire.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapView : ContentPage
    {
        public static int loginCount = 0;
        public static int locationCount = 0;
        public static int notificationCount = 0;
        public static int reportedIndicator = 0;
        public static Pin recent;
        public static int fireNotCount = 0;

        public static readonly BindableProperty FocusOriginCommandProperty =
           BindableProperty.Create(nameof(FocusOriginCommand), typeof(ICommand), typeof(SearchView), null, BindingMode.TwoWay);

        public ICommand FocusOriginCommand
        {
            get { return (ICommand)GetValue(FocusOriginCommandProperty); }
            set { SetValue(FocusOriginCommandProperty, value); }
        }

        FirebaseHelper firebaseHelper = new FirebaseHelper();
        public MapView()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            await ChecksFires();
           
            overlay.IsVisible = true;
            loading.IsVisible = true;
            map.IsVisible = false;
            Report_Clicked.IsVisible = false;
            searchPopup.IsVisible = false;
            Location_Clicked.IsVisible = false;
            await Task.Delay(200);

            if (locationCount == 0)
            {
                await LoadCurrentPosition();
                await LoadFires();
                overlay.IsVisible = false;
                loading.IsVisible = false;
                map.IsVisible = true;
                Report_Clicked.IsVisible = true;
                searchPopup.IsVisible = true;
                Location_Clicked.IsVisible = true;
                locationCount++;
            }
            else if (loginCount == 0)
            {
                await LoadCurrentPosition();
                await LoadFires();
                
                overlay.IsVisible = false;
                loading.IsVisible = false;
                map.IsVisible = true;
                Report_Clicked.IsVisible = true;
                searchPopup.IsVisible = true;
                Location_Clicked.IsVisible = true;
                loginCount++;
            }
            else
            {
                await LoadFires();
                overlay.IsVisible = false;
                loading.IsVisible = false;
                map.IsVisible = true;
                Report_Clicked.IsVisible = true;
                searchPopup.IsVisible = true;
                Location_Clicked.IsVisible = true;
            }
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            if (BindingContext != null)
            {
                FocusOriginCommand = new Command(OnOriginFocus);
            }
        }

        void OnOriginFocus()
        {
            originEntry.Focus();
        }

        async Task ChecksFires()
        {
            if(reportedIndicator == 0)
            {

            }
            if(reportedIndicator == 2)
            {
                reportedIndicator = 0;
                map.Pins.Remove(recent);
                OnPropertyChanged();
            }
            if(reportedIndicator == 1)
            {
                reportedIndicator = 0;

            }
        }


        async Task LoadFires()
        {
            

            var displayFires = await firebaseHelper.GetAllFires();
            foreach(var i in displayFires)
            {
                if (i.Description.Length <= 10)
                {
                    Pin newFire = new Pin()
                    {
                        Icon = (Device.RuntimePlatform == Device.Android) ? BitmapDescriptorFactory.FromBundle("FlamePins.png") : BitmapDescriptorFactory.FromView(new Image() { Source = "FlamePins.png", WidthRequest = 20, HeightRequest = 20 }),
                        Label = i.Description.ToString(),
                        Position = new Position(Convert.ToDouble(i.Latitude), Convert.ToDouble(i.Longitude)),
                        Address = i.PlaceName.ToString(),
                        Tag = i.FireID.ToString()

                    };
                    map.PinClicked += (sender, e) =>
                    {

                        //await Task.Delay(2000);
                        //await Navigation.PushModalAsync(new ResolveFireInfoView(e.Pin.Label));




                    };
                    map.Pins.Add(newFire);
                }
                else if (i.Description.Length > 10)
                {
                    string newW = String.Concat(i.Description.Select((c, j) => j > 0 && (j % 20) == 0 ? c.ToString() + Environment.NewLine : c.ToString()));


                    Pin newFire = new Pin()
                    {
                        Icon = (Device.RuntimePlatform == Device.Android) ? BitmapDescriptorFactory.FromBundle("FlamePins.png") : BitmapDescriptorFactory.FromView(new Image() { Source = "FlamePins.png", WidthRequest = 20, HeightRequest = 20 }),
                        Label = newW.ToString(),
                        Position = new Position(Convert.ToDouble(i.Latitude), Convert.ToDouble(i.Longitude)),
                        Address = i.PlaceName.ToString(),
                        Tag = i.FireID.ToString()

                    };
                    map.PinClicked += (sender, e) =>
                    {
                       
                    };
                    map.Pins.Add(newFire);
                }    
            }  
        }

        async Task LoadCurrentPosition()
        {
            try
            {
                var phonePermissions = await Permissions.CheckStatusAsync<Permissions.Phone>();

                phonePermissions = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();

                if (phonePermissions != PermissionStatus.Granted)
                {
                    phonePermissions = await Permissions.RequestAsync<Permissions.Phone>();
                    phonePermissions  = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
                }

                if (phonePermissions != PermissionStatus.Granted)
                {
                    return;
                }

                var permissions = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();

                if (permissions != PermissionStatus.Granted)
                {
                    permissions = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
                }

                if (permissions != PermissionStatus.Granted)
                {
                    return;
                }

                var permission = await Permissions.CheckStatusAsync<Permissions.Camera>();

                if (permission != PermissionStatus.Granted)
                {
                    permission = await Permissions.RequestAsync<Permissions.Camera>();
                }

                if (permission != PermissionStatus.Granted)
                {
                    return;
                }

                var storagePermissionRead = await Permissions.CheckStatusAsync<Permissions.StorageRead>();

                if (storagePermissionRead != PermissionStatus.Granted)
                {
                    storagePermissionRead = await Permissions.RequestAsync<Permissions.StorageRead>();
                }

                if (storagePermissionRead != PermissionStatus.Granted)
                {
                    return;
                }

                var storagePermissionWrite = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();

                if (storagePermissionWrite != PermissionStatus.Granted)
                {
                    storagePermissionWrite = await Permissions.RequestAsync<Permissions.StorageWrite>();
                }

                if (storagePermissionWrite != PermissionStatus.Granted)
                {
                    return;
                }

                var location = await Geolocation.GetLocationAsync();
                Circle circle = new Circle()
                {
                    Center = new Position(location.Latitude, location.Longitude),
                    Radius = new Distance(Convert.ToDouble(SettingsView.radius) * 1000),
                    StrokeColor = Color.FromHex("#88FF0000"),
                    StrokeWidth = 4,
                    FillColor = Color.FromHex("#88FFC0CB"),
                    IsClickable = true
                };
                map.Circles.Clear();
                map.Pins.Clear();

                var x = SettingsView.radius;


                if (location != null)
                {
                    //Notification SET + Raduis SET
                    if (SettingsView.isChecked == true && SettingsView.radius != null)
                    {
                        
                        Pin newLoc = new Pin()
                        {
                            Label = "Current Location",
                            Position = new Position(location.Latitude, location.Longitude)
                        };
                        map.Pins.Add(newLoc);
                        map.Circles.Add(circle);
                        map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(location.Latitude, location.Longitude), Distance.FromMeters(2000)));
                        var allFires = await firebaseHelper.GetAllFires();
                        foreach (var i in allFires)
                        {
                            var Loc = new Location(Convert.ToDouble(i.Latitude), Convert.ToDouble(i.Longitude));

                            var comp = Location.CalculateDistance(location.Latitude, location.Longitude, Loc, DistanceUnits.Kilometers);
                            comp.ToString();
                            // Count fires within the radius
                            try
                            {
                                if (Convert.ToDouble(comp) <= Convert.ToDouble(SettingsView.radius))
                                {
                                    fireNotCount++;
                                }
                                else
                                {
                                }
                            }
                            catch (Exception ex)
                            {
                                await DisplayAlert("Faild", ex.Message, "OK");
                            }
                        }
                        // Send Notification
                        try
                        {
                            if (LoginPageView.token != null)
                            {
                                if (notificationCount == 0)
                                {
                                    if (fireNotCount == 1)
                                    {
                                        DependencyService.Get<INotification>().CreateNotification("Wildfire", "A fire is active in your area.");
                                        notificationCount++;
                                    }
                                    else
                                    {
                                        DependencyService.Get<INotification>().CreateNotification("Wildfire", fireNotCount.ToString() + " fires are active in your area.");
                                        notificationCount++;
                                    }
                                }
                                else
                                {
                                }
                            }
                            else
                            {
                                if (notificationCount == 0)
                                {
                                    if (fireNotCount == 1)
                                    {
                                        DependencyService.Get<INotification>().CreateNotification("Wildfire", "A fire is active in your area.");
                                        notificationCount++;
                                    }
                                    else if (fireNotCount == 0)
                                    {
                                    }
                                    else
                                    {
                                        DependencyService.Get<INotification>().CreateNotification("Wildfire", fireNotCount.ToString() + " fires are active in your area.");
                                        notificationCount++;
                                    }
                                }
                                else
                                {
                                }

                            }
                        }
                        catch (Exception ex)
                        {
                            ex.Message.ToString();
                        }

                    }
                    //Notification SET + Raduis NOT SET
                    else if (SettingsView.isChecked == true && SettingsView.radius == null)
                    {
                        
                        Pin newLoc = new Pin()
                        {
                            Label = "Current Location",
                            Position = new Position(location.Latitude, location.Longitude)
                        };
                        map.Pins.Add(newLoc);
                        map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(location.Latitude, location.Longitude), Distance.FromMeters(2000)));
                        var allFires = await firebaseHelper.GetAllFires();
                        foreach (var i in allFires)
                        {
                            var Loc = new Location(Convert.ToDouble(i.Latitude), Convert.ToDouble(i.Longitude));

                            var comp = Location.CalculateDistance(location.Latitude, location.Longitude, Loc, DistanceUnits.Kilometers);
                            comp.ToString();
                            try
                            {
                                if (Convert.ToDouble(comp) <= Convert.ToDouble(5))
                                {
                                    fireNotCount++;
                                }
                                else
                                {
                                }
                            }
                            catch (Exception ex)
                            {
                                ex.Message.ToString();
                            }
                        }
                        // Send Notification
                        try
                        {
                            if (LoginPageView.token != null)
                            {
                                if (notificationCount == 0)
                                {
                                    if (fireNotCount == 1)
                                    {
                                        DependencyService.Get<INotification>().CreateNotification("Wildfire", "A fire is active in your area.");
                                        notificationCount++;
                                    }
                                    else if (fireNotCount == 0)
                                    {

                                    }
                                    else
                                    {
                                        DependencyService.Get<INotification>().CreateNotification("Wildfire", fireNotCount.ToString() + " fires are active in your area.");
                                        notificationCount++;
                                    }
                                }
                                else
                                {
                                }
                            }
                            else
                            {
                                if (notificationCount == 0)
                                {
                                    if (fireNotCount == 1)
                                    {
                                        DependencyService.Get<INotification>().CreateNotification("Wildfire", "A fire is active in your area.");
                                        notificationCount++;
                                    }
                                    else
                                    {
                                        DependencyService.Get<INotification>().CreateNotification("Wildfire", fireNotCount.ToString() + " fires are active in your area.");
                                        notificationCount++;
                                    }
                                }
                                else
                                {
                                }

                            }
                        }
                        catch (Exception ex)
                        {
                            ex.Message.ToString();
                        }

                    }
                    //Notification NOT SET  + Raduis SET
                    else if (SettingsView.isChecked == false && SettingsView.radius != null)
                    {
                        
                        Pin newLoc = new Pin()
                        {
                            Label = "Current Location",
                            Position = new Position(location.Latitude, location.Longitude)
                        };
                        map.Pins.Add(newLoc);
                        map.Circles.Add(circle);
                        map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(location.Latitude, location.Longitude), Distance.FromMeters(2000)));
                        var allFires = await firebaseHelper.GetAllFires();
                        foreach (var i in allFires)
                        {
                            var Loc = new Location(Convert.ToDouble(i.Latitude), Convert.ToDouble(i.Longitude));

                            var comp = Location.CalculateDistance(location.Latitude, location.Longitude, Loc, DistanceUnits.Kilometers);
                            comp.ToString();
                            try
                            {

                                if (Convert.ToDouble(comp) <= Convert.ToDouble(SettingsView.radius))
                                {
                                    fireNotCount++;
                                }
                                else
                                {

                                }
                            }
                            catch (Exception ex)
                            {
                                ex.Message.ToString();
                            }
                        }
                        //Send Notifications
                    }

                    //Notification NOT SET + Raduis NOT SET
                    else if (SettingsView.isChecked == false && SettingsView.radius == null)
                    {
                        
                        Pin newLoc = new Pin()
                        {
                            Label = "Current Location",
                            Position = new Position(location.Latitude, location.Longitude)
                        };
                        map.Pins.Add(newLoc);
                        map.Circles.Remove(circle);
                        map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(location.Latitude, location.Longitude), Distance.FromMeters(2000)));
                        var allFires = await firebaseHelper.GetAllFires();
                        foreach (var i in allFires)
                        {
                            var Loc = new Location(Convert.ToDouble(i.Latitude), Convert.ToDouble(i.Longitude));

                            var comp = Location.CalculateDistance(location.Latitude, location.Longitude, Loc, DistanceUnits.Kilometers);
                            comp.ToString();
                            try
                            {
                                if (Convert.ToDouble(comp) <= Convert.ToDouble(5))
                                {

                                }
                                else
                                {

                                }
                            }
                            catch (Exception ex)
                            {
                                ex.Message.ToString();
                            }

                        }
                        //Send Notification
                    }
                }
            }
            catch(Exception ex)
            {
                ex.Message.ToString();

                

                var permissionLocation = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
                var current = Connectivity.NetworkAccess;
                if (permissionLocation != PermissionStatus.Granted)
                {
                    await DisplayAlert("Error", "Please enable Location services on your mobile device and restart the application", "ok");
                    throw new Exception();
                }
                if(current != NetworkAccess.Internet)
                {
                    await DisplayAlert("Error", "Please enable WiFi services on your mobile device and restart the application", "ok");
                    throw new Exception();
                }
            }
            //await DisplayAlert("Error", "Bug Found", "ok");
            //throw new Exception();
            

        }

        private void Search_Clicked(object sender, EventArgs e)
        {
            popupSearch.IsVisible = true;
        }

        void RemovePopupTapped(object sender, EventArgs e)
        {
            popupSearch.IsVisible = false;
        }

        private async void map_MapClicked(object sender, MapClickedEventArgs e)
        {
            var permissions = await Permissions.CheckStatusAsync<Permissions.Phone>();

            if (permissions != PermissionStatus.Granted)
            {
                permissions = await Permissions.RequestAsync<Permissions.Phone>();
            }

            if (permissions != PermissionStatus.Granted)
            {
                return;
            }
            var location = await Geolocation.GetLocationAsync();
            var plLat = location.Latitude;
            var plLong = location.Longitude;
            var placemark1 = await Geocoding.GetPlacemarksAsync(plLat, plLong);
            var placemarkDetails1 = placemark1?.FirstOrDefault();
            string locality1 = placemarkDetails1.AdminArea;
            string areaCode1 = placemarkDetails1.CountryCode;
            string Place1 = locality1 + ", " + areaCode1;


            Pin newFire = new Pin()
            {
                Icon = (Device.RuntimePlatform == Device.Android) ? BitmapDescriptorFactory.FromBundle("FlamePins.png") : BitmapDescriptorFactory.FromView(new Image() { Source = "FlamePins.png", WidthRequest = 20, HeightRequest = 20 }),
                Label = "New Fire",
                Position = new Position(e.Point.Latitude, e.Point.Longitude),
                Address = Place1,
                IsDraggable = true
                
                
            };
            
            map.Pins.Add(newFire);

            await Task.Delay(500);
            recent = newFire;
            var Lat = e.Point.Latitude;
            var Long = e.Point.Longitude;
            var Place = Place1;
            Lat.ToString();
            Long.ToString();
            await Navigation.PushModalAsync(new ReportFireInfoView(Lat, Long, Place) { BindingContext = this.BindingContext }, false);
        }

       

        private async void Location_Button_Clicked(object sender, EventArgs e)
        {
            var location = await Geolocation.GetLocationAsync();
            if (location != null)
            {
                Pin newLoc = new Pin()
                {
                    Label = "Current Location",
                    Position = new Position(location.Latitude, location.Longitude)
                };
                map.Pins.Add(newLoc);
                map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(location.Latitude, location.Longitude), Distance.FromMeters(2000)));
            }
        }

        private async void ReportFire_Clicked(object sender, EventArgs e)
        {
            var permissions = await Permissions.CheckStatusAsync<Permissions.Phone>();

            if(permissions != PermissionStatus.Granted)
            {
                permissions = await Permissions.RequestAsync<Permissions.Phone>();
            }

            if(permissions != PermissionStatus.Granted)
            {
                return;
            }
            var location = await Geolocation.GetLocationAsync();
            var plLat = location.Latitude;
            var plLong = location.Longitude;
            var placemark1 = await Geocoding.GetPlacemarksAsync(plLat, plLong);
            var placemarkDetails1 = placemark1?.FirstOrDefault();
            string locality1 = placemarkDetails1.AdminArea;
            string areaCode1 = placemarkDetails1.CountryCode;
            string Place1 = locality1 + ", " + areaCode1;
            if(location != null)
            {
                Pin newLoc = new Pin()
                {
                    Icon = (Device.RuntimePlatform == Device.Android) ? BitmapDescriptorFactory.FromBundle("FlamePins.png") : BitmapDescriptorFactory.FromView(new Image() { Source = "FlamePins.png", WidthRequest = 20, HeightRequest = 20 }),
                    Label = "New Fire",
                    Position = new Position(location.Latitude, location.Longitude),
                    Address = Place1
                };
               
                map.Pins.Add(newLoc);
                var Lat = location.Latitude;
                var Long = location.Longitude;
                var Indicator = 0;
                var placemarks = await Geocoding.GetPlacemarksAsync(Lat, Long);
                var placemarkDetails = placemarks?.FirstOrDefault();
                string areaCode = placemarkDetails.AdminArea;
                string localityName = placemarkDetails.Locality;
                string Place = localityName + " " + areaCode;
                Lat.ToString();
                Long.ToString();
               
                await Navigation.PushModalAsync(new ReportFireInfoView(Lat, Long, Place) { BindingContext = this.BindingContext }, false);
            }
        }

        private async void map_PinClicked(object sender, PinClickedEventArgs e)
        {

            if (LoginPageView.token == null)
            {
               
            }
            else
            {
                await Task.Delay(10);
                await Navigation.PushModalAsync(new ResolveFireInfoView(e.Pin.Label, e.Pin.Address, e.Pin.Tag.ToString()),false);
            } 
            
        }

        public async void SearchPlace_Clicked(object sender, EventArgs e)
        {
            var search = originEntry.Text;
            var searchLocation = await Geocoding.GetLocationsAsync(search);
            
            var sourceLocations = searchLocation?.FirstOrDefault();
            if(sourceLocations != null)
            {
            Location sourceCoordinates = new Location(sourceLocations.Latitude, sourceLocations.Longitude);

            Pin pin = new Pin()
            {
                Icon = (Device.RuntimePlatform == Device.Android) ? BitmapDescriptorFactory.FromBundle("SearchPins.png") : BitmapDescriptorFactory.FromView(new Image() { Source = "SearchPins.png", WidthRequest= 20, HeightRequest = 20}),
                Type = PinType.Place,
                Label = originEntry.Text,
                Position = new Position(sourceCoordinates.Latitude, sourceCoordinates.Longitude)
            };
            map.Pins.Add(pin);

            map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(sourceCoordinates.Latitude, sourceCoordinates.Longitude), Distance.FromMeters(500)));
            originEntry.Text = string.Empty;
            search = string.Empty;
            popupSearch.IsVisible = false;
            }
            else
            {
                search = string.Empty;
                await DisplayAlert("Cant Find Location", "", "Yes");
            }
        }
    }
}