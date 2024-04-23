namespace Werkzeugverleih.Classes
{
    /// <summary>
    /// Data class for lendings
    /// </summary>
    public sealed class Lending
    {
        // counter for lending Ids
        public static int countLendingId;
        public int LendingId;
        public DateTime LendingBegin;
        public int LendingDuration;
        public DateTime LendingEnd;
        public int CustomerId;
        public int ToolId;
        public int CategoryId;
        public decimal LendingCosts;
        public string CustomerFullName;
        public string CustomerAdress;
        public string ToolManufactorer;
        public string ToolName;

        public Lending() { }

        /// <summary>
        /// Constructor for a new lending
        /// </summary>
        /// <param name="lendingDuration">lending duration</param>
        /// <param name="singleCosts">lending costs for one day</param>
        /// <param name="customerId">customers Id</param>
        /// <param name="toolId">Tool Id</param>
        /// <param name="categoryId">Tool category Id</param>
        /// <param name="customerFullName">customers full name</param>
        /// <param name="customerAdress">customers adress</param>
        /// <param name="toolManufactorer">tool manufactorer</param>
        /// <param name="toolName">tool name</param>
        public Lending(TimeSpan lendingDuration, decimal singleCosts, int customerId, int toolId,
                        int categoryId, string customerFullName, string customerAdress,
                        string toolManufactorer, string toolName)
        {
            LendingId = ++countLendingId;
            LendingDuration = lendingDuration.Days;
            CustomerId = customerId;
            ToolId = toolId;
            CategoryId = categoryId;
            CustomerFullName = customerFullName;
            CustomerAdress = customerAdress;
            ToolManufactorer = toolManufactorer;
            ToolName = toolName;
            LendingBegin = DateTime.Now;
            LendingCosts = lendingDuration.Days * singleCosts;
            LendingEnd = LendingBegin.Add(lendingDuration);
        }
    }
}