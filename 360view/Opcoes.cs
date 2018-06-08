using System;

namespace _360view
{
    public class Opcoes
    {

        public String texto { get; set; }
        public String valor { get; set; }
        public Opcoes(String texto, String valor)
        {
            this.texto = texto;
            this.valor = valor;
        }
    }
}