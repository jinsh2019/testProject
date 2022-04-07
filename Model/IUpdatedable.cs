using System;
using System.Collections.Generic;
using System.Text;

namespace EFCoreDemo
{
    public interface IUpdatedable
    {
        public DateTime UpdatedDateTime { get; set; }
    }
}
