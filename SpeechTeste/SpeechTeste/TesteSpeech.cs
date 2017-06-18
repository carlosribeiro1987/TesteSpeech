using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Synthesis;
using Microsoft.Speech.Recognition;
using System.IO.Ports;



namespace SpeechTeste {
    class TesteSpeech {
        private static SpeechRecognitionEngine engineVoz = new SpeechRecognitionEngine();
        private static SpeechSynthesizer synthVoz = new SpeechSynthesizer();
        private static bool ola = false;


        static void Main(string[] args) {
            Console.Title = "Teste Reconhecimento de Fala com Arduino";
            Console.WriteLine("Iniciando...");
            engineVoz = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("pt-BR"));
            engineVoz.SetInputToDefaultAudioDevice();
            synthVoz.SpeakAsync(Saudacao());

            //Conversas
            string[] conversas = { "olá", "boa noite", "boa tarde", "bom dia", "tudo bem",
                                    "estou bem", "bem e você", "como você está"};
            Choices c_conversas = new Choices(conversas);
            GrammarBuilder gb_conversas = new GrammarBuilder();
            gb_conversas.Append(c_conversas);
            Grammar g_conversas = new Grammar(gb_conversas);
            g_conversas.Name = "conversas";

            //Comandos
            string[] comandosSistema = { "que horas são", "que dia é hoje", "adeus", "até mais",
                                         "acender lâmpada", "apagar lâmpada", "acender luz", "apagar luz",
                                         "abrir calculadora", "fechar janela"};
            Choices c_comandosSistema = new Choices(comandosSistema);
            GrammarBuilder gb_comandosSistema = new GrammarBuilder();
            gb_comandosSistema.Append(c_comandosSistema);
            Grammar g_comandosSistema = new Grammar(gb_comandosSistema);
            g_comandosSistema.Name = "sistema";

            //Carregar Grammar
            Console.Write("<======================================");
            engineVoz.LoadGrammar(g_conversas);
            engineVoz.LoadGrammar(g_comandosSistema);
            Console.Write("=======================================>");


            //   Console.ReadKey();

            synthVoz.SelectVoiceByHints(VoiceGender.Male);
            engineVoz.SpeechRecognized += rec;
            engineVoz.RecognizeAsync(RecognizeMode.Multiple);
            Console.WriteLine("\n\nEstou ouvindo. O que deseja?");


            Console.ReadKey();
        }

        private static void rec(object sender, SpeechRecognizedEventArgs e) {
            if (e.Result.Confidence >= 0.65F) {
                string vcFalou = e.Result.Text;
                Console.WriteLine("\n\nMinha ordem: " + vcFalou + "   |    Confiança: " + e.Result.Confidence);
                switch (e.Result.Grammar.Name) {
                    case "conversas":
                        processarConversa(vcFalou);
                        Console.WriteLine("CONVERSA...");
                        break;
                    case "sistema":
                        processarComando(vcFalou);
                        Console.WriteLine("COMANDO");
                        break;
                    default:
                        break;
                }
            }
            else {
                Falar("Desculpe-me, mestre. Não entendi, pode falar novamente?");
            }

        }

        private static void processarConversa(string conversa) {
            switch (conversa) {
                case "olá":
                    if (!ola) {
                        Falar("Olá");
                        ola = true;
                    }
                    else
                        Falar("Eu já disse olá!");
                    break;

                case "bom dia":
                    Falar("bom dia mestre.");
                    break;
                case "boa tarde":
                    Falar("boa tarde mestre.");
                    break;
                case "boa noite":
                    Falar("boa noite mestre.");
                    break;
                case "tudo bem":
                    Falar("Fico feliz em saber.");
                    break;
                case "bem":
                    Falar("Que bom!");
                    break;
                case "bem e você":
                    Falar("Se você está bem, eu estou bem.");
                    break;
                case "como você está":
                    Falar("Estou ótima! É uma honra serví-lo!");
                    break;
                default:
                    return;

            }
        }

        private static void processarComando(string comando) {
            DateTime agora;
            DateTime fechar;
            switch (comando) {
                case "que horas são":
                    Falar(HoraCompleta());
                    break;
                case "que dia é hoje":
                    Falar("Hoje é " + DataCompleta());
                    break;
                case "adeus":
                    Falar("Até mais, mestre! Estou às suas órdens");
                    Console.WriteLine(Convert.ToString(DateTime.Now));
                    agora = DateTime.Now;
                    fechar = DateTime.Now.AddSeconds(5);
                    for (; DateTime.Now <= fechar;) { }
                    Console.WriteLine(Convert.ToString(DateTime.Now));
                    // Console.ReadKey();
                    Environment.Exit(0);
                    break;
                case "até mais":
                    Falar("Até mais, mestre! Estou às suas órdens");
                    agora = DateTime.Now;
                    fechar = DateTime.Now.AddSeconds(5);
                    for (; DateTime.Now <= fechar;) { }
                    Environment.Exit(0);
                    break;
                    
                    //ARDUINO

                //case "acender lâmpada":
                //    controleLampada("L");
                //    Falar("Lâmpada acesa");
                //    break;
                //case "apagar lâmpada":
                //    controleLampada("D");
                //    Falar("Lâmpada apagada");
                //    break;
                default:
                    return;
            }
        }

        private static void Falar(string texto) {
            synthVoz.SpeakAsyncCancelAll();
            synthVoz.SpeakAsync(texto);
        }

        private static string HoraCompleta() {
            int hora = DateTime.Now.Hour;
            int minuto = DateTime.Now.Minute;
            string horaCompleta = string.Empty;
            if (hora != 1) {
                if (minuto == 1)
                    horaCompleta = string.Format("Agora são {0} horas e {1} minuto", hora, minuto);
                else
                    horaCompleta = string.Format("Agora são {0} horas e {1} minutos", hora, minuto);
            }
            else {
                if (minuto == 1)
                    horaCompleta = string.Format("Agora é {0} hora e {1} minuto", hora, minuto);
                else
                    horaCompleta = string.Format("Agora é {0} hora e {1} minutos", hora, minuto);
            }
            return horaCompleta;
        }

        public static void controleLampada(string Comando) {
            SerialPort arduino = new SerialPort("COM8", 9600, Parity.None, 8, StopBits.One);
            arduino.Open();
            arduino.WriteLine(Comando);
            arduino.Close();
        }


 
        public static string Mes() {
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
        public static string DiaDaSemana() {
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


        public static string DataCompleta() {
            string Dia = Convert.ToString(DateTime.Now.Day);
            string Ano = Convert.ToString(DateTime.Now.Year);
            return String.Format("{0}, {1} de {2} de {3}", DiaDaSemana(), Dia, Mes(), Ano);
        }

        private static string Saudacao() {
            string[] frases = {
                               "Estou às suas órdens, meu lindo, maravilhoso e modésto méstri!",
                               "Estou às suas órdens. Me ilumine com sua sapiência, meu sábio e humilde méstri.",
                               "Fico feliz em serví-lo, onisciente méstri!", "meu  maravilhôso, sábio, perfeito e modésto méstri!"
            };
            int sort;
            Random rand = new Random(DateTime.Now.Millisecond);
            sort = rand.Next(frases.Length);
            int hora = DateTime.Now.Hour;
            string bomDia;
            if (hora <= 12)
                bomDia = "Bom dia! ";
            else if (hora > 12 && hora < 18)
                bomDia = "Boa tarde! ";
            else {
                bomDia = "Boa noite! ";
            }


            return bomDia + frases[sort];
        }



    }
}


