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
using System.Diagnostics;
#endregion

namespace HeroReturn;

public class Basic2d
{
    public Vector2 pos, dims;
    public Texture2D texture1;
    public Texture2D texture2;
    public Texture2D texture3;
    public Texture2D texture4;
    public Basic2d(string path, Vector2 pos, Vector2 dims) 
    {
        this.pos = pos;
        this.dims = dims;

        texture1 = Globals.content.Load<Texture2D>(path);
    }

    public Basic2d(string path1, string path2, string path3, Vector2 pos, Vector2 dims)
    {
        this.pos = pos;
        this.dims = dims;

        texture1 = Globals.content.Load<Texture2D>(path1);
        texture2 = Globals.content.Load<Texture2D>(path2);
        texture3 = Globals.content.Load<Texture2D>(path3);
    }

    public Basic2d(string path1, string path2, string path3, string path4, Vector2 pos, Vector2 dims)
    {
        this.pos = pos;
        this.dims = dims;

        texture1 = Globals.content.Load<Texture2D>(path1);
        texture2 = Globals.content.Load<Texture2D>(path2);
        texture3 = Globals.content.Load<Texture2D>(path3);
        texture4 = Globals.content.Load<Texture2D>(path4);
    }

    public virtual void Update(Vector2 offset) 
    { 

    }

    public virtual bool Hover(Vector2 offset)
    {
        return HoverImg(offset);
    }

    public virtual bool HoverImg(Vector2 offset)
    {
        Vector2 mousePos = new(Globals.mouse.newMousePos.X, Globals.mouse.newMousePos.Y);

        if (mousePos.X >= (pos.X + offset.X) - dims.X / 2 && mousePos.X <= (pos.X + offset.X) + dims.X / 2 && mousePos.Y <= (pos.Y + offset.Y) + dims.Y / 2 && mousePos.Y >= (pos.Y + offset.X) - dims.X / 2)
            return true;

        return false;
    }

    public virtual void Draw(Vector2 offset) 
    {
        if (texture1 != null)
            Globals.spriteBatch.Draw(texture1,
                new Rectangle((int)(pos.X + offset.X), (int)(pos.Y + offset.Y), (int)dims.X, (int)dims.Y),
                null,
                Color.White,
                0.0f,
                new Vector2(texture1.Bounds.Width / 2, texture1.Bounds.Height / 2),
                new SpriteEffects(),
                0
            );
    }

    public virtual void Draw(Vector2 offset, Vector2 origin)
    {
        if (texture1 != null)
            Globals.spriteBatch.Draw(texture1,
                new Rectangle((int)(pos.X + offset.X), (int)(pos.Y + offset.Y), (int)dims.X, (int)dims.Y),
                null,
                Color.White,
                0.0f,
                new Vector2(origin.X, origin.Y),
                new SpriteEffects(),
                0
            );
    }

    public virtual void Draw(Vector2 offset, Vector2 origin, int frame)
    {
        if (frame == 1) 
        {
            if (texture1 != null)
                Globals.spriteBatch.Draw(texture1,
                    new Rectangle((int)(pos.X + offset.X), (int)(pos.Y + offset.Y), (int)dims.X, (int)dims.Y),
                    null,
                    Color.White,
                    0.0f,
                    new Vector2(origin.X, origin.Y),
                    new SpriteEffects(),
                    0
                );
        }
            
        if (frame == 2)
            if (texture2 != null)
                Globals.spriteBatch.Draw(texture2,
                    new Rectangle((int)(pos.X + offset.X), (int)(pos.Y + offset.Y), (int)dims.X, (int)dims.Y),
                    null,
                    Color.White,
                    0.0f,
                    new Vector2(origin.X, origin.Y),
                    new SpriteEffects(),
                    0
                );
        if (frame == 3)
            if (texture3 != null)
                Globals.spriteBatch.Draw(texture3,
                    new Rectangle((int)(pos.X + offset.X), (int)(pos.Y + offset.Y), (int)dims.X, (int)dims.Y),
                    null,
                    Color.White,
                    0.0f,
                    new Vector2(origin.X, origin.Y),
                    new SpriteEffects(),
                    0
                );

        if (frame == 4)
            if (texture4 != null)
                Globals.spriteBatch.Draw(texture4,
                    new Rectangle((int)(pos.X + offset.X), (int)(pos.Y + offset.Y), (int)dims.X, (int)dims.Y),
                    null,
                    Color.White,
                    0.0f,
                    new Vector2(origin.X, origin.Y),
                    new SpriteEffects(),
                    0
                );
    }
}

