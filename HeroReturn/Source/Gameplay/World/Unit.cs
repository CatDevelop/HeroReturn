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
using MonoGame.Extended.Sprites;
#endregion
namespace HeroReturn;

public class Unit
{
    public string animation = "rightWalk";
    private SpriteSheet walkSpriteSheet;
    private SpriteSheet actionSpriteSheet;
    public AnimatedSprite sprite;
    public Vector2 position;
    public Vector2 offset;
    public Unit(SpriteSheet walk, SpriteSheet action, string animation, Vector2 pos, Vector2 offset)
    {
        walkSpriteSheet = walk;
        actionSpriteSheet = action;
        this.animation = animation;
        position = pos;
        this.offset = offset;
    }

    public virtual void Update(float deltaSeconds)
    {
        sprite.Update(deltaSeconds);
    }

    public virtual void Draw()
    {
        Globals.spriteBatch.Draw(sprite, new Vector2(offset.X+position.X, offset.Y+position.Y));
    }
}

