using ExpenseTracker.Helpers;

namespace ExpenseTracker.Models;

public class ExpenseModel : PropertyChangedBase
{
    private decimal _amount;
    public decimal Amount
    {
        get { return _amount; }
        set
        {
            _amount = value;
            OnPropertyChanged(nameof(Amount));
        }
    }

    private string _description;
    public string Description
    {
        get { return _description; }
        set
        {
            _description = value;
            OnPropertyChanged(nameof(Description));
        }
    }

    private DateTime _date;
    public DateTime Date
    {
        get { return _date; }
        set
        {
            _date = value;
            OnPropertyChanged(nameof(Date));
        }
    }

    private string _category;
    public string Category
    {
        get { return _category; }
        set
        {
            _category = value;
            OnPropertyChanged(nameof(Category));
        }
    }
}
