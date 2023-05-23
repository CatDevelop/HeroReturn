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
public class Button2d : Basic2d
{
    public bool isPressed, isHovered;
    SoundEffect pressKey;

    public object info;

    PassObject ButtonClicked;

    public Button2d(string path, Vector2 pos, Vector2 dims, PassObject buttonClicked, object info, SoundEffect pressKey) : base(path, pos, dims)
    {
        ButtonClicked = buttonClicked;
        isPressed = false;
        this.info = info;
        this.pressKey = pressKey;
    }

    public override void Update(Vector2 offset)
    {
        if(Hover(offset))
        {
            isHovered = true;
            if(Globals.mouse.LeftClick())
            {
                isHovered = false;
                isPressed = true;
            } else if(Globals.mouse.LeftClickRelease())
            {
                pressKey.Play();
                RunBtnClick();
            }
        } else {
            isHovered = false;
        }


        if(!Globals.mouse.LeftClick() && !Globals.mouse.LeftClickHold())
        {
            isPressed = false;
        }
        base.Update(offset);
    }

    public virtual void Reset()
    {
        isPressed = false;
        isHovered = false;
    }

    public virtual void RunBtnClick()
    {
        ButtonClicked?.Invoke(info);
        Reset();
    }

    public override void Draw(Vector2 offset)
    {

        base.Draw(offset, Vector2.Zero);
    }
}

