using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq.Expressions;

namespace CRUD_DB
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MenuPrincipal();
        }
        public static void OpcaoInvaolida()
        {
            Console.WriteLine("Opção inválida! Tente novamente!");
            Console.ReadKey(true);
            Console.Clear();
        }
        public static void AbrirCadastros()
        {
            Console.Clear();

            int opcao = 0;

            Console.WriteLine("+-------------------------+");
            Console.WriteLine("| O que deseja cadastrar? |");
            Console.WriteLine("+-------------------------+");
            Console.WriteLine("| 1 - Reserva             |");
            Console.WriteLine("| 2 - Cliente             |");
            Console.WriteLine("| 3 - Vôos                |");
            Console.WriteLine("+-------------------------+");

            if (int.TryParse(Console.ReadLine(), out opcao))
            {
                switch (opcao)
                {
                    case 1:
                        Reserva.CadastrarReserva();
                        break;
                    case 2:
                        Cliente.CadastrarCliente();
                        break;
                    case 3:
                        Voo.CadastrarVoo();
                        break;
                    default:
                        OpcaoInvaolida();
                        return;
                }
            }
            else
            {
                OpcaoInvaolida();
                return;
            }
        }
        public static void AbrirExibicoes()
        {
            Console.Clear();

            int opcao = 0;

            Console.WriteLine("+-------------------------+");
            Console.WriteLine("| O que deseja exibir?    |");
            Console.WriteLine("+-------------------------+");
            Console.WriteLine("| 1 - Reserva             |");
            Console.WriteLine("| 2 - Cliente             |");
            Console.WriteLine("| 3 - Vôos                |");
            Console.WriteLine("+-------------------------+");

            if (int.TryParse(Console.ReadLine(), out opcao))
            {
                switch (opcao)
                {
                    case 1:
                        Reserva.ExibirReserva();
                        break;
                    case 2:
                        Cliente.ExibirCliente();
                        break;
                    case 3:
                        Voo.ExibirVoo();
                        break;
                    default:
                        OpcaoInvaolida();
                        return;
                }
            }
            else
            {
                OpcaoInvaolida();
                return;
            }
        }
        public static void AbrirEdicoes()
        {
            Console.Clear();

            int opcao = 0;

            Console.WriteLine("+-------------------------+");
            Console.WriteLine("| O que deseja editar?    |");
            Console.WriteLine("+-------------------------+");
            Console.WriteLine("| 1 - Reserva             |");
            Console.WriteLine("| 2 - Cliente             |");
            Console.WriteLine("| 3 - Vôos                |");
            Console.WriteLine("+-------------------------+");

            if (int.TryParse(Console.ReadLine(), out opcao))
            {
                switch (opcao)
                {
                    case 1:
                        Reserva.EditarReserva();
                        break;
                    case 2:
                        Cliente.EditarCliente();
                        break;
                    case 3:
                        Voo.EditarVoo();
                        break;
                    default:
                        OpcaoInvaolida();
                        return;
                }
            }
            else
            {
                OpcaoInvaolida();
                return;
            }
        }
        public static void AbrirDeletar()
        {
            Console.Clear();

            int opcao = 0;

            Console.WriteLine("+-------------------------+");
            Console.WriteLine("| O que deseja deletar?   |");
            Console.WriteLine("+-------------------------+");
            Console.WriteLine("| 1 - Reserva             |");
            Console.WriteLine("| 2 - Cliente             |");
            Console.WriteLine("| 3 - Vôos                |");
            Console.WriteLine("+-------------------------+");

            if (int.TryParse(Console.ReadLine(), out opcao))
            {
                switch (opcao)
                {
                    case 1:
                        Reserva.ExcluirReserva();
                        break;
                    case 2:
                        Cliente.ExcluirCliente();
                        break;
                    case 3:
                        Voo.ExcluirVoo();
                        break;
                    default:
                        OpcaoInvaolida();
                        return;
                }
            }
            else
            {
                OpcaoInvaolida();
                return;
            }
        }
        public static string DataBaseString()
        {
            string connectionString = "Data Source=nome_desktop_aqui;Initial Catalog=VENDAS_AEREAS;Integrated Security=True";

            return connectionString;
        }
        public static void MenuPrincipal()
        {
            Console.Clear();

            Console.WriteLine("+---------------------------------------+");
            Console.WriteLine("| Sistema de vendas de passagens aéreas |");
            Console.WriteLine("+---------------------------------------+");
            Console.WriteLine("| 1 - Cadastrar                         |");
            Console.WriteLine("| 2 - Editar                            |");
            Console.WriteLine("| 3 - Exibir                            |");
            Console.WriteLine("| 4 - Deletar                           |");
            Console.WriteLine("+---------------------------------------+");
            Console.WriteLine("+---------------------------------------+");
            Console.WriteLine("| 5 - Efetivar reserva                  |");
            Console.WriteLine("| 6 - Consultas e Procedimentos         |");
            Console.WriteLine("+---------------------------------------+");

            if (int.TryParse(Console.ReadLine(), out int opcao))
            {
                switch (opcao)
                {
                    case 1:
                        AbrirCadastros();
                        break;
                    case 2:
                        AbrirEdicoes();
                        break;
                    case 3:
                        AbrirExibicoes();
                        break;
                    case 4:
                        AbrirDeletar();
                        break;
                    case 5:
                        Reserva.EfetuarReserva();
                        break;
                    case 6:
                        MenuConsultasEProcedimentos();
                        break;
                    default:
                        OpcaoInvalida();
                        break;
                }
            }
            else
            {
                OpcaoInvalida();
            }
        }
        public static void OpcaoInvalida()
        {
            Console.WriteLine("Opção inválida! Pressione qualquer tecla para voltar.");
            Console.ReadKey();
            MenuPrincipal();
        }
        public static void MenuConsultasEProcedimentos()
        {
            Console.Clear();

            Console.WriteLine("+---------------------------------------+");
            Console.WriteLine("| Consultas e Procedimentos             |");
            Console.WriteLine("+---------------------------------------+");
            Console.WriteLine("| 1 - Mostrar trechos entre datas       |");
            Console.WriteLine("| 2 - Voos de um cliente em período     |");
            Console.WriteLine("| 3 - Reservas diárias                  |");
            Console.WriteLine("| 4 - Total de pagamentos por operadora |");
            Console.WriteLine("| 5 - Copiar e excluir reservas         |");
            Console.WriteLine("| 6 - Mostrar idade de um cliente       |");
            Console.WriteLine("| 7 - Clientes aniversariantes          |");
            Console.WriteLine("| 8 - Trechos comuns entre voos         |");
            Console.WriteLine("| 9 - Clientes sem voos no ano anterior |");
            Console.WriteLine("| 10 - Contar reservas de um cliente    |");
            Console.WriteLine("| 0 - Voltar ao menu principal          |");
            Console.WriteLine("+---------------------------------------+");

            if (int.TryParse(Console.ReadLine(), out int opcao))
            {
                switch (opcao)
                {
                    case 1:
                        ConsultarTrechosEntreDatas();
                        break;
                    case 2:
                        ConsultarVoosClientePeriodo();
                        break;
                    case 3:
                        ConsultarReservasDiarias();
                        break;
                    case 4:
                        ConsultarTotalPagamentosOperadora();
                        break;
                    case 5:
                        CopiarEExcluirReservasNaoConcluidas();
                        break;
                    case 6:
                        MostrarIdadeCliente();
                        break;
                    case 7:
                        ConsultarAniversariantes();
                        break;
                    case 8:
                        ConsultarTrechosComuns();
                        break;
                    case 9:
                        ConsultarClientesSemVoosAnoAnterior();
                        break;
                    case 10:
                        ContarReservasClientePeriodo();
                        break;
                    case 0:
                        MenuPrincipal();
                        break;
                    default:
                        OpcaoInvalida();
                        break;
                }
            }
            else
            {
                OpcaoInvalida();
            }
        }
        #region CONSULTAS E PROCEDIMENTOS
        public static void ConsultarTrechosEntreDatas()
        {
            Console.Clear();
            Console.WriteLine("Informe a data de início (yyyy-MM-dd):");
            string dataInicio = Console.ReadLine();
            Console.WriteLine("Informe a data de fim (yyyy-MM-dd):");
            string dataFim = Console.ReadLine();

            string query = "SELECT V.DataHora, V.Origem, V.Destino FROM VOO V WHERE V.DataHora BETWEEN @DataInicio AND @DataFim";

            using (SqlConnection connection = new SqlConnection(Program.DataBaseString()))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@DataInicio", dataInicio);
                command.Parameters.AddWithValue("@DataFim", dataFim);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine($"Data e Hora: {reader["DataHora"]}, Origem: {reader["Origem"]}, Destino: {reader["Destino"]}");
                }
                reader.Close();
                connection.Close();
            }
            Console.ReadKey();
            MenuConsultasEProcedimentos();
        }

        public static void ConsultarVoosClientePeriodo()
        {
            Console.Clear();
            Console.WriteLine("Informe o ID do cliente:");
            int clienteId = int.Parse(Console.ReadLine());
            Console.WriteLine("Informe a data de início (yyyy-MM-dd):");
            string dataInicio = Console.ReadLine();
            Console.WriteLine("Informe a data de fim (yyyy-MM-dd):");
            string dataFim = Console.ReadLine();

            string query = "SELECT V.* FROM VOO V JOIN RESERVA R ON V.VooId = R.VooId WHERE R.CliIdRes = @ClienteId AND V.DataHora BETWEEN @DataInicio AND @DataFim";

            using (SqlConnection connection = new SqlConnection(Program.DataBaseString()))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ClienteId", clienteId);
                command.Parameters.AddWithValue("@DataInicio", dataInicio);
                command.Parameters.AddWithValue("@DataFim", dataFim);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine($"Voo ID: {reader["VooId"]}, Origem: {reader["Origem"]}, Destino: {reader["Destino"]}, Data e Hora: {reader["DataHora"]}, Preço: {reader["Preco"]}, Classe: {reader["Classe"]}");
                }
                reader.Close();
                connection.Close();
            }
            Console.ReadKey();
            MenuConsultasEProcedimentos();
        }
        public static void ConsultarReservasDiarias()
        {
            Console.Clear();
            Console.WriteLine("Informe o ID do voo:");
            int vooId = int.Parse(Console.ReadLine());
            Console.WriteLine("Informe a data (yyyy-MM-dd):");
            string data = Console.ReadLine();

            // Formatar a data para o formato esperado pelo SQL Server
            DateTime dataReserva;
            if (!DateTime.TryParseExact(data, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dataReserva))
            {
                Console.WriteLine("Data informada está em formato inválido.");
                Console.ReadKey();
                MenuConsultasEProcedimentos();
                return;
            }

            string query = "SELECT C.Nome, C.Email, V.Origem, V.Destino FROM CLIENTE C " +
                           "JOIN RESERVA R ON C.CliId = R.CliIdRes " +
                           "JOIN VOO V ON R.VooId = V.VooId " +
                           "WHERE V.VooId = @VooId AND CONVERT(DATE, R.DataReserva) = @Data";

            using (SqlConnection connection = new SqlConnection(Program.DataBaseString()))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@VooId", vooId);
                command.Parameters.AddWithValue("@Data", dataReserva);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine($"Nome: {reader["Nome"]}, Email: {reader["Email"]}, Origem: {reader["Origem"]}, Destino: {reader["Destino"]}");
                }
                reader.Close();
                connection.Close();
            }
            Console.ReadKey();
            MenuConsultasEProcedimentos();
        }

        public static void ConsultarTotalPagamentosOperadora()
        {
            Console.Clear();
            Console.WriteLine("Informe a operadora de cartão:");
            string operadora = Console.ReadLine().ToUpper();
            Console.WriteLine("Informe o mês (1-12):");
            int mes = int.Parse(Console.ReadLine());
            Console.WriteLine("Informe o ano:");
            int ano = int.Parse(Console.ReadLine());

            string query = "SELECT SUM(V.ValorTotal) AS TotalPagamentos FROM VENDA V JOIN RESERVA R ON V.ResIdVenda = R.ResId JOIN CLIENTE C ON R.CliIdRes = C.CliId WHERE V.Operadora = @OperadoraCartao AND MONTH(V.DataVenda) = @Mes AND YEAR(V.DataVenda) = @Ano";

            using (SqlConnection connection = new SqlConnection(Program.DataBaseString()))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@OperadoraCartao", operadora);
                command.Parameters.AddWithValue("@Mes", mes);
                command.Parameters.AddWithValue("@Ano", ano);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    Console.WriteLine($"Total de Pagamentos: {reader["TotalPagamentos"]}");
                }
                reader.Close();
                connection.Close();
            }
            Console.ReadKey();
            MenuConsultasEProcedimentos();
        }
        public static void CopiarEExcluirReservasNaoConcluidas()
        {
            string procedureName = "CopiarExcluirReservasNaoEfetivadas";

            using (SqlConnection connection = new SqlConnection(Program.DataBaseString()))
            {
                SqlCommand command = new SqlCommand(procedureName, connection);
                command.CommandType = CommandType.StoredProcedure;

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    Console.WriteLine("Reservas não concluídas copiadas e excluídas.");
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Erro ao executar a procedure: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }

            Console.ReadKey();
            MenuConsultasEProcedimentos();
        }

        public static void MostrarIdadeCliente()
        {
            Console.Clear();
            Console.WriteLine("Informe o ID do cliente:");
            int clienteId = int.Parse(Console.ReadLine());

            string function = "SELECT dbo.CalcularIdadeCliente(@ClienteId) AS Idade";

            using (SqlConnection connection = new SqlConnection(Program.DataBaseString()))
            {
                SqlCommand command = new SqlCommand(function, connection);
                command.Parameters.AddWithValue("@ClienteId", clienteId);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    Console.WriteLine($"Idade do Cliente: {reader["Idade"]}");
                }
                reader.Close();
                connection.Close();
            }
            Console.ReadKey();
            MenuConsultasEProcedimentos();
        }
        public static void ConsultarAniversariantes()
        {
            Console.Clear();
            Console.WriteLine("Informe a data (MM-dd):");
            string data = Console.ReadLine();

            string query = "SELECT * FROM CLIENTE WHERE CONVERT(VARCHAR(5), DataNasc, 110) = @Data";

            using (SqlConnection connection = new SqlConnection(Program.DataBaseString()))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Data", data);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine($"ID: {reader["CliId"]}, Nome: {reader["Nome"]}, Email: {reader["Email"]}, Data de Nascimento: {reader["DataNasc"]}");
                }
                reader.Close();
                connection.Close();
            }
            Console.ReadKey();
            MenuConsultasEProcedimentos();
        }
        public static void ConsultarTrechosComuns()
        {
            Console.Clear();
            Console.WriteLine("Informe o ID do primeiro voo:");
            int vooId1 = int.Parse(Console.ReadLine());
            Console.WriteLine("Informe o ID do segundo voo:");
            int vooId2 = int.Parse(Console.ReadLine());

            string query = "SELECT Origem, Destino FROM VOO WHERE VooId = @VooId1 INTERSECT SELECT Origem, Destino FROM VOO WHERE VooId = @VooId2";

            using (SqlConnection connection = new SqlConnection(Program.DataBaseString()))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@VooId1", vooId1);
                command.Parameters.AddWithValue("@VooId2", vooId2);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine($"Origem: {reader["Origem"]}, Destino: {reader["Destino"]}");
                }
                reader.Close();
                connection.Close();
            }
            Console.ReadKey();
            MenuConsultasEProcedimentos();
        }
        public static void ConsultarClientesSemVoosAnoAnterior()
        {
            Console.Clear();
            Console.WriteLine("Informe o ano anterior:");
            int anoAnterior = int.Parse(Console.ReadLine());

            string query = "SELECT C.Nome, C.Email FROM CLIENTE C WHERE C.CliId NOT IN (SELECT R.CliIdRes FROM RESERVA R JOIN VOO V ON R.VooId = V.VooId WHERE YEAR(V.DataHora) = @AnoAnterior)";

            using (SqlConnection connection = new SqlConnection(Program.DataBaseString()))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@AnoAnterior", anoAnterior);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine($"Nome: {reader["Nome"]}, Email: {reader["Email"]}");
                }
                reader.Close();
                connection.Close();
            }
            Console.ReadKey();
            MenuConsultasEProcedimentos();
        }
        public static void ContarReservasClientePeriodo()
        {
            Console.Clear();
            Console.WriteLine("Informe o ID do cliente:");
            int clienteId = int.Parse(Console.ReadLine());
            Console.WriteLine("Informe a data de início (yyyy-MM-dd):");
            string dataInicio = Console.ReadLine();
            Console.WriteLine("Informe a data de fim (yyyy-MM-dd):");
            string dataFim = Console.ReadLine();

            string function = "SELECT dbo.ContarReservas(@ClienteId, @DataInicio, @DataFim) AS NumeroReservas";

            using (SqlConnection connection = new SqlConnection(Program.DataBaseString()))
            {
                SqlCommand command = new SqlCommand(function, connection);
                command.Parameters.AddWithValue("@ClienteId", clienteId);
                command.Parameters.AddWithValue("@DataInicio", dataInicio);
                command.Parameters.AddWithValue("@DataFim", dataFim);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    Console.WriteLine($"Número de Reservas: {reader["NumeroReservas"]}");
                }
                reader.Close();
                connection.Close();
            }
            Console.ReadKey();
            MenuConsultasEProcedimentos();
        }
        #endregion
    }
}