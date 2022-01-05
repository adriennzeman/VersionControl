using ProgramTervezesiMintak.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramTervezesiMintak.Entities
{
    public class ToyFactory
    {
        public Toy CreateNew()
        {
            return new Ball();
        }
    }
}
