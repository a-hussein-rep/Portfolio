using System.Windows;
using ExpenseTracker.ViewModels;

namespace ExpenseTracker.Views;

public partial class AddExpenseView : Window
{
    public AddExpenseView()
    {
        InitializeComponent();

        DataContext = new AddExpenseViewModel();
    }
}
