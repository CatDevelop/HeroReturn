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

public class Gameplay
{
    int playState;
    World world;
    GameStats gameStats;
    private SoundEffect pressKey;
    private PassObject GoTo;
    public Gameplay(GameStats gameStats, SoundEffect pressKey, PassObject GoTo)
    {
        this.gameStats = gameStats;
        playState = 0;
        this.pressKey = pressKey;
        this.GoTo = GoTo;
        ResetWorld(null); 
    }

    public void Update(float deltaSeconds)
    {
        if(playState == 0)
        {
            world.Update(Vector2.Zero, Vector2.Zero, deltaSeconds);
        }
    }
    public void ResetWorld(object info)
    {
        world = new World(gameStats, pressKey, GoTo);
    }

    public void Draw()
    {
        if (playState == 0)
        {
            world.Draw(Vector2.Zero, Vector2.Zero);
        }
    }
}

