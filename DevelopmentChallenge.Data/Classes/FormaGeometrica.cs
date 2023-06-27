/******************************************************************************************************************/
/******* ¿Qué pasa si debemos soportar un nuevo idioma para los reportes, o agregar más formas geométricas? *******/
/******************************************************************************************************************/

/*
 * TODO: 
 * Refactorizar la clase para respetar principios de la programación orientada a objetos.
 * Implementar la forma Trapecio/Rectangulo. 
 * Agregar el idioma Italiano (o el deseado) al reporte.
 * Se agradece la inclusión de nuevos tests unitarios para validar el comportamiento de la nueva funcionalidad agregada (los tests deben pasar correctamente al entregar la solución, incluso los actuales.)
 * Una vez finalizado, hay que subir el código a un repo GIT y ofrecernos la URL para que podamos utilizar la nueva versión :).
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DevelopmentChallenge.Data.Classes
{
    public class FormaGeometrica: IFormaGeometrica
    {
        public int Tipo { get; set; }
        public decimal Lado { get; set; }
        public decimal Altura { get; set; }

        public FormaGeometrica(int tipo, decimal ancho, decimal altura = 0)
        {
            Tipo = tipo;
            Lado = ancho;
            if(tipo == (int)Utils.Utils.Formas.Rectangulo && altura < 1)
            {
                throw new ArgumentException(message: "Altura requerida para esta figura.");
            }
            Altura = altura;
        }

        public static string Imprimir(List<FormaGeometrica> formas, int idioma)
        {
            var sb = new StringBuilder();

            if (!formas.Any())
            {
                if (idioma == (int)Utils.Utils.Idiomas.Castellano)
                    sb.Append("<h1>Lista vacía de formas!</h1>");
                else if (idioma == (int)Utils.Utils.Idiomas.Italiano)
                    sb.Append("<h1>Elenco vuoto di forme!</h1>");
                else
                    sb.Append("<h1>Empty list of shapes!</h1>");
            }
            else
            {
                // Hay por lo menos una forma
                // HEADER
                if (idioma == (int)Utils.Utils.Idiomas.Castellano)
                    sb.Append("<h1>Reporte de Formas</h1>");
                else if (idioma == (int)Utils.Utils.Idiomas.Castellano)
                    sb.Append("<h1>Report Moduli</h1>");
                else
                    // default es inglés
                    sb.Append("<h1>Shapes report</h1>");

                var numeroCuadrados = 0;
                var numeroCirculos = 0;
                var numeroTriangulos = 0;
                var numeroRectangulos = 0;

                var areaCuadrados = 0m;
                var areaCirculos = 0m;
                var areaTriangulos = 0m;
                var areaRectangulos = 0m;

                var perimetroCuadrados = 0m;
                var perimetroCirculos = 0m;
                var perimetroTriangulos = 0m;
                var perimetroRectangulos = 0m;

                for (var i = 0; i < formas.Count; i++)
                {
                    if (formas[i].Tipo == (int)Utils.Utils.Formas.Cuadrado)
                    {
                        numeroCuadrados++;
                        areaCuadrados += formas[i].CalcularArea();
                        perimetroCuadrados += formas[i].CalcularPerimetro();
                    }
                    if (formas[i].Tipo == (int)Utils.Utils.Formas.Circulo)
                    {
                        numeroCirculos++;
                        areaCirculos += formas[i].CalcularArea();
                        perimetroCirculos += formas[i].CalcularPerimetro();
                    }
                    if (formas[i].Tipo == (int)Utils.Utils.Formas.TrianguloEquilatero)
                    {
                        numeroTriangulos++;
                        areaTriangulos += formas[i].CalcularArea();
                        perimetroTriangulos += formas[i].CalcularPerimetro();
                    }
                    if (formas[i].Tipo == (int)Utils.Utils.Formas.Rectangulo)
                    {
                        numeroRectangulos++;
                        areaRectangulos += formas[i].CalcularArea();
                        perimetroRectangulos += formas[i].CalcularPerimetro();
                    }
                }
                
                sb.Append(ObtenerLinea(numeroCuadrados, areaCuadrados, perimetroCuadrados, (int)Utils.Utils.Formas.Cuadrado, idioma));
                sb.Append(ObtenerLinea(numeroCirculos, areaCirculos, perimetroCirculos, (int)Utils.Utils.Formas.Circulo, idioma));
                sb.Append(ObtenerLinea(numeroTriangulos, areaTriangulos, perimetroTriangulos, (int)Utils.Utils.Formas.TrianguloEquilatero, idioma));
                sb.Append(ObtenerLinea(numeroRectangulos, areaRectangulos, perimetroRectangulos, (int)Utils.Utils.Formas.Rectangulo, idioma));

                // FOOTER
                sb.Append("TOTAL:<br/>");
                sb.Append(numeroCuadrados + numeroCirculos + numeroTriangulos + numeroRectangulos + " " + (idioma == (int)Utils.Utils.Idiomas.Castellano ? "formas" : idioma == (int)Utils.Utils.Idiomas.Castellano ? "forme" : "shapes") + " ");
                sb.Append((idioma == (int)Utils.Utils.Idiomas.Castellano || idioma == (int)Utils.Utils.Idiomas.Italiano ? "Perimetro " : "Perimeter ") + (perimetroCuadrados + perimetroTriangulos + perimetroCirculos + perimetroRectangulos).ToString("#.##") + " ");
                sb.Append("Area " + (areaCuadrados + areaCirculos + areaTriangulos + areaRectangulos).ToString("#.##"));
            }

            return sb.ToString();
        }

        private static string ObtenerLinea(int cantidad, decimal area, decimal perimetro, int tipo, int idioma)
        {
            if (cantidad > 0)
            {
                if ( (idioma == (int)Utils.Utils.Idiomas.Castellano) || (idioma == (int)Utils.Utils.Idiomas.Italiano) )
                    return $"{cantidad} {TraducirForma(tipo, cantidad, idioma)} | Area {area:#.##} | Perimetro {perimetro:#.##} <br/>"; 

                return $"{cantidad} {TraducirForma(tipo, cantidad, idioma)} | Area {area:#.##} | Perimeter {perimetro:#.##} <br/>";
            }

            return string.Empty;
        }

        private static string TraducirForma(int tipo, int cantidad, int idioma)
        {
            switch (tipo)
            {
                case (int)Utils.Utils.Formas.Cuadrado:
                    if (idioma == (int)Utils.Utils.Idiomas.Castellano) return cantidad == 1 ? "Cuadrado" : "Cuadrados";
                    else if (idioma == (int)Utils.Utils.Idiomas.Italiano) return cantidad == 1 ? "Quadrato" : "Quadratos";
                    else return cantidad == 1 ? "Square" : "Squares";
                case (int)Utils.Utils.Formas.Circulo:
                    if (idioma == (int)Utils.Utils.Idiomas.Castellano) return cantidad == 1 ? "Círculo" : "Círculos";
                    else if (idioma == (int)Utils.Utils.Idiomas.Italiano) return cantidad == 1 ? "Cerchio" : "Cerchios";
                    else return cantidad == 1 ? "Circle" : "Circles";
                case (int)Utils.Utils.Formas.TrianguloEquilatero:
                    if (idioma == (int)Utils.Utils.Idiomas.Castellano) return cantidad == 1 ? "Triángulo" : "Triángulos";
                    else if (idioma == (int)Utils.Utils.Idiomas.Italiano) return cantidad == 1 ? "Triangolo" : "Triangolos";
                    else return cantidad == 1 ? "Triangle" : "Triangles";
                case (int)Utils.Utils.Formas.Rectangulo:
                    if (idioma == (int)Utils.Utils.Idiomas.Castellano) return cantidad == 1 ? "Rectangulo" : "Rectangulos";
                    else if (idioma == (int)Utils.Utils.Idiomas.Italiano) return cantidad == 1 ? "Rettangolo" : "Rettangolos";
                    else return cantidad == 1 ? "Rectangle" : "Rectangles";
            }

            return string.Empty;
        }

        public decimal CalcularArea()
        {
            switch (Tipo)
            {
                case (int)Utils.Utils.Formas.Cuadrado: return Lado * Lado;
                case (int)Utils.Utils.Formas.Circulo: return (decimal)Math.PI * (Lado / 2) * (Lado / 2);
                case (int)Utils.Utils.Formas.TrianguloEquilatero: return ((decimal)Math.Sqrt(3) / 4) * Lado * Lado;
                case (int)Utils.Utils.Formas.Rectangulo: return  Lado * Altura;
                default:
                    throw new ArgumentOutOfRangeException(@"Forma desconocida");
            }
        }

        public decimal CalcularPerimetro()
        {
            switch (Tipo)
            {
                case (int)Utils.Utils.Formas.Cuadrado: return Lado * 4;
                case (int)Utils.Utils.Formas.Circulo: return (decimal)Math.PI * Lado;
                case (int)Utils.Utils.Formas.TrianguloEquilatero: return Lado * 3;
                case (int)Utils.Utils.Formas.Rectangulo: return (Lado * 2) + (Altura * 2);
                default:
                    throw new ArgumentOutOfRangeException(@"Forma desconocida");
            }
        }
    }
}
