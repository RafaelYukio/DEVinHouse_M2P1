using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M2P1.Fintech.Entidades
{
    public abstract class BaseFintech
    {
        public string Id { get; private set; }
        protected BaseFintech(string id) => Id = id;
        protected BaseFintech() { }
    }
}
