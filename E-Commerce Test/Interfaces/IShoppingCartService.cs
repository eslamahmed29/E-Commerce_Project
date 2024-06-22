using E_Commerce_Test.ViewModels;

namespace E_Commerce_Test.Interfaces
{
    public interface IShoppingCartService
    {
        Task<List<ShoppingCartViewModel>> GetShoppingCartItemsAsync(string userId);
        Task<ShoppingCartViewModel> GetShoppingCartItemAsync(string Id);
        Task AddShoppingCartItemAsync(string productId, string userId);
        Task UpdateShoppingCartItemAsync(ShoppingCartViewModel shoppingCartItem);
       Task RemoveFromCartAsync(string Id);
        Task<int> GetCartCount(string userId);
        Task ClearShoppingCartAsync(string userId);
    }
}
