using System;
using System.Collections.Generic;
using System.Text;

namespace FuncTest
{
    /// <summary>
    /// 事件类型
    /// </summary>
    public class CarInfoEventArgs
    {
        public CarInfoEventArgs(string car)
        {
            Car = car;
        }
        public string Car { get; set; }
    }

    /// <summary>
    /// 发布者
    /// </summary>
    public class CarDealer
    {
        /// <summary>
        /// 事件
        /// </summary>
        public event EventHandler<CarInfoEventArgs> NewCarInfoEvent;

        public void NewCar(string car)
        {
            Console.WriteLine($"CarDealer.new car {car}");
            NewCarInfoEvent?.Invoke(this, new CarInfoEventArgs(car));
        }
    }
    /// <summary>
    /// 消费者
    /// </summary>
    public class Consumer
    {
        private string _name;
        public Consumer(string name)
        {
            name = _name;
        }

        public void NewCarIsHere(object senders, CarInfoEventArgs e)
        {
            Console.WriteLine($"{_name},{e.Car}来了！");
        }
    }
}
