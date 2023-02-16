namespace AquaPlayground.Models
{
    public class Manufacturer
    {
        #region [main properties]
        public long ManufacturerId { get; set; }

        public string Name { get; set; }

        public string Adress { get; set; }
        #endregion

        #region [navigation properties]
        public virtual List<Product> ManufacturerProducts { get; set; }
        #endregion
    }
}
