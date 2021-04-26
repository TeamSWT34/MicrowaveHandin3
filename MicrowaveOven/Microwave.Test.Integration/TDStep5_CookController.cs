using System;
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
        private Button sut_PowerButton;
        private Button sut_TimeButton;
        private Button sut_StartCancelButton;
        private Door sut_Door;
        
        private CookController cookController;
        private ITimer fakeTimer;
        private IPowerTube fakePowerTube;


        private UserInterface userInterface;
        private IUserInterface fakeUserInterface;
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
            fakeUserInterface = Substitute.For<IUserInterface>();

            cookController = new CookController(fakeTimer, fakeDisplay,fakePowerTube, fakeUserInterface);

            userInterface = new UserInterface(sut_PowerButton, sut_TimeButton,
                sut_StartCancelButton, sut_Door, fakeDisplay,
                fakeLight, cookController);

     
        }


        [Test]
        public void Cooking_Open_Door_StopCooking()
        {
            int powerValue = 50;

            sut_PowerButton.Press();
            sut_TimeButton.Press();
            sut_StartCancelButton.Press();

            //Cooking Start
            fakeLight.Received().TurnOn();
            sut_Door.Open();
            //Cooking Stop

            fakeDisplay.Clear();

            sut_Door.Close();
            fakeLight.TurnOff();


            sut_PowerButton.Press();
            fakeDisplay.Received(2).ShowPower(powerValue);
        }


        [Test]
        public void Cooking_Start_StopButton_Press_StopCooking()
        {
            int powerValue = 50;

            sut_PowerButton.Press();
            sut_PowerButton.Press();
            sut_TimeButton.Press();
            sut_StartCancelButton.Press();

            //Cooking Start
            fakeLight.Received().TurnOn();
            sut_StartCancelButton.Press();
            //Cooking Stop

            fakeDisplay.Clear();
            fakeLight.Received().TurnOff();

            sut_PowerButton.Press();
            fakeDisplay.Received(2).ShowPower(powerValue);

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

            //Cooking Start
            fakeTimer.Expired += Raise.EventWith(this, EventArgs.Empty);
            //Cooking Finished

            fakeUserInterface.Received().CookingIsDone();
            fakePowerTube.Received().TurnOff();

        }
    }
}