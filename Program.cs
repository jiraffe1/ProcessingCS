using System.Windows.Forms;
using ProcessingCS;

/*
 * HOW TO:
 * 
 * - Include the file "ProcessingCS.cs in the project"
 * - use the namespace ProcessingCS
 * - Make it so that this class extends from PCSWindow
 * - In the class Program's function Main, change the main variable's class type to be the same name as the class
 * */
class TestProgram : PCSWindow
{
    //do not edit V
    public TestProgram()
    {
        Initialise();
    }
    //do not edit ^

    public override void Setup()
    {
        Title("ProcessingC# test");
        CreateCanvas(800, 800);
    }

    public override void Draw()
    {
        RectMode(CENTER);
        Background(0, 150, 255, 255);
        StrokeWeight(5.0f);
        Stroke(0, 0, 0);
        if (MouseIsPressed()) {
            Fill(255, 0, 0, 150);
        }
        else
        {
            Fill(0, 255, 0, 150);
        }

        if (KeyIsPressed(Keys.A))
        {
            Fill(45, 45, 45);
        }
        if(KeyIsPressed(Keys.S))
        {
            NoFill();
        }

        if(Dist(MouseX(), MouseY(), Width / 2, Height / 2) < 100)
        {
            Fill(255, 255, 0);
            NoStroke();
        }
        
        Rect(MouseX(), MouseY(), 56, 56);
        Line(0, 0, MouseX(), MouseY());

    }

    public override void KeyPressed()
    {
    }
}

class Program
{
    static void Main(string[] args)
    {
        // this must be changed to the class name
        TestProgram main = new TestProgram();
        Application.Run(main);
    }
}
