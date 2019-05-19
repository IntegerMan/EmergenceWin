using MattEland.Emergence.Model;

namespace MattEland.Emergence.LevelData
{
    public class RoomPlacement
    {
        private readonly RoomData _room;
        private readonly Position _upperLeftCorner;

        public RoomPlacement(RoomData room, Position upperLeftCorner)
        {
            _room = room;
            _upperLeftCorner = upperLeftCorner;
        }

        public char GetChar(Position pos, char currentChar)
        {
            var relativePos = pos.Subtract(_upperLeftCorner);
            char roomChar = _room.GetCharacterAtPosition(relativePos);
            return MergeChars(currentChar, roomChar);
        }

        private static char MergeChars(char oldChar, char newChar) => oldChar == '+' || newChar == ' ' ? oldChar : newChar;
    }

}