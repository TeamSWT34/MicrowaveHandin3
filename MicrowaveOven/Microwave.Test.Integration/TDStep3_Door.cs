using Microwave.Classes.Boundary;
using Microwave.Classes.Controllers;
using Microwave.Classes.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Test.Integration
{
    [TestFixture]
    public class TDStep3_Door
    {
	    private Button sut_PowerButton;
	    private Button sut_TimeButton;
        private Door sut_Door;

		private IButton fakeStartCancelButton;
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
		    sut_Door = new Door();
		    fakeDisplay = Substitute.For<IDisplay>();
		    fakeLight = Substitute.For<ILight>();
		    fakeCookController = Substitute.For<ICookController>();

		    userInterface = new UserInterface(sut_PowerButton, sut_TimeButton, 
			    fakeStartCancelButton, sut_Door, fakeDisplay,
			    fakeLight, fakeCookController);
	    }

	    [Test] 
	    public void Open_Door_LightOn_StateDOOROPEN()
	    {
			sut_Door.Open();
			fakeLight.Received().TurnOn();

			sut_PowerButton.Press();
			fakeDisplay.Received(0).ShowPower(Arg.Any<int>());
	    }

		[Test] 
	    public void Open_Closed_Door_LightOff_StateREADY()
	    {
			sut_Door.Open();
			fakeLight.Received().TurnOn();
			sut_Door.Close();
			fakeLight.Received().TurnOff();

			sut_PowerButton.Press();
			fakeDisplay.Received(1).ShowPower(Arg.Any<int>());
		}

	    [Test]
	    public void PowerPress_OpenDoor_ClearDisplay_LightOn()
	    {
			sut_PowerButton.Press();
			sut_PowerButton.Press();
			fakeDisplay.Received().ShowPower(100);

			sut_Door.Open();

			fakeLight.Received().TurnOn();
			fakeDisplay.Received().Clear();

			sut_Door.Close();
			fakeLight.Received().TurnOff();

			sut_PowerButton.Press();
			fakeDisplay.Received(2).ShowPower(50);
		}


	    [Test]
	    public void TimePress_OpenDoor_ClearDisplay_LightOn()
	    {
			sut_PowerButton.Press();
			sut_PowerButton.Press();
			fakeDisplay.Received().ShowPower(100);

			sut_TimeButton.Press();
			sut_TimeButton.Press();
			fakeDisplay.Received().ShowTime(2, 0);
			
			sut_Door.Open();
			
			fakeLight.Received().TurnOn();
			fakeDisplay.Received().Clear();

			sut_Door.Close();
			fakeLight.Received().TurnOff();

			sut_PowerButton.Press();
			fakeDisplay.Received(2).ShowPower(50);

			sut_TimeButton.Press();
			fakeDisplay.Received(2).ShowTime(1, 0);

			
	    }
    }
}