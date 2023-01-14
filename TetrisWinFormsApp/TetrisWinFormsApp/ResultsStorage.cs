using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisWinFormsApp
{
    public class ResultsStorage
    {
        private readonly string FilePath = "Таблица всех игроков.json";

        public void SaveResultOfTesting(User user)
        {
            var userResults = GetUsersResults();
            userResults.Add(user);
            Save(userResults);
        }
        public List<User> GetUsersResults()
        {
            if (!File.Exists(FilePath))
            {
                return new List<User>();
            }
            var fileData = FilePath.GetInformation();
            var userResults = JsonConvert.DeserializeObject<List<User>>(fileData);
            return userResults;
        }

        private void Save(List<User> usersResults)
        {
            var JsonData = JsonConvert.SerializeObject(usersResults);
            JsonData.SaveInformation(FilePath);
        }
    }
}
