using System;

namespace Gof
{
    public class Factory
    {

    }

    public interface IVehicle
    {
    }
    /// <summary>
    /// 简单工厂
    /// </summary>
    class SimpleFactory
    {
        public IVehicle CreateInstacne(string type)
        {
            IVehicle iVehicle = null;
            switch (type.ToLower())
            {
                case "car":
                    iVehicle = new Car();
                    break;
                case "bike":
                    iVehicle = new Bike();
                    break;
                default:
                    throw new Exception();
            }
            return iVehicle;
        }
    }

    public class Car : IVehicle
    {

    }
    public class Bike : IVehicle
    {

    }

    public interface IFactory
    {
        public IVehicle CreateInstance();
    }
    /// <summary>
    /// 工厂方法 1
    /// 把创建对象放在工厂里面执行，减少对简单工厂的修改
    /// </summary>
    public class CarFactory : IFactory
    {
        public IVehicle CreateInstance()
        {
            return new Car();
        }
    }
    /// <summary>
    /// 工厂方法 2
    /// 把创建对象放在工厂里面执行，减少对简单工厂的修改
    /// </summary>
    public class BikeFactory : IFactory
    {
        public IVehicle CreateInstance()
        {
            return new Bike();
        }
    }

    /// 抽象工厂用于创建 一组固定产品蔟
    /// 

}