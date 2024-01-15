using bytebank.Modelos.Conta;
using bytebank_ATENDIMENTO.bytebank.Exceptions;
using Newtonsoft.Json;
using System.Xml.Serialization;

#nullable disable
namespace bytebank_ATENDIMENTO.bytebank.Atendimento
{
    internal class ByteBankAtendimento
    {

             private List<ContaCorrente> _listaDeContas = new List<ContaCorrente>()
            {
                new ContaCorrente(995, "123456-X") {Saldo=100,Titular = new Cliente{Cpf="125.132.350-48", Nome="Henrique"} },
                new ContaCorrente(995, "987654-Y") {Saldo=200, Titular = new Cliente{Cpf="264.759.450-52", Nome="Marisa"} },
                new ContaCorrente(678, "246810-W") {Saldo=60, Titular = new Cliente{Cpf="456.598.550-27", Nome="Pedro"} }
             };

                public void AtendimentoCliente()
                {
                    try
                    {
                        char opcao = '0';
                        while (opcao != '8')
                        {
                            Console.Clear();
                            Console.WriteLine("===============================");
                            Console.WriteLine("===        Atendimento        ===");
                            Console.WriteLine("===  1 - Cadastrar Conta      ===");
                            Console.WriteLine("===  2 - Listar Contas        ===");
                            Console.WriteLine("===  3 - Remover Conta        ===");
                            Console.WriteLine("===  4 - Ordenar Contas       ===");
                            Console.WriteLine("===  5 - Pesquisar Conta      ===");
                            Console.WriteLine("===  6 - Exportar Contas      ===");
                            Console.WriteLine("===  7 - Exportar Contas XML  ===");
                            Console.WriteLine("===  8 - Sair do Sistema      ===");
                            Console.WriteLine("===============================");
                            Console.WriteLine("\n\n");
                            Console.Write("Digite a opção desejada: ");
                            try
                            {
                                opcao = Console.ReadLine()[0];
                            }
                            catch (Exception excecao)
                            {
                                throw new ByteBankException(excecao.Message);
                            }

                            switch (opcao)
                            {
                                case '1':
                                    CadastrarConta();
                                    break;
                                case '2':
                                    ListarConta();
                                    break;
                                case '3':
                                    RemoverConta();
                                    break;
                                case '4':
                                    OrdenarConta();
                                    break;
                                case '5':
                                    PesquisarConta();
                                    break;
                                case '6':
                                    ExportarContas();
                                    break;
                                case '7':
                                    ExportarContasxml();
                                    break;
                                case '8':
                                    EncerrarAplicacao();
                                    break;
                                default:
                                    Console.WriteLine("Opcão não implementada.");
                                    break;
                            }
                        }
                    }
                    catch (ByteBankException excecao)
                    {
                        Console.WriteLine($"{excecao.Message}");
                    }

                }

                 //1 - Cadastrar Contas
                 private void CadastrarConta()
                {
                    Console.Clear();
                    Console.WriteLine("===============================");
                    Console.WriteLine("===   CADASTRO DE CONTAS    ===");
                    Console.WriteLine("===============================");
                    Console.WriteLine("\n");
                    Console.WriteLine("=== Informe dados da conta ===");
                    Console.Write("Numero da Agência: ");
                    int numeroAgencia = int.Parse(Console.ReadLine());
                    ContaCorrente conta = new ContaCorrente(numeroAgencia);
                    Console.WriteLine($"Numero da conta[NOVA] : {conta.Conta}");

                    Console.Write("Informe o saldo inicial: ");
                    conta.Saldo = double.Parse(Console.ReadLine());

                    Console.Write("Informe nome do titular: ");
                    conta.Titular.Nome = Console.ReadLine();

                    Console.Write("Informe CPF do titular: ");
                    conta.Titular.Cpf = Console.ReadLine();

                    Console.Write("Informe Profissão do titular: ");
                    conta.Titular.Profissao = Console.ReadLine();

                    _listaDeContas.Add(conta);

                    Console.WriteLine("...Conta cadastrada com sucesso!...");
                    Console.ReadLine();
                }

