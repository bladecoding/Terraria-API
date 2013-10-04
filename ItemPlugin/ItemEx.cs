using System.ComponentModel;
using Microsoft.Xna.Framework;
using Terraria;

namespace ItemPlugin
{
    public class ItemEx
    {
        public Item Item { get; private set; }

        public ItemEx(Item i)
        {
            Item = i;
        }

        [DefaultValue(false)]
        public System.Boolean Wet
        {
            get
            {
                return Item.wet;
            }
            set
            {
                Item.wet = value;
            }
        }

        [DefaultValue((byte)0)]
        public System.Byte WetCount
        {
            get
            {
                return Item.wetCount;
            }
            set
            {
                Item.wetCount = value;
            }
        }

        [DefaultValue(false)]
        public System.Boolean LavaWet
        {
            get
            {
                return Item.lavaWet;
            }
            set
            {
                Item.lavaWet = value;
            }
        }

        [DefaultValue(typeof(Vector2), "0, 0")]
        public Microsoft.Xna.Framework.Vector2 Position
        {
            get
            {
                return Item.position;
            }
            set
            {
                Item.position = value;
            }
        }

        [DefaultValue(typeof(Vector2), "0, 0")]
        public Microsoft.Xna.Framework.Vector2 Velocity
        {
            get
            {
                return Item.velocity;
            }
            set
            {
                Item.velocity = value;
            }
        }

        [DefaultValue(0)]
        public System.Int32 Width
        {
            get
            {
                return Item.width;
            }
            set
            {
                Item.width = value;
            }
        }

        [DefaultValue(0)]
        public System.Int32 Height
        {
            get
            {
                return Item.height;
            }
            set
            {
                Item.height = value;
            }
        }

        [DefaultValue(true)]
        public System.Boolean Active
        {
            get
            {
                return Item.active;
            }
            set
            {
                Item.active = value;
            }
        }

        [DefaultValue(0)]
        public System.Int32 NoGrabDelay
        {
            get
            {
                return Item.noGrabDelay;
            }
            set
            {
                Item.noGrabDelay = value;
            }
        }

        [DefaultValue(false)]
        public System.Boolean BeingGrabbed
        {
            get
            {
                return Item.beingGrabbed;
            }
            set
            {
                Item.beingGrabbed = value;
            }
        }

        [DefaultValue(0)]
        public System.Int32 SpawnTime
        {
            get
            {
                return Item.spawnTime;
            }
            set
            {
                Item.spawnTime = value;
            }
        }

        [DefaultValue(false)]
        public System.Boolean WornArmor
        {
            get
            {
                return Item.wornArmor;
            }
            set
            {
                Item.wornArmor = value;
            }
        }

        [DefaultValue(-1)]
        public System.Int32 OwnIgnore
        {
            get
            {
                return Item.ownIgnore;
            }
            set
            {
                Item.ownIgnore = value;
            }
        }

        [DefaultValue(0)]
        public System.Int32 OwnTime
        {
            get
            {
                return Item.ownTime;
            }
            set
            {
                Item.ownTime = value;
            }
        }

        [DefaultValue(0)]
        public System.Int32 KeepTime
        {
            get
            {
                return Item.keepTime;
            }
            set
            {
                Item.keepTime = value;
            }
        }

        [DefaultValue(0)]
        public System.Int32 Type
        {
            get
            {
                return Item.type;
            }
            set
            {
                Item.type = value;
            }
        }

        [DefaultValue("")]
        public System.String Name
        {
            get
            {
                return Item.name;
            }
            set
            {
                Item.name = value;
            }
        }

        [DefaultValue(0)]
        public System.Int32 HoldStyle
        {
            get
            {
                return Item.holdStyle;
            }
            set
            {
                Item.holdStyle = value;
            }
        }

        [DefaultValue(0)]
        public System.Int32 UseStyle
        {
            get
            {
                return Item.useStyle;
            }
            set
            {
                Item.useStyle = value;
            }
        }

