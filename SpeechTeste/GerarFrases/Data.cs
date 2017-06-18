using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frases {
    class Data {
        public static string PorExtenso() {
            string strData = string.Empty;
            string dia;
            int ano = DateTime.Now.Year;
            if (DateTime.Now.Day == 1)
                dia = "primeiro";
            else
                dia = Convert.ToString(DateTime.Now.Day);

            strData = string.Format("Hoje é {0}, {1} de {2} de {3}", DiaDaSemana(), dia, Mes(), ano);
            return strData;
        }


        static string Mes() {
            int Mes = DateTime.Now.Month;
            string NomeMes = "";
            switch (Mes) {
                case 1:
                    NomeMes = "Janeiro";
                    break;
                case 2:
                    NomeMes = "Fevereiro";
                    break;
                case 3:
                    NomeMes = "Março";
                    break;
                case 4:
                    NomeMes = "Abril";
                    break;
                case 5:
                    NomeMes = "Maio";
                    break;
                case 6:
                    NomeMes = "Junho";
                    break;
                case 7:
                    NomeMes = "Julho";
                    break;
                case 8:
                    NomeMes = "Agosto";
                    break;
                case 9:
                    NomeMes = "Setembro";
                    break;
                case 10:
                    NomeMes = "Outubro";
                    break;
                case 11:
                    NomeMes = "Novembro";
                    break;
                case 12:
                    NomeMes = "Dezembro";
                    break;
            }
            return NomeMes;
        }
        static string DiaDaSemana() {
            int DiaSemana = Convert.ToInt16(DateTime.Now.DayOfWeek);
            string NomeDiaSemana = "";
            switch (DiaSemana) {
                case 0:
                    NomeDiaSemana = "Domingo";
                    break;
                case 1:
                    NomeDiaSemana = "Segunda-Feira";
                    break;
                case 2:
                    NomeDiaSemana = "Terça-Feira";
                    break;
                case 3:
                    NomeDiaSemana = "Quarta-Feira";
                    break;
                case 4:
                    NomeDiaSemana = "Quinta-Feira";
                    break;
                case 5:
                    NomeDiaSemana = "Sexta-Feira";
                    break;
                case 6:
                    NomeDiaSemana = "Sábado";
                    break;
            }
            return NomeDiaSemana;
        }
    }
}
