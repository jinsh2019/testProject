using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatternStudy.Patterns
{

    /// <summary>
    /// The 'Prototype' abstract class
    /// </summary>
    public abstract class Prototype
    {
        private string _id;

        /// <summary>
        /// Constructor
        /// </summary>
        public Prototype(string id)
        {
            this._id = id;
        }

        /// <summary>
        /// Gets id
        /// </summary> 
        public string Id
        {
            get { return _id; }
        }

        public abstract Prototype Clone();
    }
    public class ConcretePrototype1 : Prototype
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ConcretePrototype1(string id)
            : base(id)
        {
        }

        /// <summary>
        /// Returns a shallow copy
        /// </summary>
        /// <returns></returns>
        public override Prototype Clone()
        {
            return (Prototype)this.MemberwiseClone();
        }
    }

    public class ConcretePrototype2 : Prototype
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ConcretePrototype2(string id)
            : base(id)
        {
        }

        /// <summary>
        /// Returns a shallow copy
        /// </summary>
        /// <returns></returns>
        public override Prototype Clone()
        {
            return (Prototype)this.MemberwiseClone();
        }
    }
}
