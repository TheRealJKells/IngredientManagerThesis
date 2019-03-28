using LiteDB;
using System;
using System.Collections.Generic;

namespace LoginNavigation
{
	public class User
	{
        [BsonId]
        public Guid ID { get; set; }

		public string Username { get; set; }

		public string Password { get; set; }

		public string Email { get; set; }

        public List<Guid> RecipesUsed { get; set; }
        public List<Guid> RecipesCreated { get; set; }
	}
}
