﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JFJRepresentacoes.Models
{
    public class Usuario
    {
        public int ID { get; set; }

        public string nome { get; set; }

        public string Email { get; set; }
        public string Senha { get; set; }

        public string autorizacao { get; set; }
    }
}
