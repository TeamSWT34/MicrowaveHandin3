using Microwave.Classes.Boundary;
using Microwave.Classes.Controllers;
using Microwave.Classes.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Test.Integration
{
    [TestFixture]
    public class TDStep4_StartCancelButton
    {
		private Button sut_PowerButton;
        private Button sut_TimeButton;
        private Button sut_StartCancelButton;
        private Door sut_Door;

        private UserInterface userInterface;
        private IDisplay fakeDisplay;
        private ILight fakeLight;
        private ICookController fakeCookController;

        [SetUp]
        public void Setup()
        {
            sut_PowerButton = new Button();
            sut_TimeButton = new Button();
            sut_StartCancelButton = new Button();
            sut_Door = new Door();
            fakeDisplay = Substitute.For<IDisplay>();
            fakeLight = Substitute.For<ILight>();
            fakeCookController = Substitute.For<ICookController>();

            userInterface = new UserInterface(sut_PowerButton, sut_TimeButton,
                sut_StartCancelButton, sut_Door, fakeDisplay,
                fakeLight, fakeCookController);
        }



        [Test]
        public void PowerPress_StartCancelButton_ClearDisplay_ResetValues()
        {
            sut_PowerButton.Press();
            sut_PowerButton.Press();
            fakeDisplay.Received(1).ShowPower(100);

            sut_StartCancelButton.Press();
            fakeDisplay.Received().Clear();

            sut_PowerButton.Press();
            fakeDisplay.Received(2).ShowPower(50);
        }


        [Test]
        public void TimePress_StartCancelButton_ClearDisplay_ResetValues()
        {
            int powerValue = 50;
            int timeInMin = 1;
            int timeInSek = timeInMin * 60;

            sut_PowerButton.Press();
            fakeDisplay.Received(1).ShowPower(powerValue);

            sut_TimeButton.Press();
            fakeDisplay.Received(1).ShowTime(timeInMin, 0);

            sut_StartCancelButton.Press();
            fakeLight.Received().TurnOn();
            fakeCookController.Received().StartCooking(powerValue, timeInSek);

        }

	}
}