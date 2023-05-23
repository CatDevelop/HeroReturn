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

public class FinanceController
{
    public int gameMoney;
    public int worldMoney;
    public FinanceController(int gameMoney, int worldMoney) 
    {
        this.gameMoney = gameMoney;
        this.worldMoney = worldMoney;
    }
}