        [DefaultValue(false)]
        public System.Boolean Channel
        {
            get
            {
                return Item.channel;
            }
            set
            {
                Item.channel = value;
            }
        }

        [DefaultValue(false)]
        public System.Boolean Accessory
        {
            get
            {
                return Item.accessory;
            }
            set
            {
                Item.accessory = value;
            }
        }

        [DefaultValue(100)]
        public System.Int32 UseAnimation
        {
            get
            {
                return Item.useAnimation;
            }
            set
            {
                Item.useAnimation = value;
            }
        }

        [DefaultValue(100)]
        public System.Int32 UseTime
        {
            get
            {
                return Item.useTime;
            }
            set
            {
                Item.useTime = value;
            }
        }

        [DefaultValue(1)]
        public System.Int32 Stack
        {
            get
            {
                return Item.stack;
            }
            set
            {
                Item.stack = value;
            }
        }

        [DefaultValue(1)]
        public System.Int32 MaxStack
        {
            get
            {
                return Item.maxStack;
            }
            set
            {
                Item.maxStack = value;
            }
        }

        [DefaultValue(0)]
        public System.Int32 Pick
        {
            get
            {
                return Item.pick;
            }
            set
            {
                Item.pick = value;
            }
        }

        [DefaultValue(0)]
        public System.Int32 Axe
        {
            get
            {
                return Item.axe;
            }
            set
            {
                Item.axe = value;
            }
        }

        [DefaultValue(0)]
        public System.Int32 Hammer
        {
            get
            {
                return Item.hammer;
            }
            set
            {
                Item.hammer = value;
            }
        }

        [DefaultValue(0)]
        public System.Int32 TileBoost
        {
            get
            {
                return Item.tileBoost;
            }
            set
            {
                Item.tileBoost = value;
            }
        }

        [DefaultValue(-1)]
        public System.Int32 CreateTile
        {
            get
            {
                return Item.createTile;
            }
            set
            {
                Item.createTile = value;
            }
        }

        [DefaultValue(-1)]
        public System.Int32 CreateWall
        {
            get
            {
                return Item.createWall;
            }
            set
            {
                Item.createWall = value;
            }
        }

        [DefaultValue(-1)]
        public System.Int32 Damage
        {
            get
            {
                return Item.damage;
            }
            set
            {
                Item.damage = value;
            }
        }

        [DefaultValue(0f)]
        public System.Single KnockBack
        {
            get
            {
                return Item.knockBack;
            }
            set
            {
                Item.knockBack = value;
            }
        }

        [DefaultValue(0)]
        public System.Int32 HealLife
        {
            get
            {
                return Item.healLife;
            }
            set
            {
                Item.healLife = value;
            }
        }

        [DefaultValue(0)]
        public System.Int32 HealMana
        {
            get
            {
                return Item.healMana;
            }
            set
            {
                Item.healMana = value;
            }
        }

        [DefaultValue(false)]
        public System.Boolean Potion
        {
            get
            {
                return Item.potion;
            }
            set
            {
                Item.potion = value;
            }
        }

        [DefaultValue(false)]
        public System.Boolean Consumable
        {
            get
            {
                return Item.consumable;
            }
            set
            {
                Item.consumable = value;
            }
        }

        [DefaultValue(false)]
        public System.Boolean AutoReuse
        {
            get
            {
                return Item.autoReuse;
            }
            set
            {
                Item.autoReuse = value;
            }
        }

        [DefaultValue(false)]
        public System.Boolean UseTurn
        {
            get
            {
                return Item.useTurn;
            }
            set
            {
                Item.useTurn = value;
            }
        }

        [DefaultValue(typeof(Color), "0 , 0, 0, 0")]
        public Microsoft.Xna.Framework.Color Color
        {
            get
            {
                return Item.color;
            }
            set
            {
                Item.color = value;
            }
        }

