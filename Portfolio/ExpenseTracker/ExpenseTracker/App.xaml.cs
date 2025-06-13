﻿using System.Windows;

namespace ExpenseTracker;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        using var db = new Data.ExpensesDbContext();
        db.Database.EnsureCreated();
    }
}
