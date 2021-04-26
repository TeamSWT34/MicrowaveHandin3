using Microwave.Classes.Boundary;
using Microwave.Classes.Controllers;
using Microwave.Classes.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Test.Integration
{
    [TestFixture]
    public class TDStep6_Light
    {
        private IButton sut_PowerButton;
        private IButton sut_TimeButton;
        private IButton sut_StartCancelButton;
        private IDoor sut_Door;

        private ICookController sut_CookController;
        private ITimer fakeTimer;
        private IPowerTube fakePowerTube;


        private UserInterface userInterface;
        private IDisplay fakeDisplay;
        private ILight sut_Light;
        private IOutput fakeOutput;


        [SetUp]
        public void Setup()
        {
            sut_PowerButton = new Button();
            sut_TimeButton = new Button();
            sut_StartCancelButton = new Button();
            sut_Door = new Door();
            sut_CookController = new CookController(fakeTimer, fakeDisplay, fakePowerTube);
            sut_Light = new Light(fakeOutput);

            fakeDisplay = Substitute.For<IDisplay>();
            fakeTimer = Substitute.For<ITimer>();
            fakePowerTube = Substitute.For<IPowerTube>();

            userInterface = new UserInterface(sut_PowerButton, sut_TimeButton,
                sut_StartCancelButton, sut_Door, fakeDisplay,
                sut_Light, sut_CookController);
        }

/*
        [Test]
        public void Open_Door_LightOn_LogLine_Output()
        {
            string stringLine = "Light is turned on";

            sut_Door.Open();
            sut_Light.TurnOn();

            fakeOutput.OutputLine(stringLine);
        }

        [Test]
        public void Close_Door_LightOFF_LogLine_Output()
        {
            string stringLine = "Light is turned off";

            sut_Door.Open();
            sut_Door.Close();
            sut_Light.TurnOff();
             
            fakeOutput.OutputLine(stringLine);
        }
*/
    }
}