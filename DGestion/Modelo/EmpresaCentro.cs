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
    
    public partial class EmpresaCentro
    {
        public int IDEMPRESACENTRO { get; set; }
        public string NROCENTRO { get; set; }
        public string DOMICILIOCENTRO { get; set; }
        public Nullable<int> IDEMPRESA { get; set; }
    
        public virtual Empresa Empresa { get; set; }
    }
}
