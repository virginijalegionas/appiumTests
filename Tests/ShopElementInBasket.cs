public class ShopElementInBasket : ShopElement
{
    public string Color { get; set; }
    public int Amount { get; set; }

    public override bool Equals(object obj)
    {
        ShopElementInBasket p2 = obj as ShopElementInBasket;
        if (object.ReferenceEquals(null, p2)) return false;
        return Color == p2.Color && Amount == p2.Amount && Name == p2.Name && Price == p2.Price;
    }

    public override int GetHashCode()
    {
        return Color.GetHashCode() + Amount.GetHashCode() + Name.GetHashCode() + Price.GetHashCode();
    }
}