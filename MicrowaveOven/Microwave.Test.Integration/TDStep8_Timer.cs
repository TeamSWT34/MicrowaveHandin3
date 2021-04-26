using System;
using System.Threading;
using Microwave.Classes.Boundary;
using Microwave.Classes.Controllers;
using Microwave.Classes.Interfaces;
using NSubstitute;
using NUnit.Framework;
using Timer = Microwave.Classes.Boundary.Timer;

namespace Microwave.Test.Integration
{
    [TestFixture]
    public class TDStep8_Timer
    {
        private Button sut_PowerButton;
        private Button sut_TimeButton;
        private Button sut_StartCancelButton;
        private Door sut_Door;

        private CookController cookController;
        private Timer timer;
        private IPowerTube fakePowerTube;


        private UserInterface userInterface;
        private Display display;
        private Light light;

        private IOutput fakeOutput;


        [SetUp]
        public void Setup()
        {
            sut_PowerButton = new Button();
            sut_TimeButton = new Button();
            sut_StartCancelButton = new Button();
            sut_Door = new Door();

            fakeOutput = Substitute.For<IOutput>();

            display = new Display(fakeOutput);
            timer = new Timer();
            fakePowerTube = Substitute.For<IPowerTube>();


            cookController = new CookController(timer, display, fakePowerTube);
            light = new Light(fakeOutput);

            userInterface = new UserInterface(sut_PowerButton, sut_TimeButton,
                sut_StartCancelButton, sut_Door, display,
                light, cookController);

            cookController.UI = userInterface;
        }


        [TestCase(1,1,0,59)]
        [TestCase(2, 5, 1, 55)]
        [TestCase(10, 12,9,48)]
        public void Timer_Sleeps_LogLine_Time_Output(int startMin, int waitTimeInSec,int expectMin,int expectSec)
        {
            int zeroSec = 0;

            sut_PowerButton.Press();

            for (int i = 0; i < startMin; i++)
                sut_TimeButton.Press();



            sut_StartCancelButton.Press();

            fakeOutput.Received().OutputLine($"Display shows: {startMin:D2}:{zeroSec:D2}");

            Thread.Sleep(waitTimeInSec*1000);

            fakeOutput.Received().OutputLine($"Display shows: {expectMin:D2}:{expectSec:D2}");

        }

        [Test]
        public void Time_Expired_LogLine_Time_Output()
        {
            int min = 1;
            int sec = 0;
            int sleepMilSec = 60000;

            int count = 0;

            

            sut_PowerButton.Press();
            sut_TimeButton.Press();
            sut_StartCancelButton.Press();

            fakeOutput.Received().OutputLine($"Display shows: {min:D2}:{sec:D2}");

            


            Thread.Sleep(sleepMilSec+1000); // Add extra millisec to ensure to get over the expected time 
            

            int timeAfterSleep = min * 60 - sleepMilSec / 1000;
            fakeOutput.Received().OutputLine($"Display shows: {0:D2}:{timeAfterSleep:D2}");

            fakePowerTube.Received().TurnOff();

            //test timer stopped after expire
            timer.TimerTick += (o2, e2) => count++;

            Thread.Sleep(5000);
            Assert.AreEqual(0, count);

        }
        
    }
}