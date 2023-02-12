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
    float squareXV = 0;
    float squareYV = 0;
    float squareX = 400;
    float squareY = 400;

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
        Background(0, 150, 255);
        RectMode(CENTER);
        Fill(255, 0, 0);
        Stroke(0, 0, 0);
        StrokeWeight(6.0f);
        Rect(squareX, squareY, 50, 50);

        if(KeyIsPressed(Keys.Up)) {
            squareYV -= 1;
        }
        if(KeyIsPressed(Keys.Down)) {
            squareYV += 1;
        }
        if(KeyIsPressed(Keys.Left)) {
            squareXV -= 1;
        }
        if(KeyIsPressed(Keys.Right)) {
            squareXV += 1;
        }

        if(squareX + squareXV < 0 || squareX + squareXV > Width) {
            squareXV *= -1;
        }
        if(squareY + squareYV < 0 || squareY + squareYV > Height) {
            squareYV *= -1;
        }

        squareXV *= 0.95f;
        squareYV *= 0.95f;
        squareX += squareXV;
        squareY += squareYV;

        Fill(0, 0, 0);
        Stroke(0, 0, 0);
        TextSize(20);
        DrawText("ASDASDASD", 400, 400);
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
