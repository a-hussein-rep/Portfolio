using Microsoft.EntityFrameworkCore;

using ExpenseTracker.Models;

namespace ExpenseTracker.Data;

public class ExpensesDbContext : DbContext
{
    public DbSet<ExpenseModel> Expenses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite("Data Source=expenses.db");
}
