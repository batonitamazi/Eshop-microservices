namespace Shopping.Web.Pages
{
    public class ProductListModel(ICatalogService catalogService, IBasketService basketService, ILogger<ProductListModel> logger) : PageModel
    {
        public IEnumerable<string> CategoryList { get; set; } = [];

        public IEnumerable<ProductModel> ProductList { get; set; } = [];

        [BindProperty(SupportsGet = true)]
        public string SelectedCategory { get; set; } = default!;


        public async Task<IActionResult> OnGetAsync(string categoryName)
        {
            var response = await catalogService.GetProducts();

            CategoryList = response.Products.SelectMany(k => k.Category).Distinct();

            if (!string.IsNullOrEmpty(categoryName))
            {
                ProductList = response.Products.Where(k => k.Category.Contains(categoryName)).ToList();
                SelectedCategory = categoryName;
            }
            else
            {
                ProductList = response.Products;
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAddToCart(Guid productId)
        {
            logger.LogInformation("Add to cart button clicked");

            var productResponse = await catalogService.GetProduct(productId);

            var basket = await basketService.LoadUserBasket();

            basket.Items.Add(new ShoppingCartItemModel
            {
                ProductId = productId,
                ProductName = productResponse.Product.Name,
                Price = productResponse.Product.Price,
                Quantity = 1,
                Color = "BLack"
            });
            await basketService.StoreBasket(new StoreBasketRequest(basket));

            return RedirectToPage("Cart");
        }
    }
}