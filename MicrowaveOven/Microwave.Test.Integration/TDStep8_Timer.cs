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


        [Test]
        public void Timer_Sleeps_LogLine_Time_Output()
        {
            int min = 1;
            int sec = 0;
            int sleepMilSec = 2000;

            ManualResetEvent pause = new ManualResetEvent(false);

            sut_PowerButton.Press();
            sut_TimeButton.Press();
            sut_StartCancelButton.Press();

            fakeOutput.Received().OutputLine($"Display shows: {min:D2}:{sec:D2}");

            Thread.Sleep(sleepMilSec);

            int timeAfterSleep = min * 60 - sleepMilSec / 1000;
            fakeOutput.Received().OutputLine($"Display shows: {00}:{timeAfterSleep:D2}");

        }

        [Test]
        public void Time_Expired_LogLine_Time_Output()
        {
            int min = 1;
            int sec = 0;
            int sleepMilSec = 60000;

            ManualResetEvent pause = new ManualResetEvent(false);

            sut_PowerButton.Press();
            sut_TimeButton.Press();
            sut_StartCancelButton.Press();

            fakeOutput.Received().OutputLine($"Display shows: {min:D2}:{sec:D2}");

            Thread.Sleep(sleepMilSec);

            int timeAfterSleep = min * 60 - sleepMilSec / 1000;
            fakeOutput.Received().OutputLine($"Display shows: {00}:{timeAfterSleep:D2}");

            fakePowerTube.Received().TurnOff();
            
        }

    }
}