//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace shoeyStore.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Orden
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Orden()
        {
            this.DetallesOrdens = new HashSet<DetallesOrden>();
        }
    
        public int IDOrden { get; set; }
        public Nullable<int> IDCliente { get; set; }
        public Nullable<int> IDDireccion { get; set; }
        public Nullable<int> IDTarjeta { get; set; }
        public Nullable<System.DateTime> FechaOrden { get; set; }
        public Nullable<decimal> MontoTotal { get; set; }
        public string Estado { get; set; }
        public Nullable<int> IDVendedor { get; set; }
    
        public virtual Cliente Clientes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetallesOrden> DetallesOrdens { get; set; }
        public virtual Direccion Direccions { get; set; }
        public virtual Orden Ordens1 { get; set; }
        public virtual Orden Ordens2 { get; set; }
        public virtual Tarjeta Tarjetas { get; set; }
        public virtual Vendedor Vendedors { get; set; }
    }
}
