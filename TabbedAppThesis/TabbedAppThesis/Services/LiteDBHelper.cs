using LiteDB;
using LoginNavigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TabbedAppThesis.Models;

namespace TabbedAppThesis.Services
{
    public class LiteDBHelper
    {
        protected LiteCollection<User> userCollection;
        protected LiteCollection<Recipe> recipeCollection;

        public LiteDBHelper(string dbPath)
        {
            var db = new LiteDatabase(dbPath);
            
                userCollection = db.GetCollection<User>("Users");
                recipeCollection = db.GetCollection<Recipe>("Recipes");
                List<User> users = new List<User>();
                users = GetAllUsers();
                if (users.Count == 0)
                {
                    User user = new User()
                    {
                        Username = "user",
                        Password = "a"
                    };
                    Adduser(user);
                }
            

        }

        //Get All users
        public List<User> GetAllUsers()
        {
            List<User> users = new List<User>(userCollection.FindAll());
            return users;
        }

        //Add New user
        public void Adduser(User user)
        {
            userCollection.Insert(user);
        }

        //Update user
        public void UpdateUser(User user)
        {
            userCollection.Update(user);
        }

        //Get user
        public User GetUser(User loginUser)
        {
            //User user = new User(userCollection.Find(Query.Where("Username", i => i.Equals(loginUser.Username))).FirstOrDefault());
            var user = userCollection.Find(Query.Where("Username", i => i.Equals(loginUser.Username))).FirstOrDefault();
            if (user != null)
            {
                if (user.Password != loginUser.Password)
                {
                    user = null;
                }
            }

            //User user = GetAllUsers().Where("Users", a => a.Username == loginUser.Username);
            return user;
        }
        //Delete user
        public void DeleteUser(int ID)
        {
            userCollection.Delete(a => a.ID == ID);
        }

    }
}
    