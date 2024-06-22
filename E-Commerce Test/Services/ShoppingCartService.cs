using E_Commerce_Test.Interfaces;
using E_Commerce_Test.Models.Models;
using E_Commerce_Test.Models.Repositories;
using E_Commerce_Test.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_Test.Services
{
    public class ShoppingCartService: IShoppingCartService
    {
        private readonly IBaseRepository<ShoppingCart> _shoppingCart;
        private readonly IBaseRepository<Product> _product;
        private readonly UserManager<AppUser> _userManager;
        
        public ShoppingCartService(UserManager<AppUser> userManager, IBaseRepository<ShoppingCart> shoppingCart, IBaseRepository<Product> product)
        {
            _shoppingCart = shoppingCart;
            _product = product;
            _userManager = userManager;
        }


        /*public async Task AddShoppingCartItemAsync()
        {
            var cartItem = new ShoppingCart
            {
                ProductId = shoppingCartItem.ProductId,
                Quantity = shoppingCartItem.Quantity,
                UserId = shoppingCartItem.AppUserId,

            };
            await _shoppingCart.AddAsync(cartItem);
        }*/

        public async Task AddShoppingCartItemAsync(string productId, string userId)
        {
            var product = await _product.GetByIdAsync(productId);
            if (product == null || product.UnitsInStock <= 0)
            {
                return;
            }

            var cartItem = await _shoppingCart.FirstOrDefaultASync(x => x.ProductId == productId && x.UserId == userId);
            if (cartItem == null)
            {
                if (product.UnitsInStock < 1)
                {
                    return;
                }

                cartItem = new ShoppingCart
                {
                    ProductId = productId,
                    UserId = userId,
                    Quantity = 1,
                };
                await _shoppingCart.AddAsync(cartItem);
                product.UnitsInStock--;
            }
            else
            {
                
                cartItem.Quantity++;
                product.UnitsInStock--;
                await _shoppingCart.UpdateAsync(cartItem);
            }

            await _product.UpdateAsync(product);

        }

        public async Task ClearShoppingCartAsync(string userId)
        {
            var cartItems= await _shoppingCart
                .WhereAsync(x=>x.UserId == userId);
                
            await _shoppingCart.DeleteRangeAsync(cartItems);
            
        }

        public async Task<int> GetCartCount(string userId)
        {
            var count = _shoppingCart
                .Where(x=>x.UserId == userId)
                .Count();
            return count;
        }

        public async Task<ShoppingCartViewModel> GetShoppingCartItemAsync(string Id)
        {
            var cartitem = await _shoppingCart.Include(p=>p.Product).FirstOrDefaultAsync(c=>c.Id == Id);
            if(cartitem == null) 
            {
                return null;
            }

            return new ShoppingCartViewModel
            {
                Id = cartitem.Id,
                ProductId = cartitem.ProductId,
                ProductName = cartitem.Product.Name,
                Quantity = cartitem.Quantity,
                ProductImage = cartitem.Product.ImageUrl,
                AppUserId=cartitem.UserId,
                Price=cartitem.Product.Price
            };
        }

        public async Task<List<ShoppingCartViewModel>> GetShoppingCartItemsAsync(string userId)
        {
            var items = await _shoppingCart
                .Include(c=>c.Product)
                .Where(x=>x.UserId==userId)
                .Select
            (x=> new ShoppingCartViewModel
            {
                Id = x.Id,
                ProductId = x.ProductId,
                ProductName = x.Product.Name,
                Quantity = x.Quantity,
                ProductImage=x.Product.ImageUrl,
                Price = x.Product.Price,
                AppUserId = x.UserId
            }).ToListAsync();
            return items;
        }

        public async Task RemoveFromCartAsync(string Id)
        {
            
            var cartItem =await _shoppingCart.GetByIdAsync(Id);
            if(cartItem != null)
            {
                var product = await _product.GetByIdAsync(cartItem.ProductId);
                if(product != null)
                {
                    product.UnitsInStock += cartItem.Quantity;
                    await _product.UpdateAsync(product);
                }
                await _shoppingCart.DeleteAsync(cartItem);
            }
        }

        public async Task UpdateShoppingCartItemAsync(ShoppingCartViewModel shoppingCartItem)
        {

            var cartItem = await _shoppingCart.GetByIdAsync(shoppingCartItem.Id);
            if (cartItem != null)
            {
                var product = await _product.GetByIdAsync(cartItem.ProductId);
                if (product != null)
                {
                    int stockChange = shoppingCartItem.Quantity - cartItem.Quantity;
                    if (stockChange > 0 && product.UnitsInStock < stockChange)
                    {
                        
                        // Handle the case where increasing the quantity would exceed the stock
                        return;
                    }

                    cartItem.Quantity = shoppingCartItem.Quantity < 1 ? 1 : shoppingCartItem.Quantity;
                    product.UnitsInStock -= stockChange;
                    await _product.UpdateAsync(product);
                    await _shoppingCart.UpdateAsync(cartItem);
                }
            }
        }
    }
}
