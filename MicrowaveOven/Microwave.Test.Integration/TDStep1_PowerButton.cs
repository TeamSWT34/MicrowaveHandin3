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
        private Button sut;
        private UserInterface userInterface;
        private IButton fakeTimeButton;
        private IButton fakeStartCancelButton;
        private IDoor fakeDoor;
        private IDisplay fakeDisplay;
        private ILight fakeLight;
        private ICookController fakeCookController;
        


        [SetUp]
        public void Setup()
        {
            sut = new Button();
            fakeTimeButton = Substitute.For<IButton>();
            fakeStartCancelButton = Substitute.For<IButton>();
            fakeDoor = Substitute.For<IDoor>();
            fakeDisplay = Substitute.For<IDisplay>();
            fakeLight = Substitute.For<ILight>();
            fakeCookController = Substitute.For<ICookController>();

            userInterface = new UserInterface(sut, fakeTimeButton, fakeStartCancelButton, fakeDoor, fakeDisplay,
                fakeLight, fakeCookController);

        }

        //Press-Numbers, Watts-Value
        [TestCase(1, 50)]
        [TestCase(10, 500)]
        [TestCase(14, 700)] //Max Boundary
        [TestCase(15, 50)] //Max Boundary + 1
        public void Press_PowerButton_CorretValues(int press, int watts)
        {
            
            for (int i = 0; i < press-1; i++)
            {
                sut.Press();
            }
            fakeDisplay.ClearReceivedCalls();

            sut.Press();
            fakeDisplay.Received().ShowPower(watts);
        }


    }
}