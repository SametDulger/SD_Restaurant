using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SD_Restaurant.Core.Enums;

namespace SD_Restaurant.Application.DTOs
{
    public class UpdateOrderDto
    {
        public int Id { get; set; }

        [Required]
        public int TableId { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        public OrderStatus Status { get; set; }

        public string? SpecialInstructions { get; set; }

        public decimal DiscountAmount { get; set; }

        public decimal TaxAmount { get; set; }

        public decimal TotalAmount { get; set; }

        public decimal FinalAmount { get; set; }

        public DateTime OrderDate { get; set; }

        public List<UpdateOrderItemDto> OrderItems { get; set; } = new List<UpdateOrderItemDto>();
    }

    public class UpdateOrderItemDto
    {
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Miktar 1'den büyük olmalıdır")]
        public int Quantity { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Birim fiyat 0'dan büyük olmalıdır")]
        public decimal UnitPrice { get; set; }

        public decimal TotalAmount { get; set; }

        public string? SpecialInstructions { get; set; }

        public OrderStatus Status { get; set; }
    }
} 