                //2 - Listar Contas
                private void ListarConta()
                {
                    Console.Clear();
                    Console.WriteLine("===============================");
                    Console.WriteLine("===     LISTA DE CONTAS     ===");
                    Console.WriteLine("===============================");
                    Console.WriteLine("\n");
                    if (_listaDeContas.Count <= 0)
                    {
                        Console.WriteLine("... Não há contas cadastradas ...");
                        Console.ReadKey();
                        return;
                    }
                    foreach (ContaCorrente item in _listaDeContas)
                    {
                        //Console.WriteLine("===  Dados da Contas  ===");
                        //Console.WriteLine("Numero da Conta: " + item.Conta);
                        //Console.WriteLine("Saldo da Conta: " + item.Saldo);
                        //Console.WriteLine("Titular da Conta: " + item.Titular.Nome);
                        //Console.WriteLine("CPF do Titular:  " + item.Titular.Cpf);
                        //Console.WriteLine("Profissão do Titular: " + item.Titular.Profissao);
                        Console.WriteLine(item.ToString());
                        Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");
                        Console.ReadKey();
                    }
                }

                //3 - Remover Contas
                private void RemoverConta()
                {
                    Console.Clear();
                    Console.WriteLine("===============================");
                    Console.WriteLine("===      REMOVER CONTAS     ===");
                    Console.WriteLine("===============================");
                    Console.WriteLine("\n");
                    Console.Write("Informe o número da Conta: ");
                    string numeroConta = Console.ReadLine();
                    ContaCorrente conta = null;
                    foreach (var item in _listaDeContas)
                    {
                        if (item.Conta.Equals(numeroConta))
                        {
                            conta = item;
                        }
                    }
                    if (conta != null)
                    {
                        _listaDeContas.Remove(conta);
                        Console.WriteLine("... Conta removida da lista! ...");
                    }
                    else
                    {
                        Console.WriteLine("... Conta pra remoção não encontrada ...");
                    }
                    Console.ReadKey();
                }

                //4 - Ordenar Contas
                private void OrdenarConta()
                {
                    _listaDeContas.Sort();
                    Console.WriteLine("...  Lista de contas ordenada  ...");
                    Console.ReadKey();
                }

                //5 - Pesquisar Contas
                private void PesquisarConta()
                {
                    Console.Clear();
                    Console.WriteLine("===============================");
                    Console.WriteLine("===    PESQUISAR CONTAS     ===");
                    Console.WriteLine("===============================");
                    Console.WriteLine("\n");
                    Console.Write("Deseja pesquisar por (1) NUMERO DA CONTA ou (2) CPF TITULAR ou (3) N° Agencia ? ");
                    switch (int.Parse(Console.ReadLine()))
                    {
                        case 1:
                            {
                                Console.Write("Informe o número da Conta: ");
                                string _numeroConta = Console.ReadLine();
                                var consultaConta = ConsultaPorNumeroConta(_numeroConta);
                                Console.WriteLine(consultaConta.ToString());
                                Console.ReadKey();
                                break;
                            }
                        case 2:
                            {
                                Console.Write("Informe o CPF do Titular: ");
                                string _cpf = Console.ReadLine();
                                ContaCorrente consultaCpf = ConsultaPorCPFTitular(_cpf);
                                Console.WriteLine(consultaCpf.ToString());
                                Console.ReadKey();
                                break;

                            }
                        case 3:
                            {
                                Console.Write("Informe o N° da Agencia: ");
                                int _numeroAgencia = int.Parse(Console.ReadLine());
                                var contasPorAgencia = ConsultaPorAgencia(_numeroAgencia);
                                ExibirListaDeContas(contasPorAgencia);

                                Console.ReadKey();
                                break;

                            }
                        default:
                            Console.WriteLine("Opção não implementada.");
                            break;
                    }
                }
                
