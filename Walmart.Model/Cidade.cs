using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Walmart.Model
{
    [Serializable]
    public class Cidade
    {
        [ScaffoldColumn(false)]
        public int Codigo { get; set; }
        [Required]
        [ScaffoldColumn(false)]
        [DisplayName("Estado")]
        public int CodEstado { get; set; }
        public Estado Estado { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public bool Capital { get; set; }
    }
}
