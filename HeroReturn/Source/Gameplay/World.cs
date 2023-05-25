#region Includes
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using HeroReturn;
using System.Diagnostics;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Sprites;
using System.Reflection.Metadata;
using MonoGame.Extended.Content;
using Assimp;
#endregion

namespace HeroReturn
{
    
    public class World
    {
        public Basic2d background;
        public Basic2d firstHouse;
        public Basic2d secondHouse;
        public Basic2d thirdHouse;
        public SpriteSheet collectorWalkSpriteSheet, collectorActionSpriteSheet;
        public SpriteSheet woodcutterWalkSpriteSheet, woodcutterActionSpriteSheet;
        public SpriteSheet knightWalkSpriteSheet, knightActionSpriteSheet;
        public SpriteSheet enemyWalkSpriteSheet, enemyActionSpriteSheet;
        public SpriteSheet treeSpriteSheet, vegetableSpriteSheet;
        public List<Collector> heroes = new();
        public List<Woodcutter> woodcutters = new();
        public List<Vegetable> vegetables = new();
        public List<Tree> trees = new();
        public Queue<Knight> knights = new();
        public Queue<Enemy> enemies = new();
        public Queue<Enemy> enemiesPoolList = new();

        public SoundEffect getMoneySound;
        public SoundEffect woodcutterActionSound;
        public SoundEffect knightActionSound;
        public SoundEffect defeat;
        public SoundEffect victory;

        public GameStats gameStats;

        public int enemyDelay = 0;

        public Vector2[] vegetablesCoordinates = { new Vector2(288, 200), new Vector2(335, 184), new Vector2(380, 195), new Vector2(425, 179), new Vector2(470, 190), new Vector2(520, 200) };
        public Vector2[] treesCoordinates = { new Vector2(596, 91), new Vector2(723, 91), new Vector2(849, 91) };
        public int[] enemiesCounts = { 2, 4, 5, 7, 10, 15 };
        public int[] collectorCounts = { 1, 2, 3 };
        public int[] woodcutterCounts = { 1, 1, 2 };
        public int[] knightCounts = { 2, 2, 3 };
        public int[] rewardsCounts = { 200, 300, 400, 700, 1000, 1500 };

        public UI ui;

        PassObject GoTo;

        public World(GameStats gameStats, SoundEffect pressKey, PassObject GoTo)
        {
            this.gameStats = gameStats;
            var font = Globals.content.Load<SpriteFont>("Fonts/WorldMoneyFont");
            background = new Basic2d("2D\\Backgrounds\\BACKGROUND_Game", new Vector2(640, 360), new Vector2(1280, 720));
            firstHouse = new Basic2d("2D\\Objects\\OBJECTS_FirstHouse", new Vector2(640, 360), new Vector2(1280, 720));
            secondHouse = new Basic2d("2D\\Objects\\OBJECTS_SecondHouse", new Vector2(640, 360), new Vector2(1280, 720));
            thirdHouse = new Basic2d("2D\\Objects\\OBJECTS_ThirdHouse", new Vector2(640, 360), new Vector2(1280, 720));
            collectorWalkSpriteSheet = Globals.content.Load<SpriteSheet>("2D\\Heroes\\Collector\\Collector_Walk.sf", new JsonContentLoader());
            collectorActionSpriteSheet = Globals.content.Load<SpriteSheet>("2D\\Heroes\\Collector\\Collector_Collect.sf", new JsonContentLoader());

            woodcutterWalkSpriteSheet = Globals.content.Load<SpriteSheet>("2D\\Heroes\\Woodcutter\\Woodcutter_Walk.sf", new JsonContentLoader());
            woodcutterActionSpriteSheet = Globals.content.Load<SpriteSheet>("2D\\Heroes\\Woodcutter\\Woodcutter_Action.sf", new JsonContentLoader());

            knightWalkSpriteSheet = Globals.content.Load<SpriteSheet>("2D\\Heroes\\Knight\\Knight_Walk.sf", new JsonContentLoader());
            knightActionSpriteSheet = Globals.content.Load<SpriteSheet>("2D\\Heroes\\Knight\\Knight_Action.sf", new JsonContentLoader());

            enemyWalkSpriteSheet = Globals.content.Load<SpriteSheet>("2D\\Heroes\\Lizard\\Lizard_Walk.sf", new JsonContentLoader());
            enemyActionSpriteSheet = Globals.content.Load<SpriteSheet>("2D\\Heroes\\Lizard\\Lizard_Action.sf", new JsonContentLoader());

            treeSpriteSheet = Globals.content.Load<SpriteSheet>("2D\\Objects\\Tree\\Trees.sf", new JsonContentLoader());
            vegetableSpriteSheet = Globals.content.Load<SpriteSheet>("2D\\Objects\\Vegetable\\Vegetables.sf", new JsonContentLoader());


            getMoneySound = Globals.content.Load<SoundEffect>("Sounds\\Effects\\GetMoney");
            woodcutterActionSound = Globals.content.Load<SoundEffect>("Sounds\\Effects\\WoodcutterActionSound");
            knightActionSound = Globals.content.Load<SoundEffect>("Sounds\\Effects\\SwordAttack");
            defeat = Globals.content.Load<SoundEffect>("Sounds\\Effects\\Defeat");
            victory = Globals.content.Load<SoundEffect>("Sounds\\Effects\\Victory");

            Restart();

            GameGlobals.PassHeroes = AddCollector;
            GameGlobals.PassWoodcutter = AddWoodcutter;
            GameGlobals.PassKnight = AddKnight;
            GameGlobals.PassVegetable = AddVegetable;

            this.GoTo = GoTo;

            ui = new UI(AddCollector, AddWoodcutter, AddKnight, font, gameStats, pressKey);
        }

