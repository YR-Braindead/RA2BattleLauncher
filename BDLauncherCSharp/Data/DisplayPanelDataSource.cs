﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using BDLauncherCSharp.Extensions;

namespace BDLauncherCSharp.Data
{
    public class DisplayPanelDataSource
    {
        public static string ScreenSize_Full()
        {
            int SH = Screen.PrimaryScreen.Bounds.Height;
            int SW = Screen.PrimaryScreen.Bounds.Width;
            var CurSize = SW.ToString() + "*" + SH.ToString();
            return CurSize;
        }

        public static IEnumerable<string> ScreeSize_ListAll()
        {
            var list = new HashSet<string>();
            for (var i = 0; EnumDisplaySettings(null, i, out var vDevMode); i++)
                list.Add(vDevMode.dmPelsWidth + "*" + vDevMode.dmPelsHeight);
            return list;
        }

        public static IEnumerable<string> Renderers_ListAll()
        {
            return new HashSet<string>{
                I18NExtension.I18N("cbRenderer.None"),
                I18NExtension.I18N("cbRenderer.CNCDDraw")
            };
        }

        //Thanks to yirol
        [StructLayout(LayoutKind.Sequential)]
        public struct DEVMODE
        {
            private const int CCHDEVICENAME = 0x20;
            private const int CCHFORMNAME = 0x20;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
            public string dmDeviceName;
            public short dmSpecVersion;
            public short dmDriverVersion;
            public short dmSize;
            public short dmDriverExtra;
            public int dmFields;
            public int dmPositionX;
            public int dmPositionY;
            public ScreenOrientation dmDisplayOrientation;
            public int dmDisplayFixedOutput;
            public short dmColor;
            public short dmDuplex;
            public short dmYResolution;
            public short dmTTOption;
            public short dmCollate;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
            public string dmFormName;
            public short dmLogPixels;
            public int dmBitsPerPel;
            public int dmPelsWidth;
            public int dmPelsHeight;
            public int dmDisplayFlags;
            public int dmDisplayFrequency;
            public int dmICMMethod;
            public int dmICMIntent;
            public int dmMediaType;
            public int dmDitherType;
            public int dmReserved1;
            public int dmReserved2;
            public int dmPanningWidth;
            public int dmPanningHeight;
        }

        [DllImport("user32.dll")]
        public static extern bool EnumDisplaySettings(string deviceName, int modeNum, out DEVMODE devMode);
    }
}
