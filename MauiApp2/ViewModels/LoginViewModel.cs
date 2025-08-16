using System.Windows.Input;
using MauiApp2.Services;
namespace MauiApp2.ViewModels;
public class LoginViewModel : BaseViewModel
{
    private readonly AuthService _auth;
    public string Username { get; set; } = "";
    public string Password { get; set; } = "";
    public ICommand LoginCommand { get; }
    public LoginViewModel(AuthService auth){ _auth=auth; LoginCommand = new Command(async () => await LoginAsync()); }
    private async Task LoginAsync()
    {
        if (string.IsNullOrWhiteSpace(Username))
        { await Application.Current.MainPage.DisplayAlert("تنبيه", "من فضلك أدخل اسم المستخدم", "حسناً"); return; }
        if (string.IsNullOrWhiteSpace(Password))
        { await Application.Current.MainPage.DisplayAlert("تنبيه", "من فضلك أدخل كلمة المرور", "حسناً"); return; }
        IsBusy = true;
        var user = await _auth.LoginAsync(Username, Password);
        IsBusy = false;
        if (user is null)
        { await Application.Current.MainPage.DisplayAlert("خطأ", "بيانات الدخول غير صحيحة", "حسناً"); return; }
        await Shell.Current.GoToAsync("//DashboardPage");
    }
}
