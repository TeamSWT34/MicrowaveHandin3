using Microwave.Classes.Boundary;
using Microwave.Classes.Controllers;
using Microwave.Classes.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Test.Integration
{
    [TestFixture]
    public class TDStep5_CookController
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
        private ILight fakeLight;
        

        [SetUp]
        public void Setup()
        {
            sut_PowerButton = new Button();
            sut_TimeButton = new Button();
            sut_StartCancelButton = new Button();
            sut_Door = new Door();
            fakeDisplay = Substitute.For<IDisplay>();
            fakeLight = Substitute.For<ILight>();
            fakeTimer = Substitute.For<ITimer>();
            fakePowerTube = Substitute.For<IPowerTube>();
            sut_CookController = new CookController(fakeTimer, fakeDisplay,fakePowerTube);

            userInterface = new UserInterface(sut_PowerButton, sut_TimeButton,
                sut_StartCancelButton, sut_Door, fakeDisplay,
                fakeLight, sut_CookController);
        }

/*
        [Test]
        public void Cooking_Open_Door_StopCooking()
        {
            int powerValue = 50;
            int timeInMin = 1;
            int timeInSek = timeInMin * 60;

            sut_PowerButton.Press();
            sut_TimeButton.Press();
            sut_StartCancelButton.Press();

            sut_CookController.StartCooking(powerValue, timeInSek);

            sut_Door.Open();
            sut_CookController.Stop();
            fakeDisplay.Clear();

            fakeLight.Received().TurnOn();

            sut_Door.Close();
            fakeLight.TurnOff();

        }


        [Test]
        public void Cooking_Start_StopButton_Press_StopCooking()
        {
            int powerValue = 50;
            int timeInMin = 1;
            int timeInSek = timeInMin * 60;

            sut_PowerButton.Press();
            sut_TimeButton.Press();
            sut_StartCancelButton.Press();

            sut_CookController.StartCooking(powerValue, timeInSek);
            sut_StartCancelButton.Press();


            sut_CookController.Stop();
            fakeDisplay.Clear();
            fakeLight.Received().TurnOff();

            sut_Door.Open();
            fakeLight.TurnOn();

        }


        [Test]
        public void Cooking_Finished()
        {
            int powerValue = 50;
            int timeInMin = 1;
            int timeInSek = timeInMin * 60;

            sut_PowerButton.Press();
            sut_TimeButton.Press();
            sut_StartCancelButton.Press();

            sut_CookController.StartCooking(powerValue, timeInSek);
            


            sut_CookController.Stop();
            fakeDisplay.Clear();
            fakeLight.Received().TurnOff();

            sut_Door.Open();
            fakeLight.TurnOn();

        }
*/
    }
}