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
    
    public partial class Inventario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Inventario()
        {
            this.Carritoes = new HashSet<Carrito>();
            this.DetallesOrdens = new HashSet<DetallesOrden>();
        }
    
        public int IDInventario { get; set; }
        public int IDProducto { get; set; }
        public Nullable<decimal> TallaUS { get; set; }
        public Nullable<int> Cantidad { get; set; }
        public Nullable<decimal> Precio { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Carrito> Carritoes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetallesOrden> DetallesOrdens { get; set; }
        public virtual Producto Productoes { get; set; }
    }
}
