﻿using System;
namespace testeBuscarLatitudeLongitude.Models
{
	public class Endereco
	{
		public string Tipo { get; set; }
		public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Bairro { get; set; }

        public Endereco()
		{
		}

	}
}

