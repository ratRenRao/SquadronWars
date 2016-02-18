using System;
using Assets.Data;
using Assets.GameClasses;
using Assets.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SquadronWarsUnitTests
{
    [TestClass]
    public class UnitTests
    {
        private Utilities util;
        private DbConnection dbConnection;
        
        [TestInitialize]
        private void Initialize()
        {
            util = new Utilities();     
            dbConnection = new DbConnection();
        }

        [TestMethod]
        public void PlayerPopulationTest()
        {

        }

        [TestMethod]
        public void FlattenedJsonIsNotNull()
        {
            
        }

        [TestMethod]
        public void PlayerApiCallReturnsData()
        {
            var player = new Player();
            player.logins.username = "test";
            player.logins.password = "testing123";
            var url = "http://squadronwars.ddns.net/api/auth";
            var parameters = util.CreatePublicPropertyDictionary(player.logins);
            var response = dbConnection.ExecuteApiCall(url, dbConnection.PopulateParameters(parameters));
            var data = util.DeserializeData(response.text);

            Assert.IsNotNull(data);
        }
    }
}
