using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Monopoly.Data;
using Monopoly.Models;

namespace Monopoly.Utilities
{
    public class ConnectionsDatabaseOperations
    {
        public ConnectionsDatabaseOperations() { }


        public async Task AddRoom(ConnectionIds room)
        {
            var connectionIdsContext = new MonopolyDbContext();
            connectionIdsContext.ConnectionIds.Add(room);
            connectionIdsContext.SaveChanges();
        }

        public List<string> GetRoomConnections(int roomId)
        {
            List<string> connectionsIds = new List<string>();
            var connectionIdsContext = new MonopolyDbContext();
            List<ConnectionIds> connections = connectionIdsContext.ConnectionIds.Where(x => x.RoomId == roomId).ToList();
            foreach(ConnectionIds connection in connections)
            {
                connectionsIds.Add(connection.connectionId);
            }
            return connectionsIds;
        }

        public int GetRoomId(string connection)
        {
            var connectionIdsContext = new MonopolyDbContext();
            ConnectionIds room = connectionIdsContext.ConnectionIds.Where(x => x.connectionId == connection).FirstOrDefault();
            return room.RoomId;
        }

        public string getConnectionForPlayer(string playerName, int roomId)
        {
            List<string> connectionsIds = new List<string>();
            var connectionIdsContext = new MonopolyDbContext();
            return connectionIdsContext.ConnectionIds.Where(x => x.RoomId == roomId && x.PlayerName == playerName).FirstOrDefault().connectionId;
        }
    }
}