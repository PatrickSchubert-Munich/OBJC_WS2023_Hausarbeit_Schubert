namespace Werkzeugverleih.Classes
{
    /// <summary>
    /// Data class for a new tool category
    /// </summary>
    public sealed class ToolCategory
    {
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public decimal PricePerDay { get; set; }
        public decimal PricePerWeek { get; }

        public ToolCategory() { }

        /// <summary>
        /// Constructor of new tool category
        /// </summary>
        /// <param name="categoryId">tool category Id</param>
        /// <param name="name">namw of the tool category</param>
        /// <param name="description">description of tool category</param>
        /// <param name="pricePerDay">price for lending tool one day</param>
        public ToolCategory(int categoryId, string name, string description, decimal pricePerDay)
        {
            CategoryId = categoryId;
            Name = name;
            Description = description;
            PricePerDay = pricePerDay;
            // special price for one week, because we calculate only 6 days
            PricePerWeek = pricePerDay * 6;
        }
    }
}
