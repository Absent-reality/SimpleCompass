namespace SimpleCompass;

public partial class Loading : ContentPage
{
	public Loading()
	{
		InitializeComponent();
	}

    private async void OnArrived(object sender, NavigatedToEventArgs e)
    {
		await lbl_txt.FadeTo(1, 1000);
		await logo.FadeTo(1, 1000);
		await Stuff.FadeTo(1, 1000);			
		await Task.Delay(1000);
		await Stuff.FadeTo(0, 1000);
        await lbl_txt.FadeTo(1, 250);
        await logo.FadeTo(1, 250);
        await Shell.Current.GoToAsync("///MainPage");
    }
}