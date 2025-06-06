using Microsoft.AspNetCore.Mvc;
using FlowApp.Domain;

namespace FlowApp.Shared
{
    public static class ErrorHandler
    {
        public static IActionResult HandleError(
            ILogger logger,
            Func<object, IActionResult> statusFunction,
            int code,
            string property,
            Exception ex)
        {
            var status = GetStatusCodeFromDelegate(statusFunction);

            var error = new Error
            {
                Status = status,
                Code = code,
                Property = property,
                Message = ex.Message,
                DeveloperMessage = "Revise os dados inseridos ou a rota utilizada"
            };

            // Log
            logger.LogError(ex, "Erro {Status} - Código {Code} na propriedade {Property}: {Message}",
                status, code, property, ex.Message);

            return statusFunction(error);
        }

        private static int GetStatusCodeFromDelegate(Func<object, IActionResult> statusFunction)
        {
            if (statusFunction.Method.Name.Contains("BadRequest")) return 400;
            if (statusFunction.Method.Name.Contains("Conflict")) return 409;
            if (statusFunction.Method.Name.Contains("NotFound")) return 404;
            if (statusFunction.Method.Name.Contains("Unauthorized")) return 401;
            return 500;
        }
    }
}
