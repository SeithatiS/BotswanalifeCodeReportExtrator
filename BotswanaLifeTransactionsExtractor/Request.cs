namespace BotswanaLifeTransactionsExtractor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Request
    {
        public string RequestID { get; set; }

        public int RequestType { get; set; }

        public DateTime DateTime { get; set; }

        [Required]
        [StringLength(25)]
        public string MessageIDDateTimeRequest { get; set; }

        public DateTime? ResponseDateTime { get; set; }

        [StringLength(25)]
        public string MessageIDDateTimeResponse { get; set; }

        [StringLength(25)]
        public string ContractID { get; set; }

        [StringLength(50)]
        public string TransactionID { get; set; }

        public string XMLResponse { get; set; }

        public bool? IsSuccessful { get; set; }

        public string ErrorMessage { get; set; }

        public decimal? TransactionValue { get; set; }

        [StringLength(10)]
        public string GroupId { get; set; }

        [StringLength(50)]
        public string ResponseNumber { get; set; }

        [StringLength(75)]
        public string CustomerName { get; set; }

        [StringLength(25)]
        public string IdNumber { get; set; }

        [StringLength(128)]
        public string Client_WebServiceClientId { get; set; }

        [StringLength(10)]
        public string StatusCode { get; set; }

        [StringLength(25)]
        public string MobileNumber { get; set; }

        public virtual Client Client { get; set; }
    }
}
