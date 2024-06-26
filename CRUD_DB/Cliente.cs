using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_DB
{
    public class Cliente
    {
        #region ENTIDADES
        private int ClienteId { get; set; }
        private string? ClienteNome { get; set; }
        private Int64 ClienteCPF { get; set;}
        private int ClienteRG { get; set;}
        private DateTime ClienteDataNascimento { get; set;}
        private string? ClienteEmail { get; set;}
        private string? ClienteCidade { get; set;}
        private string? ClienteUF { get; set;}
        #endregion

        #region SERVIÇOS
        public static void CadastrarCliente()
        {
            Console.Clear();
            Cliente cliente = new Cliente();

            Console.WriteLine("+----------------------------+");
            Console.WriteLine("| Informe o nome do cliente: |");
            Console.WriteLine("+----------------------------+");
            cliente.ClienteNome = Console.ReadLine().ToUpper();

            Console.WriteLine("+---------------------------+");
            Console.WriteLine("| Informe o CPF do cliente: |");
            Console.WriteLine("+---------------------------+");
            if (!long.TryParse(Console.ReadLine(), out long cpf))
            {
                Console.WriteLine("+---------------+");
                Console.WriteLine("| CPF inválido! |");
                Console.WriteLine("+---------------+");
                Console.ReadKey(true);
                return;
            }
            cliente.ClienteCPF = cpf;

            Console.WriteLine("+--------------------------+");
            Console.WriteLine("| Informe o RG do cliente: |");
            Console.WriteLine("+--------------------------+");
            if (!int.TryParse(Console.ReadLine(), out int rg))
            {
                Console.WriteLine("RG inválido!");
                Console.ReadKey(true);
                return;
            }
            cliente.ClienteRG = rg;

            Console.WriteLine("+-------------------------------------------------------+");
            Console.WriteLine("| Informe a data de aniversário do cliente (00/00/0000) |");
            Console.WriteLine("+-------------------------------------------------------+");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime dataNasc))
            {
                Console.WriteLine("+------------------------------+");
                Console.WriteLine("| Data de nascimento inválida! |");
                Console.WriteLine("+------------------------------+");
                return;
            }
            cliente.ClienteDataNascimento = dataNasc;

            Console.WriteLine("+-----------------------------+");
            Console.WriteLine("| Informe o email do cliente: |");
            Console.WriteLine("+-----------------------------+");
            cliente.ClienteEmail = Console.ReadLine();

            Console.WriteLine("+------------------------------+");
            Console.WriteLine("| Informe a cidade do cliente: |");
            Console.WriteLine("+------------------------------+");
            cliente.ClienteCidade = Console.ReadLine().ToUpper();

            Console.WriteLine("+--------------------------+");
            Console.WriteLine("| Informe a UF do cliente: |");
            Console.WriteLine("+--------------------------+");
            cliente.ClienteUF = Console.ReadLine().ToUpper();

            GravarCliente(cliente.ClienteNome, cliente.ClienteCPF, cliente.ClienteRG, cliente.ClienteDataNascimento, cliente.ClienteEmail, cliente.ClienteCidade, cliente.ClienteUF);
        }
        public static void GravarCliente(string nome, long cpf, int rg, DateTime dataNasc, string email, string cidade, string uf)
        {
            string connectionString = Program.DataBaseString();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string sql = "INSERT INTO CLIENTE(Nome, CPF, RG, DataNasc, Email, Cidade, UF) " +
                                 "VALUES(@Nome, @CPF, @RG, @DataNasc, @Email, @Cidade, @UF);";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Nome", nome);
                        command.Parameters.AddWithValue("@CPF", cpf);
                        command.Parameters.AddWithValue("@RG", rg);
                        command.Parameters.AddWithValue("@DataNasc", dataNasc);
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@Cidade", cidade);
                        command.Parameters.AddWithValue("@UF", uf);

                        int linhasAfetadas = command.ExecuteNonQuery();

                        Console.Clear();
                        if (linhasAfetadas > 0)
                        {
                            Console.WriteLine("+----------------------------------+");
                            Console.WriteLine("| Registro cadastrado com sucesso! |");
                            Console.WriteLine("+----------------------------------+\n");
                        }
                        else
                        {
                            Console.WriteLine("Erro ao cadastrar o cliente");
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
        public static void EditarCliente()
        {
            Console.Clear();
            Console.WriteLine("+-------------------------------+");
            Console.WriteLine("| Informe o ID do cliente:      |");
            Console.WriteLine("+-------------------------------+");
            if (!int.TryParse(Console.ReadLine(), out int clienteId))
            {
                Console.WriteLine("ID inválido!");
                return;
            }

            Cliente cliente = BuscarClientePorId(clienteId);
            if (cliente == null)
            {
                Console.WriteLine("Cliente não encontrado!");
                return;
            }

            Console.WriteLine("Editando cliente: " + cliente.ClienteNome);
            Console.WriteLine("+----------------------------+");
            Console.WriteLine("| Informe o novo nome:       |");
            Console.WriteLine("+----------------------------+");
            cliente.ClienteNome = Console.ReadLine().ToUpper();

            Console.WriteLine("+-------------------------+");
            Console.WriteLine("Informe o novo CPF:       |");
            Console.WriteLine("+-------------------------+");
            cliente.ClienteCPF = Convert.ToInt64(Console.ReadLine());

            Console.WriteLine("+------------------------+");
            Console.WriteLine("Informe o novo RG:        |");
            Console.WriteLine("+------------------------+");
            cliente.ClienteRG = int.Parse(Console.ReadLine());

            Console.WriteLine("+-------------------------------------------------------+");
            Console.WriteLine("| Informe a nova data de aniversário (00/00/0000)       |");
            Console.WriteLine("+-------------------------------------------------------+");
            cliente.ClienteDataNascimento = Convert.ToDateTime(Console.ReadLine());

            Console.WriteLine("+-----------------------------+");
            Console.WriteLine("| Informe o novo email:       |");
            Console.WriteLine("+-----------------------------+");
            cliente.ClienteEmail = Console.ReadLine();

            Console.WriteLine("+------------------------------+");
            Console.WriteLine("| Informe a nova cidade:       |");
            Console.WriteLine("+------------------------------+");
            cliente.ClienteCidade = Console.ReadLine();

            Console.WriteLine("+--------------------------+");
            Console.WriteLine("| Informe a nova UF:       |");
            Console.WriteLine("+--------------------------+");
            cliente.ClienteUF = Console.ReadLine();

            AtualizarCliente(cliente);
        }
        public static void AtualizarCliente(Cliente cliente)
        {
            string connectionString = Program.DataBaseString();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string sql = "UPDATE CLIENTE SET CliNome = @Nome, CliCPF = @CPF, CliRG = @RG, CliDataNasc = @DataNasc, " +
                                 "CliEmail = @Email, CliCidade = @Cidade, CliUF = @UF WHERE CliId = @Id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Nome", cliente.ClienteNome);
                        command.Parameters.AddWithValue("@CPF", cliente.ClienteCPF);
                        command.Parameters.AddWithValue("@RG", cliente.ClienteRG);
                        command.Parameters.AddWithValue("@DataNasc", cliente.ClienteDataNascimento);
                        command.Parameters.AddWithValue("@Email", cliente.ClienteEmail);
                        command.Parameters.AddWithValue("@Cidade", cliente.ClienteCidade);
                        command.Parameters.AddWithValue("@UF", cliente.ClienteUF);
                        command.Parameters.AddWithValue("@Id", cliente.ClienteId);

                        int linhasAfetadas = command.ExecuteNonQuery();
                        if (linhasAfetadas > 0)
                        {
                            Console.WriteLine("Cliente atualizado com sucesso!");
                        }
                        else
                        {
                            Console.WriteLine("Erro ao atualizar cliente.");
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
        public static void ExibirCliente()
        {
            Console.Clear();
            Console.WriteLine("+-----------------------------+");
            Console.WriteLine("| Informe o ID do cliente:    |");
            Console.WriteLine("+-----------------------------+");
            if (!int.TryParse(Console.ReadLine(), out int clienteId))
            {
                Console.WriteLine("ID inválido!");
                return;
            }

            Cliente cliente = BuscarClientePorId(clienteId);
            if (cliente == null)
            {
                Console.WriteLine("Cliente não encontrado!");
                return;
            }

            Console.WriteLine($"\nID: {cliente.ClienteId}");
            Console.WriteLine($"Nome: {cliente.ClienteNome}");
            Console.WriteLine($"CPF: {cliente.ClienteCPF}");
            Console.WriteLine($"RG: {cliente.ClienteRG}");
            Console.WriteLine($"Data de Nascimento: {cliente.ClienteDataNascimento:dd/MM/yyyy}");
            Console.WriteLine($"Email: {cliente.ClienteEmail}");
            Console.WriteLine($"Cidade: {cliente.ClienteCidade}");
            Console.WriteLine($"UF: {cliente.ClienteUF}");

            Console.ReadKey(true);
            Program.MenuPrincipal();
        }
        public static void ExcluirCliente()
        {
            Console.Clear();
            Console.WriteLine("+-----------------------------+");
            Console.WriteLine("| Informe o ID do cliente:    |");
            Console.WriteLine("+-----------------------------+");
            if (!int.TryParse(Console.ReadLine(), out int clienteId))
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
                    string sql = "DELETE FROM CLIENTE WHERE CliId = @Id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Id", clienteId);

                        int linhasAfetadas = command.ExecuteNonQuery();
                        if (linhasAfetadas > 0)
                        {
                            Console.WriteLine("Cliente excluído com sucesso!");
                        }
                        else
                        {
                            Console.WriteLine("Erro ao excluir cliente.");
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
        public static Cliente BuscarClientePorId(int clienteId)
        {
            string connectionString = Program.DataBaseString();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string sql = "SELECT * FROM CLIENTE WHERE CliId = @Id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Id", clienteId);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Cliente
                                {
                                    ClienteId = reader.GetInt32(0),
                                    ClienteNome = reader.GetString(1),
                                    ClienteCPF = reader.GetInt64(2),
                                    ClienteRG = reader.GetInt32(3),
                                    ClienteDataNascimento = reader.GetDateTime(4),
                                    ClienteEmail = reader.GetString(5),
                                    ClienteCidade = reader.GetString(6),
                                    ClienteUF = reader.GetString(7)
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
