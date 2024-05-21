
using CommunityToolkit.Maui.Alerts;
using System.Diagnostics;

namespace SimpleCompass
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
           delayTime = new Stopwatch();
        }

        public Stopwatch delayTime;
        public int timeSinceLastCheck;
        public double compassHeading; 

        private void OnToggleClicked(object sender, EventArgs e)
        {           
            ToggleCompass();
        }

        private async void ToggleCompass()
        {
            if (Compass.Default.IsSupported)
            {
                if (!Compass.Default.IsMonitoring)
                {   
                    Announce("Hold phone flat."); 
                    
                    Compass.Default.ReadingChanged += Compass_ReadingChanged;
                    Compass.Default.Start(SensorSpeed.UI, true);
                    delayTime.Start();

                    await Toast.Make("Compass Started").Show();
                }
                else
                {
                    Announce("Paused..");
                    await Toast.Make("Compass paused.").Show();
                    
                    Compass.Default.Stop();
                    Compass.Default.ReadingChanged -= Compass_ReadingChanged;

                    delayTime.Reset();
                    timeSinceLastCheck = 0;
                }
            }
        }
        
        private void Compass_ReadingChanged(object sender, CompassChangedEventArgs e)
        {                                         
            compassHeading = ConvertRange(0, 360, 0, -360, e.Reading.HeadingMagneticNorth);             
            timeSinceLastCheck = (int)delayTime.ElapsedMilliseconds;
            Update_Position(timeSinceLastCheck); 
        }

        public static double ConvertRange(double oldMin, double oldmax, double newMin, double newMax, double value)
        {   
            double scale = (double)(newMax - newMin) / (oldmax - oldMin);
            return (double)(newMin + ((value - oldMin) * scale));
        }

        public void Update_Position(int delay) // adding a delay smooths the rotation.
        {
          if (delay < 100)
            {
                return;
            }
          else
            {
                CenterComp.Rotation = compassHeading;
                delayTime.Restart();
            }
        }

        private async void Announce(string words)
        {
            notice_lbl.Text = words;
           await notice_lbl.FadeTo(1, 1000);
           await Task.Delay(5000);
           await notice_lbl.FadeTo(0, 1000);
        }

        private async void OnArrival(object sender, NavigatedToEventArgs e)
        {
            DeviceDisplay.KeepScreenOn = true;
             notice_lbl.Text = "Press red button";
            await notice_lbl.FadeTo(1, 1000);
            await Task.Delay(2000);
            await notice_lbl.FadeTo(0, 1000);
             notice_lbl.Text = "to start compass.";
            await notice_lbl.FadeTo(1, 1000);
            await Task.Delay(2000);
            await notice_lbl.FadeTo(0, 1000);
        }

        private void RunAway(object sender, NavigatedFromEventArgs e)
        {
            DeviceDisplay.KeepScreenOn = false;
        }
    }
}