        private void Restart()
        {
            heroes.Clear();
            woodcutters.Clear();
            knights.Clear();
            enemies.Clear();
            enemiesPoolList.Clear();
            vegetables.Clear();
            trees.Clear();
            gameStats.finance.worldMoney = 200;
            enemyDelay = 0;

            for (int i = 0; i < 6; i++)
                AddVegetable(new Vegetable(vegetableSpriteSheet, vegetablesCoordinates[i]));

            for(int i = 0; i<3; i++)
                AddTree(new Tree(treeSpriteSheet, treesCoordinates[i]));

            //for (int i = 0; i < enemiesCounts[gameStats.act-1]; i++)
                //AddEnemy(null);

            for (int i = 0; i < enemiesCounts[gameStats.act - 1]; i++)
                AddEnemyToPool(null);

        }

        public virtual void Update(Vector2 backgroundOffset, Vector2 heroOffset, float deltaSeconds)
        {
            background.Update(backgroundOffset);

            if(enemyDelay <= 1000)
                enemyDelay += 1;

            if (enemyDelay > 1000)
            {
                if(enemies.Count < 5 && enemiesPoolList.Count > 0)
                {
                    AddEnemy(null);
                    enemyDelay = 0;
                }
            }

            if (enemiesPoolList.Count == 0 && enemies.Count == 0)
            {
                
                victory.Play();
                gameStats.finance.gameMoney += rewardsCounts[gameStats.act - 1];

                gameStats.act += 1;
                Restart();

                Engine.MetaData.UpdateData(gameStats.act, gameStats.finance.gameMoney, gameStats.houseUpgrade, gameStats.treesUpgrade, gameStats.vegetableUpgrade);
                GoTo(GameState.Map);
            }
                

            if (enemies.Count != 0 && heroes.Count == 0 && woodcutters.Count == 0 && knights.Count == 0 && (gameStats.finance.worldMoney < 100)) // Проигрыш
            {
                Restart();
                defeat.Play();
                Engine.MetaData.UpdateData(gameStats.act, gameStats.finance.gameMoney, gameStats.houseUpgrade, gameStats.treesUpgrade, gameStats.vegetableUpgrade);
                GoTo(GameState.Map);
            }

            for (int i = 0; i < heroes.Count; i++)
            {
                heroes[i].Update(deltaSeconds, vegetables);
            }

            for (int i = 0; i < woodcutters.Count; i++)
            {
                woodcutters[i].Update(deltaSeconds, trees);
            }

            foreach (var knight in knights)
            {
                if (knight.hp <= 0)
                {
                    knights.Dequeue();
                    break;
                }
            }

            foreach (var enemy in enemies)
            {
                if (enemy.hp <= 0)
                {
                    enemies.Dequeue();
                    break;
                }
            }

            var knightNumber = 0;
            foreach (var knight in knights)
            {
                knight.Update(deltaSeconds, knightNumber, enemies);
                knightNumber += 1;
            }

            var enemyNumber = 0;
            foreach (var enemy in enemies)
            {
                enemy.Update(deltaSeconds, enemyNumber, knights);
                enemyNumber += 1;
            }



            for (int i = 0; i < vegetables.Count; i++)
            {
                vegetables[i].Update(deltaSeconds, gameStats.vegetableUpgrade);
            }

            for (int i = 0; i < trees.Count; i++)
            {
                trees[i].Update(deltaSeconds, gameStats.treesUpgrade);
            }

            ui.Update(this);
        }

