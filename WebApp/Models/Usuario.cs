﻿using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class Usuario : BaseEntity
    {
        [Key]
        public int IdUsuario { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Nombre { get; set; }
        [Required]
        public string? Apellido { get; set; }
        [Required]
        public string? Telefono { get; set; }
        [Required]
        public string? Rol { get; set; }
        [Required]
        public string? Clave { get; set; }
    }
}
