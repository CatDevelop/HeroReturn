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
        public SpriteSheet collectorSpriteSheetWalk;
        public SpriteSheet collectorSpriteSheetAction;
        public List<Collector> heroes = new();
        public List<Woodcutter> woodcutters = new();
        public List<Vegetable> vegetables = new();

        public UI ui;

        public World()
        {
            background = new Basic2d("2D\\Backgrounds\\BACKGROUND_Game", new Vector2(640, 360), new Vector2(1280, 720));
            collectorSpriteSheetWalk = Globals.content.Load<SpriteSheet>("2D\\Heroes\\Collector\\Collector_Walk.sf", new JsonContentLoader());
            collectorSpriteSheetAction = Globals.content.Load<SpriteSheet>("2D\\Heroes\\Collector\\Collector_Collect.sf", new JsonContentLoader());

            GameGlobals.PassHeroes = AddHero;
            GameGlobals.PassWoodcutter = AddWoodcutter;
            GameGlobals.PassVegetable = AddVegetable;

            AddVegetable(new Vegetable(new Vector2(288, 200)));
            AddVegetable(new Vegetable(new Vector2(335, 184)));
            AddVegetable(new Vegetable(new Vector2(380, 195)));
            AddVegetable(new Vegetable(new Vector2(425, 179)));
            AddVegetable(new Vegetable(new Vector2(470, 190)));
            AddVegetable(new Vegetable(new Vector2(520, 200)));

            ui = new UI(AddHero, AddWoodcutter);
        }

        public virtual void Update(Vector2 backgroundOffset, Vector2 heroOffset, float deltaSeconds)
        {
            background.Update(backgroundOffset);

            for(int i = 0; i < heroes.Count; i++)
            {
                heroes[i].Update(deltaSeconds);
            }

            for (int i = 0; i < woodcutters.Count; i++)
            {
                woodcutters[i].Update(heroOffset);
            }

            for(int i = 0; i < vegetables.Count; i++)
            {
                vegetables[i].Update(heroOffset);
            }

            ui.Update(this);
        }

        public virtual void AddHero(object info)
        {
            heroes.Add(new Collector(collectorSpriteSheetWalk, collectorSpriteSheetAction));
        }

        public virtual void AddWoodcutter(object info)
        {
            woodcutters.Add(new Woodcutter(new Vector2(139, 194)));
        }

        public virtual void AddVegetable(object info)
        {
            vegetables.Add((Vegetable) info);
        }

        public virtual void Draw(Vector2 backgroundOffset, Vector2 heroOffset)
        {
            background.Draw(backgroundOffset);

            for (int i = 0; i < vegetables.Count; i++)
            {
                vegetables[i].Draw(heroOffset, Vector2.Zero, vegetables[i].frame);
            }

            for (int i = 0; i < heroes.Count; i++)
            {
                heroes[i].Draw();
            }

            for (int i = 0; i < woodcutters.Count; i++)
            {
                woodcutters[i].Draw(heroOffset, Vector2.Zero, woodcutters[i].frame);
            }

            ui.Draw();
        }
    }
}
