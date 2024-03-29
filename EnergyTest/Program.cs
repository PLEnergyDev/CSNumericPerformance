﻿// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using CsharpRAPL.Benchmarking;
using CsharpRAPL.CommandLine;

//Console.WriteLine("Hello, World!!");

/* LoopIterations C#:
 * DFE: 33554432
 * 
 * DIL: 1
 *
 * MM standard: 256
 *
 * MM unsafe: 512
 *
 * MM javalike: 512
 *
 * PE: 67108864
 * 
 */

var options = CsharpRAPLCLI.Parse(args);
options.PlotResults = true;
options.ZipResults = true;

var suite = new BenchmarkCollector(options.Iterations, options.LoopIterations);
suite.RunAll(false);
