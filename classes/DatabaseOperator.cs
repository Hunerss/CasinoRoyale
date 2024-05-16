using MySqlConnector;
using Org.BouncyCastle.Crypto.Digests;
using System;
using System.Collections.ObjectModel;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography;
using CasinoRoyale.windows.pages;

namespace ToDoList.classes
{
    class DatabaseOperator
    {
        private static string db_adress = "SERVER=localhost;DATABASE=casino_royale;UID=root;PASSWORD=;ConvertZeroDateTime=True;";
        private static MySqlConnection connector = new(db_adress);
        // admin password admin in sha3-512

        public Boolean Add(string login, string name, string desc, DateTime date)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(desc))
                return false;

            try
            {
                connector.Open();
                string query1 = "SELECT id FROM user WHERE login=@Login";
                using MySqlCommand command1 = new(query1, connector);
                command1.Parameters.AddWithValue("@Login", login);
                int userId = Convert.ToInt32(command1.ExecuteScalar());
                if (userId > 0 || userId == -1)
                {
                    string query2 = "INSERT INTO event(user_id, name, descr, finish_date) VALUES (@UserId, @Name, @Desc, @Finish)";
                    using MySqlCommand command2 = new(query2, connector);
                    command2.Parameters.AddWithValue("@UserId", userId);
                    command2.Parameters.AddWithValue("@Name", name);
                    command2.Parameters.AddWithValue("@Desc", desc);
                    string formattedDate = date.ToString("yyyy-MM-dd HH:mm:ss");
                    command2.Parameters.AddWithValue("@Finish", formattedDate);
                    command2.ExecuteNonQuery();
                    Console.WriteLine("DatabaseOperator - Add - succes log - Event addded successfully");
                    return true;
                }
                else
                {
                    Console.WriteLine("DatabaseOperator - Add - error log - Error occured when adding the event");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("DatabaseOperator - Add - error log - Failed to add event");
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
            CheckUserEvents(login);
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
                return false;

            try
            {
                connector.Open();
                string querry = "SELECT id FROM user WHERE login = @Login AND password = @Password";
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

        public Boolean ValidateUser(string login)
        {
            if (string.IsNullOrEmpty(login))
                return false;

            try
            {
                connector.Open();
                string query = "SELECT COUNT(*) FROM user WHERE login = @Login";
                using MySqlCommand command = new(query, connector);
                command.Parameters.AddWithValue("@Login", login);

                int userCount = Convert.ToInt32(command.ExecuteScalar());
                if (userCount == 1)
                {
                    Console.WriteLine("DatabaseOperator - ValidateUser - succes log - User data is fine");
                    return true;
                }
                else if (userCount > 1)
                {
                    Console.WriteLine("DatabaseOperator - ValidateUser - error log - This login is used by more than one user");
                    return false;
                }
                else
                {
                    Console.WriteLine("DatabaseOperator - ValidateUser - error log - This user dosen't exist");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("DatabaseOperator - ValidateUser - error log - Failed to validate data");
                Console.WriteLine("DatabaseOperator - ValidateUser - exception message - " + ex.Message);
                return false;
            }
            finally
            {
                connector.Close();
            }
        }

        public Boolean Modify(int id, string name, string desc, DateTime date)
        {
            if (id < 0) return false;
            try
            {
                connector.Open();
                string query = "UPDATE event SET name=@Name,descr=@Desc,finish_date=@Finish WHERE id = @Id";
                using MySqlCommand command = new(query, connector);
                command.Parameters.AddWithValue("@Id", id);
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@Desc", desc);
                string formattedDate = date.ToString("yyyy-MM-dd HH:mm:ss");
                command.Parameters.AddWithValue("@Finish", formattedDate);
                command.ExecuteNonQuery();
                
                Console.WriteLine("DatabaseOperator - Modify - succes log - Task modified successfully");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("DatabaseOperator - Modify - error log - Failed to modify task");
                Console.WriteLine("DatabaseOperator - Modify - exception message - " + ex.Message);
                return false;
            }
            finally
            {
                connector.Close();
            }
        }

        private Boolean CheckUserData(string login, string name, string surname, int age)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(name) || age <= 0)
                return false;

            try
            {
                connector.Open();
                string querry = "SELECT * FROM user WHERE (name = @Name AND surname = @Surname AND age = @Age) OR login = @Login";
                using MySqlCommand command = new(querry, connector);
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@Surname", surname);
                command.Parameters.AddWithValue("@Age", age);
                command.Parameters.AddWithValue("@Login", login);
                using MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    Console.WriteLine("DatabaseOperator - CheckUserData - error log - An account with these personal data already exists");
                    return false;
                }
                Console.WriteLine("DatabaseOperator - CheckUserData - succes log - Users data is fine");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("DatabaseOperator - CheckUserData - error log - Failed to check user data");
                Console.WriteLine("DatabaseOperator - CheckUserData - exception message - " + ex.Message);
                return false;
            }
            finally
            {
                connector.Close();
            }
        }

        private Boolean CheckUserEvents(string login)
        {
            if (string.IsNullOrEmpty(login))
                return false;

            try
            {
                connector.Open();
                string query1 = "SELECT event.id, event.finish_date FROM event JOIN user ON event.user_id = user.id WHERE user.login = @Login";
                using MySqlCommand command = new(query1, connector);
                command.Parameters.AddWithValue("@Login", login);
                using MySqlDataReader reader = command.ExecuteReader();
                DateTime now = DateTime.Now;
                while (reader.Read())
                    if (reader.GetDateTime(1) > now)
                        Fail(reader.GetInt32(0), login);
                Console.WriteLine("DatabaseOperator - CheckUserEvents - succes log - Users events are checked and if needed corected");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("DatabaseOperator - CheckUserEvents - error log - Failed to check user events");
                Console.WriteLine("DatabaseOperator - CheckUserEvents - exception message - " + ex.Message);
                return false;
            }
            finally
            {
                connector.Close();
            }
        }

        public Boolean Register(string login, string password, string name, string surname, int age)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password) ||
                string.IsNullOrEmpty(name) || string.IsNullOrEmpty(surname) || age <= 0)
                return false;
            if (!CheckUserData(login, name, surname, age))
                return false;

            try
            {
                connector.Open();
                string querry = "INSERT INTO user(login, password, name, surname, age, creation_date, created_tasks, finished_tasks, failed_tasks) " +
                                "VALUES(@Login, @Password, @Name, @Surname, @Age, @CreationDate, @CreatedTasks, @FinishedTasks, @FailedTasks)";
                using MySqlCommand command = new(querry, connector);
                command.Parameters.AddWithValue("@Login", login);
                command.Parameters.AddWithValue("@Password", HashPassword(password));
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@Surname", surname);
                command.Parameters.AddWithValue("@Age", age);
                command.Parameters.AddWithValue("@CreationDate", DateTimeOffset.UtcNow.ToUnixTimeSeconds());
                command.Parameters.AddWithValue("@CreatedTasks", 0);
                command.Parameters.AddWithValue("@FinishedTasks", 0);
                command.Parameters.AddWithValue("@FailedTasks", 0);
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

        public Boolean Finish(int id, string login)
        {
            if (id < 0) return false;

            try
            {
                connector.Open();
                string query1 = "DELETE FROM event WHERE id = @Id";
                string query2 = "UPDATE user SET finished_tasks=finished_tasks+1 WHERE login = @Login";
                using MySqlCommand command1 = new(query1, connector);
                command1.Parameters.AddWithValue("@Id", id);
                command1.ExecuteNonQuery();
                using MySqlCommand command2 = new(query2, connector);
                command2.Parameters.AddWithValue("@Login", login);
                command2.ExecuteNonQuery();
                Console.WriteLine("DatabaseOperator - Finish - succes log - Task finished successfully");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("DatabaseOperator - Finish - error log - Failed to finish task");
                Console.WriteLine("DatabaseOperator - Finish - exception message - " + ex.Message);
                return false;
            }
            finally
            {
                connector.Close();
            }
        }

        public Boolean Fail(int id, string login)
        {
            if (id < 0) return false;

            try
            {
                connector.Open();
                string query1 = "DELETE FROM event WHERE id = @Id";
                string query2 = "UPDATE user SET failed_tasks=failed_tasks+1 WHERE login = @Login";
                using MySqlCommand command1 = new(query1, connector);
                command1.Parameters.AddWithValue("@Id", id);
                command1.ExecuteNonQuery();
                using MySqlCommand command2 = new(query2, connector);
                command2.Parameters.AddWithValue("@Login", login);
                command2.ExecuteNonQuery();
                Console.WriteLine("DatabaseOperator - Fail - succes log - Task failed successfully");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("DatabaseOperator - Fail - error log - Failed to finish task");
                Console.WriteLine("DatabaseOperator - Fail - exception message - " + ex.Message);
                return false;
            }
            finally
            {
                connector.Close();
            }
        }

        //public ObservableCollection<Event> GetEvents(string login)
        //{
        //    if (string.IsNullOrEmpty(login))
        //        return null;

        //    try
        //    {
        //        connector.Open();
        //        string querry = "SELECT event.id, event.name, event.descr,event.creation_date, event.finish_date FROM event " +
        //                       "JOIN user ON event.user_id = user.id WHERE user.login = @Login ORDER BY event.finish_date ASC";
        //        using MySqlCommand command = new(querry, connector);
        //        command.Parameters.AddWithValue("@Login", login);
        //        using MySqlDataReader reader = command.ExecuteReader();
        //        ObservableCollection<Event> events = new ObservableCollection<Event>();
        //        while (reader.Read())
        //        {
        //            events.Add(new Event
        //            {
        //                Id = reader.GetInt32(0),
        //                Name = reader.GetString(1),
        //                Description = reader.GetString(2),
        //                CreationDate = reader.GetDateTime(3),
        //                FinishDate = reader.GetDateTime(4)
        //            });
        //            Console.WriteLine("DatabaseOperator - GetEvents - succes log - Event retrived");
        //        }
        //        connector.Close();
        //        Console.WriteLine("DatabaseOperator - GetEvents - succes log - Events retrived successfully");
        //        return events;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("DatabaseOperator - GetEvents - error log - Failed to get events");
        //        Console.WriteLine("DatabaseOperator - GetEvents - exception message - " + ex.Message);
        //        return null;
        //    }
        //}

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
