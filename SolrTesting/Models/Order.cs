using System;
using System.ComponentModel.DataAnnotations;
using SolrNet.Attributes;

namespace SolrTesting.Models
{
    public class Order
    {
        [SolrUniqueKey]
        public int Id { get; set; }
     
        [SolrField]
        [Required]
        public DateTime? OrderDate { get; set; }

        [SolrField]
        public DateTime? DeliveryDate { get; set; }

        [SolrField]
        [Required]
        public string CustomerName { get; set; }

        [SolrField]
        [Required]
        public string OrderName { get; set; }

        [SolrField]
        public bool IsActive { get; set; }
    }
}