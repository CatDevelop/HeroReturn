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
    public FinanceController finance;

    SpriteFont font;

    public Basic2d moneyIcon;

    public UI(PassObject SummonCollector, PassObject SummonWoodcutter, SpriteFont font, FinanceController financeController, SoundEffect pressKey )
    {
        moneyIcon = new Basic2d("2D\\UI\\MoneyIcon", new Vector2(10, 10), new Vector2(32, 32));
        this.font = font;
        finance = financeController;
        SummonHero = new Button2d("2D\\UI\\BTN_Summon_Collector", 
            new Vector2(555, 630), 
            new Vector2(90, 90), 
            SummonCollector, 
            null,
            pressKey
        );

        this.SummonWoodcutter = new Button2d("2D\\UI\\BTN_Summon_Woodcutter",
            new Vector2(462, 630),
            new Vector2(90, 90),
            SummonWoodcutter,
            null,
            pressKey
        );
    }

    public void Update(World world)
    {
        SummonHero.Update(new Vector2(45, 45));
        SummonWoodcutter.Update(new Vector2(45, 45));
    }

    public void Draw()
    {
        moneyIcon.Draw(new Vector2(16, 16));
        Globals.spriteBatch.DrawString(font, finance.worldMoney.ToString(), new Vector2(47, 7), Color.White);
        SummonHero.Draw(Vector2.Zero);
        SummonWoodcutter.Draw(Vector2.Zero);
    }
}
