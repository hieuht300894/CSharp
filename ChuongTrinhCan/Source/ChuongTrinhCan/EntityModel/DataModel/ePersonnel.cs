namespace EntityModel.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ePersonnel")]
    public partial class ePersonnel
    {
        [Key]
        public int KeyID { get; set; }

        public int IDAgency { get; set; }

        [Required]
        [StringLength(20)]
        public string Code { get; set; }

        [Required]
        [StringLength(255)]
        public string FullName { get; set; }

        [StringLength(255)]
        public string Address { get; set; }

        [StringLength(255)]
        public string Email { get; set; }

        [StringLength(255)]
        public string Phone { get; set; }

        public string Description { get; set; }

        public bool IsEnable { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public int? ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public virtual eAccount eAccount { get; set; }
    }
}
