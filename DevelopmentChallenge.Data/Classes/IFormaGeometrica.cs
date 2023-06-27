using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevelopmentChallenge.Data.Classes
{
    interface IFormaGeometrica
    {
        decimal Lado { get; set; }
        decimal Altura { get; set; }

        int Tipo { get; set; }

        decimal CalcularArea();

        decimal CalcularPerimetro();
    }
}
