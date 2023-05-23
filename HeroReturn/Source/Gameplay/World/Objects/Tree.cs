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
using System.IO;
using System.Diagnostics;
using MonoGame.Extended.Sprites;
#endregion
namespace HeroReturn;

public class Tree : Object
{
    public int level = 1;
    public int frameDelay = 0;
    public bool isCollecting = false;
    private Random rnd = new Random();
    public Tree(SpriteSheet treeSpriteSheet, Vector2 pos) :
        base(treeSpriteSheet, "firstLevel", pos, new Vector2(60, 78))
    {
 
    }

    public void Update(float deltaSeconds, int level)
    {
        base.UpdateObject(deltaSeconds);
        if (animation.IsPlaying)
            animation.Pause();

        if(frameDelay > 50000 && !animation.IsComplete)
        {
            animation.Play();
            frameDelay = 0;
        }

        if (level != this.level)
        {
            this.level = level;
            switch(level)
            {
                case 1:
                    animation = sprite.Play("firstLevel");
                    break;
                case 2:
                    animation = sprite.Play("secondLevel");
                    break;
                case 3:
                    animation = sprite.Play("thirdLevel");
                    break;
            }
            
        }


        if (!animation.IsComplete)
            frameDelay += rnd.Next(-40, 100);
    }

    public override void Draw()
    {
        base.Draw();
    }
}

