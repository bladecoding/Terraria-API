using Terraria;

namespace ItemPlugin
{
    public class ItemEx
    {
        private Item item;

        public ItemEx(Item i)
        {
            item = i;
        }

        public static explicit operator Item(ItemEx i)
        {
            return i.item;
        }

        public System.Boolean Wet
        {
            get
            {
                return item.wet;
            }
            set
            {
                item.wet = value;
            }
        }

        public System.Byte WetCount
        {
            get
            {
                return item.wetCount;
            }
            set
            {
                item.wetCount = value;
            }
        }

        public System.Boolean LavaWet
        {
            get
            {
                return item.lavaWet;
            }
            set
            {
                item.lavaWet = value;
            }
        }

        public Microsoft.Xna.Framework.Vector2 Position
        {
            get
            {
                return item.position;
            }
            set
            {
                item.position = value;
            }
        }

        public Microsoft.Xna.Framework.Vector2 Velocity
        {
            get
            {
                return item.velocity;
            }
            set
            {
                item.velocity = value;
            }
        }

        public System.Int32 Width
        {
            get
            {
                return item.width;
            }
            set
            {
                item.width = value;
            }
        }

        public System.Int32 Height
        {
            get
            {
                return item.height;
            }
            set
            {
                item.height = value;
            }
        }

        public System.Boolean Active
        {
            get
            {
                return item.active;
            }
            set
            {
                item.active = value;
            }
        }

        public System.Int32 NoGrabDelay
        {
            get
            {
                return item.noGrabDelay;
            }
            set
            {
                item.noGrabDelay = value;
            }
        }

        public System.Boolean BeingGrabbed
        {
            get
            {
                return item.beingGrabbed;
            }
            set
            {
                item.beingGrabbed = value;
            }
        }

        public System.Int32 SpawnTime
        {
            get
            {
                return item.spawnTime;
            }
            set
            {
                item.spawnTime = value;
            }
        }

        public System.Boolean WornArmor
        {
            get
            {
                return item.wornArmor;
            }
            set
            {
                item.wornArmor = value;
            }
        }

        public System.Int32 OwnIgnore
        {
            get
            {
                return item.ownIgnore;
            }
            set
            {
                item.ownIgnore = value;
            }
        }

        public System.Int32 OwnTime
        {
            get
            {
                return item.ownTime;
            }
            set
            {
                item.ownTime = value;
            }
        }

        public System.Int32 KeepTime
        {
            get
            {
                return item.keepTime;
            }
            set
            {
                item.keepTime = value;
            }
        }

        public System.Int32 Type
        {
            get
            {
                return item.type;
            }
            set
            {
                item.type = value;
            }
        }

        public System.String Name
        {
            get
            {
                return item.name;
            }
            set
            {
                item.name = value;
            }
        }

        public System.Int32 HoldStyle
        {
            get
            {
                return item.holdStyle;
            }
            set
            {
                item.holdStyle = value;
            }
        }

        public System.Int32 UseStyle
        {
            get
            {
                return item.useStyle;
            }
            set
            {
                item.useStyle = value;
            }
        }

        public System.Boolean Channel
        {
            get
            {
                return item.channel;
            }
            set
            {
                item.channel = value;
            }
        }

        public System.Boolean Accessory
        {
            get
            {
                return item.accessory;
            }
            set
            {
                item.accessory = value;
            }
        }

        public System.Int32 UseAnimation
        {
            get
            {
                return item.useAnimation;
            }
            set
            {
                item.useAnimation = value;
            }
        }

        public System.Int32 UseTime
        {
            get
            {
                return item.useTime;
            }
            set
            {
                item.useTime = value;
            }
        }

        public System.Int32 Stack
        {
            get
            {
                return item.stack;
            }
            set
            {
                item.stack = value;
            }
        }

        public System.Int32 MaxStack
        {
            get
            {
                return item.maxStack;
            }
            set
            {
                item.maxStack = value;
            }
        }

        public System.Int32 Pick
        {
            get
            {
                return item.pick;
            }
            set
            {
                item.pick = value;
            }
        }

        public System.Int32 Axe
        {
            get
            {
                return item.axe;
            }
            set
            {
                item.axe = value;
            }
        }

        public System.Int32 Hammer
        {
            get
            {
                return item.hammer;
            }
            set
            {
                item.hammer = value;
            }
        }

        public System.Int32 TileBoost
        {
            get
            {
                return item.tileBoost;
            }
            set
            {
                item.tileBoost = value;
            }
        }

        public System.Int32 CreateTile
        {
            get
            {
                return item.createTile;
            }
            set
            {
                item.createTile = value;
            }
        }

