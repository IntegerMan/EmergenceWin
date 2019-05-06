using System.Collections.Generic;
using LanguageExt;
using MattEland.Emergence.Domain;
using MattEland.Emergence.LevelData.Properties;
using MattEland.Shared.Collections;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MattEland.Emergence.LevelData
{
    public class RoomDataProvider
    {
        private readonly IDictionary<string, Rooms.RoomData> _rooms = new Dictionary<string, Rooms.RoomData>();

        public RoomDataProvider()
        {
            var prefabs = JsonConvert.DeserializeObject<JArray>(Resources.Prefabs);
            prefabs.Each(p =>
            {
                var room = p.ToObject<Rooms.RoomData>();
                _rooms[room.Id] = room;
            });
        }

        public Rooms.RoomData GetRoomById(string id)
        {
            _rooms.TryGetValue(id, out var room);

            return room;
        }
    }
}
