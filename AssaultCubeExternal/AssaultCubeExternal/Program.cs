using ImGuiNET;
using ClickableTransparentOverlay;
using Swed32;
using System.Numerics;

namespace AssaultCubeExternal
{
    class Program : Overlay
    {
        Swed swed = new Swed("ac_client");

        int currentTab = 0;
        bool aimbot = false;
        bool esp = false;
        bool skeleton = false;
        bool box = false;
        bool enabled1 = false;
        bool enabled2 = false;
        bool infiniteammo = false;
        bool infhealth = false;
        bool norecoil = false;

        static void Main(string[] args)
        {
            Program program = new Program();
            program.Start().Wait();
            Thread MainLogicThread = new Thread(program.MainLogic) { IsBackground = true };
            MainLogicThread.Start();
        }
        public void MainLogic()
        {
            IntPtr moduleBase = swed.GetModuleBase("ac_client.exe");
            IntPtr healthOffset = swed.ReadPointer(moduleBase + 0x17E0A8) + 0xEC;
            IntPtr ammoOffset = swed.ReadPointer(moduleBase + 0x17E0A8) + 0x140;
            IntPtr RecoilOffset = swed.ReadPointer(moduleBase + 0x17E0A8) + 0x18;



            while (true)
            {
                if (infhealth)
                {
                    swed.WriteInt(healthOffset, 999);
                }
                if (infiniteammo)
                {
                    swed.WriteInt(ammoOffset, 999);
                }
                if (norecoil)
                {
                    swed.WriteInt(RecoilOffset, 9999);
                    
                }
            }
        }


        protected override void Render() // render stuff here
        {
            ImGui.Begin("Assault Cube");
            ImGui.BeginTabBar("Tabbarlol");
            if (ImGui.TabItemButton("Aim"))
            {
                currentTab = 0;
            }
            if (ImGui.TabItemButton("Visuals"))
            {
                currentTab = 1;
            }
            if (ImGui.TabItemButton("misc"))
            {
                currentTab = 2;
            }
            switch (currentTab)
            {
                case 0:
                    renderAim();
                    break;
                case 1:
                    renderVisuals();
                    break;
                case 2:
                    renderMisc();
                    break;
            }
            void renderAim()
            {

            }
            void renderVisuals()
            {

            }
            void renderMisc()
            {
                ImGui.Checkbox("Infinite health", ref infhealth);
                ImGui.Spacing();
                ImGui.Spacing();
                ImGui.Checkbox("Infinite Ammo", ref infiniteammo);
                ImGui.Spacing();
                ImGui.Spacing();
                ImGui.Checkbox("No recoil", ref norecoil);
            }

        }
        
    }
}