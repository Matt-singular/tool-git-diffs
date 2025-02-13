namespace Application.GUI;

public partial class App : Microsoft.Maui.Controls.Application
{
  public App()
  {
    InitializeComponent();

    MainPage = new AppShell();
  }
}