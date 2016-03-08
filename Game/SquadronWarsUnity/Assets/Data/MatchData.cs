using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.GameClasses;
using Action = System.Action;

namespace Assets.Data
{
    class MatchData
    {
        private DbConnection _dbConnection = new DbConnection();
        public List<Action> _actions { get; set; } 
        public List<BattleAction> _battleActions { get; set; }

        public void SendMatchData()
        {
            JSONObject matchData = new JSONObject();
            matchData.Add(_dbConnection.ConvertToJson(_actions));
            matchData.Add(_dbConnection.ConvertToJson(_battleActions));
            matchData = _dbConnection.WrapJsonInGameObject(matchData);

            _dbConnection.ExecuteApiCall(GlobalConstants.CheckGameStatusUrl, _dbConnection.CreatePostForm(matchData));
        }
    }
}
