using SharpDX;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace HeroReturn.Engine
{
    public static class MetaData
    {
        private static readonly string _filePath = Path.Combine(Directory.GetCurrentDirectory(), "metadata.txt");

        private static void CreateFileIfNotExist()
        {
            if (File.Exists(_filePath))
            {
                return;
            }

            using (var f = File.Create(_filePath)) 
            {
                f.Write(new UTF8Encoding(true).GetBytes("0" + ';'), 0, 2);
                f.Write(new UTF8Encoding(true).GetBytes("0" + ';'), 0, 2);
                f.Write(new UTF8Encoding(true).GetBytes("0" + ';'), 0, 2);
                f.Write(new UTF8Encoding(true).GetBytes("0" + ';'), 0, 2);
                f.Write(new UTF8Encoding(true).GetBytes("0"), 0, 1);
                f.Close();
            }
        }
        public static void UpdateData(int act, int money, int houseUpgrade, int treesUpgrade, int vegetablesUpgrade)
        {
            CreateFileIfNotExist();
            File.WriteAllText(_filePath, String.Empty);
            using (FileStream f = File.OpenWrite(_filePath))
            {
                f.Write(new UTF8Encoding(true).GetBytes(act.ToString() + ';'), 0, act.ToString().Length+1);
                f.Write(new UTF8Encoding(true).GetBytes(money.ToString() + ';'), 0, money.ToString().Length + 1);
                f.Write(new UTF8Encoding(true).GetBytes(houseUpgrade.ToString() + ';'), 0, houseUpgrade.ToString().Length + 1);
                f.Write(new UTF8Encoding(true).GetBytes(treesUpgrade.ToString() + ';'), 0, treesUpgrade.ToString().Length + 1);
                f.Write(new UTF8Encoding(true).GetBytes(vegetablesUpgrade.ToString()), 0, vegetablesUpgrade.ToString().Length);
                f.Close();
            }
        }

        public static (int act, int money, int houseUpgrade, int treesUpgrade, int vegetablesUpgrade) ReadData()
        {
            CreateFileIfNotExist();

            (int, int, int, int, int) result;
            //var listInfo = new List<int>();

            using (var f = File.OpenText(_filePath))
            {
                var info = f.ReadLine();
                var listInfo = info.Split(';');

                result = (int.Parse(listInfo[0]), int.Parse(listInfo[1]), int.Parse(listInfo[2]), int.Parse(listInfo[3]), int.Parse(listInfo[4]));
                f.Close();
            }

            return result;
        }
    }
}
