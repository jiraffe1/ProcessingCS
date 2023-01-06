using System.Windows.Forms;
using System.Drawing;
using System;
using System.Collections.Generic;

namespace ProcessingCS
{
    /*
     * Replicating Processing/p5.js functionality
     * 
     * - This script is as a helper/library and is not intended to be viewed or edited by a user
     * */

    public class PCSWindow : Form
    {
        #region Variables

        public int frameCount = 0;
        private Graphics currentGraphics;
        private Pen strokeColour = new Pen(Color.FromArgb(0, 0, 0), 2.0f);
        private SolidBrush fillColour = new SolidBrush(Color.FromArgb(0, 0, 0, 0));

        private int CanvasWidth = 0;
        private int CanvasHeight = 0;

        private Timer frameTimer;

        public readonly int CORNER = 0;
        public readonly int CENTER = 1;
        private int currentRectMode = 0;

        public bool mouseDown = false;

        public char key;
        public List<Keys> keysDown = new List<Keys>();

        private bool fillEnabled = true;
        private bool strokeEnabled = true;

        public PVector Transform = new PVector(0, 0);

        #endregion

        #region Initialisation

        public PCSWindow()
        {
            Initialise();
        }

        public void Initialise()
        {
            strokeColour = Pens.Black;
            fillColour = new SolidBrush(Color.FromArgb(0, 0, 0, 0));
            Text = "New Window";
            DoubleBuffered = true;

            ClientSize = new System.Drawing.Size(800, 600);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;

            CanvasWidth = 0;
            CanvasHeight = 0;

            frameTimer = new Timer();
            frameTimer.Interval = 1000 / 60;
            frameTimer.Tick += TimerTick;
            frameTimer.Start();


            KeyUp += new KeyEventHandler(KeyReleasedEvent);
            KeyDown += new KeyEventHandler(KeyDownEvent);
            KeyPress += new KeyPressEventHandler(KeyPressedEvent);

            Start(0, 0);
        }

        #endregion

        #region EventFunctions

        private void Start(int width, int height)
        {
            ClientSize = new System.Drawing.Size(width, height);
            CanvasWidth = width;
            CanvasHeight = height;
            Preload();
            Setup();
        }

        public virtual async void Preload()
        {

        }

        public virtual void Setup()
        {
            //CALLED BEFORE THE FIRST FRAME
        }

        public virtual void Draw()
        {
            //CALLED EVERY FRAME
        }

        #endregion

        #region CanvasSettings


        public void FrameRate(int fps)
        {
            frameTimer.Interval = 1000 / fps;
        }

        public void CreateCanvas(int w, int h)
        {
            CanvasWidth = w;
            CanvasHeight = h;
            ClientSize = new System.Drawing.Size(w, h);
        }

        public void Title(string newName)
        {
            Text = newName;
        }

        #endregion

        #region Console

        public void PrintLine(string txt)
        {
            Console.WriteLine(txt);
        }

        public void Print(string txt)
        {
            Console.Write(txt);
        }

        #endregion

        #region DrawingSettings

        public void NoFill()
        {
            fillEnabled = false;
        }

        public void NoStroke()
        {
            strokeEnabled = false;
        }

        public void StrokeWeight(float s)
        {
            strokeColour.Width = s;
            strokeEnabled = true;

            if(s == 0)
            {
                strokeEnabled = false;
            }
        }

        public void RectMode(int rm)
        {
            currentRectMode = rm;
        }

        public void Stroke(int r, int g, int b)
        {
            Color c = Color.FromArgb(255, r, g, b);

            Pen temp = strokeColour;
            strokeColour = new Pen(c, temp.Width);
        }

        public void Stroke(int r, int g, int b, int a)
        {
            Color c = Color.FromArgb(a, r, g, b);

            Pen temp = strokeColour;
            strokeColour = new Pen(c, temp.Width);
        }

        public void Stroke(Pen p)
        {
            Pen temp = strokeColour;
            strokeColour = new Pen(p.Color, temp.Width);
        }

        public void Fill(int r, int g, int b)
        {
            Color c = Color.FromArgb(255, r, g, b);
            fillColour = new SolidBrush(c);
            fillEnabled = true;
        }

        public void Fill(int r, int g, int b, int a)
        {
            Color c = Color.FromArgb(a, r, g, b);
            fillColour = new SolidBrush(c);
            fillEnabled = true;
        }


        public void Background(int r, int g, int b)
        {
            Fill(r, g, b);
            Stroke(0, 0, 0, 0);
            int pRectMode = currentRectMode;
            RectMode(CORNER);
            Rect(0, 0, Width, Height);
            currentRectMode = pRectMode;
        }

        public void Background(int r, int g, int b, int a)
        {
            Fill(r, g, b, a);
            Stroke(0, 0, 0, 0);
            int pRectMode = currentRectMode;
            RectMode(CORNER);
            Rect(0, 0, Width, Height);
            currentRectMode = pRectMode;
        }

        public void Translate(float x, float y)
        {
            Transform.x += x;
            Transform.y += y;
        }

        public void Translate(PVector v)
        {
            Transform = Transform.add(v);
        }

        public void SetTranslation(float x, float y)
        {
            Transform = new PVector(x, y);
        }

        public void SetTranslation(PVector v)
        {
            Transform = v;
        }

