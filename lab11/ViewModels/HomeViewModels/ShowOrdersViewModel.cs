using lab_11.Models;
namespace lab_11.ViewModels.HomeViewModels
{
    public enum ShowStyles
    {
        List,
        Table,
        Row
    }
    public record class ShowOrdersViewModel(IEnumerable<Order> Orders, ShowStyles ShowStyle);
}

