using CasinoRoyale.windows.pages;
using MySqlConnector;
using Org.BouncyCastle.Crypto.Digests;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Xml.Linq;

namespace ToDoList.classes
{
    class DatabaseOperator
    {
        private static string db_adress = "SERVER=localhost;DATABASE=casino_royale;UID=root;PASSWORD=;ConvertZeroDateTime=True;";
        private static MySqlConnection connector = new(db_adress);
        // admin password admin in sha3-512

        private Boolean ModifyChips(int id, int change)
        {
            if (id < 0) return false;
            try
            {
                connector.Open();
                string query = "UPDATE users SET chips=@Chips WHERE id = @Id";
                using MySqlCommand command = new(query, connector);
                command.Parameters.AddWithValue("@Chips", change);
                command.Parameters.AddWithValue("@Id", id);
                command.ExecuteNonQuery();

                Console.WriteLine("DatabaseOperator - ModifyChips - succes log - Chips amount modified successfully");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("DatabaseOperator - ModifyChips - error log - Failed to modify chips amount");
                Console.WriteLine("DatabaseOperator - ModifyChips - exception message - " + ex.Message);
                return false;
            }
            finally
            {
                connector.Close();
            }
        }

        public Boolean UpdateChips(string login, int change)
        {
            if (string.IsNullOrEmpty(login))
                return false;

            try
            {
                connector.Open();
                string getUserIdQuery = "SELECT id FROM users WHERE login=@Login";
                using MySqlCommand getUserIdCommand = new MySqlCommand(getUserIdQuery, connector);
                getUserIdCommand.Parameters.AddWithValue("@Login", login);
                object userIdObj = getUserIdCommand.ExecuteScalar();

                if (userIdObj == null || userIdObj == DBNull.Value)
                {
                    Console.WriteLine("DatabaseOperator - UpdateChips - error log - User with specified login not found");
                    return false;
                }

                int userId = Convert.ToInt32(userIdObj);

                string updateChipsQuery = "UPDATE users SET chips=chips+@Chips WHERE id = @Id";
                using MySqlCommand updateChipsCommand = new MySqlCommand(updateChipsQuery, connector);
                updateChipsCommand.Parameters.AddWithValue("@Chips", change);
                updateChipsCommand.Parameters.AddWithValue("@Id", userId);
                updateChipsCommand.ExecuteNonQuery();

                Console.WriteLine("DatabaseOperator - UpdateChips - success log - Chips amount updated successfully");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("DatabaseOperator - UpdateChips - error log - Failed to update chips amount");
                Console.WriteLine("DatabaseOperator - UpdateChips - exception message - " + ex.Message);
                return false;
            }
            finally
            {
                connector.Close();
            }
        }

        public int GetChips(string login)
        {
            if (string.IsNullOrEmpty(login))
                return -1;

            try
            {
                connector.Open();
                string query = "SELECT chips FROM users WHERE login = @Login";
                using MySqlCommand command = new MySqlCommand(query, connector);
                command.Parameters.AddWithValue("@Login", login);
                object result = command.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    int chips = Convert.ToInt32(result);
                    Console.WriteLine("DatabaseOperator - GetChips - success log - User chips retrieved successfully");
                    return chips;
                }
                else
                {
                    Console.WriteLine("DatabaseOperator - GetChips - error log - User not found in database");
                    return -1;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("DatabaseOperator - GetChips - error log - Failed to retrieve user chips");
                Console.WriteLine("DatabaseOperator - GetChips - exception message - " + ex.Message);
                return -1;
            }
            finally
            {
                connector.Close();
            }
        }

