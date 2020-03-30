using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenCvSharp;

namespace MacroExample
{
    public partial class Main : Form
    {
        private const string QT5_WINDOW_ICON = "Qt5QWindowIcon";
        private const string SCREEN_BOARD_CLASS_WINDOW = "ScreenBoardClassWindow";
        private const string Q_WIDGET_CLASS_WINDOW = "QWidgetClassWindow";

        [System.Runtime.InteropServices.DllImport("User32", EntryPoint = "FindWindow")]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        internal static extern bool PrintWindow(IntPtr hWnd, IntPtr hdcBlt, int nFlags);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, IntPtr lParam);

        [System.Runtime.InteropServices.DllImport("User32.dll")]
        public static extern IntPtr FindWindowEx(IntPtr Parent, IntPtr Child, string lpszClass, string lpszWindows);

        public const int WM_LBUTTONDOWN = 0x201;
        public const int WM_LBUTTONUP = 0x202;

        private const int STAGE_COUNT = 8;

        String AppPlayerName = "NoxPlayer";

        List<Bitmap> stageImages = new List<Bitmap>();
        List<Bitmap> ingameImages = new List<Bitmap>();

        Bitmap testImage = null;
        Bitmap autoOffButtonImage= null;
        Bitmap fightCompleteRainImage = null;
        Bitmap stageImage = null;
        Bitmap stageTeamSelectButtonImage = null;
        Bitmap stageFightButtonImage = null;

        Bitmap stagePageImage = null;

        //앱플레이어 크기를 저정할 변수
        double full_width = 0;
        double full_height = 0;
        //앱플레이어 지정한 크기
        double pix_width = 615;
        double pix_height = 375;

        bool roopContinue = false;

        public Main()
        {
            InitializeComponent();

            testImage = new Bitmap(@"img\test_img.PNG");
            autoOffButtonImage = new Bitmap(@"img\auto_button_off_image.PNG");
            fightCompleteRainImage = new Bitmap(@"img\fightcomplete_rain_image.PNG");
            stageImage = new Bitmap(@"img\4-4_image.PNG");
            stageTeamSelectButtonImage = new Bitmap(@"img\stage_team_select_button_image.PNG");
            stageFightButtonImage = new Bitmap(@"img\stage_fight_button_image.PNG");

            stagePageImage = new Bitmap(@"img\stage_page.PNG");


            stageImages.Add(stageImage);
            stageImages.Add(stageTeamSelectButtonImage);
            stageImages.Add(stageFightButtonImage);

            //ingameImages.Add(autoOffButtonImage);
            ingameImages.Add(fightCompleteRainImage);
            ingameImages.Add(fightCompleteRainImage);
        }

