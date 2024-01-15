namespace eCommerce_App.DTOs
{
    public class ProductDto
    {
       
            public int Id { get; set; }
            public string Name { get; set; }
            public decimal Price { get; set; }
            public int ProductTypeId { get; set; }
            public int ProductBrandId { get; set; }
        
    }  
}
