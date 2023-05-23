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

public class Object
{
    private SpriteSheet spriteSheet;
    public AnimatedSprite sprite;
    public SpriteSheetAnimation animation;
    public Vector2 position;
    public Vector2 offset;
    public Object(SpriteSheet spriteSheet, string animationName, Vector2 pos, Vector2 offset)
    {
        this.spriteSheet = spriteSheet;
        position = pos;
        this.offset = offset;
        sprite = new AnimatedSprite(spriteSheet);
        animation = sprite.Play(animationName);
    }

    public virtual void UpdateObject(float deltaSeconds)
    {
        sprite.Update(deltaSeconds);
    }

    public virtual void Draw()
    {
        Globals.spriteBatch.Draw(sprite, new Vector2(offset.X+position.X, offset.Y+position.Y));
    }
}

