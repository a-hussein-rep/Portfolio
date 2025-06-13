using System.Windows;

using ExpenseTracker.ViewModels;

namespace ExpenseTracker.Views;

public partial class MainView : Window
{
    public MainView()
    {
        InitializeComponent();

        DataContext = new MainViewModel();
    }
}