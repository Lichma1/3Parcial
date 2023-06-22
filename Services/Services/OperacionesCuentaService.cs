using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class OperacionesCuentaService
    {
        private CuentaRepository repositoryCuenta;

        public OperacionesCuentaService(CuentaRepository repositoryCuenta)
        {
            this.repositoryCuenta = repositoryCuenta;
        }

        public string Transferir(int idCuentaOriginal, int idCuentaDestino, double monto)
        {

            var cuentaOr = repositoryCuenta.consultarCuenta(idCuentaOriginal);
            var cuentaDes = repositoryCuenta.consultarCuenta(idCuentaDestino);
     
            if (cuentaOr.numeroCuenta == null || cuentaOr.saldo < 0)

            {
                throw new ArgumentException("No debe ser un monto negativo.");
            }

            if (cuentaOr.Id == cuentaDes.Id)
            {
                throw new ArgumentException("No se puede transferir a la misma cuenta.");
            }

            if (cuentaOr.limiteTransferencia < monto || cuentaDes.limiteSaldo < (cuentaDes.saldo + monto))
            {
                throw new ArgumentException("El monto de transferencia es mayor al límite.");
            }

            if (cuentaOr.estadocuenta == false || cuentaDes.estadocuenta == false)
            {
                throw new ArgumentException("Cuenta inactiva o deshabilitada.");
            }

            if (cuentaOr.saldo < monto)
            {
                throw new ArgumentException("Saldo insuficiente.");
            }

            cuentaOr.saldo -= monto;
            cuentaDes.saldo += monto;

            repositoryCuenta.modificarCuenta(cuentaOr, idCuentaOriginal);
            repositoryCuenta.modificarCuenta(cuentaDes, idCuentaDestino);
            return "Transferencia realizada con éxito.";
        }

        public string Depositar(int idCuentaOriginal, double monto)
        {
            var cuentaOri = repositoryCuenta.consultarCuenta(idCuentaOriginal);

            if (cuentaOri.estadocuenta == false)
            {
                throw new ArgumentException("Cuenta inactiva o inhabilitada.");
            }

            if (cuentaOri.limiteSaldo < (cuentaOri.saldo + monto))
            {
                throw new ArgumentException("Límite de saldo superado.");
            }

            cuentaOri.saldo += monto;

            repositoryCuenta.modificarCuenta(cuentaOri, idCuentaOriginal);

            return "Depósito realizado con éxito.";
        }
        public string Extraer(double monto, int idCuentaOriginal)
        {
            var cuentaOri = repositoryCuenta.consultarCuenta(idCuentaOriginal);

            if (cuentaOri.estadocuenta == false)
            {
                throw new ArgumentException("Cuenta inactiva o inhabilitada.");
            }
            if (cuentaOri.saldo < monto)
            {
                throw new InvalidOperationException("Saldo insuficiente");
            }

            cuentaOri.saldo -= monto;
            repositoryCuenta.modificarCuenta(cuentaOri, idCuentaOriginal);

            return "Extracción exitosa.";

        }

        public string Bloquear(int idCuentaOriginal)
        {
            var cuentaOri = repositoryCuenta.consultarCuenta(idCuentaOriginal);

            if (cuentaOri == null)
            {
                throw new ArgumentException("Cuenta inválida.");
            }

            cuentaOri.estadocuenta = false;

            repositoryCuenta.modificarCuenta(cuentaOri, idCuentaOriginal);

            return "Cuenta bloqueada con éxito.";
        }

        public string Imprimir(int idCuentaOriginal)
        {
            var cuentaOri = repositoryCuenta.consultarCuenta(idCuentaOriginal);

            return "Tu saldo actual es: " + cuentaOri.saldo;
        }
    }
}
