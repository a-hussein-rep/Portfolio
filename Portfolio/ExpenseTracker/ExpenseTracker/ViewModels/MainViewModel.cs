using System.Collections.ObjectModel;
using System.Windows.Input;

using ExpenseTracker.Helpers;
using ExpenseTracker.Models;
using ExpenseTracker.Views;

namespace ExpenseTracker.ViewModels;

public class MainViewModel : PropertyChangedBase
{
    public ObservableCollection<ExpenseModel> Expenses { get; }

    public ICommand AddExpenseCommand { get; }
    
    public MainViewModel()
    {
        Expenses = new();
        
        AddExpenseCommand = new RelayCommand(OpenAddExpenseWindow);
    }

    private void OpenAddExpenseWindow(object? obj)
    {
        var addExpenseView = new AddExpenseView();

        if (addExpenseView.DataContext is AddExpenseViewModel addExpenseViewModel)
        {
            addExpenseViewModel.ExpenseAdded += expense => Expenses.Add(expense);
        }

        addExpenseView.ShowDialog();
    }
}