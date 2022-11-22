using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IArduinoData
{
    public float Roll { get; }
    public float Pitch { get; }
    public float Speed { get; }
}
