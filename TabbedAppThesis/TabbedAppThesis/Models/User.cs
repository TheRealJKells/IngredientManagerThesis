using System.Collections.Generic;

namespace LoginNavigation
{
	public class User
	{
        public int ID { get; set; }

		public string Username { get; set; }

		public string Password { get; set; }

		public string Email { get; set; }

        public List<int> RecipesUsed { get; set; }
	}
}
