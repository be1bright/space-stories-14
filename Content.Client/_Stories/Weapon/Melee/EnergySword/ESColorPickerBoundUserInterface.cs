using Content.Client.Toggleable;
using Content.Shared.Ame.Components;
using Content.Shared.Toggleable;
using Content.Shared.Weapons.Melee.EnergySword;
using JetBrains.Annotations;
using Robust.Client.GameObjects;
using Robust.Client.UserInterface;
using Robust.Shared.Console.Commands;
using Robust.Shared.Prototypes;

namespace Content.Client._Stories.Weapon.Melee.EnergySword
{
    [UsedImplicitly]
    public sealed class ESColorPickerBoundUserInterface(EntityUid owner, Enum uiKey) : BoundUserInterface(owner, uiKey)
    {
        [Dependency] private readonly IPrototypeManager _prototypeManager = default!;
        [Dependency] private readonly IEntitySystemManager _entitySystem = default!;

        private ESColorPicker? _window;
        private EntityUid _prototypeView;

        protected override void Open()
        {
            base.Open();
            _window = this.CreateWindow<ESColorPicker>();

            if (!EntMan.TryGetComponent<MetaDataComponent>(Owner, out var metadata) && metadata == null || metadata.EntityPrototype == null)
                return;
            _prototypeView = EntMan.Spawn(metadata.EntityPrototype.ID);

            _window.SetEntity(_prototypeView, Owner);

            _window.OnConfirmButtonPressed += color =>
            {
                SendPredictedMessage(new ESColorChangedMessage(color));
            };
            _window.OnSecretButtonPressed += state =>
            {
                 SendPredictedMessage(new ESHackedStateChangedMessage(state));
            };
        }
    }
}
