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
    
    public partial class ArticuloPrecioVenta
    {
        public long IDPRECIOVENTA { get; set; }
        public Nullable<int> IDLISTAPRECIO { get; set; }
        public Nullable<int> IDARTICULO { get; set; }
        public string PRECIO { get; set; }
        public System.DateTime ULTIMACTUALIZACION { get; set; }
    
        public virtual Articulo Articulo { get; set; }
        public virtual ListaPrecio ListaPrecio { get; set; }
    }
}