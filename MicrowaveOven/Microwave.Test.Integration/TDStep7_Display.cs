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
        private IButton sut_PowerButton;
        private IButton sut_TimeButton;
        private IButton sut_StartCancelButton;
        private IDoor sut_Door;

        private ICookController sut_CookController;
        private ITimer fakeTimer;
        private IPowerTube fakePowerTube;


        private UserInterface userInterface;
        private IDisplay sut_Display;
        private ILight sut_Light;
        private IOutput fakeOutput;


        [SetUp]
        public void Setup()
        {
            sut_PowerButton = new Button();
            sut_TimeButton = new Button();
            sut_StartCancelButton = new Button();
            sut_Door = new Door();
            
            sut_Light = new Light(fakeOutput);

            sut_Display = new Display(fakeOutput);

            sut_CookController = new CookController(fakeTimer, sut_Display, fakePowerTube);
            fakeTimer = Substitute.For<ITimer>();
            fakePowerTube = Substitute.For<IPowerTube>();

            userInterface = new UserInterface(sut_PowerButton, sut_TimeButton,
                sut_StartCancelButton, sut_Door, sut_Display,
                sut_Light, sut_CookController);
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