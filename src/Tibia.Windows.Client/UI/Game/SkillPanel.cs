﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tibia.Windows.Client
{
    /// <summary>
    /// The view that displays the skills of the current viewport
    /// </summary>
    class SkillPanel : UIVirtualFrame
    {
        protected ClientViewport Viewport;
        
        /// <summary>
        /// Need a reference to be able to unsubscribe to state change events.
        /// </summary>
        protected GameDesktop Desktop;


        public SkillPanel(GameDesktop Desktop)
        {
            Name = "Skills";

            ((UIStackView)ContentView).StretchOtherDirection = true;

            this.Desktop = Desktop;
            Desktop.ActiveViewportChanged += ViewportChanged;
            ViewportChanged(Desktop.ActiveViewport);
        }

        protected void ViewportChanged(ClientViewport NewViewport)
        {
            ContentView.RemoveAllSubviews();

            // Don't do anything if we didn't receive a new state
            Viewport = NewViewport;
            if (Viewport == null)
                return;

            ContentView.AddSubview(new SkillLabel(Viewport.Player.Level));

            SkillLabel ExperienceLabel = new SkillLabel("Experience", delegate()
            {
                return new SkillLabel.SkillValue()
                {
                    Value = Viewport.Player.Experience,
                    Percent = 0
                };
            });
            ContentView.AddSubview(ExperienceLabel);

            SkillLabel HitpointLabel = new SkillLabel("Hitpoints", delegate()
            {
                return new SkillLabel.SkillValue()
                {
                    Value = Viewport.Player.Health,
                    Percent = 0
                };
            });
            ContentView.AddSubview(HitpointLabel);

            SkillLabel ManaLabel = new SkillLabel("Mana", delegate()
            {
                return new SkillLabel.SkillValue()
                {
                    Value = Viewport.Player.Mana,
                    Percent = 0
                };
            });
            ContentView.AddSubview(ManaLabel);

            SkillLabel CapacityLabel = new SkillLabel("Capacity", delegate()
            {
                return new SkillLabel.SkillValue()
                {
                    Value = Viewport.Player.Capacity,
                    Percent = 0
                };
            });
            ContentView.AddSubview(CapacityLabel);

            ContentView.AddSubview(new SkillLabel(Viewport.Player.MagicLevel));

            foreach (ClientSkill Skill in Viewport.Player.Skill.Values)
                ContentView.AddSubview(new SkillLabel(Skill));

            NeedsLayout = true;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    class SkillLabel : UIView
    {
        public struct SkillValue
        {
            public int Value;
            public int Percent;
        };

        public delegate SkillValue SkillInspector();

        private SkillInspector Inspector;
        private ClientSkill Skill;
        private UILabel NameLabel;
        private UILabel ValueLabel;

        public SkillLabel(ClientSkill Skill)
            : base(null, UIElementType.None)
        {
            this.Skill = Skill;

            NameLabel = new UILabel();
            AddSubview(NameLabel);

            ValueLabel = new UILabel();
            ValueLabel.TextAlignment = UITextAlignment.Right;
            AddSubview(ValueLabel);
        }

        public SkillLabel(String SkillName, SkillInspector Inspector)
            : base(null, UIElementType.None)
        {
            this.Inspector = Inspector;

            NameLabel = new UILabel(SkillName);
            AddSubview(NameLabel);

            ValueLabel = new UILabel();
            ValueLabel.TextAlignment = UITextAlignment.Right;
            AddSubview(ValueLabel);
        }

        public override void LayoutSubviews()
        {
            NameLabel.Bounds.Width = ClientBounds.Width;
            ValueLabel.Bounds.Width = ClientBounds.Width;
            Bounds.Height = Math.Max(NameLabel.Bounds.Height, ValueLabel.Bounds.Height);

            base.LayoutSubviews();
        }

        public override void Update(GameTime time)
        {
            SkillValue s;
            if (Inspector != null)
            {
                // Read from the callback
                s = Inspector();
            }
            else
            {
                // Read from the client state instead
                NameLabel.Text = Skill.LongName;
                s = new SkillValue()
                {
                    Value = Skill.Value,
                    Percent = Skill.Percent
                };
            }

            ValueLabel.Text = FormatNumber(s.Value);

            base.Update(time);
        }

        private string FormatNumber(int n)
        {
            string s = n.ToString();
            int splits = s.Length / 3;
            if (splits == 0)
                return s;
            string o = "";
            while (true)
            {
                if (s.Length <= 3)
                {
                    o = s + o;
                    break;
                }

                string triplet = s.Substring(s.Length - 3, 3);
                s = s.Substring(0, s.Length - 3);
                o = "," + triplet + o;
            }
            return o;
        }
    }
}