        private void TestCaptureButtonClick(object sender, EventArgs e)
        {
            IntPtr findWindow = FindWindow(null, AppPlayerName);
            if(findWindow != IntPtr.Zero)
            {
                //플레이어를 찾았을 경우
                Debug.WriteLine("앱플레이어를 찾았습니다.");

                //찾은 플레이어를 바타응로 그래픽스 정보를 가져온다.
                Graphics Graphicsdata = Graphics.FromHwnd(findWindow);

                //찾은 플레이어 창 크기 및 위치를 가져온다. 
                Rectangle rect = Rectangle.Round(Graphicsdata.VisibleClipBounds);

                //현재 앱플레이어 화면 크기를 지정해줍니다.
                full_width = rect.Width;
                full_height = rect.Height;

                pictureBox1.Width = rect.Width + 50;
                pictureBox1.Height = rect.Height + 50;

                //플레이어 창 크기만큼의 비트맵을 선언
                Bitmap bmp = new Bitmap(rect.Width, rect.Height);

                Debug.WriteLine(rect.Width + " / " + rect.Height);

                //비트맵을 바탕으로 그래픽스 함수로 선언
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    //찾은 플레이어의 크기만큼 화면을 캡쳐
                    IntPtr hdc = g.GetHdc();
                    PrintWindow(findWindow, hdc, 0x2);
                    g.ReleaseHdc(hdc);
                }

                //캡처해온 이미지 사이즈를 변경하여 저장합니다.
                System.Drawing.Size resize = new System.Drawing.Size((int)pix_width, (int)pix_height);
                bmp = new Bitmap(bmp, resize);

                pictureBox1.Image = bmp;

                int[] ClcikValue = SearchImagePosition(bmp, testImage);
                InClick(ClcikValue[0] + (int)Int32.Parse(XPositionTextbox.Text), ClcikValue[1] + (int)Int32.Parse(YPositionTextbox.Text));
            }
            else
            {
                //플레이어를 못찾은 경우
                Debug.WriteLine("못찾았어요.");
            }
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            //4-4 -> 팀선택 -> 전투 -> autocheck -> 작전완료 -> 작전완료
            //autocheck -> 작전완료 -> 작전완료
            roopContinue = true;
            for (int i = 0; roopContinue; ++i)
            {
                Bitmap screen = CaptureScreen();
                if (screen != null)
                {
                    pictureBox1.Image = screen;

                    int[] clickValue = SearchImagePosition(screen, stageImages[i]);
                    Debug.WriteLine("stageImages" + i);
                    if (clickValue[0] > 0 && clickValue[1] > 0)
                        InClick(clickValue[0] + (int)Int32.Parse(XPositionTextbox.Text), clickValue[1] + (int)Int32.Parse(YPositionTextbox.Text));
                    else
                        --i;

                    if (i == 2)
                    {
                        //for(int j = 0; j < STAGE_COUNT; ++j)
                        StartIngameLoop();
                        i = -1;
                    }
                }
                else
                {
                    Debug.WriteLine("이미지를 찾을 수 없습니다.");
                    break;
                }

                Thread.Sleep(1000);
            }
            pictureBox1.Image = null;
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            roopContinue = false;
        }

        public int[] SearchImagePosition(Bitmap screenImage, Bitmap findImage)
        {
            //스크린 이미지 선언
            using (Mat ScreenMat = OpenCvSharp.Extensions.BitmapConverter.ToMat(screenImage))
            // 찾을 이미지 선언
            using (Mat FindMat = OpenCvSharp.Extensions.BitmapConverter.ToMat(findImage))
            //스크린 이미지에서 FindMat 이미지를 찾아라
            using (Mat res = ScreenMat.MatchTemplate(FindMat, TemplateMatchModes.CCoeffNormed))
            {
                //찾은 이미지의 유사도를 담은 더블형 최대 최소 값을 선언
                double minval, maxval = 0;

                //찾은 이미지의 위치를 담은 포인트형을 선언한다.
                OpenCvSharp.Point minloc, maxloc;

                //찾은 이미지의 유사도 및 위치 값을 받는다. 
                Cv2.MinMaxLoc(res, out minval, out maxval, out minloc, out maxloc);
                Debug.WriteLine($"찾은 이미지의 유사도 : {maxval}, 위치 : {minloc}, {maxloc}");

                double change_size = full_width / pix_width;

                int[] returnValue = new int[2];
                if (maxval >= 0.7)
                {
                    returnValue[0] = maxloc.X + findImage.Width/2;
                    returnValue[1] = maxloc.Y + findImage.Height/2;
                }

                return returnValue;
            }
        }

        public void InClick(int x, int y)
        {
            //클릭이벤트를 발생시킬 플레이어를 찾습니다.
            IntPtr findwindow = FindWindow(null, AppPlayerName);
            if (findwindow != IntPtr.Zero)
            {
                //플레이어를 찾았을 경우 클릭이벤트를 발생시킬 핸들을 가져옵니다.
                IntPtr hwnd_child = FindWindowEx(findwindow, IntPtr.Zero, QT5_WINDOW_ICON, SCREEN_BOARD_CLASS_WINDOW);
                IntPtr lparam = new IntPtr(x | (y << 16));

                //플레이어 핸들에 클릭 이벤트를 전달합니다.
                SendMessage(hwnd_child, WM_LBUTTONDOWN, 1, lparam);
                SendMessage(hwnd_child, WM_LBUTTONUP, 0, lparam);
            }
        }

        private Bitmap CaptureScreen()
        {
            IntPtr findWindow = FindWindow(null, AppPlayerName);
            if (findWindow != IntPtr.Zero)
            {
                //플레이어를 찾았을 경우
                Debug.WriteLine("앱플레이어를 찾았습니다.");

                //찾은 플레이어를 바탕으로 그래픽스 정보를 가져온다.
                Graphics Graphicsdata = Graphics.FromHwnd(findWindow);

                //찾은 플레이어 창 크기 및 위치를 가져온다. 
                Rectangle rect = Rectangle.Round(Graphicsdata.VisibleClipBounds);

                //현재 앱플레이어 화면 크기를 지정해줍니다.
                full_width = rect.Width;
                full_height = rect.Height;

                pictureBox1.Width = rect.Width + 50;
                pictureBox1.Height = rect.Height + 50;

                //플레이어 창 크기만큼의 비트맵을 선언
                Bitmap bmp = new Bitmap(rect.Width, rect.Height);

                //비트맵을 바탕으로 그래픽스 함수로 선언
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    //찾은 플레이어의 크기만큼 화면을 캡쳐
                    IntPtr hdc = g.GetHdc();
                    PrintWindow(findWindow, hdc, 0x2);
                    g.ReleaseHdc(hdc);
                }

                //캡처해온 이미지 사이즈를 변경하여 저장합니다.
                System.Drawing.Size resize = new System.Drawing.Size((int)pix_width, (int)pix_height);
                bmp = new Bitmap(bmp, resize);

                return bmp;
            }
            //플레이어를 못찾은 경우
            Debug.WriteLine("못찾았어요.");
            return null;
        }

        private void StartIngameLoop()
        {
            if (CheckStagePage())
                return;

            Debug.WriteLine("StartIngameLoop");
            for (int i = 0; ingameImages.Count > i; ++i)
            {
                Bitmap screen = CaptureScreen();
                if (screen != null)
                {
                    if (CheckStagePage())
                        return;

                    pictureBox1.Image = screen;

                    int[] clickValue = SearchImagePosition(screen, ingameImages[i]);
                    //Debug.WriteLine("ingameImages" + i);
                    if (clickValue[0] > 0 && clickValue[1] > 0)
                        InClick(clickValue[0] + (int)Int32.Parse(XPositionTextbox.Text), clickValue[1] + (int)Int32.Parse(YPositionTextbox.Text));
                    else
                        --i;
                }
                else
                {
                    Debug.WriteLine("이미지를 찾을 수 없습니다.");
                    break;
                }

                Thread.Sleep(1000);
            }
            if (!CheckStagePage())
                StartIngameLoop();
        }

        private bool CheckStagePage()
        {
            Thread.Sleep(2000);
            Debug.WriteLine("CheckStagePage");
            Bitmap screen = CaptureScreen();
            if (screen != null)
            {
                pictureBox1.Image = screen;

                int[] clickValue = SearchImagePosition(screen, stagePageImage);
                if (clickValue[0] > 0 && clickValue[1] > 0)
                    return true;
            }
            return false;
        }
    }
}
