using Microwave.Classes.Boundary;
using Microwave.Classes.Controllers;
using Microwave.Classes.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Test.Integration
{
    [TestFixture]
    public class TDStep1_PowerButton
    {
        private Button sut_PowerButton;
        private IButton fakeTimeButton;
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
            fakeTimeButton = Substitute.For<IButton>();
            fakeStartCancelButton = Substitute.For<IButton>();
            fakeDoor = Substitute.For<IDoor>();
            fakeDisplay = Substitute.For<IDisplay>();
            fakeLight = Substitute.For<ILight>();
            fakeCookController = Substitute.For<ICookController>();

            userInterface = new UserInterface(sut_PowerButton, fakeTimeButton, fakeStartCancelButton, fakeDoor, fakeDisplay,
                fakeLight, fakeCookController);
        }

        //Press-Numbers, Watts-Value
        [TestCase(1, 50)]
        [TestCase(10, 500)]
        [TestCase(14, 700)] //Max Boundary
        [TestCase(15, 50)] //Max Boundary + 1
        [TestCase(19, 250)] //Max Boundary + 5
        public void Press_PowerButton_DisplayCorrectValues(int press, int watts)
        {
            
            for (int i = 0; i < press-1; i++)
            {
                sut_PowerButton.Press();
            }

            //press = 18;
            fakeDisplay.ClearReceivedCalls();
            fakeDisplay.Received(0).ShowPower(Arg.Any<int>());

            sut_PowerButton.Press();
            fakeDisplay.Received().ShowPower(watts);
        }

    }
}