        public virtual void AddCollector(object info)
        {
            if(gameStats.finance.worldMoney >= FinanceStats.CollectorCost && heroes.Count < collectorCounts[gameStats.houseUpgrade-1])
            {
                gameStats.finance.worldMoney -= FinanceStats.CollectorCost;
                heroes.Add(new Collector(collectorWalkSpriteSheet, collectorActionSpriteSheet, getMoneySound, gameStats));
            }
        }

        public virtual void AddWoodcutter(object info)
        {
            if (gameStats.finance.worldMoney >= FinanceStats.WoodcutterCost && woodcutters.Count < woodcutterCounts[gameStats.houseUpgrade-1])
            {
                gameStats.finance.worldMoney -= FinanceStats.WoodcutterCost;
                woodcutters.Add(new Woodcutter(woodcutterWalkSpriteSheet, woodcutterActionSpriteSheet, getMoneySound, woodcutterActionSound, gameStats));
            }
        }

        public virtual void AddKnight(object info)
        {
            if (gameStats.finance.worldMoney >= FinanceStats.KnightCost && knights.Count < knightCounts[gameStats.houseUpgrade-1])
            {
                gameStats.finance.worldMoney -= FinanceStats.KnightCost;
                knights.Enqueue(new Knight(knightWalkSpriteSheet, knightActionSpriteSheet, getMoneySound, knightActionSound, gameStats));
            }
        }

        public virtual void AddEnemy(object info)
        {
            enemies.Enqueue(enemiesPoolList.Dequeue());
        }

        public virtual void AddEnemyToPool(object info)
        {
            enemiesPoolList.Enqueue(new Enemy(enemyWalkSpriteSheet, enemyActionSpriteSheet, getMoneySound, knightActionSound, gameStats));
        }

        public virtual void AddVegetable(object info)
        {
            vegetables.Add((Vegetable) info);
        }

        public virtual void AddTree(object info)
        {
            trees.Add((Tree)info);
        }

        public virtual void Draw(Vector2 backgroundOffset, Vector2 heroOffset)
        {
            background.Draw(backgroundOffset);
            if (gameStats.houseUpgrade == 1)
                firstHouse.Draw(new Vector2(0, 0));
            if (gameStats.houseUpgrade == 2)
                secondHouse.Draw(new Vector2());
            if (gameStats.houseUpgrade == 3)
                thirdHouse.Draw(new Vector2());

            for (int i = 0; i < vegetables.Count; i++)
            {
                vegetables[i].Draw();
            }

            for (int i = 0; i < trees.Count; i++)
            {
                trees[i].Draw();
            }

            for (int i = 0; i < heroes.Count; i++)
            {
                heroes[i].Draw();
            }

            for (int i = 0; i < woodcutters.Count; i++)
            {
                woodcutters[i].Draw();
            }

            for (int i = 0; i < knights.Count; i++)
            {
                knights.ToArray()[i].Draw();
            }

            for (int i = 0; i < enemies.Count; i++)
            {
                enemies.ToArray()[i].Draw();
            }

            ui.Draw();
        }
    }
}
