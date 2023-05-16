namespace STGenetics.Models
{
    public class AnimalPruchase
    {

        public int PurchaseId { get; set; }
        public int AnimalId { get; set; }
        public string UserName { get; set; }
        public int Quantity { get; set; }
        public double TotalPrice { get; set; }
        public double Freight { get; set; }
        public int Status { get; set; }

        internal string Dump()
        {
            return
                "PurchaseId:" + this.PurchaseId + 
                "AnimalId:" + this.AnimalId +
                "UserName:" + this.UserName +
                "Quantity:" + this.Quantity +
                "TotalPrice:" + this.TotalPrice +
                "Freight:" + this.Freight +
                "Status:" + this.Status;
        }
    }
}
