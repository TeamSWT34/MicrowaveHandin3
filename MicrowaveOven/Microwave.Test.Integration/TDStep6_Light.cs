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
        private Button sut_PowerButton;
        private Button sut_TimeButton;
        private Button sut_StartCancelButton;
        private Door sut_Door;

        private CookController cookController;
        private Light light;

        private ITimer fakeTimer;
        private IPowerTube fakePowerTube;


        private UserInterface userInterface;
        private IDisplay fakeDisplay;
        
        private IOutput fakeOutput;


        [SetUp]
        public void Setup()
        {
            sut_PowerButton = new Button();
            sut_TimeButton = new Button();
            sut_StartCancelButton = new Button();
            sut_Door = new Door();
            

            fakeDisplay = Substitute.For<IDisplay>();
            fakeTimer = Substitute.For<ITimer>();
            fakePowerTube = Substitute.For<IPowerTube>();
            fakeOutput = Substitute.For<IOutput>();

            cookController = new CookController(fakeTimer, fakeDisplay, fakePowerTube);
            light = new Light(fakeOutput);

            userInterface = new UserInterface(sut_PowerButton, sut_TimeButton,
                sut_StartCancelButton, sut_Door, fakeDisplay,
                light, cookController);

            cookController.UI = userInterface;
        }


        [Test]
        public void Open_Door_LightOn_LogLine_Output()
        {
            string stringLine = "Light is turned on";

            sut_Door.Open();
            fakeOutput.Received().OutputLine(stringLine);
        }

        [Test]
        public void Close_Door_LightOFF_LogLine_Output()
        {
            string stringLine = "Light is turned off";

            sut_Door.Open();
            sut_Door.Close();

            fakeOutput.Received().OutputLine(stringLine);
        }

    }
}