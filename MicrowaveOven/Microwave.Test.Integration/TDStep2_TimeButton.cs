using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microwave.Classes.Boundary;
using Microwave.Classes.Controllers;
using Microwave.Classes.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Test.Integration
{
    [TestFixture]
    public class TDStep2_TimeButton
    {
    
        private Button sut_PowerButton;
        private Button sut_TimeButton;
        private IButton fakeStartCancelButton;
        private IDoor fakeDoor;
        private UserInterface userInterface;
        private IDisplay fakeDisplay;
        private ILight fakeLight;
        private ICookController fakeCookController;

        [SetUp]
        public void Setup()
        {
            sut_PowerButton = new Button();
            sut_TimeButton = new Button();
            fakeStartCancelButton = Substitute.For<IButton>();
            fakeDoor = Substitute.For<IDoor>();
            fakeDisplay = Substitute.For<IDisplay>();
            fakeLight = Substitute.For<ILight>();
            fakeCookController = Substitute.For<ICookController>();

            userInterface = new UserInterface(sut_PowerButton, sut_TimeButton, fakeStartCancelButton, fakeDoor, fakeDisplay,
                fakeLight, fakeCookController);
        }

        [TestCase(1, 1)]
        [TestCase(3, 3)]
        [TestCase(66, 66)]
        public void Press_TimerButton_PowerSet(int pressTimer, int displayMin, int displaySek = 0)
        {
            sut_PowerButton.Press(); // Watts is 50W
            fakeDisplay.Received().ShowPower(50);

            for (int i = 0; i < pressTimer; i++)
            {
                sut_TimeButton.Press();
            }

            fakeDisplay.Received().ShowTime(displayMin, displaySek);
        }

        [Test]
        public void Press_TimerButton_PowerNOTSet()
        {
            sut_TimeButton.Press();

            fakeDisplay.Received(0).ShowTime(Arg.Any<int>(), Arg.Any<int>());
        }

        [Test]
        public void Press_TimerButton_Power_PressAgain()
        {
            int powerFirstPress = 50;

            sut_PowerButton.Press(); // Watts is 50W
            fakeDisplay.Received().ShowPower(powerFirstPress);

            sut_TimeButton.Press();
            fakeDisplay.Received().ShowTime(1,0);

            sut_PowerButton.Press(); // Press power again
            sut_PowerButton.Press(); // Press power again
            fakeDisplay.Received().ShowPower(powerFirstPress);
            fakeDisplay.Received().ShowTime(1, 0);

        }
    }
}