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
            //db.DropCollection("Users");
            //db.DropCollection("Recipes");
            List<User> users = new List<User>();
            List<Recipe> recipes = new List<Recipe>();
            users = GetAllUsers();
            if (users.Count == 0)
            {
                User user = new User()
                {
                    Username = "user",
                    Password = "a",
                    RecipesUsed = new List<Guid>(),
                    RecipesCreated = new List<Guid>(),
                    Email = "user@carthage.edu",

                };
                Adduser(user);
            }
            
        }

        public List<Guid> GetSessionRecipesCreated()
        {
            List<Guid> recipesCreated = new List<Guid>(App.SessionUser.RecipesCreated);

            return recipesCreated;
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
        public List<Recipe> GetRecipesBySessionID()
        {
            List<Recipe> recipes = new List<Recipe>();
            foreach (Guid i in App.SessionUser.RecipesCreated)
            {
                
                recipes.Add(GetRecipesByRecipeID(i));
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
        public User GetUserByID(Guid ID)
        {
            userCollection.EnsureIndex(x => x.ID);
            User User = userCollection.Find(Query.Where("ID", 
                i => i.Equals(ID)))
                .FirstOrDefault();

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
        public bool GetUserByUsername(string userName)
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
        public User GetUserByUsernameUser(string userName)
        {
            userCollection.EnsureIndex(x => x.Username);
            var User = userCollection.Find(Query.Where("Username", i => i.Equals(userName))).FirstOrDefault();

            return User;
        }
        //Delete user
        public void DeleteUser(Guid ID)
        {
            userCollection.Delete(a => a.ID == ID);
        }

        //Recipes functions
        //*******************************************
        public IEnumerable<Recipe> GetRecipesByIngredientName(List<string> names)
        {
            IEnumerable<Recipe> recipes = recipeCollection.Find(i => i.IngredientList.Contains(names.First()));
            
            int counter = 1;
            List<Recipe> newList = new List<Recipe>();
            while(counter != names.Count)
            {
                newList = new List<Recipe>();
                foreach (Recipe r in recipes)
                {
                    if (r.IngredientList.Contains(names.ElementAt(counter)))
                    {
                        newList.Add(r);
                    }
                }
                recipes = newList;
                counter++;
            }
           
            return recipes;
        }
        public Recipe GetRecipesByRecipeID(Guid ID)
        {
            recipeCollection.EnsureIndex(i => i.ID);
            var recipe = recipeCollection.Find(i => i.ID.Equals(ID)).First();

            return recipe;
        }
        //get all recipes
        public List<Recipe> GetAllRecipes()
        {
            List<Recipe> recipes = new List<Recipe>(recipeCollection.FindAll());

            return recipes;
        }

        public void AddRecipe(Recipe recipe)
        {
            recipeCollection.Insert(recipe);
        }

        public void DeleteRecipe(Guid ID)
        {
            recipeCollection.Delete(a => a.ID == ID);
        }

        public void UpdateRecipe(Recipe recipe)
        {
            recipeCollection.Update(recipe);
        }
    }
}
    