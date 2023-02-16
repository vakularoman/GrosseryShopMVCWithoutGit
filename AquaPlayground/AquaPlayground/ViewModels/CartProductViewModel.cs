namespace AquaPlayground.ViewModels
{
    public class CartProductViewModel
    {
        public List<SingleProductCartViewModel> Products { get; set; }

        public double PriceSum { get; set; }

        public int CountInOrder { get; set; }
    }
}
