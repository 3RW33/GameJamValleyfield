using System;

namespace Game
{
    [Serializable]
    struct GameData
    {
        Level currentLevel;
        Level highestUnlockedLevel;
    }
}