        public Boolean Add(string login, int game, int win)
        {
            if (string.IsNullOrEmpty(login) || game > 3)
                return false;

            try
            {
                connector.Open();
                string query1 = "SELECT id FROM users WHERE login=@Login";
                using MySqlCommand command1 = new(query1, connector);
                command1.Parameters.AddWithValue("@Login", login);
                int userId = Convert.ToInt32(command1.ExecuteScalar());
                if (userId > 0 || userId == -1)
                {
                    string query2 = "INSERT INTO wins(user_id, game, win) VALUES (@UserId, @Game, @Win)";
                    using MySqlCommand command2 = new(query2, connector);
                    command2.Parameters.AddWithValue("@UserId", userId);
                    command2.Parameters.AddWithValue("@Game", game);
                    command2.Parameters.AddWithValue("@Win", win);
                    command2.ExecuteNonQuery();
                    Console.WriteLine("DatabaseOperator - Add - succes log - Win addded successfully");
                    ModifyChips(userId, win);
                    return true;
                }
                else
                {
                    Console.WriteLine("DatabaseOperator - Add - error log - Error occured when adding the Win");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("DatabaseOperator - Add - error log - Failed to add Win");
                Console.WriteLine("DatabaseOperator - Add - exception message - " + ex.Message);
                return false;
            }
            finally
            {
                connector.Close();
            }
        }

        public Boolean Login(string login, string password)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
                return false;

            try
            {
                connector.Open();
                string querry = "SELECT id FROM users WHERE login = @Login AND password = @Password";
                using MySqlCommand command = new(querry, connector);
                command.Parameters.AddWithValue("@Login", login);
                command.Parameters.AddWithValue("@Password", HashPassword(password));

                using var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    Console.WriteLine("DatabaseOperator - Login - succes log - Logged in successfully");
                    connector.Close();
                    return true;
                }
                connector.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("DatabaseOperator - Login - error log - Failed to log in");
                Console.WriteLine("DatabaseOperator - Login - exception message - " + ex.Message);
                return false;
            }
            connector.Close();
            return false;
        }

        private int ReturnAge(DateTime birthday)
        {
            DateTime now = DateTime.Today;
            int age = now.Year - birthday.Year;
            if (now < birthday.AddYears(age))
            {
                age--;
            }
            return age;
        }

        private static Boolean CheckPassword(string password)
        {
            if (password.Length < 8)
                return false;

            if (!password.Any(char.IsPunctuation))
                return false;

            if (!password.Any(char.IsLower))
                return false;

            if (!password.Any(char.IsUpper))
                return false;

            if (!password.Any(char.IsDigit))
                return false;

            return true;
        }

        public Boolean Register(string login, string password, DateTime birthdate, Boolean licence, Boolean instruction)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password) || birthdate == DateTime.MinValue || birthdate >= DateTime.Now || !licence || !instruction)
                return false;

            if (ReturnAge(birthdate) < 18)
                return false;

            if (!CheckPassword(password))
                return false;

            try
            {
                connector.Open();
                string querry = "INSERT INTO users(login, password, birthday, age, instruction, licence) " +
                                "VALUES(@Login, @Password, @Birthday, @Age, @Instruction, @Licence)";
                using MySqlCommand command = new(querry, connector);
                command.Parameters.AddWithValue("@Login", login);
                command.Parameters.AddWithValue("@Password", HashPassword(password));
                command.Parameters.AddWithValue("@Birthday", birthdate);
                command.Parameters.AddWithValue("@Age", ReturnAge(birthdate));
                command.Parameters.AddWithValue("@Instruction", licence);
                command.Parameters.AddWithValue("@Licence", instruction);
                command.ExecuteNonQuery();
                Console.WriteLine("DatabaseOperator - Register - succes log - Registered successfully");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("DatabaseOperator - Register - error log - Failed to register");
                Console.WriteLine("DatabaseOperator - Register - exception message - " + ex.Message);
                return false;
            }
            finally
            {
                connector.Close();
            }
        }

        private static string HashPassword(string input)
        {
            if (string.IsNullOrEmpty(input))
                return "";

            byte[] inputBytes = System.Text.Encoding.UTF8.GetBytes(input);
            byte[] salt = GenerateSalt();
            byte[] saltedInput = new byte[inputBytes.Length + salt.Length];
            Buffer.BlockCopy(saltedInput, 0, inputBytes, 0, inputBytes.Length);
            Buffer.BlockCopy(salt, 0, saltedInput, inputBytes.Length, salt.Length);

            Sha3Digest sha3 = new(512);
            byte[] outputBytes = new byte[sha3.GetDigestSize()];
            sha3.BlockUpdate(inputBytes, 0, inputBytes.Length);
            sha3.DoFinal(outputBytes, 0);
            return Convert.ToBase64String(outputBytes);
        }

        private static byte[] GenerateSalt()
        {
            byte[] salt = new byte[32];
            using RNGCryptoServiceProvider rng = new();
            rng.GetBytes(salt);
            return salt;
        }
    }
}
