//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Vovinam.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class TheLuc
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TheLuc()
        {
            this.LevelUps = new HashSet<LevelUp>();
        }
    
        public int Id { get; set; }
        public double Point { get; set; }
        public Nullable<int> UserId { get; set; }
    
        public virtual User User { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LevelUp> LevelUps { get; set; }
    }
}
