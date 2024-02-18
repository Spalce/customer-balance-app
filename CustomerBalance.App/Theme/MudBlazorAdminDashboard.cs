using MudBlazor;
using Colors = MudBlazor.Colors;
using Button = MudBlazor.Button;
using Shadow = MudBlazor.Shadow;

namespace CustomerBalance.App.Theme
{
    public class MudBlazorAdminDashboard : MudTheme
    {
        public MudBlazorAdminDashboard()
        {
            Palette = new PaletteLight
            {
                // Primary = Colors.Blue.Darken1,
                // Secondary = Colors.DeepPurple.Accent2,
                Background = Colors.Grey.Lighten5,
                // AppbarBackground = Colors.Blue.Darken1,
                // DrawerBackground = "#FFF",
                // DrawerText = "rgba(0,0,0, 0.7)",
                // Success = "#06d79c",

                //Black = "#000000",
                White = "#ffffff",
                // Background = "#f2f2f2",
                BackgroundGrey = "#e0e0e0",
                Surface = "#ffffff",
                AppbarBackground = "#2596be", // Your logo color
                AppbarText = "#ffffff",
                LinesDefault = "#e0e0e0",
                LinesInputs = "#2596be", // Your logo color
                TableLines = "#e0e0e0",
                TableStriped = "#f2f2f2",
                TableHover = "#e0e0e0",
                Divider = "#bdbdbd",
                DividerLight = "#f2f2f2",
                PrimaryDarken = "#004c99",
                PrimaryLighten = "#80b3ff",
                SecondaryDarken = "#1d3d5c",
                SecondaryLighten = "#93bfe8",
                TertiaryDarken = "#2b6c87",
                TertiaryLighten = "#a8d3f0",
                InfoDarken = "#194d7f",
                InfoLighten = "#8fb1d9",
                SuccessDarken = "#1c603b",
                SuccessLighten = "#7ad29d",
                WarningDarken = "#ad4d0f",
                WarningLighten = "#ffae4d",
                ErrorDarken = "#a61b1b",
                ErrorLighten = "#e89090",
                DarkDarken = "#1c1c1c",
                DarkLighten = "#4d4d4d",
                HoverOpacity = 0.04f,
                GrayDefault = "#757575",
                GrayLight = "#f5f5f5",
                GrayLighter = "#fafafa",
                GrayDark = "#494949",
                GrayDarker = "#212121",
                //OverlayDark = "#000000",
                OverlayLight = "#ffffff",
                DrawerBackground = "#ffffff",
                DrawerText = "#2596be", // Your logo color
                DrawerIcon = "#2596be", // Your logo color
                Primary = "#2596be", // Your logo color
                PrimaryContrastText = "#ffffff",
                Secondary = "#3F89CB",
                SecondaryContrastText = "#ffffff",
                Tertiary = "#5FCDA9",
                TertiaryContrastText = "#ffffff",
                Error = "#a61b1b",
                ErrorContrastText = "#ffffff",
                Dark = "#212121",
                DarkContrastText = "#ffffff",
                Info = "#194d7f",
                InfoContrastText = "#ffffff",
                Success = "#1c603b",
                SuccessContrastText = "#ffffff",
                Warning = "#ad4d0f",
                WarningContrastText = "#ffffff",
                TextPrimary = "rgba(0, 0, 0, 0.87)",
                TextSecondary = "rgba(0, 0, 0, 0.54)",
                TextDisabled = "rgba(0, 0, 0, 0.38)",
                ActionDefault = "#2596be", // Your logo color
                ActionDisabled = "#bdbdbd",
                ActionDisabledBackground = "#f2f2f2"
            };

            LayoutProperties = new LayoutProperties
            {
                DefaultBorderRadius = "3px"
            };

            Typography = new Typography
            {
                Default = new Default
                {
                    FontFamily = new[] { "Montserrat", "Helvetica", "Arial", "sans-serif" },
                    FontSize = ".875rem",
                    FontWeight = 400,
                    LineHeight = 1.43,
                    LetterSpacing = ".01071em"
                },
                H1 = new H1
                {
                    FontFamily = new[] { "Montserrat", "Helvetica", "Arial", "sans-serif" },
                    FontSize = "6rem",
                    FontWeight = 300,
                    LineHeight = 1.167,
                    LetterSpacing = "-.01562em"
                },
                H2 = new H2
                {
                    FontFamily = new[] { "Montserrat", "Helvetica", "Arial", "sans-serif" },
                    FontSize = "3.75rem",
                    FontWeight = 300,
                    LineHeight = 1.2,
                    LetterSpacing = "-.00833em"
                },
                H3 = new H3
                {
                    FontFamily = new[] { "Montserrat", "Helvetica", "Arial", "sans-serif" },
                    FontSize = "3rem",
                    FontWeight = 400,
                    LineHeight = 1.167,
                    LetterSpacing = "0"
                },
                H4 = new H4
                {
                    FontFamily = new[] { "Montserrat", "Helvetica", "Arial", "sans-serif" },
                    FontSize = "2.125rem",
                    FontWeight = 400,
                    LineHeight = 1.235,
                    LetterSpacing = ".00735em"
                },
                H5 = new H5
                {
                    FontFamily = new[] { "Montserrat", "Helvetica", "Arial", "sans-serif" },
                    FontSize = "1.5rem",
                    FontWeight = 400,
                    LineHeight = 1.334,
                    LetterSpacing = "0"
                },
                H6 = new H6
                {
                    FontFamily = new[] { "Montserrat", "Helvetica", "Arial", "sans-serif" },
                    FontSize = "1.25rem",
                    FontWeight = 400,
                    LineHeight = 1.6,
                    LetterSpacing = ".0075em"
                },
                Button = new Button
                {
                    FontFamily = new[] { "Montserrat", "Helvetica", "Arial", "sans-serif" },
                    FontSize = ".875rem",
                    FontWeight = 500,
                    LineHeight = 1.75,
                    LetterSpacing = ".02857em"
                },
                Body1 = new Body1
                {
                    FontFamily = new[] { "Montserrat", "Helvetica", "Arial", "sans-serif" },
                    FontSize = "1rem",
                    FontWeight = 400,
                    LineHeight = 1.5,
                    LetterSpacing = ".00938em"
                },
                Body2 = new Body2
                {
                    FontFamily = new[] { "Montserrat", "Helvetica", "Arial", "sans-serif" },
                    FontSize = ".875rem",
                    FontWeight = 400,
                    LineHeight = 1.43,
                    LetterSpacing = ".01071em"
                },
                Caption = new Caption
                {
                    FontFamily = new[] { "Montserrat", "Helvetica", "Arial", "sans-serif" },
                    FontSize = ".75rem",
                    FontWeight = 400,
                    LineHeight = 1.66,
                    LetterSpacing = ".03333em"
                },
                Subtitle2 = new Subtitle2
                {
                    FontFamily = new[] { "Montserrat", "Helvetica", "Arial", "sans-serif" },
                    FontSize = ".875rem",
                    FontWeight = 500,
                    LineHeight = 1.57,
                    LetterSpacing = ".00714em"
                }
            };
            Shadows = new Shadow();
            ZIndex = new ZIndex();
        }
    }
}
