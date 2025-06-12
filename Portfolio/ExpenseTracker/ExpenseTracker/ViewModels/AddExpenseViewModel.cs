using ExpenseTracker.Helpers;
using ExpenseTracker.Models;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ExpenseTracker.ViewModels;

public class AddExpenseViewModel : PropertyChangedBase
{
    public string Description { get; set; }

    public string Amount { get; set; }

    public DateTime Date { get; set; } = DateTime.Now;

    public string Category { get; set; }

    public ICommand SaveCommand { get; }

    public ICommand CancelCommand { get; }

    public ObservableCollection<string> Categories { get; private set; }


    public event Action<ExpenseModel> ExpenseAdded;

    public AddExpenseViewModel()
    {
        SaveCommand = new RelayCommand(SaveExpense, CanSave);

        CancelCommand = new RelayCommand(CancelAddExpense);

        Categories = new ObservableCollection<string>
        {
            "Food",
            "Transport",
            "Utilities",
            "Entertainment",
            "Health",
            "Other"
        };
    }

    private bool CanSave(object? obj)
    {
        return !string.IsNullOrWhiteSpace(Amount) && !string.IsNullOrWhiteSpace(Category);
    }

    private void SaveExpense(object? obj)
    {
        var newExpense = new ExpenseModel
        {
            Description = Description,
            Amount = decimal.Parse(Amount, NumberStyles.Number, new CultureInfo("de-DE")),
            Date = Date,
            Category = Category
        };

        ExpenseAdded?.Invoke(newExpense);

        (obj as Window)?.Close();
    }

    private void CancelAddExpense(object? obj)
    {
        (obj as Window)?.Close();
    }
}
