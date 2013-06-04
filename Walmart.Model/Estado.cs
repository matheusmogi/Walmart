using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Walmart.Model
{
    [Serializable]
    public class Estado
    {
        [ScaffoldColumn(false)]
        public int Codigo { get; set; }
        [Required]
        [DisplayName("País")]
        public string Pais { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Sigla { get; set; }
        [Required]
        [DisplayName("Região")]
        public string Regiao { get; set; }
    }
}
