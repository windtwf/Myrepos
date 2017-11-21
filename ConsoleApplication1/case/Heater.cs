using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class Heater
    {
        private int temperature;
        private string type = "RealFire 001";
        public string area = "China Xian";

        // 声明委托
        public delegate void BoiledEventHandle(Object sender, BoiledEventArgs e);
        public event BoiledEventHandle Boiled; // 声明事件

        //定义BoiledEventArgs类，传递给Observer所感兴趣的信息
        public class BoiledEventArgs : EventArgs
        {
            public readonly int temperture;
            public BoiledEventArgs(int temperture)
            {
                this.temperture = temperture;
            }
        }


        // 可以供继承自 Heater 的类重写，以便继承类拒绝其他对象对它的监视
        protected virtual void OnBoiled(BoiledEventArgs e)
        {
            if (Boiled != null)  // 如果有对象注册
            {
                Boiled(this, e); // 调用所有注册对象的方法
            }
        }


        public void BoilWater()
        {
            for (int i = 0; i <= 100; i++)
            {
                temperature = i;

                if (temperature > 95)
                {
                    BoiledEventArgs e = new BoiledEventArgs(temperature);
                    OnBoiled(e);
                }
            }
        }

    }

    public class Alarm
    {
        public void MakeAlert(Object sender,Heater.BoiledEventArgs e)
        {
            Heater heater = (Heater)sender;
            Console.WriteLine("Alarm: di di di, the water is on '{0}' temperature", e.temperture);
        }
    }

    public class Display
    {
        public static void ShowMsg(Object sender, Heater.BoiledEventArgs e)
        {
            Heater heat = (Heater)sender;
            Console.WriteLine("Dispaly: the water is boiling , the temperture is '{0}'", e.temperture);
        }
    }
}
