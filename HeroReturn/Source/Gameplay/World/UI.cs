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

public class UI
{
    public Button2d SummonHero;
    public Button2d SummonWoodcutter;

    public UI(PassObject SummonCollector, PassObject SummonWoodcutter)
    {
        SummonHero = new Button2d("2D\\UI\\BTN_Summon_Collector", 
            new Vector2(555, 630), 
            new Vector2(90, 90), 
            SummonCollector, 
            new Hero(new Vector2(139, 194))
        );

        this.SummonWoodcutter = new Button2d("2D\\UI\\BTN_Summon_Woodcutter",
            new Vector2(462, 630),
            new Vector2(90, 90),
            SummonWoodcutter,
            new Woodcutter(new Vector2(139, 194))
        );
    }

    public void Update(World world)
    {
        SummonHero.Update(new Vector2(45, 45));
        SummonWoodcutter.Update(new Vector2(45, 45));
    }

    public void Draw()
    {
        SummonHero.Draw(Vector2.Zero);
        SummonWoodcutter.Draw(Vector2.Zero);
    }
}
