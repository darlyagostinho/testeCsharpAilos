using System;
using System.Globalization;
using static System.Net.Mime.MediaTypeNames;

namespace Questao1
{
    class ContaBancaria {
        private int _numero { get; }
        public string Titular { get; set; }
        private double _saldo { get; set; }
        private float _taxa { get; set; } = 3.5f;

        public ContaBancaria(int numero, string titular, double depositoInicial = 0.0) { 
            _numero = numero;
            Titular = titular;
            _saldo = depositoInicial;
        }

        public void Deposito(double quantia)
        {
            if (quantia > 0.0)
            {
               _saldo += quantia;
            }
        }

        public void Saque(double quantia)
        {
            if (quantia > 0.0)
            {
                _saldo -= _taxa;
                _saldo -= quantia;
            }
        }

        public override string ToString()
        {
            return $"Conta {_numero}, Titular: {Titular}, Saldo: $ {_saldo.ToString("F2", CultureInfo.InvariantCulture)}";
        }
    }
}
