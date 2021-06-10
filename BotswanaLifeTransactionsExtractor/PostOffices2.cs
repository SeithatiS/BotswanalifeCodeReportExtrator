namespace BotswanaLifeTransactionsExtractor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PostOffices2
    {
        [Key]
        public string PostOfficeSK { get; set; }

        [StringLength(200)]
        public string PostOfficeName { get; set; }

        [StringLength(50)]
        public string IPSCode { get; set; }

        [StringLength(1)]
        public string Status { get; set; }
    }
}