        #endregion

        #region PrimitiveShapes

        public void Rect(float x, float y, float w, float h)
        {
            Graphics g = getGraphics();

            if (currentRectMode == CORNER)
            {
                if(fillEnabled)g.FillRectangle(fillColour, x + Transform.x, y + Transform.y, w, h);
                if(strokeEnabled)g.DrawRectangle(strokeColour, x + Transform.x, y + Transform.y, w, h);
            }
            else if (currentRectMode == CENTER)
            {
                if (fillEnabled) g.FillRectangle(fillColour, x + Transform.x - w / 2, y + Transform.y - h / 2, w, h);
                if (strokeEnabled) g.DrawRectangle(strokeColour, x + Transform.x - w / 2, y + Transform.y - h / 2, w, h);
            }
        }

        public void Ellipse(float x, float y, float w, float h)
        {
            Graphics g = getGraphics();

            if (fillEnabled) g.FillEllipse(fillColour, x + Transform.x, y + Transform.y, w, h);
            if (strokeEnabled) g.DrawEllipse(strokeColour, x + Transform.x, y + Transform.y, w, h);
        }

        public void Circle(float x, float y, float r)
        {
            Graphics g = getGraphics();

            if (fillEnabled) g.FillEllipse(fillColour, x + Transform.x, y + Transform.y, r * 2, r * 2);
            if (strokeEnabled) g.DrawEllipse(strokeColour, x + Transform.x, y + Transform.y, r  * 2, r * 2);
        }

        public void Line(float x1, float y1, float x2, float y2)
        {
            Graphics g = getGraphics();

            if (strokeEnabled) g.DrawLine(strokeColour, x1 + Transform.x, y1 + Transform.y, x2 + Transform.x, y2 + Transform.y);
        }

        public void Line(PVector v1, PVector v2)
        {
            float x1 = v1.x;
            float y1 = v1.y;
            float x2 = v2.x;
            float y2 = v2.y;
            Graphics g = getGraphics();

            if (strokeEnabled) g.DrawLine(strokeColour, x1 + Transform.x, y1 + Transform.y, x2 + Transform.x, y2 + Transform.y);
        }

        #endregion

        #region KeyboardInput


        public virtual void KeyPressed()
        {

        }

        public bool KeyIsPressed(Keys k)
        {
            foreach (Keys e in keysDown)
            {
                if (k == e)
                {
                    return true;
                }
            }

            return false;
        }

        #endregion

        #region MouseInput

        public int MouseX()
        {
            return PointToClient(Cursor.Position).X;
        }

        public int MouseY()
        {
            return PointToClient(Cursor.Position).Y;
        }

        public bool MouseIsPressed()
        {
            // is the left mouse button down?
            return Control.MouseButtons == MouseButtons.Left;
        }

        public bool RightMouseIsPressed()
        {
            // is the right mouse button down?
            return Control.MouseButtons == MouseButtons.Right;
        }

        public bool MiddleMouseIsPressed()
        {
            // is the middle mouse button down (scrollwheel) 
            return Control.MouseButtons == MouseButtons.Middle;
        }

        public MouseButtons MousePressed()
        {
            // which mouse button is pressed
            return Control.MouseButtons;
        }

        #endregion

        #region Maths

        public double Dist(float x1, float y1, float x2, float y2)
        {
            return Math.Sqrt((Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2)));
        }

        public double Dist(PVector v1, PVector v2)
        {
            return Dist(v1.x, v1.y, v2.x, v2.y);
        }

        #endregion

        #region System.Drawing Interface

        public void KeyDownEvent(object o, KeyEventArgs e)
        {
            Keys nkey = e.KeyCode;
            bool alreadyContains = false;

            foreach (Keys c in keysDown)
            {
                if (c == nkey)
                {
                    alreadyContains = true;
                }
            }

            if (!alreadyContains)
            {
                keysDown.Add(nkey);
            }
        }

        public void KeyReleasedEvent(object o, KeyEventArgs e)
        {
            Keys nkey = e.KeyCode;
            keysDown.Remove(nkey);
        }

        public void KeyPressedEvent(object o, KeyPressEventArgs e)
        {
            key = e.KeyChar;
            KeyPressed();

        }

        public void DrawFrame(Graphics graphics)
        {
            Draw();
        }

        public Graphics getGraphics()
        {
            //this is useless
            return currentGraphics;
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            //base.OnPaint(e);
            currentGraphics = e.Graphics;
            DrawFrame(e.Graphics);
        }

        private void TimerTick(object sender, EventArgs e)
        {
            //trigger a redraw

            Invalidate();

            frameCount++;
        }

        #endregion
    }
    #region Vector
    public class PVector //2D Vector
    {
        public float x;
        public float y;

        public PVector()
        {
            x = 0;
            y = 0;
        }

        public PVector(float nx, float ny)
        {
            x = nx;
            y = ny;
        }

        public PVector add(PVector toAdd)
        {
            return new PVector(x + toAdd.x, y + toAdd.y);
        }

        public PVector bub(PVector toSub)
        {
            return new PVector(x - toSub.x, y - toSub.y);
        }

        public double Dist(PVector other)
        {
            return Math.Sqrt((Math.Pow(x - other.x, 2) + Math.Pow(y - other.y, 2)));
        }
    }
    #endregion
}