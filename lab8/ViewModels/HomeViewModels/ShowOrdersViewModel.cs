using lab8.Models;

namespace lab8.ViewModels.HomeViewModels
{
    public enum ShowStyles
    {
        List,
        Table
    }
    public record class ShowOrdersViewModel(IEnumerable<Order> Orders, ShowStyles ShowStyle);
}
