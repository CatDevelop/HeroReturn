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
#endregion
namespace HeroReturn;

public class Unit : Basic2d
{ 
    public Unit(string path, Vector2 pos, Vector2 dims) : base(path, pos, dims)
    {

    }

    public override void Update(Vector2 offset)
    {
        base.Update(offset);
    }

    public override void Draw(Vector2 offset)
    {
        base.Draw(offset, Vector2.Zero);
    }
}

