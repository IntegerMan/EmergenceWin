namespace MattEland.Emergence.Definitions.Services
{
    public static class RarityHelper {
        public static Rarity Upgrade(this Rarity baseRarity)
        {
            switch (baseRarity)
            {
                case Rarity.None:
                    return Rarity.None; // None should never spawn anything

                case Rarity.Common:
                    return Rarity.Uncommon;

                case Rarity.Uncommon:
                    return Rarity.Rare;

                case Rarity.Rare:
                    return Rarity.Epic;

                case Rarity.Epic:
                case Rarity.Legendary:
                    return Rarity.Legendary;

                default:
                    return baseRarity;
            }

        }
    }
}