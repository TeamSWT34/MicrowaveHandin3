using System;
using Microwave.Classes.Boundary;
using Microwave.Classes.Controllers;
using Microwave.Classes.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Test.Integration
{
    [TestFixture]
    public class TDStep7_Display
    {
        private Button sut_PowerButton;
        private Button sut_TimeButton;
        private Button sut_StartCancelButton;
        private Door sut_Door;

        private CookController cookController;
        private ITimer fakeTimer;
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
            fakeTimer = Substitute.For<ITimer>();
            fakePowerTube = Substitute.For<IPowerTube>();
            

            cookController = new CookController(fakeTimer, display, fakePowerTube);
            light = new Light(fakeOutput);

            userInterface = new UserInterface(sut_PowerButton, sut_TimeButton,
                sut_StartCancelButton, sut_Door, display,
                light, cookController);

            cookController.UI = userInterface;
        }


        [Test]
        public void Display_PowerPress_LogLine_Output()
        {
            int power = 50;

            sut_PowerButton.Press();
            fakeOutput.Received().OutputLine($"Display shows: {power} W");
        }

        [Test]
        public void Display_TimePress_LogLine_Output()
        {
            int min = 2;
            int sec = 0;

            sut_PowerButton.Press();
            sut_TimeButton.Press();
            sut_TimeButton.Press();
            fakeOutput.Received().OutputLine($"Display shows: {min:D2}:{sec:D2}");
        }


        [Test]
        public void Display_StartStopButton_Press_Clear_LogLine_Output()
        {
            sut_PowerButton.Press();
            sut_StartCancelButton.Press();
            fakeOutput.Received().OutputLine($"Display cleared");
        }

        [Test]
        public void Display_OpenDoor_Press_Clear_LogLine_Output()
        {

            sut_PowerButton.Press();
            sut_Door.Open();
            fakeOutput.Received().OutputLine($"Display cleared");
            sut_Door.Close();

            sut_PowerButton.Press();
            sut_TimeButton.Press();
            sut_Door.Open();
            fakeOutput.Received().OutputLine($"Display cleared");
            sut_Door.Close();

            sut_PowerButton.Press();
            sut_TimeButton.Press();
            sut_StartCancelButton.Press();
            sut_Door.Open();
            fakeOutput.Received().OutputLine($"Display cleared");
        }

        [Test]
        public void Display_TimeLoop_LogLine_Time_Output()
        {
            int min = 1;
            int sec = 0;
            int timeInSec = min*60;

            sut_PowerButton.Press();
            sut_TimeButton.Press();
            sut_StartCancelButton.Press();

            
            fakeTimer.TimeRemaining.Returns(59); //TimeRemaining is set to 59 sec.
            fakeTimer.TimerTick += Raise.EventWith(this, EventArgs.Empty);
            fakeOutput.Received().OutputLine($"Display shows: 00:59");


        }

        

    }
}