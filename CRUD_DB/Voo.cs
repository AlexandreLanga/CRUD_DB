using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_DB
{
    public class Voo
    {
        #region ENTIDADES
        private int VooId { get; set; }
        private string Origem { get; set; }
        private string Destino { get; set; }
        private DateTime DataHora { get; set; }
        private decimal Preco { get; set; }
        private int Classe { get; set; }
        private string Empresa { get; set; }
        #endregion

        #region SERVIÇOS
        public static void CadastrarVoo()
        {
            Console.Clear();
            Voo voo = new Voo();

            Console.WriteLine("+--------------------------+");
            Console.WriteLine("| Informe a origem do voo: |");
            Console.WriteLine("+--------------------------+");
            voo.Origem = Console.ReadLine().ToUpper();

            Console.WriteLine("+---------------------------+");
            Console.WriteLine("| Informe o destino do voo: |");
            Console.WriteLine("+---------------------------+");
            voo.Destino = Console.ReadLine().ToUpper();

            Console.WriteLine("+--------------------------------------------------+");
            Console.WriteLine("| Informe a data e hora do voo (dd/mm/yyyy hh:mm): |");
            Console.WriteLine("+--------------------------------------------------+");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime dataHora))
            {
                Console.WriteLine("Data e hora inválidas!");
                return;
            }
            voo.DataHora = dataHora;

            Console.WriteLine("+-------------------------+");
            Console.WriteLine("| Informe o preço do voo: |");
            Console.WriteLine("+-------------------------+");
            if (!decimal.TryParse(Console.ReadLine(), out decimal preco))
            {
                Console.WriteLine("Preço inválido!");
                return;
            }
            voo.Preco = preco;

            Console.WriteLine("+----------------------------+");
            Console.WriteLine("| Informe o a classe do voo: |");
            Console.WriteLine("+----------------------------+");
            Console.WriteLine("| 1 - Classe econômica       |");
            Console.WriteLine("| 2 - Classe executiva       |");
            Console.WriteLine("| 3 - Primeira classe        |");
            Console.WriteLine("+----------------------------+");

            if (!int.TryParse(Console.ReadLine(), out int classe))
            {
                Console.WriteLine("Opção inválida!");
                return;
            }
            voo.Classe = classe;

            Console.WriteLine("+-----------------------------+");
            Console.WriteLine("| Selecione a empresa do voo: |");
            Console.WriteLine("+-----------------------------+");
            Console.WriteLine("| 1 - LATAM                   |");
            Console.WriteLine("| 2 - GOL                     |");
            Console.WriteLine("| 3 - AZUL                    |");
            Console.WriteLine("+-----------------------------+");

            if (!int.TryParse(Console.ReadLine(), out int empresa))
            {
                Console.WriteLine("Opção inválida!");
                return;
            }
           
            switch (empresa)
            {
                case 1:
                    voo.Empresa = "LATAM" ;
                    break;
                case 2:
                    voo.Empresa = "GOL";
                    break;
                case 3:
                    voo.Empresa = "AZUL";
                    break;
            }

            GravarVoo(voo.Origem, voo.Destino, voo.DataHora, voo.Preco, voo.Classe, voo.Empresa);
        }
        public static void GravarVoo(string origem, string destino, DateTime dataHora, decimal preco, int classe, string empresa)
        {
            string connectionString = Program.DataBaseString();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string sql = "INSERT INTO VOO(Origem, Destino, DataHora, Preco, Classe, Empresa) " +
                                 "VALUES(@Origem, @Destino, @DataHora, @Preco, @Classe, @Empresa);";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Origem", origem);
                        command.Parameters.AddWithValue("@Destino", destino);
                        command.Parameters.AddWithValue("@DataHora", dataHora);
                        command.Parameters.AddWithValue("@Preco", preco);
                        command.Parameters.AddWithValue("@Classe", classe);
                        command.Parameters.AddWithValue("@Empresa", empresa);

                        int linhasAfetadas = command.ExecuteNonQuery();

                        Console.Clear();
                        if (linhasAfetadas > 0)
                        {
                            Console.WriteLine("+----------------------------+");
                            Console.WriteLine("| Voo cadastrado com sucesso!|");
                            Console.WriteLine("+----------------------------+\n");
                        }
                        else
                        {
                            Console.WriteLine("Erro ao cadastrar o voo");
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
        public static void EditarVoo()
        {
            Console.Clear();
            Console.WriteLine("+-------------------------------+");
            Console.WriteLine("| Informe o ID do voo:          |");
            Console.WriteLine("+-------------------------------+");
            if (!int.TryParse(Console.ReadLine(), out int vooId))
            {
                Console.WriteLine("ID inválido!");
                return;
            }

            Voo voo = BuscarVooPorId(vooId);
            if (voo == null)
            {
                Console.WriteLine("Voo não encontrado!");
                return;
            }

            Console.WriteLine("Editando voo: " + voo.VooId);
            Console.WriteLine("+-----------------------------+");
            Console.WriteLine("| Informe a nova origem:      |");
            Console.WriteLine("+-----------------------------+");
            voo.Origem = Console.ReadLine();

            Console.WriteLine("+-----------------------------+");
            Console.WriteLine("| Informe o novo destino:     |");
            Console.WriteLine("+-----------------------------+");
            voo.Destino = Console.ReadLine();

            Console.WriteLine("+-----------------------------------------+");
            Console.WriteLine("| Informe a nova data e hora (dd/mm/yyyy hh:mm): |");
            Console.WriteLine("+-----------------------------------------+");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime dataHora))
            {
                Console.WriteLine("Data e hora inválidas!");
                return;
            }
            voo.DataHora = dataHora;

            Console.WriteLine("+-----------------------------+");
            Console.WriteLine("| Informe o novo preço:       |");
            Console.WriteLine("+-----------------------------+");
            if (!decimal.TryParse(Console.ReadLine(), out decimal preco))
            {
                Console.WriteLine("Preço inválido!");
                return;
            }
            voo.Preco = preco;

            AtualizarVoo(voo);
        }
        public static void AtualizarVoo(Voo voo)
        {
            string connectionString = Program.DataBaseString();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string sql = "UPDATE VOO SET Origem = @Origem, Destino = @Destino, DataHora = @DataHora, Preco = @Preco WHERE VooId = @Id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Origem", voo.Origem);
                        command.Parameters.AddWithValue("@Destino", voo.Destino);
                        command.Parameters.AddWithValue("@DataHora", voo.DataHora);
                        command.Parameters.AddWithValue("@Preco", voo.Preco);
                        command.Parameters.AddWithValue("@Id", voo.VooId);

                        int linhasAfetadas = command.ExecuteNonQuery();
                        if (linhasAfetadas > 0)
                        {
                            Console.WriteLine("Voo atualizado com sucesso!");
                        }
                        else
                        {
                            Console.WriteLine("Erro ao atualizar voo.");
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
        public static void ExcluirVoo()
        {
            Console.Clear();
            Console.WriteLine("+-----------------------------+");
            Console.WriteLine("| Informe o ID do voo:        |");
            Console.WriteLine("+-----------------------------+");
            if (!int.TryParse(Console.ReadLine(), out int vooId))
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
                    string sql = "DELETE FROM VOO WHERE VooId = @Id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Id", vooId);

                        int linhasAfetadas = command.ExecuteNonQuery();
                        if (linhasAfetadas > 0)
                        {
                            Console.WriteLine("Voo excluído com sucesso!");
                        }
                        else
                        {
                            Console.WriteLine("Erro ao excluir voo.");
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
        public static void ExibirVoo()
        {
            Console.Clear();
            Console.WriteLine("+-----------------------------+");
            Console.WriteLine("| Informe o ID do voo:        |");
            Console.WriteLine("+-----------------------------+");
            if (!int.TryParse(Console.ReadLine(), out int vooId))
            {
                Console.WriteLine("ID inválido!");
                return;
            }

            Voo voo = BuscarVooPorId(vooId);
            if (voo == null)
            {
                Console.WriteLine("Voo não encontrado!");
                return;
            }

            Console.WriteLine($"\nID: {voo.VooId}");
            Console.WriteLine($"Origem: {voo.Origem}");
            Console.WriteLine($"Destino: {voo.Destino}");
            Console.WriteLine($"Data e Hora: {voo.DataHora:dd/MM/yyyy HH:mm}");
            Console.WriteLine($"Preço: {voo.Preco:C}");

            Console.ReadKey(true);
            Program.MenuPrincipal();
        }
        public static Voo BuscarVooPorId(int vooId)
        {
            string connectionString = Program.DataBaseString();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string sql = "SELECT * FROM VOO WHERE VooId = @Id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Id", vooId);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Voo
                                {
                                    VooId = reader.GetInt32(0),
                                    Origem = reader.GetString(1),
                                    Destino = reader.GetString(2),
                                    DataHora = reader.GetDateTime(3),
                                    Preco = reader.GetDecimal(4)
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
