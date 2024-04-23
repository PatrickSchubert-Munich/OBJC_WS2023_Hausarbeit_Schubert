namespace Werkzeugverleih.Classes
{
    /// <summary>
    /// Data class for a new tool
    /// </summary>
    public sealed class Tool
    {
        public static int countId;
        public int CategoryId { get; set; }
        public int ToolId { get; set; }
        public string Manufactorer { get; set; }
        public string Description { get; set; }
        public string PowerSupply { get; set; }
        public Tool() { }

        /// <summary>
        /// Constructor for a new tool
        /// </summary>
        /// <param name="categoryId">Id for tool category</param>
        /// <param name="manufactorer">tool manufactorer</param>
        /// <param name="description">specific name of the tool</param>
        /// <param name="powerSupply">power supply for tool</param>
        public Tool(int categoryId, string manufactorer, string description, int powerSupply)
        {
            ToolId = ++countId;
            CategoryId = categoryId;
            Manufactorer = manufactorer;
            Description = description;

            if (Enum.IsDefined(typeof(PowerSupplies), powerSupply))
            {
                PowerSupply = ((PowerSupplies)powerSupply).ToString();
            }
            else
            {
                // Wenn ungültiger Wert, dann setze defaultmäßig auf Stromlos
                PowerSupply = PowerSupplies.DeEnergized.ToString();
            }
        }
    }

    enum PowerSupplies
    {
        BatteryPack,
        Cable,
        DeEnergized
    }
}
