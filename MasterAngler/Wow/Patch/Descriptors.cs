namespace MasterAngler.Wow.Patch {
    public class Descriptors {
        public struct ObjectFields {
            public const uint Guid = 0x0; // Size: 4, Flags: 1
            public const uint Data = Guid + 0x10; // Size: 4, Flags: 1
            public const uint Type = Data + 0x10; // Size: 1, Flags: 1
            public const uint EntryId = Type + 0x04; // Size: 1, Flags: 128
            public const uint DynamicFlags = EntryId + 0x04; // Size: 1, Flags: 640
            public const uint Scale = DynamicFlags + 0x04; // Size: 1, Flags: 1
        }

        public struct AreaTriggerFields {
            public const uint Caster = ObjectFields.Scale + 0x4; // Size: 4, Flags: 1
            public const uint Duration = Caster + 0x10; // Size: 1, Flags: 1
            public const uint TimeToTarget = Duration + 0x04; // Size: 1, Flags: 513
            public const uint TimeToTargetScale = TimeToTarget + 0x04; // Size: 1, Flags: 513
            public const uint TimeToTargetExtraScale = TimeToTargetScale + 0x04; // Size: 1, Flags: 513
            public const uint SpellId = TimeToTargetExtraScale + 0x04; // Size: 1, Flags: 1
            public const uint SpellVisualId = SpellId + 0x04; // Size: 1, Flags: 128
            public const uint BoundsRadius2D = SpellVisualId + 0x04; // Size: 1, Flags: 640
            public const uint DecalPropertiesId = BoundsRadius2D + 0x04; // Size: 1, Flags: 1   
        }

        public struct ConversationFields {
            public const uint LastLineDuration = ObjectFields.Scale + 0x4; // Size: 1, Flags: 128
        }

        public struct CorpseFields {
            public const uint Owner = ObjectFields.Scale + 0x4; // Size: 4, Flags: 1
            public const uint PartyGuid = Owner + 0x10; // Size: 4, Flags: 1
            public const uint DisplayId = PartyGuid + 0x10; // Size: 1, Flags: 1
            public const uint Items = DisplayId + 0x04; // Size: 19, Flags: 1
            public const uint SkinId = Items + 0x4C; // Size: 1, Flags: 1
            public const uint FacialHairStyleId = SkinId + 0x04; // Size: 1, Flags: 1
            public const uint Flags = FacialHairStyleId + 0x04; // Size: 1, Flags: 1
            public const uint DynamicFlags = Flags + 0x04; // Size: 1, Flags: 128
            public const uint FactionTemplate = DynamicFlags + 0x04; // Size: 1, Flags: 1
            public const uint CustomDisplayOption = FactionTemplate + 0x04; // Size: 1, Flags: 1
        }

        public struct DynamicObjectFields {
            public const uint Caster = ObjectFields.Scale + 0x4; // Size: 4, Flags: 1
            public const uint TypeAndVisualId = Caster + 0x10; // Size: 1, Flags: 128
            public const uint SpellId = TypeAndVisualId + 0x04; // Size: 1, Flags: 1
            public const uint Radius = SpellId + 0x04; // Size: 1, Flags: 1
            public const uint CastTime = Radius + 0x04; // Size: 1, Flags: 1
        }

        public struct GameObjectFields {
            public const uint CreatedBy = ObjectFields.Scale + 0x4; // Size: 4, Flags: 1
            public const uint DisplayId = CreatedBy + 0x10; // Size: 1, Flags: 640
            public const uint Flags = DisplayId + 0x04; // Size: 1, Flags: 513
            public const uint ParentRotation = Flags + 0x04; // Size: 4, Flags: 1
            public const uint FactionTemplate = ParentRotation + 0x10; // Size: 1, Flags: 1
            public const uint Level = FactionTemplate + 0x04; // Size: 1, Flags: 1
            public const uint PercentHealth = Level + 0x04; // Size: 1, Flags: 513
            public const uint SpellVisualId = PercentHealth + 0x04; // Size: 1, Flags: 641
            public const uint StateSpellVisualId = SpellVisualId + 0x04; // Size: 1, Flags: 640
            public const uint SpawnTrackingStateAnimId = StateSpellVisualId + 0x04; // Size: 1, Flags: 640
            public const uint SpawnTrackingStateAnimKitId = SpawnTrackingStateAnimId + 0x04; // Size: 1, Flags: 640
            public const uint StateWorldEffectId = SpawnTrackingStateAnimKitId + 0x04; // Size: 4, Flags: 640
        }

        public struct ItemFields {
            public const uint Owner = ObjectFields.Scale + 0x4; // Size: 4, Flags: 1
            public const uint ContainedIn = Owner + 0x10; // Size: 4, Flags: 1
            public const uint Creator = ContainedIn + 0x10; // Size: 4, Flags: 1
            public const uint GiftCreator = Creator + 0x10; // Size: 4, Flags: 1
            public const uint StackCount = GiftCreator + 0x10; // Size: 1, Flags: 4
            public const uint Expiration = StackCount + 0x04; // Size: 1, Flags: 4
            public const uint SpellCharges = Expiration + 0x04; // Size: 5, Flags: 4
            public const uint DynamicFlags = SpellCharges + 0x14; // Size: 1, Flags: 1
            public const uint Enchantment = DynamicFlags + 0x04; // Size: 39, Flags: 1
            public const uint PropertySeed = Enchantment + 0x9C; // Size: 1, Flags: 1
            public const uint RandomPropertiesId = PropertySeed + 0x04; // Size: 1, Flags: 1
            public const uint Durability = RandomPropertiesId + 0x04; // Size: 1, Flags: 4
            public const uint MaxDurability = Durability + 0x04; // Size: 1, Flags: 4
            public const uint CreatePlayedTime = MaxDurability + 0x04; // Size: 1, Flags: 1
            public const uint ModifiersMask = CreatePlayedTime + 0x04; // Size: 1, Flags: 4
            public const uint Context = ModifiersMask + 0x04; // Size: 1, Flags: 1
            public const uint ArtifactXp = Context + 0x04; // Size: 1, Flags: 4
            public const uint ItemAppearanceModId = ArtifactXp + 0x04; // Size: 1, Flags: 4 
        }

        public struct ContainerFields {
            public const uint Slots = ItemFields.ItemAppearanceModId + 0x04; // Size: 144, Flags: 1
            public const uint NumSlots = Slots + 0x240; // Size: 1, Flags: 1
        }

        public struct UnitFields {
            public const uint Charm = ObjectFields.Scale + 0x4; // Size: 4, Flags: 1
            public const uint Summon = Charm + 0x10; // Size: 4, Flags: 1
            public const uint Critter = Summon + 0x10; // Size: 4, Flags: 2
            public const uint CharmedBy = Critter + 0x10; // Size: 4, Flags: 1
            public const uint SummonedBy = CharmedBy + 0x10; // Size: 4, Flags: 1
            public const uint CreatedBy = SummonedBy + 0x10; // Size: 4, Flags: 1
            public const uint DemonCreator = CreatedBy + 0x10; // Size: 4, Flags: 1
            public const uint Target = DemonCreator + 0x10; // Size: 4, Flags: 1
            public const uint BattlePetCompanionGuid = Target + 0x10; // Size: 4, Flags: 1
            public const uint BattlePetDbid = BattlePetCompanionGuid + 0x10; // Size: 2, Flags: 1
            public const uint ChannelObject = BattlePetDbid + 0x08; // Size: 4, Flags: 513
            public const uint ChannelSpell = ChannelObject + 0x10; // Size: 1, Flags: 513
            public const uint ChannelSpellXSpellVisual = ChannelSpell + 0x04; // Size: 1, Flags: 513
            public const uint SummonedByHomeRealm = ChannelSpellXSpellVisual + 0x04; // Size: 1, Flags: 1
            public const uint Sex = SummonedByHomeRealm + 0x04; // Size: 1, Flags: 1
            public const uint DisplayPower = Sex + 0x04; // Size: 1, Flags: 1
            public const uint OverrideDisplayPowerId = DisplayPower + 0x04; // Size: 1, Flags: 1
            public const uint Health = OverrideDisplayPowerId + 0x04; // Size: 2, Flags: 1
            public const uint Power = Health + 0x08; // Size: 6, Flags: 1025
            public const uint MaxHealth = Power + 0x18; // Size: 2, Flags: 1
            public const uint MaxPower = MaxHealth + 0x08; // Size: 6, Flags: 1
            public const uint PowerRegenFlatModifier = MaxPower + 0x18; // Size: 6, Flags: 70
            public const uint PowerRegenInterruptedFlatModifier = PowerRegenFlatModifier + 0x18; // Size: 6, Flags: 70
            public const uint Level = PowerRegenInterruptedFlatModifier + 0x18; // Size: 1, Flags: 1
            public const uint EffectiveLevel = Level + 0x04; // Size: 1, Flags: 1
            public const uint ScalingLevelMin = EffectiveLevel + 0x04; // Size: 1, Flags: 1
            public const uint ScalingLevelMax = ScalingLevelMin + 0x04; // Size: 1, Flags: 1
            public const uint ScalingLevelDelta = ScalingLevelMax + 0x04; // Size: 1, Flags: 1
            public const uint FactionTemplate = ScalingLevelDelta + 0x04; // Size: 1, Flags: 1
            public const uint VirtualItems = FactionTemplate + 0x04; // Size: 6, Flags: 1
            public const uint Flags = VirtualItems + 0x18; // Size: 1, Flags: 513
            public const uint Flags2 = Flags + 0x04; // Size: 1, Flags: 513
            public const uint Flags3 = Flags2 + 0x04; // Size: 1, Flags: 513
            public const uint AuraState = Flags3 + 0x04; // Size: 1, Flags: 1
            public const uint AttackRoundBaseTime = AuraState + 0x04; // Size: 2, Flags: 1
            public const uint RangedAttackRoundBaseTime = AttackRoundBaseTime + 0x08; // Size: 1, Flags: 2
            public const uint BoundingRadius = RangedAttackRoundBaseTime + 0x04; // Size: 1, Flags: 1
            public const uint CombatReach = BoundingRadius + 0x04; // Size: 1, Flags: 1
            public const uint DisplayId = CombatReach + 0x04; // Size: 1, Flags: 640
            public const uint NativeDisplayId = DisplayId + 0x04; // Size: 1, Flags: 513
            public const uint MountDisplayId = NativeDisplayId + 0x04; // Size: 1, Flags: 513
            public const uint MinDamage = MountDisplayId + 0x04; // Size: 1, Flags: 22
            public const uint MaxDamage = MinDamage + 0x04; // Size: 1, Flags: 22
            public const uint MinOffHandDamage = MaxDamage + 0x04; // Size: 1, Flags: 22
            public const uint MaxOffHandDamage = MinOffHandDamage + 0x04; // Size: 1, Flags: 22
            public const uint AnimTier = MaxOffHandDamage + 0x04; // Size: 1, Flags: 1
            public const uint PetNumber = AnimTier + 0x04; // Size: 1, Flags: 1
            public const uint PetNameTimestamp = PetNumber + 0x04; // Size: 1, Flags: 1
            public const uint PetExperience = PetNameTimestamp + 0x04; // Size: 1, Flags: 4
            public const uint PetNextLevelExperience = PetExperience + 0x04; // Size: 1, Flags: 4
            public const uint ModCastingSpeed = PetNextLevelExperience + 0x04; // Size: 1, Flags: 1
            public const uint ModSpellHaste = ModCastingSpeed + 0x04; // Size: 1, Flags: 1
            public const uint ModHaste = ModSpellHaste + 0x04; // Size: 1, Flags: 1
            public const uint ModRangedHaste = ModHaste + 0x04; // Size: 1, Flags: 1
            public const uint ModHasteRegen = ModRangedHaste + 0x04; // Size: 1, Flags: 1
            public const uint ModTimeRate = ModHasteRegen + 0x04; // Size: 1, Flags: 1
            public const uint CreatedBySpell = ModTimeRate + 0x04; // Size: 1, Flags: 1
            public const uint NpcFlags = CreatedBySpell + 0x04; // Size: 2, Flags: 129
            public const uint EmoteState = NpcFlags + 0x08; // Size: 1, Flags: 1
            public const uint Stats = EmoteState + 0x04; // Size: 4, Flags: 6
            public const uint StatPosBuff = Stats + 0x10; // Size: 4, Flags: 6
            public const uint StatNegBuff = StatPosBuff + 0x10; // Size: 4, Flags: 6
            public const uint Resistances = StatNegBuff + 0x10; // Size: 7, Flags: 22
            public const uint ResistanceBuffModsPositive = Resistances + 0x1C; // Size: 7, Flags: 6
            public const uint ResistanceBuffModsNegative = ResistanceBuffModsPositive + 0x1C; // Size: 7, Flags: 6
            public const uint ModBonusArmor = ResistanceBuffModsNegative + 0x1C; // Size: 1, Flags: 6
            public const uint BaseMana = ModBonusArmor + 0x04; // Size: 1, Flags: 1
            public const uint BaseHealth = BaseMana + 0x04; // Size: 1, Flags: 6
            public const uint ShapeshiftForm = BaseHealth + 0x04; // Size: 1, Flags: 1
            public const uint AttackPower = ShapeshiftForm + 0x04; // Size: 1, Flags: 6
            public const uint AttackPowerModPos = AttackPower + 0x04; // Size: 1, Flags: 6
            public const uint AttackPowerModNeg = AttackPowerModPos + 0x04; // Size: 1, Flags: 6
            public const uint AttackPowerMultiplier = AttackPowerModNeg + 0x04; // Size: 1, Flags: 6
            public const uint RangedAttackPower = AttackPowerMultiplier + 0x04; // Size: 1, Flags: 6
            public const uint RangedAttackPowerModPos = RangedAttackPower + 0x04; // Size: 1, Flags: 6
            public const uint RangedAttackPowerModNeg = RangedAttackPowerModPos + 0x04; // Size: 1, Flags: 6
            public const uint RangedAttackPowerMultiplier = RangedAttackPowerModNeg + 0x04; // Size: 1, Flags: 6
            public const uint SetAttackSpeedAura = RangedAttackPowerMultiplier + 0x04; // Size: 1, Flags: 6
            public const uint MinRangedDamage = SetAttackSpeedAura + 0x04; // Size: 1, Flags: 6
            public const uint MaxRangedDamage = MinRangedDamage + 0x04; // Size: 1, Flags: 6
            public const uint PowerCostModifier = MaxRangedDamage + 0x04; // Size: 7, Flags: 6
            public const uint PowerCostMultiplier = PowerCostModifier + 0x1C; // Size: 7, Flags: 6
            public const uint MaxHealthModifier = PowerCostMultiplier + 0x1C; // Size: 1, Flags: 6
            public const uint HoverHeight = MaxHealthModifier + 0x04; // Size: 1, Flags: 1
            public const uint MinItemLevelCutoff = HoverHeight + 0x04; // Size: 1, Flags: 1
            public const uint MinItemLevel = MinItemLevelCutoff + 0x04; // Size: 1, Flags: 1
            public const uint MaxItemLevel = MinItemLevel + 0x04; // Size: 1, Flags: 1
            public const uint WildBattlePetLevel = MaxItemLevel + 0x04; // Size: 1, Flags: 1
            public const uint BattlePetCompanionNameTimestamp = WildBattlePetLevel + 0x04; // Size: 1, Flags: 1
            public const uint InteractSpellId = BattlePetCompanionNameTimestamp + 0x04; // Size: 1, Flags: 1
            public const uint StateSpellVisualId = InteractSpellId + 0x04; // Size: 1, Flags: 640
            public const uint StateAnimId = StateSpellVisualId + 0x04; // Size: 1, Flags: 640
            public const uint StateAnimKitId = StateAnimId + 0x04; // Size: 1, Flags: 640
            public const uint StateWorldEffectId = StateAnimKitId + 0x04; // Size: 4, Flags: 640
            public const uint ScaleDuration = StateWorldEffectId + 0x10; // Size: 1, Flags: 1
            public const uint LooksLikeMountId = ScaleDuration + 0x04; // Size: 1, Flags: 1
            public const uint LooksLikeCreatureId = LooksLikeMountId + 0x04; // Size: 1, Flags: 1
            public const uint LookAtControllerId = LooksLikeCreatureId + 0x04; // Size: 1, Flags: 1
            public const uint LookAtControllerTarget = LookAtControllerId + 0x04; // Size: 4, Flags: 1
        }

        public struct PlayerFields {
            public const uint DuelArbiter = UnitFields.LookAtControllerTarget + 0x10; // Size: 4, Flags: 1
            public const uint WowAccount = DuelArbiter + 0x10; // Size: 4, Flags: 1
            public const uint LootTargetGuid = WowAccount + 0x10; // Size: 4, Flags: 1
            public const uint PlayerFlags = LootTargetGuid + 0x10; // Size: 1, Flags: 1
            public const uint PlayerFlagsEx = PlayerFlags + 0x04; // Size: 1, Flags: 1
            public const uint GuildRankId = PlayerFlagsEx + 0x04; // Size: 1, Flags: 1
            public const uint GuildDeleteDate = GuildRankId + 0x04; // Size: 1, Flags: 1
            public const uint GuildLevel = GuildDeleteDate + 0x04; // Size: 1, Flags: 1
            public const uint HairColorId = GuildLevel + 0x04; // Size: 1, Flags: 1
            public const uint CustomDisplayOption = HairColorId + 0x04; // Size: 1, Flags: 1
            public const uint Inebriation = CustomDisplayOption + 0x04; // Size: 1, Flags: 1
            public const uint ArenaFaction = Inebriation + 0x04; // Size: 1, Flags: 1
            public const uint DuelTeam = ArenaFaction + 0x04; // Size: 1, Flags: 1
            public const uint GuildTimeStamp = DuelTeam + 0x04; // Size: 1, Flags: 1
            public const uint QuestLog = GuildTimeStamp + 0x04; // Size: 800, Flags: 32
            public const uint VisibleItems = QuestLog + 0xC80; // Size: 38, Flags: 1
            public const uint PlayerTitle = VisibleItems + 0x98; // Size: 1, Flags: 1
            public const uint FakeInebriation = PlayerTitle + 0x04; // Size: 1, Flags: 1
            public const uint VirtualPlayerRealm = FakeInebriation + 0x04; // Size: 1, Flags: 1
            public const uint CurrentSpecId = VirtualPlayerRealm + 0x04; // Size: 1, Flags: 1
            public const uint TaxiMountAnimKitId = CurrentSpecId + 0x04; // Size: 1, Flags: 1
            public const uint AvgItemLevel = TaxiMountAnimKitId + 0x04; // Size: 4, Flags: 1
            public const uint CurrentBattlePetBreedQuality = AvgItemLevel + 0x10; // Size: 1, Flags: 1
            public const uint Prestige = CurrentBattlePetBreedQuality + 0x04; // Size: 1, Flags: 1
            public const uint HonorLevel = Prestige + 0x04; // Size: 1, Flags: 1
            public const uint InvSlots = HonorLevel + 0x04; // Size: 748, Flags: 2
            public const uint FarsightObject = InvSlots + 0xBB0; // Size: 4, Flags: 2
            public const uint SummonedBattlePetGuid = FarsightObject + 0x10; // Size: 4, Flags: 2
            public const uint KnownTitles = SummonedBattlePetGuid + 0x10; // Size: 12, Flags: 2
            public const uint Coinage = KnownTitles + 0x30; // Size: 2, Flags: 2
            public const uint Xp = Coinage + 0x08; // Size: 1, Flags: 2
            public const uint NextLevelXp = Xp + 0x04; // Size: 1, Flags: 2
            public const uint Skill = NextLevelXp + 0x04; // Size: 448, Flags: 2
            public const uint CharacterPoints = Skill + 0x700; // Size: 1, Flags: 2
            public const uint MaxTalentTiers = CharacterPoints + 0x04; // Size: 1, Flags: 2
            public const uint TrackCreatureMask = MaxTalentTiers + 0x04; // Size: 1, Flags: 2
            public const uint TrackResourceMask = TrackCreatureMask + 0x04; // Size: 1, Flags: 2
            public const uint MainhandExpertise = TrackResourceMask + 0x04; // Size: 1, Flags: 2
            public const uint OffhandExpertise = MainhandExpertise + 0x04; // Size: 1, Flags: 2
            public const uint RangedExpertise = OffhandExpertise + 0x04; // Size: 1, Flags: 2
            public const uint CombatRatingExpertise = RangedExpertise + 0x04; // Size: 1, Flags: 2
            public const uint BlockPercentage = CombatRatingExpertise + 0x04; // Size: 1, Flags: 2
            public const uint DodgePercentage = BlockPercentage + 0x04; // Size: 1, Flags: 2
            public const uint ParryPercentage = DodgePercentage + 0x04; // Size: 1, Flags: 2
            public const uint CritPercentage = ParryPercentage + 0x04; // Size: 1, Flags: 2
            public const uint RangedCritPercentage = CritPercentage + 0x04; // Size: 1, Flags: 2
            public const uint OffhandCritPercentage = RangedCritPercentage + 0x04; // Size: 1, Flags: 2
            public const uint SpellCritPercentage = OffhandCritPercentage + 0x04; // Size: 1, Flags: 2
            public const uint ShieldBlock = SpellCritPercentage + 0x04; // Size: 1, Flags: 2
            public const uint ShieldBlockCritPercentage = ShieldBlock + 0x04; // Size: 1, Flags: 2
            public const uint Mastery = ShieldBlockCritPercentage + 0x04; // Size: 1, Flags: 2
            public const uint Speed = Mastery + 0x04; // Size: 1, Flags: 2
            public const uint Lifesteal = Speed + 0x04; // Size: 1, Flags: 2
            public const uint Avoidance = Lifesteal + 0x04; // Size: 1, Flags: 2
            public const uint Sturdiness = Avoidance + 0x04; // Size: 1, Flags: 2
            public const uint Versatility = Sturdiness + 0x04; // Size: 1, Flags: 2
            public const uint VersatilityBonus = Versatility + 0x04; // Size: 1, Flags: 2
            public const uint PvpPowerDamage = VersatilityBonus + 0x04; // Size: 1, Flags: 2
            public const uint PvpPowerHealing = PvpPowerDamage + 0x04; // Size: 1, Flags: 2
            public const uint ExploredZones = PvpPowerHealing + 0x04; // Size: 256, Flags: 2
            public const uint RestInfo = ExploredZones + 0x400; // Size: 4, Flags: 2
            public const uint ModDamageDonePos = RestInfo + 0x10; // Size: 7, Flags: 2
            public const uint ModDamageDoneNeg = ModDamageDonePos + 0x1C; // Size: 7, Flags: 2
            public const uint ModDamageDonePercent = ModDamageDoneNeg + 0x1C; // Size: 7, Flags: 2
            public const uint ModHealingDonePos = ModDamageDonePercent + 0x1C; // Size: 1, Flags: 2
            public const uint ModHealingPercent = ModHealingDonePos + 0x04; // Size: 1, Flags: 2
            public const uint ModHealingDonePercent = ModHealingPercent + 0x04; // Size: 1, Flags: 2
            public const uint ModPeriodicHealingDonePercent = ModHealingDonePercent + 0x04; // Size: 1, Flags: 2
            public const uint WeaponDmgMultipliers = ModPeriodicHealingDonePercent + 0x04; // Size: 3, Flags: 2
            public const uint WeaponAtkSpeedMultipliers = WeaponDmgMultipliers + 0x0C; // Size: 3, Flags: 2
            public const uint ModSpellPowerPercent = WeaponAtkSpeedMultipliers + 0x0C; // Size: 1, Flags: 2
            public const uint ModResiliencePercent = ModSpellPowerPercent + 0x04; // Size: 1, Flags: 2
            public const uint OverrideSpellPowerByApPercent = ModResiliencePercent + 0x04; // Size: 1, Flags: 2
            public const uint OverrideApBySpellPowerPercent = OverrideSpellPowerByApPercent + 0x04; // Size: 1, Flags: 2
            public const uint ModTargetResistance = OverrideApBySpellPowerPercent + 0x04; // Size: 1, Flags: 2
            public const uint ModTargetPhysicalResistance = ModTargetResistance + 0x04; // Size: 1, Flags: 2
            public const uint LocalFlags = ModTargetPhysicalResistance + 0x04; // Size: 1, Flags: 2
            public const uint NumRespecs = LocalFlags + 0x04; // Size: 1, Flags: 2
            public const uint SelfResSpell = NumRespecs + 0x04; // Size: 1, Flags: 2
            public const uint PvpMedals = SelfResSpell + 0x04; // Size: 1, Flags: 2
            public const uint BuybackPrice = PvpMedals + 0x04; // Size: 12, Flags: 2
            public const uint BuybackTimestamp = BuybackPrice + 0x30; // Size: 12, Flags: 2
            public const uint YesterdayHonorableKills = BuybackTimestamp + 0x30; // Size: 1, Flags: 2
            public const uint LifetimeHonorableKills = YesterdayHonorableKills + 0x04; // Size: 1, Flags: 2
            public const uint WatchedFactionIndex = LifetimeHonorableKills + 0x04; // Size: 1, Flags: 2
            public const uint CombatRatings = WatchedFactionIndex + 0x04; // Size: 32, Flags: 2
            public const uint PvpInfo = CombatRatings + 0x80; // Size: 36, Flags: 2
            public const uint MaxLevel = PvpInfo + 0x90; // Size: 1, Flags: 2
            public const uint ScalingPlayerLevelDelta = MaxLevel + 0x04; // Size: 1, Flags: 2
            public const uint MaxCreatureScalingLevel = ScalingPlayerLevelDelta + 0x04; // Size: 1, Flags: 2
            public const uint NoReagentCostMask = MaxCreatureScalingLevel + 0x04; // Size: 4, Flags: 2
            public const uint PetSpellPower = NoReagentCostMask + 0x10; // Size: 1, Flags: 2
            public const uint Researching = PetSpellPower + 0x04; // Size: 10, Flags: 2
            public const uint ProfessionSkillLine = Researching + 0x28; // Size: 2, Flags: 2
            public const uint UiHitModifier = ProfessionSkillLine + 0x08; // Size: 1, Flags: 2
            public const uint UiSpellHitModifier = UiHitModifier + 0x04; // Size: 1, Flags: 2
            public const uint HomeRealmTimeOffset = UiSpellHitModifier + 0x04; // Size: 1, Flags: 2
            public const uint ModPetHaste = HomeRealmTimeOffset + 0x04; // Size: 1, Flags: 2
            public const uint OverrideSpellsId = ModPetHaste + 0x04; // Size: 1, Flags: 1026
            public const uint LfgBonusFactionId = OverrideSpellsId + 0x04; // Size: 1, Flags: 2
            public const uint LootSpecId = LfgBonusFactionId + 0x04; // Size: 1, Flags: 2
            public const uint OverrideZonePvpType = LootSpecId + 0x04; // Size: 1, Flags: 1026
            public const uint BagSlotFlags = OverrideZonePvpType + 0x04; // Size: 4, Flags: 2
            public const uint BankBagSlotFlags = BagSlotFlags + 0x10; // Size: 7, Flags: 2
            public const uint InsertItemsLeftToRight = BankBagSlotFlags + 0x1C; // Size: 1, Flags: 2
            public const uint QuestCompleted = InsertItemsLeftToRight + 0x04; // Size: 875, Flags: 2
            public const uint Honor = QuestCompleted + 0xDAC; // Size: 1, Flags: 2
            public const uint HonorNextLevel = Honor + 0x04; // Size: 1, Flags: 2
        }

        public struct SceneObjectFields {
            public const uint ScriptPackageId = ObjectFields.Scale + 0x4; // Size: 1, Flags: 1
            public const uint RndSeedVal = ScriptPackageId + 0x04; // Size: 1, Flags: 1
            public const uint CreatedBy = RndSeedVal + 0x04; // Size: 4, Flags: 1
            public const uint SceneType = CreatedBy + 0x10; // Size: 1, Flags: 1
        }
    }
}