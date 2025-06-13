using System.Collections.ObjectModel;
using System.Windows.Input;

using ExpenseTracker.Data;
using ExpenseTracker.Helpers;
using ExpenseTracker.Models;
using ExpenseTracker.Views;

namespace ExpenseTracker.ViewModels;

public class MainViewModel : PropertyChangedBase
{
    private readonly ExpenseRepository expenseRepository = new ExpenseRepository();

    public ObservableCollection<ExpenseModel> Expenses { get; } = new();

    public ExpenseModel? SelectedExpense { get; set; }

    public ICommand OpenAddExpenseWindowCommand { get; }

    public ICommand DeleteItemCommand { get; }

    public MainViewModel()
    {
        OpenAddExpenseWindowCommand = new RelayCommand(OpenAddExpenseWindow());

        DeleteItemCommand = new RelayCommand(DeleteSelectedExpense(), CanDelete);

        _ = LoadExpensesAsync();
    }

    private Action<object?> OpenAddExpenseWindow()
    {
        return async _ =>
        {
            var addExpenseView = new AddExpenseView();

            if (addExpenseView.DataContext is AddExpenseViewModel addExpenseViewModel)
            {
                addExpenseViewModel.ExpenseAdded += async expense => await AddExpense(expense);
            }

            addExpenseView.ShowDialog();

            await LoadExpensesAsync();
        };
    }

    private async Task AddExpense(ExpenseModel expense)
    {
        await expenseRepository.AddExpenseAsync(expense);
    }

    private bool CanDelete(object? obj)
    {
        return SelectedExpense is not null && Expenses.Contains(SelectedExpense);
    }

    private Action<object?> DeleteSelectedExpense()
    {
        return async _ =>
        {
            await expenseRepository.DeleteExpenseAsync(SelectedExpense);

            SelectedExpense = null;

            await LoadExpensesAsync();
        };
    }

    private async Task LoadExpensesAsync()
    {
        Expenses.Clear();

        var expenses = await expenseRepository.GetAllExpensesAsync();
        foreach (var expense in expenses)
        {
            Expenses.Add(expense);
        }
    }
}