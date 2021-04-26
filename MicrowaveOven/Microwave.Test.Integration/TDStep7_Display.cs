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

            light = new Light(fakeOutput);

            display = new Display(fakeOutput);

            cookController = new CookController(fakeTimer, display, fakePowerTube);
            fakeTimer = Substitute.For<ITimer>();
            fakePowerTube = Substitute.For<IPowerTube>();

            userInterface = new UserInterface(sut_PowerButton, sut_TimeButton,
                sut_StartCancelButton, sut_Door, display,
                light, cookController);
        }


/*
        [Test]
        public void Display_PowerPress_LogLine_Output()
        {
            int power = 50;

            sut_PowerButton.Press();

            fakeOutput.OutputLine($"Display shows: {power} W");
        }

        [Test]
        public void Display_TimePress_LogLine_Output()
        {
            int min = 1;
            int sec = 0;

            sut_PowerButton.Press();
            sut_TimeButton.Press();

            fakeOutput.OutputLine($"Display shows: {min:D2}:{sec:D2}");
        }


        [Test]
        public void Display_StartStopButton_Press_Clear_LogLine_Output()
        {

            sut_PowerButton.Press();
            sut_StartCancelButton.Press();
            fakeOutput.OutputLine($"Display cleared");

            sut_PowerButton.Press();
            sut_TimeButton.Press();
            sut_CookController.StartCooking(50,60);
            sut_StartCancelButton.Press();
            fakeOutput.OutputLine($"Display cleared");
        }

        [Test]
        public void Display_OpenDoor_Press_Clear_LogLine_Output()
        {

            sut_PowerButton.Press();
            sut_Door.Open();
            fakeOutput.OutputLine($"Display cleared");
            sut_Door.Close();

            sut_PowerButton.Press();
            sut_TimeButton.Press();
            sut_Door.Open();
            fakeOutput.OutputLine($"Display cleared");
            sut_Door.Close();

            sut_PowerButton.Press();
            sut_TimeButton.Press();
            sut_CookController.StartCooking(50, 60);
            sut_Door.Open();
            fakeOutput.OutputLine($"Display cleared");
        }

        [Test]
        public void Display_TimeLoop_LogLine_Time_Output()
        {
            int min = 1;
            int sec = 0;
            int timeInSec = min*60;

            sut_PowerButton.Press();
            sut_TimeButton.Press();
            sut_CookController.StartCooking(50, timeInSec);

            fakeOutput.OutputLine($"Display shows: {min:D2}:{sec:D2}");

            min = 0;
            for (int i = 0; i < timeInSec-1; i++)
            {
                fakeOutput.OutputLine($"Display shows: {min:D2}:{sec:D2}");
                sec--;
            }
            
        }

        */

    }
}