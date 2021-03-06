using Celeste.Mod.StrawberryTool.Module;
using Celeste.Mod.StrawberryTool.UI;

namespace Celeste.Mod.StrawberryTool.Feature.Detector {
    public class OuiDetectorSubmenu : AbstractSubmenu {
        public OuiDetectorSubmenu() : base(DialogIds.DetectorOptions, DialogIds.DetectorOptions) { }
        private static StrawberryToolSettings Settings => StrawberryToolModule.Settings;

        protected override void addOptionsToMenu(TextMenu menu, bool inGame) {
            menu.Add(new TextMenu.OnOff(DialogIds.DetectorEnabled.DialogClean(), Settings.DetectorEnabled)
                .Change(value => Settings.DetectorEnabled = value));

            menu.Add(new TextMenu.Slider(DialogIds.DetectorOpacity.DialogClean(),
                value => $"{value * 10}%", 1, 10,
                Settings.DetectorOpacity) {
                OnValueChange = value => Settings.DetectorOpacity = value
            });
            
            menu.Add(new TextMenu.OnOff(DialogIds.OpacityGradient.DialogClean(), Settings.OpacityGradient)
                .Change( value => Settings.OpacityGradient = value));
            
            menu.Add(new TextMenu.OnOff(DialogIds.ShowPointer.DialogClean(), Settings.ShowPointer)
                .Change( value => Settings.ShowPointer = value));
            
            menu.Add(new TextMenu.OnOff(DialogIds.ShowIcon.DialogClean(), Settings.ShowIcon)
                .Change( value => Settings.ShowIcon = value));
            
            menu.Add(new TextMenu.OnOff(DialogIds.ShowIconAtScreenEdge.DialogClean(), Settings.ShowIconAtScreenEdge)
                .Change( value => Settings.ShowIconAtScreenEdge = value));
            
            menu.Add(new TextMenu.Slider(DialogIds.DetectorRange.DialogClean(),
                value => (value * 100).ToString(), 1, 99,
                Settings.DetectorRange) {
                OnValueChange = value => Settings.DetectorRange = value
            });
            
            menu.Add(new TextMenu.Slider(DialogIds.MaxPointers.DialogClean(),
                value => value.ToString(), 1, 10,
                Settings.MaxPointers) {
                OnValueChange = value => Settings.MaxPointers = value
            });

            menu.Add(new TextMenu.OnOff(DialogIds.DetectCurrentRoom.DialogClean(), Settings.DetectCurrentRoom)
                .Change(value => Settings.DetectCurrentRoom = value));
            
            menu.Add(new TextMenu.OnOff(DialogIds.DetectCollected.DialogClean(), Settings.DetectCollected)
                .Change(value => Settings.DetectCollected = value));
            
            menu.Add(new TextMenu.OnOff(DialogIds.DetectStrawberries.DialogClean(), Settings.DetectStrawberries)
                .Change(value => Settings.DetectStrawberries = value));
            
            menu.Add(new TextMenu.OnOff(DialogIds.DetectGoldenStrawberries.DialogClean(), Settings.DetectGoldenStrawberries)
                .Change(value => Settings.DetectGoldenStrawberries = value));
            
            menu.Add(new TextMenu.OnOff(DialogIds.DetectKeys.DialogClean(), Settings.DetectKeys)
                .Change(value => Settings.DetectKeys = value));
            
            menu.Add(new TextMenu.OnOff(DialogIds.DetectCassettes.DialogClean(), Settings.DetectCassettes)
                .Change(value => Settings.DetectCassettes = value));
            
            menu.Add(new TextMenu.OnOff(DialogIds.DetectHeartGems.DialogClean(), Settings.DetectHeartGems)
                .Change(value => Settings.DetectHeartGems = value));
            
            menu.Add(new TextMenu.OnOff(DialogIds.DetectSummitGems.DialogClean(), Settings.DetectSummitGems)
                .Change(value => Settings.DetectSummitGems = value));
        }

        protected override void gotoMenu(Overworld overworld) {
            Overworld.Goto<OuiDetectorSubmenu>();
        }
    }
}