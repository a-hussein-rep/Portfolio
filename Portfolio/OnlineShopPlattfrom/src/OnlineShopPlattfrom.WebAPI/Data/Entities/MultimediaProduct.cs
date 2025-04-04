namespace OnlineShopPlattfrom.WebAPI.Data.Entities;

public class MultimediaProduct : Product
{
    public string Model { get; set; }

    public decimal DisplaySize { get; set; }

    public string DisplayType { get; set; }

    public decimal WeightInKG { get; set; }
}
