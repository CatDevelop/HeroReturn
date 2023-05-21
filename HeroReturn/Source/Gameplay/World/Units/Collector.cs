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
using Assimp;
#endregion
namespace HeroReturn;

public class Collector : Unit
{
    int direction = 1;
    private AnimatedSprite walkSprite;
    private AnimatedSprite actionSprite;

    public Collector(SpriteSheet collectorWalkSpriteSheet, SpriteSheet collectorActionSpriteSheet) : 
        base(collectorWalkSpriteSheet, collectorActionSpriteSheet, "rightWalk", new Vector2(139, 194), new Vector2(28, 46))
    {
        walkSprite = new AnimatedSprite(collectorWalkSpriteSheet);
        actionSprite = new AnimatedSprite(collectorActionSpriteSheet);
        sprite = walkSprite;
        sprite.Play("rightWalk");
    }

    public override void Update(float deltaSeconds)
    {

        base.Update(deltaSeconds);

        if (direction == 1)
            position.X += 1;

        if (direction == 2)
            position.X -= 1;

        if (position.X > 640)
        {
            sprite.Play("leftWalk");
            direction = 2;
        }
            

        if (position.X < 100)
        {
            sprite.Play("rightWalk");
            direction = 1;
        }
            
    }

    public override void Draw()
    {
        base.Draw();
    }
}

