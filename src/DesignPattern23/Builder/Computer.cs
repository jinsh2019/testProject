using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPattern23.Builder
{
    public class Computer
    {
        public string cpu { get; set; }//必须
        public string ram { get; set; }//必须
        public int usbCount { get; set; }//可选
        public string keyboard { get; set; }//可选
        public string display { get; set; }//可选
        //public Builder Builder(string cup, string ram)
        //{
        //    return new Builder(cup, ram);
        //}

        public class Builder
        {
            public string cpu { get; set; }//必须
            public string ram { get; set; }//必须
            public int usbCount { get; set; }//可选
            public string keyboard { get; set; }//可选
            public string display { get; set; }//可选
            public Builder(String cup, String ram)
            {
                this.cpu = cup;
                this.ram = ram;
            }

            public Builder setUsbCount(int usbCount)
            {
                this.usbCount = usbCount;
                return this;
            }
            public Builder setKeyboard(String keyboard)
            {
                this.keyboard = keyboard;
                return this;
            }
            public Builder setDisplay(String display)
            {
                this.display = display;
                return this;
            }
        }

    }
}
