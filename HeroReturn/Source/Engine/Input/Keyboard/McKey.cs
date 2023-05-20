﻿#region Includes
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;
#endregion
namespace HeroReturn;

public class McKey
{
    public int state;
    public string key, print, display;
    
    public McKey(string KEY, int STATE)
    {
        key = KEY;
        state = STATE;
        MakePrint(key);
    }

    public virtual void Update()
    {
        state = 2;
    }


    public void MakePrint(string KEY)
    {
        display = KEY;

        var tempStr = "";

        switch (KEY)
        {
            case "A":
            case "B":
            case "C":
            case "D":
            case "E":
            case "F":
            case "G":
            case "H":
            case "I":
            case "J":
            case "K":
            case "L":
            case "M":
            case "N":
            case "O":
            case "P":
            case "Q":
            case "R":
            case "S":
            case "T":
            case "U":
            case "V":
            case "W":
            case "X":
            case "Y":
            case "Z":
                tempStr = KEY;
                break;
            case "Space":
                tempStr = " ";
                break;
            case "OemCloseBrackets":
                tempStr = "]";
                display = tempStr;
                break;
            case "OemOpenBrackets":
                tempStr = "[";
                display = tempStr;
                break;
            case "OemMinus":
                tempStr = "-";
                display = tempStr;
                break;
            case "OemPeriod":
            case "Decimal":
                tempStr = ".";
                break;
            case "D1":
            case "D2":
            case "D3":
            case "D4":
            case "D5":
            case "D6":
            case "D7":
            case "D8":
            case "D9":
            case "D0":
                tempStr = KEY.Substring(1);
                break;
            case "NumPad1":
            case "NumPad2":
            case "NumPad3":
            case "NumPad4":
            case "NumPad5":
            case "NumPad6":
            case "NumPad7":
            case "NumPad8":
            case "NumPad9":
            case "NumPad0":
                tempStr = KEY[6..];
                break;
        }

        print = tempStr;
    }
}