        public System.Int32 CreateWall
        {
            get
            {
                return item.createWall;
            }
            set
            {
                item.createWall = value;
            }
        }

        public System.Int32 Damage
        {
            get
            {
                return item.damage;
            }
            set
            {
                item.damage = value;
            }
        }

        public System.Single KnockBack
        {
            get
            {
                return item.knockBack;
            }
            set
            {
                item.knockBack = value;
            }
        }

        public System.Int32 HealLife
        {
            get
            {
                return item.healLife;
            }
            set
            {
                item.healLife = value;
            }
        }

        public System.Int32 HealMana
        {
            get
            {
                return item.healMana;
            }
            set
            {
                item.healMana = value;
            }
        }

        public System.Boolean Potion
        {
            get
            {
                return item.potion;
            }
            set
            {
                item.potion = value;
            }
        }

        public System.Boolean Consumable
        {
            get
            {
                return item.consumable;
            }
            set
            {
                item.consumable = value;
            }
        }

        public System.Boolean AutoReuse
        {
            get
            {
                return item.autoReuse;
            }
            set
            {
                item.autoReuse = value;
            }
        }

        public System.Boolean UseTurn
        {
            get
            {
                return item.useTurn;
            }
            set
            {
                item.useTurn = value;
            }
        }

        public Microsoft.Xna.Framework.Color Color
        {
            get
            {
                return item.color;
            }
            set
            {
                item.color = value;
            }
        }

        public System.Int32 Alpha
        {
            get
            {
                return item.alpha;
            }
            set
            {
                item.alpha = value;
            }
        }

        public System.Single Scale
        {
            get
            {
                return item.scale;
            }
            set
            {
                item.scale = value;
            }
        }

        public System.Int32 UseSound
        {
            get
            {
                return item.useSound;
            }
            set
            {
                item.useSound = value;
            }
        }

        public System.Int32 Defense
        {
            get
            {
                return item.defense;
            }
            set
            {
                item.defense = value;
            }
        }

        public System.Int32 HeadSlot
        {
            get
            {
                return item.headSlot;
            }
            set
            {
                item.headSlot = value;
            }
        }

        public System.Int32 BodySlot
        {
            get
            {
                return item.bodySlot;
            }
            set
            {
                item.bodySlot = value;
            }
        }

        public System.Int32 LegSlot
        {
            get
            {
                return item.legSlot;
            }
            set
            {
                item.legSlot = value;
            }
        }

        public System.String ToolTip
        {
            get
            {
                return item.toolTip;
            }
            set
            {
                item.toolTip = value;
            }
        }

        public System.Int32 Owner
        {
            get
            {
                return item.owner;
            }
            set
            {
                item.owner = value;
            }
        }

        public System.Int32 Rare
        {
            get
            {
                return item.rare;
            }
            set
            {
                item.rare = value;
            }
        }

        public System.Int32 Shoot
        {
            get
            {
                return item.shoot;
            }
            set
            {
                item.shoot = value;
            }
        }

        public System.Single ShootSpeed
        {
            get
            {
                return item.shootSpeed;
            }
            set
            {
                item.shootSpeed = value;
            }
        }

        public System.Int32 Ammo
        {
            get
            {
                return item.ammo;
            }
            set
            {
                item.ammo = value;
            }
        }

        public System.Int32 UseAmmo
        {
            get
            {
                return item.useAmmo;
            }
            set
            {
                item.useAmmo = value;
            }
        }

        public System.Int32 LifeRegen
        {
            get
            {
                return item.lifeRegen;
            }
            set
            {
                item.lifeRegen = value;
            }
        }

        public System.Int32 ManaRegen
        {
            get
            {
                return item.manaRegen;
            }
            set
            {
                item.manaRegen = value;
            }
        }

        public System.Int32 Mana
        {
            get
            {
                return item.mana;
            }
            set
            {
                item.mana = value;
            }
        }

        public System.Boolean NoUseGraphic
        {
            get
            {
                return item.noUseGraphic;
            }
            set
            {
                item.noUseGraphic = value;
            }
        }

        public System.Boolean NoMelee
        {
            get
            {
                return item.noMelee;
            }
            set
            {
                item.noMelee = value;
            }
        }

        public System.Int32 Release
        {
            get
            {
                return item.release;
            }
            set
            {
                item.release = value;
            }
        }

        public System.Int32 Value
        {
            get
            {
                return item.value;
            }
            set
            {
                item.value = value;
            }
        }

        public System.Boolean Buy
        {
            get
            {
                return item.buy;
            }
            set
            {
                item.buy = value;
            }
        }
    }
}