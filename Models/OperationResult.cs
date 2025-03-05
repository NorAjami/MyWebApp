using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyWebApp.Models;
using MyWebApp.Services;
using MyWebApp;
using Microsoft.AspNetCore.Mvc;


namespace MyWebApp.Models
{
    // Resultatklass som berättar om operationer lyckades eller misslyckades
    public class OperationResult
    {
        public bool IsSuccess { get; private set; }
        public string Message { get; private set; }

        private OperationResult(bool success, string message)
        {
            IsSuccess = success;
            Message = message;
        }

        // Skapar ett lyckat resultat
        public static OperationResult Success(string message) =>
            new OperationResult(true, message);

        // Skapar ett misslyckat resultat
        public static OperationResult Failure(string message) =>
            new OperationResult(false, message);
    }
}
