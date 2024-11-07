using System.Collections.Generic;
using System.Linq;
using InterviewTakeawayTask2.Models;

namespace InterviewTakeawayTask2.Repositories
{
    public class UserRepository
    {
        private readonly List<User> _users;
        private int _nextId = 1;

        public UserRepository()
        {
            // Initializing with some dummy users
            _users = new List<User>
            {
                new User { Id = _nextId++, Username = "alice@example.com", Password="Password", Name = "Alice", Email = "alice@example.com", Age = 25 },
                new User { Id = _nextId++, Username = "bob@example.com", Password="Password", Name = "Bob", Email = "bob@example.com", Age = 30 }
            };
        }

        // Get all users
        public List<User> GetAllUsers() => _users;

        // Get a user by ID
        public User GetUserById(int id) => _users.FirstOrDefault(u => u.Id == id);

        // Create a new user
        public User CreateUser(string name, string password, string email, int age)
        {
            var user = new User
            {
                Id = _nextId++,
                Username = email,
                Password = password,
                Name = name,
                Email = email,
                Age = age
            };

            _users.Add(user);
            return user;
        }

        //Update a User
        public User UpdateUser(User user)
        {
            var existingUser = _users.Find(x => x.Id == user.Id);

            // This is probably not needed as there is a validation check in the user controller but it's still nice to have another validation step
            if (existingUser != null)
            {
                existingUser.Username = user.Username;
                existingUser.Name = user.Name;
                existingUser.Password = user.Password;
                existingUser.Email = user.Email;
                existingUser.Age = user.Age;
            }

            return existingUser;
        }
    }
}
