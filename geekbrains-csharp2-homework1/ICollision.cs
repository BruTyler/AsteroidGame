using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace geekbrains_csharp2_homework1
{
    public interface ICollision
    {
        bool Collision(ICollision obj);
        Rectangle Rect { get; }
    }
}
