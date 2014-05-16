using System;
using System.IO;
using System.Text.RegularExpressions;
using NUnit.Core; 
using NUnit.Core.Extensibility; 

namespace Tap 
{ 
    [NUnitAddinAttribute(
        Type = ExtensionType.Core, 
        Name = "TAP Producer", 
        Description = "Produces TAP output for test results."
    )] 
    public class NUnitAddin : IAddin, EventListener
    {
        StringWriter sw;
        TextWriter savedOut;

        public bool Install(IExtensionHost host) 
        {
            //Output("YO - Install");
            IExtensionPoint listeners = host.GetExtensionPoint("EventListeners"); 
            if (listeners == null) 
                return false; 

            listeners.Install(this); 
            return true; 
        }

        public void RunStarted(string name, int testCount)
        {
            Output("YO - RunStarted");
            Output("TAP Version 13");
            Output("1..{0}", testCount);

            sw = new StringWriter();
            savedOut = Console.Out;
            Console.SetOut(sw);
        }

        public void RunFinished(TestResult result)
        {
            Console.SetOut(savedOut);
            sw.Dispose();
            Output("YO - RunFinished(TestResult)");
        }

        public void RunFinished(Exception exception)
        {
            Output("YO - RunFinshed(Exception)");
        }

        public void TestStarted(TestName testName)
        {
            Output("YO - TestStarted {0}", testName);
        } 

        public void TestFinished(TestResult result) 
        { 
            Output("YO - TestFinished");
            Console.Write(result.FullName);
            Output();
        }

        public void SuiteStarted(TestName testName)
        {
            Output("YO - SuiteStarted {0}", testName);
        }

        public void SuiteFinished(TestResult result)
        {
            Output("YO - SuiteFinished {0}", result.FullName);
        }

        public void UnhandledException(Exception exception)
        {
            Output("YO - UnhandledException");
        }

        public void TestOutput(TestOutput testOutput)
        {
            //testOutput = new TestOutput(String.Empty, TestOutputType.Out);
            //Output("YO - TestOutput");
        }

        void Output()
        {
            Output(String.Empty);
        }

        void Output(string output, params object[] args)
        {
            output = String.Format(output, args);
            Console.WriteLine(output);
        }
    } 
} 