using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace shoeyStore.Models.ViewModels
{
    public class ProductTableViewModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProductTableViewModel()
        {
            this.Calificaciones = new HashSet<Calificacion>();
            this.DetallesOrdens = new HashSet<DetallesOrden>();
        }

        public int IDProductoes { get; set; }
        public Nullable<int> IDVendedors { get; set; }
        public string Nombres { get; set; }
        public string Categorias { get; set; }
        public string Generos { get; set; }
        public string Marcas { get; set; }
        public string Modelos { get; set; }
        public byte[] Imagens { get; set; }

        public string ItemName
        {
            get
            {
                return $"{Marcas} {Modelos}";
            }

            set => ItemName = value;
        }
        public List<InventoryViewModel> InventoryEntries { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Calificacion> Calificaciones { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetallesOrden> DetallesOrdens { get; set; }
        public virtual Vendedor Vendedor { get; set; }

        public IEnumerable<SelectListItem> GenderList =>
            new List<SelectListItem>
            {
                new SelectListItem { Value = "M", Text = "Male" },
                new SelectListItem { Value = "F", Text = "Female" },
                new SelectListItem { Value = "U", Text = "Unisex" }
            };
        public decimal? GetMinimumPrice()
        {
            if (InventoryEntries == null || !InventoryEntries.Any(entry => entry.Precio.HasValue))
            {
                return null; // No valid entries with a non-null price, return null or a default value.
            }

            // Get the minimum price from the InventoryEntries list, ignoring null prices.
            decimal minPrice = InventoryEntries
                                .Where(entry => entry.Precio.HasValue)  // Only consider entries with a valid price
                                .Min(entry => entry.Precio.Value);
            return minPrice;
        }

        public decimal? GetMaximumPrice()
        {
            if (InventoryEntries == null || !InventoryEntries.Any(entry => entry.Precio.HasValue))
            {
                return null; // No valid entries with a non-null price, return null or a default value.
            }

            // Get the maximum price from the InventoryEntries list, ignoring null prices.
            decimal maxPrice = InventoryEntries
                                .Where(entry => entry.Precio.HasValue)  // Only consider entries with a valid price
                                .Max(entry => entry.Precio.Value);
            return maxPrice;
        }

    }
}