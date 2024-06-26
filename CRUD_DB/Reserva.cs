using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_DB
{
    public class Reserva
    {
        #region ENTIDADES
        public int ResId { get; set; }
        public int ClienteId { get; set; }
        public int VooId { get; set; }
        public DateTime DataReserva { get; set; }
        public string Situacao { get; set; }
        #endregion

         #region SERVIÇOS
        public static void CadastrarReserva()
        {
            Console.Clear();
            Reserva reserva = new Reserva();

            Console.WriteLine("+-----------------------------+");
            Console.WriteLine("| Informe o ID do cliente:    |");
            Console.WriteLine("+-----------------------------+");
            if (!int.TryParse(Console.ReadLine(), out int clienteId))
            {
                Console.WriteLine("ID do cliente inválido!");
                return;
            }
            reserva.ClienteId = clienteId;

            Console.WriteLine("+--------------------------+");
            Console.WriteLine("| Informe o ID do voo:     |");
            Console.WriteLine("+--------------------------+");
            if (!int.TryParse(Console.ReadLine(), out int vooId))
            {
                Console.WriteLine("ID do voo inválido!");
                return;
            }
            reserva.VooId = vooId;

            reserva.DataReserva = DateTime.Now;
            reserva.Situacao = "A";

            GravarReserva(reserva.ClienteId, reserva.VooId, reserva.DataReserva, reserva.Situacao);
        }
        public static void GravarReserva(int clienteId, int vooId, DateTime dataReserva, string situacao)
        {
            string connectionString = Program.DataBaseString();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string sql = "INSERT INTO RESERVA(CliIdRes, VooId, DataReserva, Situacao) " +
                                 "VALUES(@ClienteId, @VooId, @DataReserva, @Situacao);";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@ClienteId", clienteId);
                        command.Parameters.AddWithValue("@VooId", vooId);
                        command.Parameters.AddWithValue("@DataReserva", dataReserva);
                        command.Parameters.AddWithValue("@Situacao", situacao);

                        int linhasAfetadas = command.ExecuteNonQuery();

                        Console.Clear();
                        if (linhasAfetadas > 0)
                        {
                            Console.WriteLine("+----------------------------------+");
                            Console.WriteLine("| Reserva cadastrada com sucesso!  |");
                            Console.WriteLine("+----------------------------------+\n");
                        }
                        else
                        {
                            Console.WriteLine("Erro ao cadastrar a reserva");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao conectar ao banco de dados: " + ex.Message);
                }
                finally
                {
                    Console.ReadKey();
                    Program.MenuPrincipal();
                }
            }
        }
        public static void EditarReserva()
        {
            Console.Clear();
            Console.WriteLine("+-------------------------------+");
            Console.WriteLine("| Informe o ID da reserva:      |");
            Console.WriteLine("+-------------------------------+");
            if (!int.TryParse(Console.ReadLine(), out int reservaId))
            {
                Console.WriteLine("ID inválido!");
                return;
            }

            Reserva reserva = BuscarReservaPorId(reservaId);
            if (reserva == null)
            {
                Console.WriteLine("Reserva não encontrada!");
                return;
            }

            Console.WriteLine("Editando reserva: " + reserva.ResId);
            Console.WriteLine("+-------------------------------+");
            Console.WriteLine("| Informe o novo ID do cliente: |");
            Console.WriteLine("+-------------------------------+");
            reserva.ClienteId = int.Parse(Console.ReadLine());

            Console.WriteLine("+-------------------------------+");
            Console.WriteLine("| Informe o novo ID do voo:     |");
            Console.WriteLine("+-------------------------------+");
            reserva.VooId = int.Parse(Console.ReadLine());

            Console.WriteLine("+-----------------------------------------------------+");
            Console.WriteLine("| Informe a nova data de reserva (dd/mm/yyyy hh:mm):  |");
            Console.WriteLine("+-----------------------------------------------------+");
            reserva.DataReserva = Convert.ToDateTime(Console.ReadLine());

            AtualizarReserva(reserva);
        }
        public static void AtualizarReserva(Reserva reserva)
        {
            string connectionString = Program.DataBaseString();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string sql = "UPDATE RESERVA SET ClienteId = @ClienteId, VooId = @VooId, DataReserva = @DataReserva WHERE ResId = @Id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@ClienteId", reserva.ClienteId);
                        command.Parameters.AddWithValue("@VooId", reserva.VooId);
                        command.Parameters.AddWithValue("@DataReserva", reserva.DataReserva);
                        command.Parameters.AddWithValue("@Id", reserva.ResId);

                        int linhasAfetadas = command.ExecuteNonQuery();
                        if (linhasAfetadas > 0)
                        {
                            Console.WriteLine("Reserva atualizada com sucesso!");
                        }
                        else
                        {
                            Console.WriteLine("Erro ao atualizar reserva.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao conectar ao banco de dados: " + ex.Message);
                }
                finally
                {
                    Console.ReadKey();
                    Program.MenuPrincipal();
                }
            }
        }
        public static void ExcluirReserva()
        {
            Console.Clear();
            Console.WriteLine("+-----------------------------+");
            Console.WriteLine("| Informe o ID da reserva:    |");
            Console.WriteLine("+-----------------------------+");
            if (!int.TryParse(Console.ReadLine(), out int reservaId))
            {
                Console.WriteLine("ID inválido!");
                return;
            }

            string connectionString = Program.DataBaseString();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string sql = "DELETE FROM RESERVA WHERE ResId = @Id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Id", reservaId);

                        int linhasAfetadas = command.ExecuteNonQuery();
                        if (linhasAfetadas > 0)
                        {
                            Console.WriteLine("Reserva excluída com sucesso!");
                        }
                        else
                        {
                            Console.WriteLine("Erro ao excluir reserva.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao conectar ao banco de dados: " + ex.Message);
                }
                finally
                {
                    Console.ReadKey();
                    Program.MenuPrincipal();
                }
            }
        }
        public static void ExibirReserva()
        {
            Console.Clear();
            Console.WriteLine("+-----------------------------+");
            Console.WriteLine("| Informe o ID da reserva:    |");
            Console.WriteLine("+-----------------------------+");
            if (!int.TryParse(Console.ReadLine(), out int reservaId))
            {
                Console.WriteLine("ID inválido!");
                return;
            }

            Reserva reserva = BuscarReservaPorId(reservaId);
            if (reserva == null)
            {
                Console.WriteLine("Reserva não encontrada!");
                return;
            }

            Console.WriteLine($"\nID: {reserva.ResId}");
            Console.WriteLine($"ID do Cliente: {reserva.ClienteId}");
            Console.WriteLine($"ID do Voo: {reserva.VooId}");
            Console.WriteLine($"Data da Reserva: {reserva.DataReserva:dd/MM/yyyy HH:mm}");

            Console.ReadKey();
            Program.MenuPrincipal();
        }
        public static void EfetuarReserva()
        {
            Console.Clear();
            Console.WriteLine("+-----------------------------+");
            Console.WriteLine("| Informe o ID da reserva:    |");
            Console.WriteLine("+-----------------------------+");
            if (!int.TryParse(Console.ReadLine(), out int reservaId))
            {
                Console.WriteLine("ID inválido!");
                return;
            }

            Reserva reserva = BuscarReservaPorId(reservaId);
            if (reserva == null)
            {
                Console.WriteLine("Reserva não encontrada!");
                return;
            }

            TimeSpan diferenca = DateTime.Now - reserva.DataReserva;
            if (diferenca.TotalDays > 30)
            {
                AtualizarSituacaoReserva(reserva.ResId, 'C');
                Console.WriteLine("A reserva foi cancelada devido à expiração do prazo de 30 dias.");
                Program.MenuPrincipal();
                return;
            }

            if (reserva.Situacao == "C")
            {
                Console.WriteLine("A reserva está cancelada.");
                Program.MenuPrincipal();
                return;
            }

            Console.WriteLine("+-------------------------------------+");
            Console.WriteLine("| Escolha o número de parcelas (1-6): |");
            Console.WriteLine("+-------------------------------------+");
            if (!int.TryParse(Console.ReadLine(), out int parcelas) || parcelas < 1 || parcelas > 6)
            {
                Console.WriteLine("Número de parcelas inválido!");
                return;
            }

            Console.WriteLine("+------------------------+");
            Console.WriteLine("| Informe o valor total: |");
            Console.WriteLine("+------------------------+");
            if (!decimal.TryParse(Console.ReadLine(), out decimal valorTotal))
            {
                Console.WriteLine("Valor inválido!");
                return;
            }

            Console.WriteLine("+---------------------+");
            Console.WriteLine("| Informe a operadora |");
            Console.WriteLine("+---------------------+");
            string operadora = Console.ReadLine().ToUpper();

            RegistrarVenda(reserva.ResId, valorTotal, parcelas, operadora);
            AtualizarSituacaoReserva(reserva.ResId, 'E');

            Console.WriteLine("Pagamento efetuado e reserva atualizada com sucesso!");
            Console.ReadKey();
            Program.MenuPrincipal();
        }
        public static void RegistrarVenda(int reservaId, decimal valorTotal, int parcelas, string operadora)
        {
            string connectionString = Program.DataBaseString();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string sql = "INSERT INTO VENDA (ResIdVenda, DataVenda, ValorTotal, Parcelas, Operadora) VALUES (@ReservaId, @DataVenda, @ValorTotal, @Parcelas, @Operadora)";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@ReservaId", reservaId);
                        command.Parameters.AddWithValue("@DataVenda", DateTime.Now);
                        command.Parameters.AddWithValue("@ValorTotal", valorTotal);
                        command.Parameters.AddWithValue("@Parcelas", parcelas);
                        command.Parameters.AddWithValue("@Operadora", operadora);

                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao registrar venda: " + ex.Message);
                }
            }
        }

        public static void AtualizarSituacaoReserva(int reservaId, char situacao)
        {
            string connectionString = Program.DataBaseString();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string sql = "UPDATE RESERVA SET Situacao = @Situacao WHERE ResId = @Id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Situacao", situacao);
                        command.Parameters.AddWithValue("@Id", reservaId);

                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao atualizar situação da reserva: " + ex.Message);
                }
            }
        }

        public static Reserva BuscarReservaPorId(int reservaId)
        {
            string connectionString = Program.DataBaseString();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string sql = "SELECT * FROM RESERVA WHERE ResId = @Id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Id", reservaId);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Reserva
                                {
                                    ResId = reader.GetInt32(0),
                                    ClienteId = reader.GetInt32(1),
                                    VooId = reader.GetInt32(2),
                                    DataReserva = reader.GetDateTime(3)
                                };
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao conectar ao banco de dados: " + ex.Message);
                }
            }
            return null;
        }
        #endregion
    }
}
