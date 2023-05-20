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

public class Vegetable : Basic2d
{
    private int direction = 1;
    public int frame = 1;
    public int frameDelay = 0;
    private Random rnd = new Random();
    public Vegetable(Vector2 pos) : 
        base("2D\\Objects\\Vegetable\\OBJECTS_Vegetable_1_1",
        "2D\\Objects\\Vegetable\\OBJECTS_Vegetable_1_2",
        "2D\\Objects\\Vegetable\\OBJECTS_Vegetable_1_3", 
        "2D\\Objects\\Vegetable\\OBJECTS_Vegetable_1_4", 
        pos, 
        new Vector2(45, 42))
    {
 
    }

    public override void Update(Vector2 offset)
    {
        base.Update(offset);
        if(frameDelay > 2000)
        {
            frame = frame % 4 + 1;
            frameDelay = 0;
        }
           

        frameDelay += rnd.Next(1, 50);



        Debug.WriteLine(frame);
    }

    public override void Draw(Vector2 offset)
    {
        base.Draw(offset, Vector2.Zero, frame);
    }
}

