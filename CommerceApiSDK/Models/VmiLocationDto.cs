using System;
using Newtonsoft.Json;

namespace CommerceApiSDK.Models
{
    public class VmiLocationModel : BaseModel
    {
        public Guid Id { get; set; }

        public Guid? CustomerId { get; set; }

        public Guid? BillToId { get; set; }

        public Guid? ShipToId { get; set; }

        public string Name { get; set; }

        public bool UseBins { get; set; }

        public bool IsPrimaryLocation { get; set; }

        public string Note { get; set; }

        public Address Customer { get; set; }
    }

    public class VmiBinModel : BaseModel
    {
        public Guid Id { get; set; }

        public Guid VmiLocationId { get; set; }

        public string BinNumber { get; set; }

        public Guid? ProductId { get; set; }

        public decimal? MinimumQty { get; set; }

        public decimal? MaximumQty { get; set; }

        public DateTime? LastCountDate { get; set; }

        public decimal? LastCountQty { get; set; }

        public string LastCountUserName { get; set; }

        public DateTime? PreviousCountDate { get; set; }

        public decimal? PreviousCountQty { get; set; }

        public string PreviousCountUserName { get; set; }

        public DateTime? LastOrderDate { get; set; }

        public Product Product { get; set; }
    }

    public class VmiCountModel : BaseModel
    {
        public Guid Id { get; set; }

        public Guid VmiBinId { get; set; }

        public Guid? ProductId { get; set; }

        public decimal? Count { get; set; }

        public string CreatedBy { get; set; }

        [JsonIgnore]
        public DateTime? CreatedOn { get; set; }
    }

    public class VmiNoteModel : BaseModel
    {
        public Guid Id { get; set; }

        public Guid? VmiBinId { get; set; }

        public string Note { get; set; }

        public DateTime? CreatedOn { get; set; }

        public Guid? VmiBinProductId { get; set; }

        public bool? IncludeOnOrder { get; set; }
    }
}
