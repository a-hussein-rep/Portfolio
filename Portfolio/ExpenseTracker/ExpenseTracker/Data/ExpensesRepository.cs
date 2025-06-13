using ExpenseTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Data;

public class ExpenseRepository
{
    public async Task<List<ExpenseModel>> GetAllExpensesAsync()
    {
        using var db = new ExpensesDbContext();
        return await db.Expenses.ToListAsync();
    }

    public async Task AddExpenseAsync(ExpenseModel expense)
    {
        using var db = new ExpensesDbContext();
        db.Expenses.Add(expense);
        await db.SaveChangesAsync();
    }

    public async Task DeleteExpenseAsync(ExpenseModel expense)
    {
        using var db = new ExpensesDbContext();
        db.Expenses.Remove(expense);
        await db.SaveChangesAsync();
    }
}
