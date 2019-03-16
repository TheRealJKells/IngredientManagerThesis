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
                        Password = "a",
                        RecipesUsed = new List<int>(),
                        RecipesCreated = new List<int>(),
                        Email = "user@carthage.edu",

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
        //Get recipes the user has created
        public List<Recipe> GetRecipesBySessionID(int ID)
        {
            List<Recipe> recipes = new List<Recipe>();
            foreach (int i in App.sessionUser.RecipesCreated)
            {
                recipes.Add(GetRecipesByRecipeID(ID));
            }
           
            return recipes;
        }

        //Get user by username and password
        public User GetUser(User loginUser)
        {
            userCollection.EnsureIndex(x => x.Username);
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

        //get user by ID
        public User GetUserByID(int ID)
        {
            userCollection.EnsureIndex(x => x.ID);
            var User = userCollection.Find(Query.Where("ID", i => i.Equals(ID))).FirstOrDefault();

            return User;
        }

        //get user by email (check to see if it is taken)
        public bool GetUserByEmail(User user)
        {
            bool retVal = true;
            userCollection.EnsureIndex(x => x.Email);
            var User = userCollection.Find(Query.Where("Email", i => i.Equals(user.Email))).FirstOrDefault();
            if (User != null)
            {
                retVal = false;
            }

            return retVal;
        }

        //get user by username (check to see if it is taken)
        public bool getUserByUsername(string userName)
        {
            bool retVal = true;
            userCollection.EnsureIndex(x => x.Username);
            var User = userCollection.Find(Query.Where("Username", i => i.Equals(userName))).FirstOrDefault();

            if (User != null)
            {
                retVal = false;
            }

            return retVal;
        }
        //return user by username
        public User getUserByUsernameUser(string userName)
        {
            userCollection.EnsureIndex(x => x.Username);
            var User = userCollection.Find(Query.Where("Username", i => i.Equals(userName))).FirstOrDefault();

            return User;
        }
        //Delete user
        public void DeleteUser(int ID)
        {
            userCollection.Delete(a => a.ID == ID);
        }

        //Recipes functions
        //*******************************************
        public Recipe GetRecipesByRecipeID(int ID)
        {
            recipeCollection.EnsureIndex(i => i.ID);
            var recipe = recipeCollection.Find(Query.Where("ID", i => i.Equals(ID))).FirstOrDefault();

            return recipe;
        }
        //get all recipes
        public List<Recipe> GetAllRecipes()
        {
            List<Recipe> recipes = new List<Recipe>(recipeCollection.FindAll());

            return recipes;
        }

        public void addRecipe(Recipe recipe)
        {
            recipeCollection.Insert(recipe);
        }
    }
}
    