                //6 - Exportar Contas
                private void ExportarContas()
                {
                    Console.Clear();
                    Console.WriteLine("===============================");
                    Console.WriteLine("===     EXPORTAR CONTAS     ===");
                    Console.WriteLine("===============================");
                    Console.WriteLine("\n");

                    if (_listaDeContas.Count <= 0)
                    {
                        Console.WriteLine("... Não existe dados para exportação...");
                        Console.ReadKey();
                    }
                    else
                    {
                        string json = JsonConvert.SerializeObject(_listaDeContas, Formatting.Indented);
                        try
                        {
                            FileStream fs = new FileStream(@"c:\ProjetTh\contas.json", FileMode.Create); 
                            using (StreamWriter streamwriter =  new StreamWriter(fs))
                            {
                                streamwriter.WriteLine(json);
                            }
                            Console.WriteLine(@"Arquivo salvo em c:\ProjetTh");
                            Console.ReadKey();
                        }
                        catch (Exception excecao)
                        {

                            throw new ByteBankException(excecao.Message);
                            Console.ReadKey();
                        }
                    }
                }

                //7 - Exportar Contas XML
                private void ExportarContasxml()
                {
                    Console.Clear();
                    Console.WriteLine("===============================");
                    Console.WriteLine("===     EXPORTAR CONTAS XML ===");
                    Console.WriteLine("===============================");
                    Console.WriteLine("\n");

                        if (_listaDeContas.Count <= 0)
                        {
                            Console.WriteLine("... Não existe dados para exportação...");
                            Console.ReadKey();
                        }
                    else
                    {   //Serealizador utilizado para XML
                        var contasXML = new XmlSerializer(typeof(List<ContaCorrente>));

                    try
                    {
                    FileStream fs = new FileStream(@"c:\ProjetTh\contas.xml", FileMode.Create);
                    using (StreamWriter streamWriter = new StreamWriter(fs))
                    {
                        contasXML.Serialize(streamWriter, _listaDeContas);
                    }
                    Console.WriteLine(@"arquivo salvo em c:\ProjetTh");
                    Console.ReadKey();
                    }
                    catch (Exception excecao)
                    {

                        throw new ByteBankException(excecao.Message);
                        Console.ReadKey();
                    }
                    }
                }

                //8 - Sair do Programa
                private void EncerrarAplicacao()
                                {
                                    Console.WriteLine("...  Tchau Tchau :)  ...");
                                    Console.ReadKey();
                                }

                private void ExibirListaDeContas(List<ContaCorrente> contasPorAgencia)
                {
                    if (contasPorAgencia == null)
                    {
                        Console.WriteLine("... a consulta não retornou dados. ...");
                    }
                    else
                    {
                        foreach (var item in contasPorAgencia)
                        {
                            Console.WriteLine(item.ToString());
                        }
                    }
                }

                private List<ContaCorrente> ConsultaPorAgencia(int numeroAgencia)
                {
                    var consulta = (
                                from conta in _listaDeContas
                                where conta.Numero_agencia == numeroAgencia
                                select conta).ToList();
                    return consulta;
                }

                ContaCorrente ConsultaPorCPFTitular(string? cpf)
                {
                    //      _listaDeContas.Where(conta => conta.Titular.Cpf == cpf).FirstOrDefault();
                    return _listaDeContas.FirstOrDefault(conta => conta.Titular.Cpf.Equals(cpf));
                }

                ContaCorrente ConsultaPorNumeroConta(string? numeroConta)
                {
                    //     _listaDeContas.Where(conta => conta.Conta == numeroConta).FirstOrDefault()
                    return _listaDeContas.FirstOrDefault(conta => conta.Conta.Equals(numeroConta));
                }

    }
}
