﻿using System;
using System.Collections.Generic;
using CALCULATOR_OOP.Model;
using CALCULATOR_OOP.Operations;
using CALCULATOR_OOP.Service;
using CALCULATOR_OOP.Validation;

namespace CALCULATOR_OOP
{
    class Program
    {
        private static readonly BinaryInputValidation Validation = new BinaryInputValidation();
        private static readonly MatrixDimensionValidation MatrixValidation = new MatrixDimensionValidation();
        private static readonly ReusableOperationInputValidation ReusableOperationInputValidation = new ReusableOperationInputValidation();
        private static readonly ChoosingOperationInputValidation ChoosingOperationInputValidation = new ChoosingOperationInputValidation();
        private static readonly List<String> OperationHistory = new List<string>();
        private static readonly Calculator Calculator = new Calculator();
        private static bool _lastOperationMode = false;
        private static Operation _lastBinaryOperation = null;
        private static Operation _lastMatrixOperation = null;

        static void Main(string[] args)
        {
            while (true)
            {
                CalculatorService.ShowMenu();

                var typeOperation = ChoosingOperationInputValidation.Validate();

                switch (typeOperation)
                    {
                        case 1:
                            _lastBinaryOperation = Calculator.Execute(new Addition(_lastOperationMode && _lastBinaryOperation != null ? (double)_lastBinaryOperation.Result : Validation.Validate(), Validation.Validate()));
                            OperationHistory.Add("addition");
                            break;
                        case 2:
                            _lastBinaryOperation = Calculator.Execute(new Subtraction(_lastOperationMode && _lastBinaryOperation != null ? (double)_lastBinaryOperation.Result : Validation.Validate(), Validation.Validate()));
                            OperationHistory.Add("subtraction");
                            break;
                        case 3:
                            _lastBinaryOperation = Calculator.Execute(new Multiplication(_lastOperationMode && _lastBinaryOperation != null ? (double)_lastBinaryOperation.Result : Validation.Validate(), Validation.Validate()));
                            OperationHistory.Add("multiplication");
                            break;
                        case 4:
                            _lastBinaryOperation = Calculator.Execute(new Division(_lastOperationMode && _lastBinaryOperation != null ? (double)_lastBinaryOperation.Result : Validation.Validate(), Validation.Validate()));
                            if (_lastBinaryOperation == null)
                            {
                                _lastOperationMode = false;
                                continue;
                            }
                            OperationHistory.Add("division");
                            break;
                        case 5:

                            Matrix matrixA = null;
                            Matrix matrixB = null;

                            if (_lastOperationMode && _lastMatrixOperation == null)
                            {
                                Console.WriteLine("There is no operations with matrices yet");
                                break;
                            }

                            //initialize first matrix, either as last operation result or a new instance;
                            matrixA = (_lastOperationMode && _lastMatrixOperation != null) ? (Matrix) _lastMatrixOperation.Result : new Matrix(MatrixValidation.Validate(), MatrixValidation.Validate());
                            matrixB = new Matrix(MatrixValidation.Validate(), MatrixValidation.Validate());


                            if (matrixA.Row != matrixB.Column)
                            {
                                Console.WriteLine("Inconsistent matrix dimensions. Multiplication is forbidden");
                                break;
                            }

                            //if first argument is new matrix, fill it out
                            if (!_lastOperationMode)
                                matrixA.FillOutMatrix();

                            matrixB.FillOutMatrix();

                            _lastMatrixOperation = Calculator.Execute(new MatrixMultiplication(matrixA, matrixB));
                            OperationHistory.Add("Matrix Multiplication");
                        break;
                        case 6:
                            if (OperationHistory.Count == 0)
                            {
                                Console.WriteLine("\nNo operation has been done\n");
                                break;
                            }
                            foreach (var itemsHistory in OperationHistory)
                                Console.WriteLine($"\n{itemsHistory}\n"); 
                            break;
                    }

                // except for the history operation
                if (typeOperation != 6)
                {
                    Console.WriteLine("Would you like use the result as first argument? Yes[1], No[2]");
                    _lastOperationMode = (ReusableOperationInputValidation.Validate() == 1) ? true : false;
                }
            }
        }
    }
}