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
#endregion

namespace HeroReturn
{
    
    public class World
    {
        public Basic2d background;
        public SpriteSheet collectorWalkSpriteSheet, collectorActionSpriteSheet;
        public SpriteSheet woodcutterWalkSpriteSheet, woodcutterActionSpriteSheet;
        public SpriteSheet treeSpriteSheet, vegetableSpriteSheet;
        public List<Collector> heroes = new();
        public List<Woodcutter> woodcutters = new();
        public List<Vegetable> vegetables = new();
        public List<Tree> trees = new();

        public SoundEffect getMoneySound;
        public SoundEffect woodcutterActionSound;

        public FinanceController finance;

        public UI ui;

        public World(FinanceController financeController, SoundEffect pressKey)
        {
            finance = financeController;
            var font = Globals.content.Load<SpriteFont>("Fonts/WorldMoneyFont");
            background = new Basic2d("2D\\Backgrounds\\BACKGROUND_Game", new Vector2(640, 360), new Vector2(1280, 720));
            collectorWalkSpriteSheet = Globals.content.Load<SpriteSheet>("2D\\Heroes\\Collector\\Collector_Walk.sf", new JsonContentLoader());
            collectorActionSpriteSheet = Globals.content.Load<SpriteSheet>("2D\\Heroes\\Collector\\Collector_Collect.sf", new JsonContentLoader());

            woodcutterWalkSpriteSheet = Globals.content.Load<SpriteSheet>("2D\\Heroes\\Woodcutter\\Woodcutter_Walk.sf", new JsonContentLoader());
            woodcutterActionSpriteSheet = Globals.content.Load<SpriteSheet>("2D\\Heroes\\Woodcutter\\Woodcutter_Action.sf", new JsonContentLoader());

            treeSpriteSheet = Globals.content.Load<SpriteSheet>("2D\\Objects\\Tree\\Trees.sf", new JsonContentLoader());
            vegetableSpriteSheet = Globals.content.Load<SpriteSheet>("2D\\Objects\\Vegetable\\Vegetables.sf", new JsonContentLoader());


            getMoneySound = Globals.content.Load<SoundEffect>("Sounds\\Effects\\GetMoney");
            woodcutterActionSound = Globals.content.Load<SoundEffect>("Sounds\\Effects\\WoodcutterActionSound");

            GameGlobals.PassHeroes = AddCollector;
            GameGlobals.PassWoodcutter = AddWoodcutter;
            GameGlobals.PassVegetable = AddVegetable;

            AddVegetable(new Vegetable(vegetableSpriteSheet, new Vector2(288, 200)));
            AddVegetable(new Vegetable(vegetableSpriteSheet, new Vector2(335, 184)));
            AddVegetable(new Vegetable(vegetableSpriteSheet, new Vector2(380, 195)));
            AddVegetable(new Vegetable(vegetableSpriteSheet, new Vector2(425, 179)));
            AddVegetable(new Vegetable(vegetableSpriteSheet, new Vector2(470, 190)));
            AddVegetable(new Vegetable(vegetableSpriteSheet, new Vector2(520, 200)));

            AddTree(new Tree(treeSpriteSheet, new Vector2(596, 91)));
            AddTree(new Tree(treeSpriteSheet, new Vector2(723, 91)));
            AddTree(new Tree(treeSpriteSheet, new Vector2(849, 91)));

            ui = new UI(AddCollector, AddWoodcutter, font, finance, pressKey);
        }

        public virtual void Update(Vector2 backgroundOffset, Vector2 heroOffset, float deltaSeconds)
        {
            background.Update(backgroundOffset);

            for(int i = 0; i < heroes.Count; i++)
            {
                heroes[i].Update(deltaSeconds, vegetables);
            }

            for (int i = 0; i < woodcutters.Count; i++)
            {
                woodcutters[i].Update(deltaSeconds, trees);
            }

            for(int i = 0; i < vegetables.Count; i++)
            {
                vegetables[i].Update(deltaSeconds, 1);
            }

            for (int i = 0; i < trees.Count; i++)
            {
                trees[i].Update(deltaSeconds, 1);
            }

            ui.Update(this);
        }

        public virtual void AddCollector(object info)
        {
            if(finance.worldMoney >= FinanceStats.CollectorCost)
            {
                finance.worldMoney -= FinanceStats.CollectorCost;
                heroes.Add(new Collector(collectorWalkSpriteSheet, collectorActionSpriteSheet, getMoneySound, finance));
            }
        }

        public virtual void AddWoodcutter(object info)
        {
            if (finance.worldMoney >= FinanceStats.WoodcutterCost)
            {
                finance.worldMoney -= FinanceStats.WoodcutterCost;
                woodcutters.Add(new Woodcutter(woodcutterWalkSpriteSheet, woodcutterActionSpriteSheet, getMoneySound, woodcutterActionSound, finance));
            }
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

            ui.Draw();
        }
    }
}
