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
#endregion
namespace HeroReturn;

public class Hero : Basic2d
{
    private int direction = 1;
    public int frame = 1;
    public int frameDelay = 0;
    public Hero(Vector2 pos) : 
        base("2D\\Heroes\\Collector\\Collector_0_1_1", 
        "2D\\Heroes\\Collector\\Collector_0_1_2", 
        "2D\\Heroes\\Collector\\Collector_0_1_3", 
        pos, 
        new Vector2(57, 93))
    {
 
    }

    public override void Update(Vector2 offset)
    {
        base.Update(offset);
        if(frameDelay == 15)
        {
            frame = frame % 3 + 1;
            frameDelay = 0;
        }
           

        if (direction == 1)
        {
            pos.X += 1;
        }

        frameDelay += 1;

        

        Debug.WriteLine(frame);

        if (direction == 2)
            pos.X -= 1;

        if (pos.X > 640)
            direction = 2;

        if (pos.X < 100)
            direction = 1;
    }

    public override void Draw(Vector2 offset)
    {
        Debug.WriteLine("DRAW HERO " + frame);
        base.Draw(offset, Vector2.Zero, frame);
    }
}