        [DefaultValue(0)]
        public System.Int32 Alpha
        {
            get
            {
                return Item.alpha;
            }
            set
            {
                Item.alpha = value;
            }
        }

        [DefaultValue(1f)]
        public System.Single Scale
        {
            get
            {
                return Item.scale;
            }
            set
            {
                Item.scale = value;
            }
        }

        [DefaultValue(0)]
        public System.Int32 UseSound
        {
            get
            {
                return Item.useSound;
            }
            set
            {
                Item.useSound = value;
            }
        }

        [DefaultValue(0)]
        public System.Int32 Defense
        {
            get
            {
                return Item.defense;
            }
            set
            {
                Item.defense = value;
            }
        }

        [DefaultValue(-1)]
        public System.Int32 HeadSlot
        {
            get
            {
                return Item.headSlot;
            }
            set
            {
                Item.headSlot = value;
            }
        }

        [DefaultValue(-1)]
        public System.Int32 BodySlot
        {
            get
            {
                return Item.bodySlot;
            }
            set
            {
                Item.bodySlot = value;
            }
        }

        [DefaultValue(-1)]
        public System.Int32 LegSlot
        {
            get
            {
                return Item.legSlot;
            }
            set
            {
                Item.legSlot = value;
            }
        }

        [DefaultValue("")]
        public System.String ToolTip
        {
            get
            {
                return Item.toolTip;
            }
            set
            {
                Item.toolTip = value;
            }
        }

        [DefaultValue(0)]
        public System.Int32 Owner
        {
            get
            {
                return Item.owner;
            }
            set
            {
                Item.owner = value;
            }
        }

        [DefaultValue(0)]
        public System.Int32 Rare
        {
            get
            {
                return Item.rare;
            }
            set
            {
                Item.rare = value;
            }
        }

        [DefaultValue(0)]
        public System.Int32 Shoot
        {
            get
            {
                return Item.shoot;
            }
            set
            {
                Item.shoot = value;
            }
        }

        [DefaultValue(0f)]
        public System.Single ShootSpeed
        {
            get
            {
                return Item.shootSpeed;
            }
            set
            {
                Item.shootSpeed = value;
            }
        }

        [DefaultValue(0)]
        public System.Int32 Ammo
        {
            get
            {
                return Item.ammo;
            }
            set
            {
                Item.ammo = value;
            }
        }

        [DefaultValue(0)]
        public System.Int32 UseAmmo
        {
            get
            {
                return Item.useAmmo;
            }
            set
            {
                Item.useAmmo = value;
            }
        }

        [DefaultValue(0)]
        public System.Int32 LifeRegen
        {
            get
            {
                return Item.lifeRegen;
            }
            set
            {
                Item.lifeRegen = value;
            }
        }

        [DefaultValue(0)]
        public System.Int32 ManaRegen
        {
            get
            {
                return Item.manaIncrease;
            }
            set
            {
                Item.manaIncrease = value;
            }
        }

        [DefaultValue(0)]
        public System.Int32 Mana
        {
            get
            {
                return Item.mana;
            }
            set
            {
                Item.mana = value;
            }
        }

        [DefaultValue(false)]
        public System.Boolean NoUseGraphic
        {
            get
            {
                return Item.noUseGraphic;
            }
            set
            {
                Item.noUseGraphic = value;
            }
        }

        [DefaultValue(false)]
        public System.Boolean NoMelee
        {
            get
            {
                return Item.noMelee;
            }
            set
            {
                Item.noMelee = value;
            }
        }

        [DefaultValue(0)]
        public System.Int32 Release
        {
            get
            {
                return Item.release;
            }
            set
            {
                Item.release = value;
            }
        }

        [DefaultValue(0)]
        public System.Int32 Value
        {
            get
            {
                return Item.value;
            }
            set
            {
                Item.value = value;
            }
        }

        [DefaultValue(false)]
        public System.Boolean Buy
        {
            get
            {
                return Item.buy;
            }
            set
            {
                Item.buy = value;
            }
        }
    }
}