using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace TestSithec.Controllers
{
    [ApiController]
    [Route("Operations")]
    public class OperationsController : ControllerBase
    {

        [HttpGet]
        [Route("Calculate")]

        public dynamic Calculate()
        {
            if (!Request.Headers.TryGetValue("Value1", out var val1) || string.IsNullOrEmpty(val1))
            {
                return BadRequest("El encabezado 'Value1' no puede ser nulo o vacío.");
            }
            if (!Request.Headers.TryGetValue("Value2", out var val2) || string.IsNullOrEmpty(val2))
            {
                return BadRequest("El encabezado 'Value2' no puede ser nulo o vacío.");
            }
            if (!Request.Headers.TryGetValue("Operator", out var operation) || string.IsNullOrEmpty(operation))
            {
                return BadRequest("El encabezado 'Operator' no puede ser nulo o vacío.");
            }
            return GenerateCalculate(Int32.Parse(val1), Int32.Parse(val2), operation);

        }

        [HttpPost]
        [Route("CalculateBody")]
        public dynamic CalculateBody(int value1, int value2, string? operation)
        {
            return GenerateCalculate(value1, value2, operation);

        }
        private dynamic GenerateCalculate(int val1, int val2, string? operation)
        {
            if (val1 == 0 || val2 == 0 || operation == null)
            {
                return new
                {
                    success = false,
                    message = "No se encontraron parametres necesarios en el request. Ej. (Value1 = 12, Value2 = 10, Operator = *)",
                    result = 0
                };
            }
            double result = 0;
            switch (operation)
            {
                case "+":
                    result = val1 + val2;
                    break;
                case "-":
                    result = val1 - val2;
                    break;
                case "*":
                    result = val1 * val2;
                    break;
                case "/":
                    result = val1 / val2;
                    break;
                default:
                    break;
            }

            return new
            {
                success = true,
                message = "Operación realizada correctamente",
                result = result
            };
        }

    }

}
