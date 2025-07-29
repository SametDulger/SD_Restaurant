using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SD_Restaurant.Core.Enums;

namespace SD_Restaurant.Application.DTOs
{
    public class CreateOrderDto
    {
        [Required]
        public int TableId { get; set; }
        
        public int? CustomerId { get; set; }
        
        [Required]
        public int EmployeeId { get; set; }
        
        public string? Notes { get; set; }
        
        [Required]
        public List<CreateOrderItemDto> OrderItems { get; set; } = new List<CreateOrderItemDto>();
    }
} 