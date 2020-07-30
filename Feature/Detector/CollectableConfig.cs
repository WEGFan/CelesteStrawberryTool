﻿using System;
using System.Collections.Generic;
using System.Linq;
using Celeste.Mod.StrawberryTool.Extension;
using Monocle;
using Camera = IL.Monocle.Camera;

namespace Celeste.Mod.StrawberryTool.Feature.Detector {
    public class CollectableConfig {
        public Func<Level, EntityData, bool> ShouldBeAdded;
        public float Scale;
        public Func<Level, EntityData, Sprite> GetSprite;
        public Func<EntityData, bool> HasCollected;

        public static readonly List<CollectableConfig> All = new List<CollectableConfig> {
            new CollectableConfig {
                ShouldBeAdded = (level, data) => {
                    Session session = level.Session;
                    switch (data.Name) {
                        case "strawberry":
                            return true;
                        case "memorialTextController":
                            return session.Dashes == 0 && session.StartedFromBeginning;
                        default:
                            return false;
                    }
                },
                Scale = 0.7f,
                GetSprite = (level, data) => {
                    string spriteId;
                    bool golden = data.Name == "memorialTextController" || data.Name == "goldenBerry";
                    bool moon = data.Bool("moon");
                    bool seed = data.Nodes != null && data.Nodes.Length != 0;
                    if (SaveData.Instance.CheckStrawberry(data.ToEntityID())) {
                        if (moon) {
                            spriteId = "moonghostberry";
                        } else {
                            spriteId = golden ? "goldghostberry" : "ghostberry";
                        }
                    } else {
                        if (moon) {
                            spriteId = "moonberry";
                        } else {
                            spriteId = golden ? "goldberry" : "strawberry";
                        }
                    }

                    Sprite sprite = GFX.SpriteBank.Create(spriteId);
                    if (data.Bool("winged")) {
                        sprite.Play("flap");
                    }

                    return sprite;
                },
                HasCollected = data => SaveData.Instance.CheckStrawberry(data.ToEntityID())
            },

            new CollectableConfig {
                ShouldBeAdded = (level, data) => data.Name == "key",
                Scale = 0.6f,
                GetSprite = (level, entity) => GFX.SpriteBank.Create("key"),
                HasCollected = data => false,
            },

            new CollectableConfig {
                ShouldBeAdded = (level, data) => data.Name == "cassette",
                Scale = 0.4f,
                HasCollected = data => SaveData.Instance.Areas[(Engine.Scene as Level).Session.Area.ID].Cassette,
            }.With(config =>
                config.GetSprite = delegate(Level level, EntityData data) {
                    string id = config.HasCollected(data) ? "cassetteGhost" : "cassette";
                    return GFX.SpriteBank.Create(id);
                }),

            new CollectableConfig {
                ShouldBeAdded = (level, data) =>
                    data.Name == "blackGem" && (!level.Session.HeartGem || level.Session.Area.Mode != AreaMode.Normal),
                Scale = 0.5f,
                HasCollected = delegate(EntityData data) {
                    AreaKey area = (Engine.Scene as Level).Session.Area;
                    return !data.Bool("fake") && SaveData.Instance.Areas_Safe[area.ID].Modes[(int) area.Mode].HeartGem;
                },
            }.With(config =>
                config.GetSprite = delegate(Level level, EntityData data) {
                    AreaKey area = (Engine.Scene as Level).Session.Area;
                    string id = data.Bool("fake") ? "heartgem3" :
                        !config.HasCollected(data) ? "heartgem" + (int) area.Mode : "heartGemGhost";
                    Sprite sprite = GFX.SpriteBank.Create(id);
                    sprite.Play("spin");
                    return sprite;
                }),
        };
    }
}