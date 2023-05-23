using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroReturn
{
    internal enum GameState
    {

        StartMenu, 
        FirstUpgrade,
        Game,
        GameMenu,
        Map,
        SecondUpgrade
    }

    public class FinanceStats
    {
        public const int CollectorCost = 100;
        public const int WoodcutterCost = 200;
        public const int KnightCost = 100;
        public const int ArcherCost = 300;

        public const int VegetableUpdateFirstLevelCost = 50;
        public const int VegetableUpdateSecondLevelCost = 100;
        public const int VegetableUpdateThirdLevelCost = 200;

        public const int TreeUpdateFirstLevelCost = 100;
        public const int TreeUpdateSecondLevelCost = 300;
        public const int TreeUpdateThirdLevelCost = 500;

        public const int HouseUpdateFirstLevelCost = 300;
        public const int HouseUpdateSecondLevelCost = 500;
        public const int HouseUpdateThirdLevelCost = 1000;

        public const int FirstActEnemyCount = 2;
        public const int SecondActEnemyCount = 3;
        public const int ThirdActEnemyCount = 5;
        public const int FourthActEnemyCount = 8;
        public const int FiveActEnemyCount = 10;


        public const int FirstActReward = 200;
        public const int SecondActReward = 300;
        public const int ThirdActReward = 400;
        public const int FourthActReward = 700;
        public const int FiveActReward = 1000;
    }
}
