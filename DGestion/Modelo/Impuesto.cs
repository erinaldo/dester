//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DGestion.Modelo
{
    using System;
    using System.Collections.Generic;
    
    public partial class Impuesto
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Impuesto()
        {
            this.Articuloes = new HashSet<Articulo>();
            this.TipoIvas = new HashSet<TipoIva>();
        }
    
        public int IDIMPUESTO { get; set; }
        public string DESCRIPCION { get; set; }
        public string ABREVIATURA { get; set; }
        public string ALICUOTA { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Articulo> Articuloes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TipoIva> TipoIvas { get; set; }
    }
}
