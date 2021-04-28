using Microwave.Classes.Boundary;
using Microwave.Classes.Controllers;
using Microwave.Classes.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Test.Integration
{
    [TestFixture]
    public class TDStep9_PowerTube
    {
        private Button sut_PowerButton;
        private Button sut_TimeButton;
        private Button sut_StartCancelButton;
        private Door sut_Door;

        private CookController cookController;
        private Timer timer;
        private PowerTube powerTube;


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
            powerTube = new PowerTube(fakeOutput);


            cookController = new CookController(timer, display, powerTube);
            light = new Light(fakeOutput);

            userInterface = new UserInterface(sut_PowerButton, sut_TimeButton,
                sut_StartCancelButton, sut_Door, display,
                light, cookController);

            cookController.UI = userInterface;
        }

        [TestCase(1,50)]
        [TestCase(2,100)]
        [TestCase(5, 250)]
        [TestCase(10, 500)]
        [TestCase(14, 700)]
        [TestCase(15, 50)]
        public void PowerTube_StartStopButton_Start_Output(int press,  int power)
        {
            for (int i = 0; i < press; i++)
                sut_PowerButton.Press();
            

            sut_TimeButton.Press();
            sut_StartCancelButton.Press();
            fakeOutput.Received().OutputLine($"PowerTube works with {power}");
        }

        [Test]
        public void PowerTube_StartStopButton_Stop_Output()
        {
            sut_PowerButton.Press();
            sut_PowerButton.Press();
            sut_TimeButton.Press();
            sut_StartCancelButton.Press();
            sut_StartCancelButton.Press();
            fakeOutput.Received().OutputLine($"PowerTube turned off");
        }

        [Test]
        public void PowerTube_OpenDoor_Start_Output()
        {

            sut_PowerButton.Press();
            sut_TimeButton.Press();
            sut_StartCancelButton.Press();
            sut_Door.Open();
            fakeOutput.Received().OutputLine($"PowerTube turned off");

        }

    }
}